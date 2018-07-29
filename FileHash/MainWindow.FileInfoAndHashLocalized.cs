using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 提供本地化的结果
        /// </summary>
        public partial class FileInfoAndHashLocalized : BindableObject
        {
            /// <summary>
            /// 简体中文提示信息。
            /// </summary>
            private static readonly string[] infoChineseSimplified = {
                "", "路径：", "大小：", "修改时间：",
                "CRC32：", "MD5：", "SHA-1：", "SHA-256：", "SHA-384：", "SHA-512：" };
            /// <summary>
            /// 英文提示信息。
            /// </summary>
            private static readonly string[] infoEnglish = {
                "", "Path: ", "Size: ", "Modified: ",
                "CRC32: ", "MD5: ", "SHA-1: ", "SHA-256: ", "SHA-384: ", "SHA-512: " };
            /// <summary>
            /// 日文提示信息。
            /// </summary>
            private static readonly string[] infoJapanese = {
                "", "パス：", "サイズ：", "更新日時：",
                "CRC32：", "MD5：", "SHA-1：", "SHA-256：", "SHA-384：", "SHA-512：" };
            /// <summary>
            /// 简体中文取消提示信息。
            /// </summary>
            private static readonly string[] cancelledInfoChineseSimplified = { "文件“", "”散列计算被取消。" };
            /// <summary>
            /// 英文取消提示信息。
            /// </summary>
            private static readonly string[] cancelledInfoEnglish = { "File \"", "\" hash computing canceled." };
            /// <summary>
            /// 日文取消提示信息。
            /// </summary>
            private static readonly string[] cancelledInfoJapanese = { "ファイル「", "」ハッシュ計算停止した。" };
            /// <summary>
            /// 简体中文无法打开文件提示信息。
            /// </summary>
            private static readonly string[] fileNotFoundInfoChineseSimplified = { "无法打开文件“", "”。" };
            /// <summary>
            /// 英文无法打开文件提示信息。
            /// </summary>
            private static readonly string[] fileNotFoundInfoEnglish = { "Can not open file \"", "\"." };
            /// <summary>
            /// 日文无法打开文件提示信息。
            /// </summary>
            private static readonly string[] fileNotFoundInfoJapanese = { "ファイル「", "」開く不能。" };

            /// <summary>
            /// 提示信息。
            /// </summary>
            private readonly string[] info;
            /// <summary>
            /// 取消提示信息。
            /// </summary>
            private readonly string[] cancelledInfo;
            /// <summary>
            /// 无法打开文件提示信息。
            /// </summary>
            private readonly string[] fileNotFoundInfo;
            /// <summary>
            /// 本地化结果字符串。
            /// </summary>
            private string result;

            /// <summary>
            /// 使用特定区域设置初始化 <see cref="MainWindow.FileInfoAndHashLocalized"/> 的实例。
            /// </summary>
            /// <param name="cultureInfo">区域信息。</param>
            public FileInfoAndHashLocalized(CultureInfo cultureInfo)
            {
                // 解析语言。
                SupportedLanguage uiLanguage;
                switch (CultureInfo.CurrentUICulture.ThreeLetterWindowsLanguageName)
                {
                    case "CHS":
                        uiLanguage = SupportedLanguage.ChineseSimpified;
                        break;
                    case "CHT":
                        uiLanguage = SupportedLanguage.ChineseSimpified;
                        break;
                    case "ENU":
                        uiLanguage = SupportedLanguage.English;
                        break;
                    case "JPN":
                        uiLanguage = SupportedLanguage.Japanese;
                        break;
                    default:
                        uiLanguage = SupportedLanguage.English;
                        break;
                }

                // 初始化各属性。
                switch (uiLanguage)
                {
                    case SupportedLanguage.ChineseSimpified:
                        this.info = FileInfoAndHashLocalized.infoChineseSimplified;
                        this.cancelledInfo = FileInfoAndHashLocalized.cancelledInfoChineseSimplified;
                        this.fileNotFoundInfo = FileInfoAndHashLocalized.fileNotFoundInfoChineseSimplified;
                        break;
                    case SupportedLanguage.English:
                        this.info = FileInfoAndHashLocalized.infoEnglish;
                        this.cancelledInfo = FileInfoAndHashLocalized.cancelledInfoEnglish;
                        this.fileNotFoundInfo = FileInfoAndHashLocalized.fileNotFoundInfoEnglish;
                        break;
                    case SupportedLanguage.Japanese:
                        this.info = FileInfoAndHashLocalized.infoJapanese;
                        this.cancelledInfo = FileInfoAndHashLocalized.cancelledInfoJapanese;
                        this.fileNotFoundInfo = FileInfoAndHashLocalized.fileNotFoundInfoJapanese;
                        break;
                    default:
                        break;
                }

                this.Result = string.Empty;
            }

            /// <summary>
            /// 本地化结果字符串。
            /// </summary>
            public string Result
            {
                get => this.result;
                set => this.SetProperty(ref this.result, value);
            }

            /// <summary>
            /// 将输入的计算结果转化为本地化的字符串并附加到 <see cref="FileInfoAndHashLocalized.Result"/> 中。
            /// </summary>
            /// <param name="rawResults"><see cref="FileInfoAndHash"/> 的计算结果。</param>
            public void ResultAppend(string[] rawResults)
            {
                this.Result += this.RawDatasToLocalizedResult(rawResults) + Environment.NewLine;
            }

            /// <summary>
            /// 将文件计算取消的信息本地化后添加到 <see cref="FileInfoAndHashLocalized.Result"/> 中。
            /// </summary>
            /// <param name="filePath">取消计算的文件路径。</param>
            public void CancelledResultAppend(string filePath)
            {
                this.Result += this.cancelledInfo[0] + filePath + this.cancelledInfo[1] + Environment.NewLine;
                this.Result += Environment.NewLine;
            }

            /// <summary>
            /// 将打开文件错误的信息本地化后添加到 <see cref="FileInfoAndHashLocalized.Result"/> 中。
            /// </summary>
            /// <param name="filePath">打开错误的文件路径。</param>
            public void FileNotFoundResultAppend(string filePath)
            {
                this.Result += this.fileNotFoundInfo[0] + filePath + this.fileNotFoundInfo[1] + Environment.NewLine;
                this.Result += Environment.NewLine;
            }

            /// <summary>
            /// 将输入的计算结果转化为本地化的字符串。
            /// </summary>
            /// <param name="rawResults"></param>
            /// <returns></returns>
            private string RawDatasToLocalizedResult(string[] rawResults)
            {
                string result = string.Empty;
                for (int i = 0; i < rawResults.Length; i++)
                {
                    if (rawResults[i] != string.Empty)
                    {
                        result += info[i] + rawResults[i] + Environment.NewLine;
                    }
                }
                return result;
            }
        }
    }
}
