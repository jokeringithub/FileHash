using System.Runtime.CompilerServices;
using XstarS.ComponentModel;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示文件哈希值类型 <see cref="FileHashTypes"/> 的视图。
    /// </summary>
    public class FileHashTypesView : ObservableObject
    {
        /// <summary>
        /// 表示当前视图对应的 <see cref="FileHashTypes"/>。
        /// </summary>
        private FileHashTypes _Value;

        /// <summary>
        /// 初始化 <see cref="FileHashTypesView"/> 类的新实例。
        /// </summary>
        public FileHashTypesView() { }

        /// <summary>
        /// 获取当前视图对应的 <see cref="FileHashTypes"/>。
        /// </summary>
        public FileHashTypes Value => this._Value;

        /// <summary>
        /// 获取或设置是否包含 CRC32 哈希函数。
        /// </summary>
        public bool HasCRC32
        {
            get => this.HasFlag(FileHashTypes.CRC32);
            set => this.SetFlag(FileHashTypes.CRC32, value);
        }

        /// <summary>
        /// 获取或设置是否包含 MD5 哈希函数。
        /// </summary>
        public bool HasMD5
        {
            get => this.HasFlag(FileHashTypes.MD5);
            set => this.SetFlag(FileHashTypes.MD5, value);
        }

        /// <summary>
        /// 获取或设置是否包含 SHA-1 哈希函数。
        /// </summary>
        public bool HasSHA1
        {
            get => this.HasFlag(FileHashTypes.SHA1);
            set => this.SetFlag(FileHashTypes.SHA1, value);
        }

        /// <summary>
        /// 获取或设置是否包含 SHA-256 哈希函数。
        /// </summary>
        public bool HasSHA256
        {
            get => this.HasFlag(FileHashTypes.SHA256);
            set => this.SetFlag(FileHashTypes.SHA256, value);
        }

        /// <summary>
        /// 获取或设置是否包含 SHA-384 哈希函数。
        /// </summary>
        public bool HasSHA384
        {
            get => this.HasFlag(FileHashTypes.SHA384);
            set => this.SetFlag(FileHashTypes.SHA384, value);
        }

        /// <summary>
        /// 获取或设置是否包含 SHA-512 哈希函数。
        /// </summary>
        public bool HasSHA512
        {
            get => this.HasFlag(FileHashTypes.SHA512);
            set => this.SetFlag(FileHashTypes.SHA512, value);
        }

        /// <summary>
        /// 获取当前视图对应的枚举值是否包含指定的位域。
        /// </summary>
        /// <param name="flag">要确定是否包含的位域。</param>
        protected bool HasFlag(FileHashTypes flag)
        {
            return (this._Value | flag) == this._Value;
        }

        /// <summary>
        /// 根据指示设置当前视图对应的枚举值的指定的位域。
        /// </summary>
        /// <param name="flag">要设置的位域。</param>
        /// <param name="value">指示是添加位域还是移除位域。</param>
        /// <param name="propertyName">更改的属性名称，由编译器自动获取。</param>
        protected void SetFlag(FileHashTypes flag, bool value,
            [CallerMemberName] string propertyName = null)
        {
            var _value = value ? (this._Value | flag) : (this._Value & ~flag);
            this.SetProperty(ref this._Value, _value);
        }
    }
}
