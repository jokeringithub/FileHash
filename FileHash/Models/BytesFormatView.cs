using System.Runtime.CompilerServices;
using XstarS.ComponentModel;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示字节数组格式 <see cref="BytesFormat"/> 的视图。
    /// </summary>
    public class BytesFormatView : ObservableObject
    {
        /// <summary>
        /// 表示当前实例对应的 <see cref="BytesFormat"/>。
        /// </summary>
        private BytesFormat _Value;

        /// <summary>
        /// 初始化 <see cref="BytesFormatView"/> 类的新实例。
        /// </summary>
        public BytesFormatView() { }

        /// <summary>
        /// 获取当前实例对应的 <see cref="BytesFormat"/>。
        /// </summary>
        public BytesFormat Value => this._Value;

        /// <summary>
        /// 获取或设置格式是否为小写十六进制。
        /// </summary>
        public bool IsLowerHex
        {
            get => this.GetEnum(BytesFormat.LowerHex);
            set => this.SetEnum(BytesFormat.LowerHex, value);
        }

        /// <summary>
        /// 获取或设置格式是否为大写十六进制。
        /// </summary>
        public bool IsUpperHex
        {
            get => this.GetEnum(BytesFormat.UpperHex);
            set => this.SetEnum(BytesFormat.UpperHex, value);
        }

        /// <summary>
        /// 获取或设置格式是否为 Base64。
        /// </summary>
        public bool IsBase64
        {
            get => this.GetEnum(BytesFormat.Base64);
            set => this.SetEnum(BytesFormat.Base64, value);
        }

        /// <summary>
        /// 获取当前视图对应的枚举值是否为指定的枚举值。
        /// </summary>
        /// <param name="enum">要确定的枚举值。</param>
        protected bool GetEnum(BytesFormat @enum)
        {
            return this._Value == @enum;
        }

        /// <summary>
        /// 根据指示设置当前视图对应的枚举值。
        /// </summary>
        /// <param name="enum">要设置的枚举值。</param>
        /// <param name="value">指示是否设置枚举值。</param>
        /// <param name="propertyName">更改的属性名称，由编译器自动获取。</param>
        protected void SetEnum(BytesFormat @enum, bool value,
            [CallerMemberName] string propertyName = null)
        {
            if (value) { this.SetProperty(ref this._Value, @enum); }
        }
    }
}
