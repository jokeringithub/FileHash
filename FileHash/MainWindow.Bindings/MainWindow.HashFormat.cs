using System;
using System.Collections.Generic;
using System.Linq;
using XstarS.ComponentModel;

namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 散列值显示格式。
        /// </summary>
        public class HashFormat : BindableObject
        {
            /// <summary>
            /// 指示是否为小写十六进制格式。
            /// </summary>
            private bool isLowerHexFormat;
            /// <summary>
            /// 指示是否为大写十六进制格式。
            /// </summary>
            private bool isUpperHexFormat;
            /// <summary>
            /// 指示是否为 Base64 格式。
            /// </summary>
            private bool isBase64Format;

            /// <summary>
            /// 指示是否为小写十六进制格式。
            /// </summary>
            public bool IsLowerHexFormat
            {
                get => this.isLowerHexFormat;
                set => this.SetProperty(ref this.isLowerHexFormat, value);
            }
            /// <summary>
            /// 指示是否为大写十六进制格式。
            /// </summary>
            public bool IsUpperHexFormat
            {
                get => this.isUpperHexFormat;
                set => this.SetProperty(ref this.isUpperHexFormat, value);
            }
            /// <summary>
            /// 指示是否为 Base64 格式。
            /// </summary>
            public bool IsBase64Format
            {
                get => this.isBase64Format;
                set => this.SetProperty(ref this.isBase64Format, value);
            }
        }
    }
}
