using XstarS.ComponentModel;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示文件哈希值类型 <see cref="FileHashTypes"/> 的视图。
    /// </summary>
    public sealed class FileHashTypesView : EnumFlagsVectorView<FileHashTypes>
    {
        /// <summary>
        /// 初始化 <see cref="FileHashTypesView"/> 类的新实例。
        /// </summary>
        public FileHashTypesView() { }

        /// <summary>
        /// 获取或设置是否包含 CRC32 哈希函数。
        /// </summary>
        public bool CRC32 { get => this.HasFlag(); set => this.SetFlag(value); }

        /// <summary>
        /// 获取或设置是否包含 MD5 哈希函数。
        /// </summary>
        public bool MD5 { get => this.HasFlag(); set => this.SetFlag(value); }

        /// <summary>
        /// 获取或设置是否包含 SHA-1 哈希函数。
        /// </summary>
        public bool SHA1 { get => this.HasFlag(); set => this.SetFlag(value); }

        /// <summary>
        /// 获取或设置是否包含 SHA-256 哈希函数。
        /// </summary>
        public bool SHA256 { get => this.HasFlag(); set => this.SetFlag(value); }

        /// <summary>
        /// 获取或设置是否包含 SHA-384 哈希函数。
        /// </summary>
        public bool SHA384 { get => this.HasFlag(); set => this.SetFlag(value); }

        /// <summary>
        /// 获取或设置是否包含 SHA-512 哈希函数。
        /// </summary>
        public bool SHA512 { get => this.HasFlag(); set => this.SetFlag(value); }
    }
}
