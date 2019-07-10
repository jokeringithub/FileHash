using System;
using System.IO;
using System.Linq;

namespace FileHash.Model
{
    /// <summary>
    /// 文件信息与散列值类,可获取文件部分信息并计算散列值，依赖 <see cref="FileHashParallel"/> 类。
    /// </summary>
    public class FileInfoAndHash : FileHashParallel
    {
        /// <summary>
        /// 文件信息的数量。
        /// </summary>
        public static readonly int FileInfoCount = 4;

        /// <summary>
        /// 用于指示输出函数应该输出哪些值。
        /// </summary>
        private readonly bool[] fileInfoAndHashEnables;
        /// <summary>
        /// 文件名。
        /// </summary>
        private readonly string fileName;
        /// <summary>
        /// 文件绝对路径。
        /// </summary>
        private readonly string fileFullName;
        /// <summary>
        /// 文件大小。
        /// </summary>
        private readonly long fileLength;
        /// <summary>
        /// 文件修改时间。
        /// </summary>
        private readonly DateTime fileLastWriteTime;

        /// <summary>
        /// 初始化 <see cref="FileInfoAndHash"/> 的实例，直接获取文件信息，但并不计算文件散列值。
        /// 请使用 <see cref="FileHashParallel.StartAsync()"/> 计算文件散列值，
        /// 计算完成（无论是否正常完成）会触发 <see cref="FileInfoAndHash.Completed"/> 事件。
        /// </summary>
        /// <param name="filePath">文件的绝对或相对路径。</param>
        /// <param name="fileInfoAndHashEnables">
        /// 长度为 9 的标志向量，用于选择要输出的文件信息和散列值。
        /// 从前至后依次为：文件名、文件路径、文件大小、文件修改时间、CRC32、MD5、SHA1、SHA256、SHA512。
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public FileInfoAndHash(string filePath, bool[] fileInfoAndHashEnables)
            : base(filePath, new bool[]
            {
                fileInfoAndHashEnables[FileInfoAndHash.FileInfoCount + 0],
                fileInfoAndHashEnables[FileInfoAndHash.FileInfoCount + 1],
                fileInfoAndHashEnables[FileInfoAndHash.FileInfoCount + 2],
                fileInfoAndHashEnables[FileInfoAndHash.FileInfoCount + 3],
                fileInfoAndHashEnables[FileInfoAndHash.FileInfoCount + 4],
                fileInfoAndHashEnables[FileInfoAndHash.FileInfoCount + 5]
            })
        {
            // 输出标志向量长度错误时抛出异常。
            if (fileInfoAndHashEnables.Length != FileInfoAndHash.FileInfoCount + FileHashParallel.HashTypeCount)
            {
                throw new ArgumentException();
            }
            
            this.fileInfoAndHashEnables = fileInfoAndHashEnables;
            base.Completed += this.FileHashParallel_Completed;

            // 初始化文件信息。
            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(FilePath);
            }
            catch (Exception)
            {
                throw new FileNotFoundException(FilePath, FilePath);
            }
            // 文件名。
            this.fileName = fileInfo.Name;
            // 文件路径。
            this.fileFullName = fileInfo.FullName;
            // 文件大小。
            this.fileLength = fileInfo.Length;
            // 文件修改时间。
            this.fileLastWriteTime = fileInfo.LastWriteTime;
        }

        /// <summary>
        /// 文件散列计算完成时发生。
        /// </summary>
        public new event EventHandler<NullableResultEventArgs<FileInfoAndHash>> Completed;

        /// <summary>
        /// 计算完成，传递计算结果。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileHashParallel_Completed(
            object sender, NullableResultEventArgs<FileHashParallel> e)
        {
            // 触发散列计算完成事件。
            var args = e.HasResult ?
                new NullableResultEventArgs<FileInfoAndHash>(this) :
                new NullableResultEventArgs<FileInfoAndHash>();
            this.OnCompleted(this, args);
        }

        /// <summary>
        /// 文件信息到字符串。
        /// </summary>
        /// <returns></returns>
        private string[] FileInfoToStrings()
        {
            // 初始化字符串数组并置为空，便于处理。
            string[] fileInfoStrings = new string[FileInfoAndHash.FileInfoCount];
            for (int i = 0; i < fileInfoStrings.Length; i++)
            {
                fileInfoStrings[i] = string.Empty;
            }

            // 取出文件信息。
            if (this.fileInfoAndHashEnables[0])
            {
                fileInfoStrings[0] = fileName;
            }
            if (this.fileInfoAndHashEnables[1])
            {
                fileInfoStrings[1] = fileFullName;
            }
            if (this.fileInfoAndHashEnables[2])
            {
                fileInfoStrings[2] = fileLength.ToString();
            }
            if (this.fileInfoAndHashEnables[3])
            {
                fileInfoStrings[3] =
                    this.fileLastWriteTime.ToLongDateString() +
                    ", " +
                    this.fileLastWriteTime.ToLongTimeString();
            }

            return fileInfoStrings;
        }

        /// <summary>
        /// 获取文件信息和文件散列值（十六进制），
        /// 依次为文件名、文件绝对路径、文件大小、文件修改时间、CRC32、MD5、SHA-1、SHA-256、SHA-512。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public new string[] ToHexStrings()
        {
            return this.FileInfoToStrings().Concat(base.ToHexStrings()).ToArray();
        }

        /// <summary>
        /// 获取文件信息和文件散列值（小写十六进制），
        /// 依次为文件名、文件绝对路径、文件大小、文件修改时间、CRC32、MD5、SHA-1、SHA-256、SHA-512。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public new string[] ToLowerHexStrings()
        {
            return this.FileInfoToStrings().Concat(base.ToLowerHexStrings()).ToArray();
        }

        /// <summary>
        /// 获取文件信息和文件散列值（大写十六进制），
        /// 依次为文件名、文件绝对路径、文件大小、文件修改时间、CRC32、MD5、SHA-1、SHA-256、SHA-512。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public new string[] ToUpperHexStrings()
        {
            return this.FileInfoToStrings().Concat(base.ToUpperHexStrings()).ToArray();
        }

        /// <summary>
        /// 获取文件信息和文件散列值（Base64），
        /// 依次为文件名、文件绝对路径、文件大小、文件修改时间、CRC32、MD5、SHA-1、SHA-256、SHA-512。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public new string[] ToBase64Strings()
        {
            return this.FileInfoToStrings().Concat(base.ToBase64Strings()).ToArray();
        }

        /// <summary>
        /// 触发计算完成事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnCompleted(
            object sender, NullableResultEventArgs<FileInfoAndHash> e) =>
            this.Completed?.Invoke(sender, e);
    }
}
