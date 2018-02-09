using System.Globalization;

namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 提供本地化控件内容
        /// </summary>
        public class ComponentContenLocalized
        {
            /// <summary>
            /// 使用特定区域设置实例化此类
            /// </summary>
            /// <param name="cultureInfo">区域信息</param>
            public ComponentContenLocalized(CultureInfo cultureInfo)
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
                        mainWindowTitle = "文件散列函数";
                        copyButtonContent = "复制";
                        clearButtonContent = "清除";
                        skipButtonContent = "跳过";
                        cancelButtonContent = "取消";
                        filePathCheckBoxContent = "路径";
                        fileSizeCheckBoxContent = "大小";
                        fileUpdateTimeCheckBoxContent = "修改时间";
                        fileCRC32CheckBoxContent = "CRC32";
                        fileMD5CheckBoxContent = "MD5";
                        fileSHA1CheckBoxContent = "SHA-1";
                        fileSHA256CheckBoxContent = "SHA-256";
                        fileSHA384CheckBoxContent = "SHA-384";
                        fileSHA512CheckBoxContent = "SHA-512";
                        lowerFormatHexRadioButtonContent = "小写十六进制";
                        upperHexFormatRadioButtonContent = "大写十六进制";
                        base64FormatRadioButtonContent = "Base64";
                        break;
                    case SupportLanguage.English:
                        mainWindowTitle = "File Hash";
                        copyButtonContent = "Copy";
                        clearButtonContent = "Clear";
                        skipButtonContent = "Skip";
                        cancelButtonContent = "Cancel";
                        filePathCheckBoxContent = "Path";
                        fileSizeCheckBoxContent = "Size";
                        fileUpdateTimeCheckBoxContent = "Modified";
                        fileCRC32CheckBoxContent = "CRC32";
                        fileMD5CheckBoxContent = "MD5";
                        fileSHA1CheckBoxContent = "SHA-1";
                        fileSHA256CheckBoxContent = "SHA-256";
                        fileSHA384CheckBoxContent = "SHA-384";
                        fileSHA512CheckBoxContent = "SHA-512";
                        lowerFormatHexRadioButtonContent = "LowerHex";
                        upperHexFormatRadioButtonContent = "UpperHex";
                        base64FormatRadioButtonContent = "Base64";
                        break;
                    case SupportLanguage.Japanese:
                        mainWindowTitle = "ファイルハッシュ";
                        copyButtonContent = "コピー";
                        clearButtonContent = "クリア";
                        skipButtonContent = "スキップ";
                        cancelButtonContent = "キャンセル";
                        filePathCheckBoxContent = "パス";
                        fileSizeCheckBoxContent = "サイズ";
                        fileUpdateTimeCheckBoxContent = "更新日時";
                        fileCRC32CheckBoxContent = "CRC32";
                        fileMD5CheckBoxContent = "MD5";
                        fileSHA1CheckBoxContent = "SHA-1";
                        fileSHA256CheckBoxContent = "SHA-256";
                        fileSHA384CheckBoxContent = "SHA-384";
                        fileSHA512CheckBoxContent = "SHA-512";
                        lowerFormatHexRadioButtonContent = "小文字十六進法";
                        upperHexFormatRadioButtonContent = "大文字十六進法";
                        base64FormatRadioButtonContent = "Base64";
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 支持的语言
            /// </summary>
            private enum SupportLanguage { ChineseSimpified, English, Japanese };

            private string mainWindowTitle;
            private string copyButtonContent;
            private string clearButtonContent;
            private string skipButtonContent;
            private string cancelButtonContent;
            private string filePathCheckBoxContent;
            private string fileSizeCheckBoxContent;
            private string fileUpdateTimeCheckBoxContent;
            private string fileCRC32CheckBoxContent;
            private string fileMD5CheckBoxContent;
            private string fileSHA1CheckBoxContent;
            private string fileSHA256CheckBoxContent;
            private string fileSHA384CheckBoxContent;
            private string fileSHA512CheckBoxContent;
            private string lowerFormatHexRadioButtonContent;
            private string upperHexFormatRadioButtonContent;
            private string base64FormatRadioButtonContent;

            /// <summary>
            /// 主窗口标题
            /// </summary>
            public string MainWindowTitle { get => mainWindowTitle; }
            /// <summary>
            /// 复制按钮
            /// </summary>
            public string CopyButtonContent { get => copyButtonContent; }
            /// <summary>
            /// 跳过按钮
            /// </summary>
            public string SkipButtonContent { get => skipButtonContent; }
            /// <summary>
            /// 清除按钮
            /// </summary>
            public string ClearButtonContent { get => clearButtonContent; }
            /// <summary>
            /// 取消按钮
            /// </summary>
            public string CancelButtonContent { get => cancelButtonContent; }
            /// <summary>
            /// 文件路径复选框
            /// </summary>
            public string FilePathCheckBoxContent { get => filePathCheckBoxContent; }
            /// <summary>
            /// 文件大小复选框
            /// </summary>
            public string FileSizeCheckBoxContent { get => fileSizeCheckBoxContent; }
            /// <summary>
            /// 修改日期复选框
            /// </summary>
            public string FileEditTimeCheckBoxContent { get => fileUpdateTimeCheckBoxContent; }
            /// <summary>
            /// CRC32复选框
            /// </summary>
            public string FileCRC32CheckBoxContent { get => fileCRC32CheckBoxContent; }
            /// <summary>
            /// MD5复选框
            /// </summary>
            public string FileMD5CheckBoxContent { get => fileMD5CheckBoxContent; }
            /// <summary>
            /// SHA1复选框
            /// </summary>
            public string FileSHA1CheckBoxContent { get => fileSHA1CheckBoxContent; }
            /// <summary>
            /// SHA256复选框
            /// </summary>
            public string FileSHA256CheckBoxContent { get => fileSHA256CheckBoxContent; }
            /// <summary>
            /// SHA384复选框
            /// </summary>
            public string FileSHA384CheckBoxContent { get => fileSHA384CheckBoxContent; }
            /// <summary>
            /// SHA512复选框
            /// </summary>
            public string FileSHA512CheckBoxContent { get => fileSHA512CheckBoxContent; }
            /// <summary>
            /// 小写十六进制选框
            /// </summary>
            public string LowerHexFormatRadioButtonContent { get => lowerFormatHexRadioButtonContent; }
            /// <summary>
            /// 大写十六进制选框
            /// </summary>
            public string UpperHexFormatRadioButtonContent { get => upperHexFormatRadioButtonContent; }
            /// <summary>
            /// Base64选框
            /// </summary>
            public string Base64FormatRadioButtonContent { get => base64FormatRadioButtonContent; }
        }
    }
}
