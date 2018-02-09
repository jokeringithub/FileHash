using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileHash
{
    /// <summary>
    /// 文件信息与散列值类,可获取文件部分信息并计算散列值，依赖FileComputeParallel类
    /// </summary>
    public partial class FileInfoAndHash : FileHashParallel
    {
        /// <summary>
        /// 初始化文件信息与散列值类，直接获取文件信息，但并不计算文件散列值。
        /// 请使用StartAsync()计算文件散列值，计算完成（无论是否正常完成）会触发Completed事件。
        /// </summary>
        /// <param name="filePath">文件的绝对或相对路径</param>
        /// <param name="fileInfoAndHashEnables">
        /// 长度为9的标志向量，用于选择要输出的文件信息和散列值，从前至后依次为：文件名、文件路径、文件大小、文件修改时间、CRC32、MD5、SHA1、SHA256、SHA512
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public FileInfoAndHash(string filePath, bool[] fileInfoAndHashEnables)
            : base(filePath, new bool[] { fileInfoAndHashEnables[FileInfoCount + 0],
                                          fileInfoAndHashEnables[FileInfoCount + 1],
                                          fileInfoAndHashEnables[FileInfoCount + 2],
                                          fileInfoAndHashEnables[FileInfoCount + 3],
                                          fileInfoAndHashEnables[FileInfoCount + 4],
                                          fileInfoAndHashEnables[FileInfoCount + 5] })
        {
            // 输出标志向量长度错误时抛出异常
            if (fileInfoAndHashEnables.Length != FileInfoCount + HashTypeCount)
            {
                this.fileInfoAndHashEnables = fileInfoAndHashEnables;
            }

            this.filePath = filePath;
            this.fileInfoAndHashEnables = fileInfoAndHashEnables;
            base.Completed += FileHashParallel_Completed;

            InitializeFileInfo();
        }

        /// <summary>
        /// 输入的文件路径
        /// </summary>
        private string filePath;
        /// <summary>
        /// 用于指示输出函数应该输出哪些值
        /// </summary>
        private bool[] fileInfoAndHashEnables;

        /// <summary>
        /// 初始化文件信息
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        private void InitializeFileInfo()
        {
            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(filePath);
            }
            catch (Exception)
            {
                throw new FileNotFoundException(filePath, filePath);
            }
            // 文件名
            fileName = fileInfo.Name;
            // 文件路径
            fileFullName = fileInfo.FullName;
            // 文件大小
            fileLength = fileInfo.Length;
            // 文件修改时间
            fileLastWriteTime = fileInfo.LastWriteTime;
        }

        /// <summary>
        /// 计算完成，传递计算结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileHashParallel_Completed(object sender, FileHashParallel.CompletedEventArgs e)
        {
            // 触发散列计算完成事件
            CompletedEventArgs args = new CompletedEventArgs(e.IsNormallyCompleted, this);
            OnCompleted(this, args);
        }

        /// <summary>
        /// 文件信息到字符串
        /// </summary>
        /// <returns></returns>
        private string[] FileInfoToStrings()
        {
            // 初始化字符串数组并置为空，便于处理
            string[] fileInfoStrings = new string[FileInfoCount];
            for (int i = 0; i < fileInfoStrings.Length; i++)
            {
                fileInfoStrings[i] = string.Empty;
            }

            // 取出文件信息
            if (fileInfoAndHashEnables[0])
            {
                fileInfoStrings[0] = fileName;
            }
            if (fileInfoAndHashEnables[1])
            {
                fileInfoStrings[1] = fileFullName;
            }
            if (fileInfoAndHashEnables[2])
            {
                fileInfoStrings[2] = fileLength.ToString();
            }
            if (fileInfoAndHashEnables[3])
            {
                fileInfoStrings[3] = fileLastWriteTime.ToLongDateString() + ", " + fileLastWriteTime.ToLongTimeString();
            }

            return fileInfoStrings;
        }

        /// <summary>
        /// 获取文件信息和文件散列值（十六进制），依次为文件名、文件绝对路径、文件大小、文件修改时间、CRC32、MD5、SHA-1、SHA-256、SHA-512
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public new string[] ToHexStrings()
        {
            return EnumerableConcat.ConcatArray(FileInfoToStrings(), base.ToHexStrings());
        }

        /// <summary>
        /// 获取文件信息和文件散列值（小写十六进制），依次为文件名、文件绝对路径、文件大小、文件修改时间、CRC32、MD5、SHA-1、SHA-256、SHA-512
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public new string[] ToLowerHexStrings()
        {
            return EnumerableConcat.ConcatArray(FileInfoToStrings(), base.ToLowerHexStrings());
        }

        /// <summary>
        /// 获取文件信息和文件散列值（大写十六进制），依次为文件名、文件绝对路径、文件大小、文件修改时间、CRC32、MD5、SHA-1、SHA-256、SHA-512
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public new string[] ToUpperHexStrings()
        {
            return EnumerableConcat.ConcatArray(FileInfoToStrings(), base.ToUpperHexStrings());
        }

        /// <summary>
        /// 获取文件信息和文件散列值（Base64），依次为文件名、文件绝对路径、文件大小、文件修改时间、CRC32、MD5、SHA-1、SHA-256、SHA-512
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public new string[] ToBase64Strings()
        {
            return EnumerableConcat.ConcatArray(FileInfoToStrings(), base.ToBase64Strings());
        }

        /// <summary>
        /// 输入的文件路径
        /// </summary>
        public new string FilePath { get => filePath; }
        /// <summary>
        /// 文件信息的数量
        /// </summary>
        public static int FileInfoCount { get => 4; }
        /// <summary>
        /// 文件名
        /// </summary>
        private string fileName;
        /// <summary>
        /// 文件绝对路径
        /// </summary>
        private string fileFullName;
        /// <summary>
        /// 文件大小
        /// </summary>
        private long fileLength;
        /// <summary>
        /// 文件修改时间
        /// </summary>
        private DateTime fileLastWriteTime;
    }

    public partial class FileInfoAndHash
    {
        /// <summary>
        /// 计算完成事件委托处理
        /// </summary>
        /// <param name="isNormallyCompleted"></param>
        public new delegate void CompletedEventHandler(object sender, CompletedEventArgs e);

        /// <summary>
        /// 计算完成事件，文件散列计算完成时发生
        /// </summary>
        public new event CompletedEventHandler Completed;

        /// <summary>
        /// 触发计算完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnCompleted(object sender, CompletedEventArgs e)
        {
            Completed?.Invoke(sender, e);
        }

        /// <summary>
        /// 计算完成事件参数传递
        /// </summary>
        public new class CompletedEventArgs : EventArgs
        {
            /// <summary>
            /// 以是否正常完成标志作为参数实例化此类
            /// </summary>
            /// <param name="isNormallyCompleted">是否正常完成标志</param>
            public CompletedEventArgs(bool isNormallyCompleted)
            {
                this.isNormallyCompleted = isNormallyCompleted;
            }

            /// <summary>
            /// 以是否正常完成标志和计算结果作为参数实例化此类
            /// </summary>
            /// <param name="isNormallyCompleted">是否正常完成标志</param>
            /// <param name="result">计算结果</param>
            public CompletedEventArgs(bool isNormallyCompleted, FileInfoAndHash result)
            {
                this.isNormallyCompleted = isNormallyCompleted;
                this.result = result;
            }

            private bool isNormallyCompleted;
            private FileInfoAndHash result;

            /// <summary>
            /// 指示是否正常完成的标志位
            /// </summary>
            public bool IsNormallyCompleted { get => isNormallyCompleted; }
            /// <summary>
            /// 计算结果
            /// </summary>
            public FileInfoAndHash Result { get => result; }
        }
    }
}
