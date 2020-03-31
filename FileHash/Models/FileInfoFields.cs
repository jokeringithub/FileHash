namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示文件信息的字段。
    /// </summary>
    [System.Flags]
    public enum FileInfoFields
    {
        /// <summary>
        /// 表示文件名字段。
        /// </summary>
        Name = 0x01,
        /// <summary>
        /// 表示文件路径字段。
        /// </summary>
        FullName = 0x02,
        /// <summary>
        /// 表示文件大小字段。
        /// </summary>
        Length = 0x04,
        /// <summary>
        /// 表示文件修改日期字段。
        /// </summary>
        LastWriteTime = 0x08
    }
}
