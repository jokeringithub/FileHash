using System;
using System.Collections.Generic;
using System.IO;
using XstarS.FileHash.Helpers;
using XstarS.FileHash.Properties;
using mstring = System.Text.StringBuilder;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 提供部分文件信息和计算文件哈希的方法。
    /// </summary>
    public class FileInfoAndHash : FileHash
    {
        /// <summary>
        /// 使用文件路径和文件信息和哈希选项初始化 <see cref="FileInfoAndHash"/> 类的新实例。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="infoFields">要显示的文件信息。</param>
        /// <param name="hashTypes">要计算的文件哈希值的类型。</param>
        /// <param name="hashFormat">文件哈希值的字符串格式。</param>
        /// <exception cref="Exception">打开文件时出现错误。</exception>
        public FileInfoAndHash(string filePath, FileInfoFields infoFields,
            FileHashTypes hashTypes, BytesFormat hashFormat)
            : base(filePath, hashTypes, hashFormat)
        {
            this.FileInfo = new FileInfo(filePath);
            this.InfoFields = infoFields;
            this.InfoNames = EnumHelper.GetNames(InfoFields);
        }

        /// <summary>
        /// 获取文件的文件信息。
        /// </summary>
        public FileInfo FileInfo { get; }

        /// <summary>
        /// 获取要显示的文件信息。
        /// </summary>
        public FileInfoFields InfoFields { get; }

        /// <summary>
        /// 获取要显示的文件信息的名称。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        protected string[] InfoNames { get; }

        /// <summary>
        /// 将文件信息转换为字符串表达形式。
        /// </summary>
        /// <returns>文件信息转换得到的字符串表达形式。</returns>
        /// <exception cref="Exception">获取文件信息时出现错误。</exception>
        public Dictionary<string, string> FormatFileInfos()
        {
            var infos = new Dictionary<string, string>();
            var file = this.FileInfo;
            var names = this.InfoNames;
            foreach (var name in names)
            {
                switch (name)
                {
                    case nameof(FileInfoFields.Name):
                        infos[name] = file.Name;
                        break;
                    case nameof(FileInfoFields.FullName):
                        infos[name] = file.FullName;
                        break;
                    case nameof(FileInfoFields.Length):
                        infos[name] = file.Length.ToString();
                        break;
                    case nameof(FileInfoFields.LastWriteTime):
                        infos[name] = file.LastWriteTime.ToString();
                        break;
                    default:
                        break;
                }
            }
            return infos;
        }

        /// <summary>
        /// 将文件信息和文件哈希组合为字符串表达形式。
        /// </summary>
        /// <returns>文件信息和文件哈希组合得到的字符串表达形式。</returns>
        /// <exception cref="Exception">获取文件信息或哈希值时出现错误。</exception>
        public string ResultToString()
        {
            var result = new mstring();
            result.AppendLine();
            var infos = this.FormatFileInfos();
            var fields = this.InfoFields;
            if ((fields | FileInfoFields.Name) == fields)
            {
                result.AppendLine($"" +
                    $"{StringResources.FileNameHeader}{infos[nameof(FileInfoFields.Name)]}");
            }
            if ((fields | FileInfoFields.FullName) == fields)
            {
                result.AppendLine(
                    $"{StringResources.FileFullNameHeader}{infos[nameof(FileInfoFields.FullName)]}");
            }
            if ((fields | FileInfoFields.Length) == fields)
            {
                result.AppendLine(
                    $"{StringResources.FileLengthHeader}{infos[nameof(FileInfoFields.Length)]}");
            }
            if ((fields | FileInfoFields.LastWriteTime) == fields)
            {
                result.AppendLine(
                    $"{StringResources.FileLastWriteTimeHeader}{infos[nameof(FileInfoFields.LastWriteTime)]}");
            }
            var hashes = this.FormatFileHashes();
            var types = this.HashTypes;
            if ((types | FileHashTypes.CRC32) == types)
            {
                result.AppendLine(
                    $"{StringResources.FileHashCRC32Header}{hashes[nameof(FileHashTypes.CRC32)]}");
            }
            if ((types | FileHashTypes.MD5) == types)
            {
                result.AppendLine(
                    $"{StringResources.FileHashMD5Header}{hashes[nameof(FileHashTypes.MD5)]}");
            }
            if ((types | FileHashTypes.SHA1) == types)
            {
                result.AppendLine(
                    $"{StringResources.FileHashSHA1Header}{hashes[nameof(FileHashTypes.SHA1)]}");
            }
            if ((types | FileHashTypes.SHA256) == types)
            {
                result.AppendLine(
                    $"{StringResources.FileHashSHA256Header}{hashes[nameof(FileHashTypes.SHA256)]}");
            }
            if ((types | FileHashTypes.SHA384) == types)
            {
                result.AppendLine(
                    $"{StringResources.FileHashSHA384Header}{hashes[nameof(FileHashTypes.SHA384)]}");
            }
            if ((types | FileHashTypes.SHA512) == types)
            {
                result.AppendLine(
                    $"{StringResources.FileHashSHA512Header}{hashes[nameof(FileHashTypes.SHA512)]}");
            }
            result.AppendLine();
            return result.ToString();
        }
    }
}
