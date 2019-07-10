using System;
using System.Collections.Generic;
using System.Globalization;
using XstarS.ComponentModel;
using FileHash.Model;
using FileHash.Properties;

namespace FileHash.View
{
    /// <summary>
    /// 提供文件信息和哈希值的本地化结果。
    /// </summary>
    public class LocalizedFileInfoAndHash : BindableObject
    {
        /// <summary>
        /// 本地化结果。
        /// </summary>
        protected static readonly Dictionary<SupportedLanguage, string[]> LocalizedResult;
        /// <summary>
        /// 本地化取消提示消息。
        /// </summary>
        protected static readonly Dictionary<SupportedLanguage, string[]> LocalizedCancelledMessage;
        /// <summary>
        /// 本地化文件错误提示消息。
        /// </summary>
        protected static readonly Dictionary<SupportedLanguage, string[]> LocalizedFileErrorMessage;

        /// <summary>
        /// 结果信息。
        /// </summary>
        private readonly string[] resultInfo;
        /// <summary>
        /// 取消提示消息。
        /// </summary>
        private readonly string[] cancelledMessage;
        /// <summary>
        /// 文件错误提示消息。
        /// </summary>
        private readonly string[] fileErrorMessage;
        /// <summary>
        /// 本地化结果字符串。
        /// </summary>
        private string result;

        /// <summary>
        /// 初始化 <see cref="LocalizedFileInfoAndHash"/> 类的静态成员。
        /// </summary>
        static LocalizedFileInfoAndHash()
        {
            LocalizedFileInfoAndHash.LocalizedResult = new Dictionary<SupportedLanguage, string[]>()
            {
                {
                    SupportedLanguage.ChineseSimpified,
                    new string[] { "", "路径：", "大小：", "修改时间：",
                        "CRC32：", "MD5：", "SHA-1：", "SHA-256：", "SHA-384：", "SHA-512：" }
                },
                {
                    SupportedLanguage.English,
                    new string[] { "", "Path: ", "Size: ", "Modified: ",
                        "CRC32: ", "MD5: ", "SHA-1: ", "SHA-256: ", "SHA-384: ", "SHA-512: " }
                },
                {
                    SupportedLanguage.Japanese,
                    new string[] { "", "パス：", "サイズ：", "更新日時：",
                        "CRC32：", "MD5：", "SHA-1：", "SHA-256：", "SHA-384：", "SHA-512：" }
                }
            };
            LocalizedFileInfoAndHash.LocalizedCancelledMessage = new Dictionary<SupportedLanguage, string[]>()
            {
                { SupportedLanguage.ChineseSimpified, new string[] { "文件“", "”散列计算被取消。" } },
                { SupportedLanguage.English, new string[] { "File \"", "\" hash computing canceled." } },
                { SupportedLanguage.Japanese, new string[] { "ファイル「", "」ハッシュ計算停止した。" } }
            };
            LocalizedFileInfoAndHash.LocalizedFileErrorMessage = new Dictionary<SupportedLanguage, string[]>()
            {
                { SupportedLanguage.ChineseSimpified, new string[] { "无法打开文件“", "”。" } },
                { SupportedLanguage.English, new string[] { "Can not open file \"", "\"." } },
                { SupportedLanguage.Japanese, new string[] { "ファイル「", "」開く不能。" } }
            };
        }

        /// <summary>
        /// 使用特定区域设置初始化 <see cref="LocalizedFileInfoAndHash"/> 的实例。
        /// </summary>
        public LocalizedFileInfoAndHash()
        {
            // 解析语言。
            SupportedLanguage uiLanguage;
            switch (CultureInfo.CurrentUICulture.ThreeLetterWindowsLanguageName)
            {
                case "CHS": uiLanguage = SupportedLanguage.ChineseSimpified; break;
                case "CHT": uiLanguage = SupportedLanguage.ChineseSimpified; break;
                case "ENU": uiLanguage = SupportedLanguage.English; break;
                case "JPN": uiLanguage = SupportedLanguage.Japanese; break;
                default: goto case "ENU";
            }

            // 初始化各属性。
            this.resultInfo = LocalizedFileInfoAndHash.LocalizedResult[uiLanguage];
            this.cancelledMessage = LocalizedFileInfoAndHash.LocalizedCancelledMessage[uiLanguage];
            this.fileErrorMessage = LocalizedFileInfoAndHash.LocalizedFileErrorMessage[uiLanguage];
            this.Value = string.Empty;
        }

        /// <summary>
        /// 本地化结果字符串。
        /// </summary>
        public string Value
        {
            get => this.result;
            set => this.SetProperty(ref this.result, value);
        }

        /// <summary>
        /// 将输入的计算结果转化为本地化的字符串并附加到 <see cref="LocalizedFileInfoAndHash.Value"/> 中。
        /// </summary>
        /// <param name="rawResults"><see cref="FileInfoAndHash"/> 的计算结果。</param>
        public void Append(string[] rawResults)
        {
            this.Value += this.RawDataToLocalizedResult(rawResults) + Environment.NewLine;
        }

        /// <summary>
        /// 将文件计算取消的信息本地化后添加到 <see cref="LocalizedFileInfoAndHash.Value"/> 中。
        /// </summary>
        /// <param name="filePath">取消计算的文件路径。</param>
        public void AppendCancelled(string filePath)
        {
            this.Value += this.cancelledMessage[0] + filePath +
                this.cancelledMessage[1] + Environment.NewLine;
            this.Value += Environment.NewLine;
        }

        /// <summary>
        /// 将文件错误的信息本地化后添加到 <see cref="LocalizedFileInfoAndHash.Value"/> 中。
        /// </summary>
        /// <param name="filePath">打开错误的文件路径。</param>
        public void AppendFileError(string filePath)
        {
            this.Value += this.fileErrorMessage[0] + filePath +
                this.fileErrorMessage[1] + Environment.NewLine;
            this.Value += Environment.NewLine;
        }

        /// <summary>
        /// 将输入的计算结果转化为本地化的字符串。
        /// </summary>
        /// <param name="rawResults"></param>
        /// <returns></returns>
        private string RawDataToLocalizedResult(string[] rawResults)
        {
            string result = string.Empty;
            for (int i = 0; i < rawResults.Length; i++)
            {
                if (rawResults[i] != string.Empty)
                {
                    result += resultInfo[i] + rawResults[i] + Environment.NewLine;
                }
            }
            return result;
        }
    }
}
