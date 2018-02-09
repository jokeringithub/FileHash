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
        public class FileInfoAndHashLocalized : BindableObject
        {
            /// <summary>
            /// 使用特定区域设置实例化此类
            /// </summary>
            /// <param name="cultureInfo">区域信息</param>
            public FileInfoAndHashLocalized(CultureInfo cultureInfo)
            {
                SupportLanguage uiLanguage;
                switch (CultureInfo.CurrentUICulture.ThreeLetterWindowsLanguageName)
                {
                    case "CHS":
                        uiLanguage = SupportLanguage.ChineseSimpified;
                        break;
                    case "CHT":
                        uiLanguage = SupportLanguage.ChineseSimpified;
                        break;
                    case "ENU":
                        uiLanguage = SupportLanguage.English;
                        break;
                    case "JPN":
                        uiLanguage = SupportLanguage.Japanese;
                        break;
                    default:
                        uiLanguage = SupportLanguage.English;
                        break;
                }
                InitializeProperty(uiLanguage);

                result = string.Empty;
            }

            /// <summary>
            /// 初始化各属性
            /// </summary>
            /// <param name="uiLanguage">设定的UI语言</param>
            private void InitializeProperty(SupportLanguage uiLanguage)
            {
                switch (uiLanguage)
                {
                    case SupportLanguage.ChineseSimpified:
                        info = infoChineseSimplified;
                        cancelledInfo = cancelledInfoChineseSimplified;
                        fileNotFoundInfo = fileNotFoundInfoChineseSimplified;
                        break;
                    case SupportLanguage.English:
                        info = infoEnglish;
                        cancelledInfo = cancelledInfoEnglish;
                        fileNotFoundInfo = fileNotFoundInfoEnglish;
                        break;
                    case SupportLanguage.Japanese:
                        info = infoJapanese;
                        cancelledInfo = cancelledInfoJapanese;
                        fileNotFoundInfo = fileNotFoundInfoJapanese;
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 支持的语言
            /// </summary>
            private enum SupportLanguage { ChineseSimpified, English, Japanese };

            /// <summary>
            /// 将输入的计算结果转化为本地化的字符串并附加到Result中
            /// </summary>
            /// <param name="rawDatas">FileHashAndInfo的计算结果</param>
            public void ResultAppend(string[] rawDatas)
            {
                Result += RawDatasToLocalizedResult(rawDatas) + Environment.NewLine;
            }

            /// <summary>
            /// 将文件计算取消的信息本地化后添加到Result中
            /// </summary>
            /// <param name="filePath"></param>
            public void CancelledResultAppend(string filePath)
            {
                Result += cancelledInfo[0] + filePath + cancelledInfo[1] + Environment.NewLine;
                Result += Environment.NewLine;
            }

            /// <summary>
            /// 将打开文件错误的信息本地化后添加到Result中
            /// </summary>
            /// <param name="filePath"></param>
            public void FileNotFoundResultAppend(string filePath)
            {
                Result += fileNotFoundInfo[0] + filePath + fileNotFoundInfo[1] + Environment.NewLine;
                Result += Environment.NewLine;
            }

            /// <summary>
            /// 将输入的计算结果转化为本地化的字符串
            /// </summary>
            /// <param name="rawDatas"></param>
            /// <returns></returns>
            private string RawDatasToLocalizedResult(string[] rawDatas)
            {
                string result = string.Empty;
                for (int i = 0; i < rawDatas.Length; i++)
                {
                    if (rawDatas[i] != string.Empty)
                    {
                        result += info[i] + rawDatas[i] + Environment.NewLine;
                    }
                }
                return result;
            }

            /// <summary>
            /// 提示信息
            /// </summary>
            private string[] info;
            /// <summary>
            /// 简体中文提示信息
            /// </summary>
            private string[] infoChineseSimplified =
                { "", "路径：", "大小：", "修改时间：",
                  "CRC32：", "MD5：", "SHA-1：", "SHA-256：", "SHA-384：", "SHA-512：" };
            /// <summary>
            /// 英文提示信息
            /// </summary>
            private string[] infoEnglish =
                { "", "Path: ", "Size: ", "Modified: ",
                  "CRC32: ", "MD5: ", "SHA-1: ", "SHA-256: ", "SHA-384: ", "SHA-512: " };
            /// <summary>
            /// 日文提示信息
            /// </summary>
            private string[] infoJapanese =
                { "", "パス：", "サイズ：", "更新日時：",
                  "CRC32：", "MD5：", "SHA-1：", "SHA-256：", "SHA-384：", "SHA-512：" };

            /// <summary>
            /// 取消提示信息
            /// </summary>
            private string[] cancelledInfo;
            /// <summary>
            /// 简体中文取消提示信息
            /// </summary>
            private string[] cancelledInfoChineseSimplified = { "文件“", "”散列计算被取消。" };
            /// <summary>
            /// 英文取消提示信息
            /// </summary>
            private string[] cancelledInfoEnglish = { "File \"", "\" hash computing canceled." };
            /// <summary>
            /// 日文取消提示信息
            /// </summary>
            private string[] cancelledInfoJapanese = { "ファイル「", "」ハッシュ計算停止した。" };

            /// <summary>
            /// 无法打开文件提示信息
            /// </summary>
            private string[] fileNotFoundInfo;
            /// <summary>
            /// 简体中文无法打开文件提示信息
            /// </summary>
            private string[] fileNotFoundInfoChineseSimplified = { "无法打开文件“", "”。" };
            /// <summary>
            /// 英文无法打开文件提示信息
            /// </summary>
            private string[] fileNotFoundInfoEnglish = { "Can not open file \"", "\"." };
            /// <summary>
            /// 日文无法打开文件提示信息
            /// </summary>
            private string[] fileNotFoundInfoJapanese = { "ファイル「", "」開く不能。" };

            private string result;
            /// <summary>
            /// 本地化结果字符串
            /// </summary>
            public string Result { get => result; set => SetProperty(ref result, value); }
        }
    }
}
