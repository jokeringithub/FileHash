namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示文件哈希值的类型。
    /// </summary>
    [System.Flags]
    public enum FileHashTypes
    {
        /// <summary>
        /// 表示 CRC32 文件哈希值。
        /// </summary>
        CRC32 = 0x01,
        /// <summary>
        /// 表示 MD5 文件哈希值。
        /// </summary>
        MD5 = 0x02,
        /// <summary>
        /// 表示 SHA-1 文件哈希值。
        /// </summary>
        SHA1 = 0x04,
        /// <summary>
        /// 表示 SHA-256 文件哈希值。
        /// </summary>
        SHA256 = 0x08,
        /// <summary>
        /// 表示 SHA-384 文件哈希值。
        /// </summary>
        SHA384 = 0x10,
        /// <summary>
        /// 表示 SHA-512 文件哈希值。
        /// </summary>
        SHA512 = 0x20
    }
}
