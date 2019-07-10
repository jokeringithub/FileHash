using System.ComponentModel;
using XstarS.ComponentModel;

namespace FileHash.View
{
    /// <summary>
    /// 二进制数值显示格式。
    /// </summary>
    public abstract class BinaryFormat : INotifyPropertyChanged
    {
        /// <summary>
        /// 初始化 <see cref="BinaryFormat"/> 的新实例。
        /// </summary>
        public BinaryFormat()
        {
            this.IsLowerHexFormat = false;
            this.IsUpperHexFormat = true;
            this.IsBase64Format = false;
        }

        /// <summary>
        /// 指示是否为小写十六进制格式。
        /// </summary>
        public abstract bool IsLowerHexFormat { get; set; }
        /// <summary>
        /// 指示是否为大写十六进制格式。
        /// </summary>
        public abstract bool IsUpperHexFormat { get; set; }
        /// <summary>
        /// 指示是否为 Base64 格式。
        /// </summary>
        public abstract bool IsBase64Format { get; set; }

        /// <summary>
        /// 在属性值更改时发生。
        /// </summary>
        public abstract event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 创建一个 <see cref="BinaryFormat"/> 的实例。
        /// </summary>
        /// <returns>创建的 <see cref="BinaryFormat"/> 的实例。</returns>
        public static BinaryFormat Create() =>
            BindableTypeProvider<BinaryFormat>.Default.CreateInstance();
    }
}
