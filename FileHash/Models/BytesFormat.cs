namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示字节数组的字符串表示形式。
    /// </summary>
    public enum BytesFormat
    {
        /// <summary>
        /// 表示小写十六进制格式。
        /// </summary>
        LowerHex,
        /// <summary>
        /// 表示大写十六进制格式。
        /// </summary>
        UpperHex,
        /// <summary>
        /// 表示 Base64 格式。
        /// </summary>
        Base64
    }
}
