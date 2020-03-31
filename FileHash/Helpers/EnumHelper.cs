using System;

namespace XstarS.FileHash.Helpers
{
    internal static class EnumHelper
    {
        public static string[] GetNames(Enum value)
        {
            return value.ToString().Split(new[] { ", " }, StringSplitOptions.None);
        }
    }
}
