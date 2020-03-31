using System;
using XstarS.ComponentModel;
using XstarS.FileHash.Helpers;
using XstarS.FileHash.Models;
using XstarS.FileHash.Properties;

namespace XstarS.FileHash.Views
{
    /// <summary>
    /// 为 <see cref="MainWindow"/> 提供数据逻辑模型。
    /// </summary>
    public class MainWindowModel : ComponentModelBase
    {
        /// <summary>
        /// 初始化 <see cref="MainWindowModel"/> 类的新实例。
        /// </summary>
        public MainWindowModel()
        {
            this.HashingFiles = new FileInfoAndHashCollection();
            this.HashingFiles.CurrentComplete += this.HashingFiles_CurrentComplete;
            this.HashingFiles.AllComplete += this.HashingFiles_AllComplete;
            this.FileInfoFields = new FileInfoFieldsView()
            {
                HasName = true,
                HasFullName = true,
                HasLength = true,
                HasLastWriteTime = true
            };
            this.FileHashTypes = new FileHashTypesView()
            {
                HasMD5 = true,
                HasSHA1 = true,
                HasSHA256 = true
            };
            this.FileHashFormat = new BytesFormatView() { IsUpperHex = true };
            this.ResultText = string.Empty;
        }

        /// <summary>
        /// 获取计算文件哈希的队列。
        /// </summary>
        public FileInfoAndHashCollection HashingFiles { get; }

        /// <summary>
        /// 获取要显示的文件信息字段。
        /// </summary>
        public FileInfoFieldsView FileInfoFields { get; }

        /// <summary>
        /// 获取要计算的文件哈希值类型。
        /// </summary>
        public FileHashTypesView FileHashTypes { get; }

        /// <summary>
        /// 获取要文件哈希值的显示格式。
        /// </summary>
        public BytesFormatView FileHashFormat { get; }

        /// <summary>
        /// 获取当前正在计算哈希值的文件。
        /// </summary>
        public FileInfoAndHash HashingFile => this.HashingFiles.Current;

        /// <summary>
        /// 获取当前计算文件哈希值的进度。
        /// </summary>
        public ListProgressView HashingProgress => this.HashingFiles.Progress;

        /// <summary>
        /// 获取当前是否可以取消文件哈希值计算。
        /// </summary>
        public bool CanCancelHashing => !(this.HashingFile is null);

        /// <summary>
        /// 获取文件信息和哈希值计算结果的文本。
        /// </summary>
        public string ResultText
        {
            get => this.GetProperty<string>();
            private set
            {
                this.SetProperty(value);
                this.NotifyPropertyChanged(nameof(this.HasResult));
            }
        }

        /// <summary>
        /// 获取当前是否存在结果文本。
        /// </summary>
        public bool HasResult => this.ResultText.Length != 0;

        /// <summary>
        /// 将指定路径下的所有文件添加到文件哈希值计算队列中。
        /// </summary>
        /// <param name="path">包含文件的路径。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1031:DoNotNatchGeneralExceptionTypes")]
        public void AddHashingFiles(string path)
        {
            foreach (var filePath in PathHelper.GetFilePaths(path, recurse: true))
            {
                try
                {
                    this.HashingFiles.Add(
                        new FileInfoAndHash(filePath, this.FileInfoFields.Value,
                            this.FileHashTypes.Value, this.FileHashFormat.Value));
                    this.HashingFiles.ComputeAsync();
                }
                catch (Exception)
                {
                    this.AppendHashErrorText(filePath);
                }
                this.NotifyPropertyChanged(nameof(this.HashingFile));
                this.NotifyPropertyChanged(nameof(this.CanCancelHashing));
            }
        }

        /// <summary>
        /// 将多个指定路径下的所有文件添加到文件哈希值计算队列中。
        /// </summary>
        /// <param name="paths">包含文件的多个路径。</param>
        public void AddHashingFiles(string[] paths)
        {
            if (paths is null) { return; }
            foreach (var filePath in paths)
            {
                this.AddHashingFiles(filePath);
            }
        }

        /// <summary>
        /// 跳过当前正在计算文件哈希值的文件。
        /// </summary>
        public void SkipCurrentHashing() => this.HashingFiles.CancelCurrent();

        /// <summary>
        /// 取消所有文件的文件哈希值的计算。
        /// </summary>
        public void CancelHashing() => this.HashingFiles.Cancel();

        /// <summary>
        /// 将新的结果文本附加到结果文本。
        /// </summary>
        /// <param name="result">新的结果文本。</param>
        protected void AppendResultText(string result) => this.ResultText += result;

        /// <summary>
        /// 清除文件信息和哈希值计算结果文本。
        /// </summary>
        public void ClearResultText() => this.ResultText = string.Empty;

        /// <summary>
        /// 将文件打开错误的文本附加到结果文本。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        private void AppendHashErrorText(string filePath)
        {
            this.AppendResultText(Environment.NewLine +
                StringResources.HashErrorMessageHead + filePath +
                StringResources.HashErrorMessageTail +
                Environment.NewLine + Environment.NewLine);
        }

        /// <summary>
        /// 将文件哈希计算取消的文本附加到结果文本。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        private void AppendHashCancelText(string filePath)
        {
            this.AppendResultText(Environment.NewLine +
                StringResources.HashCancelMessageHead + filePath +
                StringResources.HashCancelMessageTail +
                Environment.NewLine + Environment.NewLine);
        }

        /// <summary>
        /// <see cref="MainWindowModel.HashingFiles"/> 当前文件哈希值计算完成的事件处理。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">提供事件数据的对象。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1031:DoNotNatchGeneralExceptionTypes")]
        private void HashingFiles_CurrentComplete(object sender, EventArgs e)
        {
            try
            {
                this.AppendResultText(this.HashingFile.ResultToString());
            }
            catch (Exception)
            {
                if (this.HashingFile.Progress == 0.0)
                {
                    this.AppendHashErrorText(this.HashingFile.FilePath);
                }
                else
                {
                    this.AppendHashCancelText(this.HashingFile.FilePath);
                }
            }
            this.NotifyPropertyChanged(nameof(this.HashingFile));
            this.NotifyPropertyChanged(nameof(this.CanCancelHashing));
        }

        /// <summary>
        /// <see cref="MainWindowModel.HashingFiles"/> 所有文件哈希值计算完成的事件处理。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">提供事件数据的对象。</param>
        private void HashingFiles_AllComplete(object sender, EventArgs e)
        {
            this.HashingFiles.Clear();
            this.NotifyPropertyChanged(nameof(this.HashingFile));
            this.NotifyPropertyChanged(nameof(this.CanCancelHashing));
        }
    }
}
