using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace XstarS.FileHash.Properties
{
    /// <summary>
    /// 提供应用程序的字符串资源。
    /// </summary>
    internal static class StringResources
    {
        /// <summary>
        /// 字符串资源内部存储。
        /// </summary>
        private static readonly Dictionary<CultureInfo, Dictionary<string, string>> LocalizedStrings;

        /// <summary>
        /// 初始化 <see cref="StringResources"/> 类。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static StringResources()
        {
            StringResources.LocalizedStrings = new Dictionary<CultureInfo, Dictionary<string, string>>()
            {
                [new CultureInfo("zh-CN")] = new Dictionary<string, string>()
                {
                    [nameof(MainWindowTitle)] = "文件哈希函数",
                    [nameof(CopyButtonContent)] = "复制",
                    [nameof(ClearButtonContent)] = "清除",
                    [nameof(SkipButtonContent)] = "跳过",
                    [nameof(CancelButtonContent)] = "取消",
                    [nameof(FileFullNameCheckBoxContent)] = "路径",
                    [nameof(FileLengthCheckBoxContent)] = "大小",
                    [nameof(FileLastWriteTimeCheckBoxContent)] = "修改时间",
                    [nameof(FileCRC32HashCheckBoxContent)] = "CRC32",
                    [nameof(FileMD5HashCheckBoxContent)] = "MD5",
                    [nameof(FileSHA1HashCheckBoxContent)] = "SHA-1",
                    [nameof(FileSHA256HashCheckBoxContent)] = "SHA-256",
                    [nameof(FileSHA384HashCheckBoxContent)] = "SHA-384",
                    [nameof(FileSHA512HashCheckBoxContent)] = "SHA-512",
                    [nameof(LowerHexHashFormatRadioButtonContent)] = "小写十六进制",
                    [nameof(UpperHexHashFormatRadioButtonContent)] = "大写十六进制",
                    [nameof(Base64HashFormatRadioButtonContent)] = "Base64",
                    [nameof(FileNameHeader)] = "",
                    [nameof(FileFullNameHeader)] = "路径：",
                    [nameof(FileLengthHeader)] = "大小：",
                    [nameof(FileLastWriteTimeHeader)] = "修改时间：",
                    [nameof(FileHashCRC32Header)] = "CRC32：",
                    [nameof(FileHashMD5Header)] = "MD5：",
                    [nameof(FileHashSHA1Header)] = "SHA-1：",
                    [nameof(FileHashSHA256Header)] = "SHA-256：",
                    [nameof(FileHashSHA384Header)] = "SHA-384：",
                    [nameof(FileHashSHA512Header)] = "SHA-512：",
                    [nameof(HashCancelMessageHead)] = "文件“",
                    [nameof(HashCancelMessageTail)] = "”哈希计算取消。",
                    [nameof(HashErrorMessageHead)] = "无法打开文件“",
                    [nameof(HashErrorMessageTail)] = "”。"
                },
                [new CultureInfo("ja-JP")] = new Dictionary<string, string>()
                {
                    [nameof(MainWindowTitle)] = "ファイルハッシュ",
                    [nameof(CopyButtonContent)] = "コピー",
                    [nameof(ClearButtonContent)] = "クリア",
                    [nameof(SkipButtonContent)] = "スキップ",
                    [nameof(CancelButtonContent)] = "キャンセル",
                    [nameof(FileFullNameCheckBoxContent)] = "パス",
                    [nameof(FileLengthCheckBoxContent)] = "サイズ",
                    [nameof(FileLastWriteTimeCheckBoxContent)] = "更新日時",
                    [nameof(FileCRC32HashCheckBoxContent)] = "CRC32",
                    [nameof(FileMD5HashCheckBoxContent)] = "MD5",
                    [nameof(FileSHA1HashCheckBoxContent)] = "SHA-1",
                    [nameof(FileSHA256HashCheckBoxContent)] = "SHA-256",
                    [nameof(FileSHA384HashCheckBoxContent)] = "SHA-384",
                    [nameof(FileSHA512HashCheckBoxContent)] = "SHA-512",
                    [nameof(LowerHexHashFormatRadioButtonContent)] = "小文字十六進法",
                    [nameof(UpperHexHashFormatRadioButtonContent)] = "大文字十六進法",
                    [nameof(Base64HashFormatRadioButtonContent)] = "Base64",
                    [nameof(FileNameHeader)] = "",
                    [nameof(FileFullNameHeader)] = "パス：",
                    [nameof(FileLengthHeader)] = "サイズ：",
                    [nameof(FileLastWriteTimeHeader)] = "更新日時：",
                    [nameof(FileHashCRC32Header)] = "CRC32：",
                    [nameof(FileHashMD5Header)] = "MD5：",
                    [nameof(FileHashSHA1Header)] = "SHA-1：",
                    [nameof(FileHashSHA256Header)] = "SHA-256：",
                    [nameof(FileHashSHA384Header)] = "SHA-384：",
                    [nameof(FileHashSHA512Header)] = "SHA-512：",
                    [nameof(HashCancelMessageHead)] = "ファイル「",
                    [nameof(HashCancelMessageTail)] = "」ハッシュ計算停止した。",
                    [nameof(HashErrorMessageHead)] = "ファイル「",
                    [nameof(HashErrorMessageTail)] = "」開く不能。"
                },
                [CultureInfo.InvariantCulture] = new Dictionary<string, string>()
                {
                    [nameof(MainWindowTitle)] = "File Hash",
                    [nameof(CopyButtonContent)] = "Copy",
                    [nameof(ClearButtonContent)] = "Clear",
                    [nameof(SkipButtonContent)] = "Skip",
                    [nameof(CancelButtonContent)] = "Cancel",
                    [nameof(FileFullNameCheckBoxContent)] = "Path",
                    [nameof(FileLengthCheckBoxContent)] = "Size",
                    [nameof(FileLastWriteTimeCheckBoxContent)] = "Modified",
                    [nameof(FileCRC32HashCheckBoxContent)] = "CRC32",
                    [nameof(FileMD5HashCheckBoxContent)] = "MD5",
                    [nameof(FileSHA1HashCheckBoxContent)] = "SHA-1",
                    [nameof(FileSHA256HashCheckBoxContent)] = "SHA-256",
                    [nameof(FileSHA384HashCheckBoxContent)] = "SHA-384",
                    [nameof(FileSHA512HashCheckBoxContent)] = "SHA-512",
                    [nameof(LowerHexHashFormatRadioButtonContent)] = "LowerHex",
                    [nameof(UpperHexHashFormatRadioButtonContent)] = "UpperHex",
                    [nameof(Base64HashFormatRadioButtonContent)] = "Base64",
                    [nameof(FileNameHeader)] = "",
                    [nameof(FileFullNameHeader)] = "Path: ",
                    [nameof(FileLengthHeader)] = "Size: ",
                    [nameof(FileLastWriteTimeHeader)] = "Modified: ",
                    [nameof(FileHashCRC32Header)] = "CRC32: ",
                    [nameof(FileHashMD5Header)] = "MD5: ",
                    [nameof(FileHashSHA1Header)] = "SHA-1: ",
                    [nameof(FileHashSHA256Header)] = "SHA-256: ",
                    [nameof(FileHashSHA384Header)] = "SHA-384: ",
                    [nameof(FileHashSHA512Header)] = "SHA-512: ",
                    [nameof(HashCancelMessageHead)] = "File \"",
                    [nameof(HashCancelMessageTail)] = "\" hash computing is canceled.",
                    [nameof(HashErrorMessageHead)] = "Can not open file \"",
                    [nameof(HashErrorMessageTail)] = "\"."
                }
            };
        }

        /// <summary>
        /// 字符串资源支持的所有区域信息。
        /// </summary>
        public static ICollection<CultureInfo> SupportedCultures =>
            StringResources.LocalizedStrings.Keys;

        /// <summary>
        /// 根据当前区域信息和资源名称获取指定类型的字符串资源的值。
        /// </summary>
        /// <param name="resourceName">字符串资源的名称。可由编译器自动获取。</param>
        /// <returns><see cref="StringResources.LocalizedStrings"/>
        /// 中对应当前区域信息且名称为 <paramref name="resourceName"/> 的字符串资源的值。</returns>
        internal static string GetString([CallerMemberName] string resourceName = null) =>
            StringResources.LocalizedStrings.ContainsKey(CultureInfo.CurrentUICulture) ?
            StringResources.LocalizedStrings[CultureInfo.CurrentUICulture][resourceName] :
            StringResources.LocalizedStrings.ContainsKey(CultureInfo.InvariantCulture) ?
            StringResources.LocalizedStrings[CultureInfo.InvariantCulture][resourceName] :
            string.Empty;

        #region 字符串资源对外接口。
        /// <summary>
        /// 主窗口的标题。
        /// </summary>
        public static string MainWindowTitle => StringResources.GetString();
        /// <summary>
        /// 复制按钮的内容。
        /// </summary>
        public static string CopyButtonContent => StringResources.GetString();
        /// <summary>
        /// 跳过按钮的内容。
        /// </summary>
        public static string SkipButtonContent => StringResources.GetString();
        /// <summary>
        /// 清除按钮的内容。
        /// </summary>
        public static string ClearButtonContent => StringResources.GetString();
        /// <summary>
        /// 取消按钮的内容。
        /// </summary>
        public static string CancelButtonContent => StringResources.GetString();
        /// <summary>
        /// 文件路径复选框的内容。
        /// </summary>
        public static string FileFullNameCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// 文件大小复选框的内容。
        /// </summary>
        public static string FileLengthCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// 修改日期复选框的内容。
        /// </summary>
        public static string FileLastWriteTimeCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// CRC32 复选框的内容。
        /// </summary>
        public static string FileCRC32HashCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// MD5 复选框的内容。
        /// </summary>
        public static string FileMD5HashCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// SHA-1 复选框的内容。
        /// </summary>
        public static string FileSHA1HashCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// SHA-256 复选框的内容。
        /// </summary>
        public static string FileSHA256HashCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// SHA-384 复选框的内容。
        /// </summary>
        public static string FileSHA384HashCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// SHA-512 复选框的内容。
        /// </summary>
        public static string FileSHA512HashCheckBoxContent => StringResources.GetString();
        /// <summary>
        /// 小写十六进制选框的内容。
        /// </summary>
        public static string LowerHexHashFormatRadioButtonContent => StringResources.GetString();
        /// <summary>
        /// 大写十六进制选框的内容。
        /// </summary>
        public static string UpperHexHashFormatRadioButtonContent => StringResources.GetString();
        /// <summary>
        /// Base64 选框的内容。
        /// </summary>
        public static string Base64HashFormatRadioButtonContent => StringResources.GetString();
        /// <summary>
        /// 文件名提示信息。
        /// </summary>
        public static string FileNameHeader => StringResources.GetString();
        /// <summary>
        /// 文件路径提示信息。
        /// </summary>
        public static string FileFullNameHeader => StringResources.GetString();
        /// <summary>
        /// 文件大小提示信息。
        /// </summary>
        public static string FileLengthHeader => StringResources.GetString();
        /// <summary>
        /// 文件修改时间提示信息。
        /// </summary>
        public static string FileLastWriteTimeHeader => StringResources.GetString();
        /// <summary>
        /// 文件 CRC32 哈希值提示信息。
        /// </summary>
        public static string FileHashCRC32Header => StringResources.GetString();
        /// <summary>
        /// 文件 MD5 哈希值提示信息。
        /// </summary>
        public static string FileHashMD5Header => StringResources.GetString();
        /// <summary>
        /// 文件 SHA-1 哈希值提示信息。
        /// </summary>
        public static string FileHashSHA1Header => StringResources.GetString();
        /// <summary>
        /// 文件 SHA-256 哈希值提示信息。
        /// </summary>
        public static string FileHashSHA256Header => StringResources.GetString();
        /// <summary>
        /// 文件 SHA-384 哈希值提示信息。
        /// </summary>
        public static string FileHashSHA384Header => StringResources.GetString();
        /// <summary>
        /// 文件 SHA-512 哈希值提示信息。
        /// </summary>
        public static string FileHashSHA512Header => StringResources.GetString();
        /// <summary>
        /// 文件哈希值计算取消提示信息头部。
        /// </summary>
        public static string HashCancelMessageHead => StringResources.GetString();
        /// <summary>
        /// 文件哈希值计算取消提示信息尾部。
        /// </summary>
        public static string HashCancelMessageTail => StringResources.GetString();
        /// <summary>
        /// 文件哈希值计算错误提示信息头部。
        /// </summary>
        public static string HashErrorMessageHead => StringResources.GetString();
        /// <summary>
        /// 文件哈希值计算错误提示信息尾部。
        /// </summary>
        public static string HashErrorMessageTail => StringResources.GetString();
        #endregion
    }
}
