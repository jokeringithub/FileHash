using System;

namespace XstarS.FileHash.Helpers
{
    /// <summary>
    /// 提供枚举类型的相关帮助方法。
    /// </summary>
    internal static class EnumHelper
    {
        /// <summary>
        /// 获取指定枚举值的所有位域名称。
        /// </summary>
        /// <param name="value">要获取名称的枚举值。</param>
        /// <returns><paramref name="value"/> 的所有位域名称。</returns>
        internal static string[] GetNames(Enum value)
        {
            return value.ToString().Split(
                new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
