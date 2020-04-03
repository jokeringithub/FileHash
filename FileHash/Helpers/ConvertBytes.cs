using mstring = System.Text.StringBuilder;

namespace XstarS.FileHash.Helpers
{
    /// <summary>
    /// 提供字节数组的转换方法。
    /// </summary>
    internal static class ConvertBytes
    {
        /// <summary>
        /// 将指定的字节数组转换为十六进制字符串。
        /// </summary>
        /// <param name="bytes">要转换的字节数组。</param>
        /// <returns><paramref name="bytes"/> 转换得到的十六进制字符串。</returns>
        internal static string ToHexString(byte[] bytes)
        {
            if (bytes is null) { return null; }
            var hex = new mstring();
            foreach (var @byte in bytes)
            {
                hex.Append(@byte.ToString("X2"));
            }
            return hex.ToString();
        }

        /// <summary>
        /// 将指定的字节数组转换为小写十六进制字符串。
        /// </summary>
        /// <param name="bytes">要转换的字节数组。</param>
        /// <returns><paramref name="bytes"/> 转换得到的小写十六进制字符串。</returns>
        internal static string ToLowerHexString(byte[] bytes) =>
            ConvertBytes.ToHexString(bytes).ToLower();

        /// <summary>
        /// 将指定的字节数组转换为大写十六进制字符串。
        /// </summary>
        /// <param name="bytes">要转换的字节数组。</param>
        /// <returns><paramref name="bytes"/> 转换得到的大写十六进制字符串。</returns>
        internal static string ToUpperHexString(byte[] bytes) =>
            ConvertBytes.ToHexString(bytes).ToUpper();
    }
}
