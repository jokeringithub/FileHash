using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using XstarS.FileHash.Helpers;
using XstarS.Security.Cryptography;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 提供计算文件哈希值的方法。
    /// </summary>
    public class FileHash : IDisposable
    {
        /// <summary>
        /// 指示此实例的资源是否已经被释放。
        /// </summary>
        private volatile bool IsDisposed = false;

        /// <summary>
        /// 使用文件和哈希值类型初始化 <see cref="FileHash"/> 的实例。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="hashTypes">要计算的文件哈希值的类型。</param>
        /// <param name="hashFormat">文件哈希值的字符串格式。</param>
        /// <exception cref="Exception">打开文件时出现错误。</exception>
        public FileHash(string filePath,
            FileHashTypes hashTypes, BytesFormat hashFormat)
        {
            this.FilePath = filePath;
            this.HashTypes = hashTypes;
            this.HashFormat = hashFormat;
            this.HashNames = EnumHelper.GetNames(hashTypes);
            this.HashingFiles = new ConcurrentDictionary<string, Stream>();
            this.HashingTasks = new ConcurrentDictionary<string, Task>();
            this.HashBytes = new ConcurrentDictionary<string, byte[]>();
        }

        /// <summary>
        /// 获取要计算哈希值的文件路径。
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// 获取要计算的哈希值的类型。
        /// </summary>
        public FileHashTypes HashTypes { get; }

        /// <summary>
        /// 获取文件哈希值的字节数组的字符串格式。
        /// </summary>
        public BytesFormat HashFormat { get; }

        /// <summary>
        /// 获取要计算的哈希值的名称。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        protected string[] HashNames { get; }

        /// <summary>
        /// 获取计算文件哈希值的流。
        /// </summary>
        protected ConcurrentDictionary<string, Stream> HashingFiles { get; }

        /// <summary>
        /// 获取计算文件哈希值的异步任务。
        /// </summary>
        protected ConcurrentDictionary<string, Task> HashingTasks { get; }

        /// <summary>
        /// 获取文件哈希值的字节数组。
        /// </summary>
        public ConcurrentDictionary<string, byte[]> HashBytes { get; }

        /// <summary>
        /// 获取当前计算进度。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1031:DoNotNatchGeneralExceptionTypes")]
        public double Progress
        {
            get
            {
                if (this.HashingTasks.Count == 0) { return 0.0; }
                if (this.IsDisposed) { return 1.0; }
                var progress = 0.0;
                var files = this.HashingFiles;
                var names = new List<string>(files.Keys);
                foreach (var name in names)
                {
                    var file = files[name];
                    try { progress += (double)file.Position / file.Length; }
                    catch (NullReferenceException) { progress += 0.0; }
                    catch (ObjectDisposedException) { progress += 1.0; }
                    catch (Exception) { progress += 1.0; }
                }
                progress /= files.Count;
                return progress;
            }
        }

        /// <summary>
        /// 当文件哈希值计算完成时发生。
        /// </summary>
        public event EventHandler Complete;

        /// <summary>
        /// 根据指定哈希类型开始异步计算文件哈希值。
        /// </summary>
        /// <param name="name">哈希函数的名称。</param>
        /// <returns>异步计算文件哈希值的任务。</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1031:DoNotNatchGeneralExceptionTypes")]
        private Task ComputeAsync(string name)
        {
            return Task.Run(() =>
            {
                using (var file = File.OpenRead(this.FilePath))
                {
                    this.HashingFiles[name] = file;
                    using (var hash = (name == nameof(CRC32)) ?
                        CRC32.Create() : HashAlgorithm.Create(name))
                    {
                        try { this.HashBytes[name] = hash.ComputeHash(file); }
                        catch (Exception) { }
                    }
                }
            });
        }

        /// <summary>
        /// 开始异步计算文件哈希值。
        /// </summary>
        /// <returns>异步计算文件哈希值的任务。</returns>
        public Task[] ComputeAsync()
        {
            var tasks = this.HashingTasks;
            if (tasks.Count == 0)
            {
                var names = this.HashNames;
                foreach (var name in names)
                {
                    tasks[name] = this.ComputeAsync(name);
                }
                Task.Run(() =>
                {
                    Task.WaitAll(tasks.Values.ToArray());
                    this.OnComplete();
                });
            }
            return tasks.Values.ToArray();
        }

        /// <summary>
        /// 取消计算文件哈希值。
        /// </summary>
        public void Cancel() => this.Dispose();

        /// <summary>
        /// 引发 <see cref="FileHash.Complete"/> 事件。
        /// </summary>
        protected virtual void OnComplete()
        {
            this.Complete?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 根据字节数组的格式将文件哈希值转换为字符串表达形式。
        /// </summary>
        /// <returns>根据字节数组的格式将文件哈希值转换得到的字符串表达形式。</returns>
        /// <exception cref="Exception">获取文件哈希值时出现错误。</exception>
        public Dictionary<string, string> FormatFileHashes()
        {
            switch (this.HashFormat)
            {
                case BytesFormat.LowerHex:
                    return this.HashBytes.ToDictionary(
                        pair => pair.Key, pair => ConvertBytes.ToLowerHexString(pair.Value));
                case BytesFormat.UpperHex:
                    return this.HashBytes.ToDictionary(
                        pair => pair.Key, pair => ConvertBytes.ToUpperHexString(pair.Value));
                case BytesFormat.Base64:
                    return this.HashBytes.ToDictionary(
                        pair => pair.Key, pair => Convert.ToBase64String(pair.Value));
                default:
                    return this.HashBytes.ToDictionary(
                        pair => pair.Key, pair => string.Empty);
            }
        }

        /// <summary>
        /// 释放此实例占用的资源。
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// 释放当前实例占用的非托管资源，并根据指示释放托管资源。
        /// </summary>
        /// <param name="disposing">指示是否释放托管资源。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1031:DoNotNatchGeneralExceptionTypes")]
        protected virtual void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                if (disposing)
                {
                    var files = new List<Stream>(this.HashingFiles.Values);
                    foreach (var file in files)
                    {
                        try { file?.Dispose(); }
                        catch (Exception) { }
                    }
                }

                this.IsDisposed = true;
            }
        }
    }
}
