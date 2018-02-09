namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 散列值显示格式类
        /// </summary>
        public class HashFormat
        {
            private bool isLowerHexFormat;
            private bool isUpperHexFormat;
            private bool isBase64Format;

            /// <summary>
            /// 指示是否为小写十六进制格式
            /// </summary>
            public bool IsLowerHexFormat { get => isLowerHexFormat; set => isLowerHexFormat = value; }
            /// <summary>
            /// 指示是否为大写十六进制格式
            /// </summary>
            public bool IsUpperHexFormat { get => isUpperHexFormat; set => isUpperHexFormat = value; }
            /// <summary>
            /// 指示是否为Base64格式
            /// </summary>
            public bool IsBase64Format { get => isBase64Format; set => isBase64Format = value; }
        }
    }
}
