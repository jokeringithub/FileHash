namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 散列值显示格式。
        /// </summary>
        public class HashFormat
        {
            /// <summary>
            /// 指示是否为小写十六进制格式。
            /// </summary>
            public bool IsLowerHexFormat { get; set; }
            /// <summary>
            /// 指示是否为大写十六进制格式。
            /// </summary>
            public bool IsUpperHexFormat { get; set; }
            /// <summary>
            /// 指示是否为 Base64 格式。
            /// </summary>
            public bool IsBase64Format { get; set; }
        }
    }
}
