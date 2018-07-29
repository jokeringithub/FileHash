using System.Globalization;

namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 提供本地化控件内容。
        /// </summary>
        public partial class ComponentContentLocalized
        {
            /// <summary>
            /// 使用特定区域设置初始化 <see cref="MainWindow.ComponentContentLocalized"/> 的实例。
            /// </summary>
            /// <param name="cultureInfo">区域信息。</param>
            public ComponentContentLocalized(CultureInfo cultureInfo)
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
                        this.MainWindowTitle = "文件散列函数";
                        this.CopyButtonContent = "复制";
                        this.ClearButtonContent = "清除";
                        this.SkipButtonContent = "跳过";
                        this.CancelButtonContent = "取消";
                        this.FilePathCheckBoxContent = "路径";
                        this.FileSizeCheckBoxContent = "大小";
                        this.FileEditTimeCheckBoxContent = "修改时间";
                        this.FileCRC32CheckBoxContent = "CRC32";
                        this.FileMD5CheckBoxContent = "MD5";
                        this.FileSHA1CheckBoxContent = "SHA-1";
                        this.FileSHA256CheckBoxContent = "SHA-256";
                        this.FileSHA384CheckBoxContent = "SHA-384";
                        this.FileSHA512CheckBoxContent = "SHA-512";
                        this.LowerHexFormatRadioButtonContent = "小写十六进制";
                        this.UpperHexFormatRadioButtonContent = "大写十六进制";
                        this.Base64FormatRadioButtonContent = "Base64";
                        break;
                    case SupportedLanguage.English:
                        this.MainWindowTitle = "File Hash";
                        this.CopyButtonContent = "Copy";
                        this.ClearButtonContent = "Clear";
                        this.SkipButtonContent = "Skip";
                        this.CancelButtonContent = "Cancel";
                        this.FilePathCheckBoxContent = "Path";
                        this.FileSizeCheckBoxContent = "Size";
                        this.FileEditTimeCheckBoxContent = "Modified";
                        this.FileCRC32CheckBoxContent = "CRC32";
                        this.FileMD5CheckBoxContent = "MD5";
                        this.FileSHA1CheckBoxContent = "SHA-1";
                        this.FileSHA256CheckBoxContent = "SHA-256";
                        this.FileSHA384CheckBoxContent = "SHA-384";
                        this.FileSHA512CheckBoxContent = "SHA-512";
                        this.LowerHexFormatRadioButtonContent = "LowerHex";
                        this.UpperHexFormatRadioButtonContent = "UpperHex";
                        this.Base64FormatRadioButtonContent = "Base64";
                        break;
                    case SupportedLanguage.Japanese:
                        this.MainWindowTitle = "ファイルハッシュ";
                        this.CopyButtonContent = "コピー";
                        this.ClearButtonContent = "クリア";
                        this.SkipButtonContent = "スキップ";
                        this.CancelButtonContent = "キャンセル";
                        this.FilePathCheckBoxContent = "パス";
                        this.FileSizeCheckBoxContent = "サイズ";
                        this.FileEditTimeCheckBoxContent = "更新日時";
                        this.FileCRC32CheckBoxContent = "CRC32";
                        this.FileMD5CheckBoxContent = "MD5";
                        this.FileSHA1CheckBoxContent = "SHA-1";
                        this.FileSHA256CheckBoxContent = "SHA-256";
                        this.FileSHA384CheckBoxContent = "SHA-384";
                        this.FileSHA512CheckBoxContent = "SHA-512";
                        this.LowerHexFormatRadioButtonContent = "小文字十六進法";
                        this.UpperHexFormatRadioButtonContent = "大文字十六進法";
                        this.Base64FormatRadioButtonContent = "Base64";
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 主窗口标题。
            /// </summary>
            public string MainWindowTitle { get; }
            /// <summary>
            /// 复制按钮。
            /// </summary>
            public string CopyButtonContent { get; }
            /// <summary>
            /// 跳过按钮。
            /// </summary>
            public string SkipButtonContent { get; }
            /// <summary>
            /// 清除按钮。
            /// </summary>
            public string ClearButtonContent { get; }
            /// <summary>
            /// 取消按钮。
            /// </summary>
            public string CancelButtonContent { get; }
            /// <summary>
            /// 文件路径复选框。
            /// </summary>
            public string FilePathCheckBoxContent { get; }
            /// <summary>
            /// 文件大小复选框。
            /// </summary>
            public string FileSizeCheckBoxContent { get; }
            /// <summary>
            /// 修改日期复选框。
            /// </summary>
            public string FileEditTimeCheckBoxContent { get; }
            /// <summary>
            /// CRC32 复选框。
            /// </summary>
            public string FileCRC32CheckBoxContent { get; }
            /// <summary>
            /// MD5 复选框。
            /// </summary>
            public string FileMD5CheckBoxContent { get; }
            /// <summary>
            /// SHA-1 复选框。
            /// </summary>
            public string FileSHA1CheckBoxContent { get; }
            /// <summary>
            /// SHA-256 复选框。
            /// </summary>
            public string FileSHA256CheckBoxContent { get; }
            /// <summary>
            /// SHA-384 复选框。
            /// </summary>
            public string FileSHA384CheckBoxContent { get; }
            /// <summary>
            /// SHA-512 复选框。
            /// </summary>
            public string FileSHA512CheckBoxContent { get; }
            /// <summary>
            /// 小写十六进制选框。
            /// </summary>
            public string LowerHexFormatRadioButtonContent { get; }
            /// <summary>
            /// 大写十六进制选框。
            /// </summary>
            public string UpperHexFormatRadioButtonContent { get; }
            /// <summary>
            /// Base64 选框。
            /// </summary>
            public string Base64FormatRadioButtonContent { get; }
        }
    }
}
