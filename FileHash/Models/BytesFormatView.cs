using XstarS.ComponentModel;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示字节数组格式 <see cref="BytesFormat"/> 的视图。
    /// </summary>
    public sealed class BytesFormatView : EnumVectorView<BytesFormat>
    {
        /// <summary>
        /// 初始化 <see cref="BytesFormatView"/> 类的新实例。
        /// </summary>
        public BytesFormatView() { }

        /// <summary>
        /// 获取或设置格式是否为小写十六进制。
        /// </summary>
        public bool LowerHex { get => this.IsEnum(); set => this.SetEnum(value); }

        /// <summary>
        /// 获取或设置格式是否为大写十六进制。
        /// </summary>
        public bool UpperHex { get => this.IsEnum(); set => this.SetEnum(value); }

        /// <summary>
        /// 获取或设置格式是否为 Base64。
        /// </summary>
        public bool Base64 { get => this.IsEnum(); set => this.SetEnum(value); }
    }
}
