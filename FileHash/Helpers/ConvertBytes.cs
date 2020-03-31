using mstring = System.Text.StringBuilder;

namespace XstarS.FileHash.Helpers
{
    internal static class ConvertBytes
    {
        public static string ToHexString(byte[] bytes)
        {
            if (bytes is null) { return null; }
            var hex = new mstring();
            foreach (var @byte in bytes)
            {
                hex.Append(@byte.ToString("X2"));
            }
            return hex.ToString();
        }

        public static string ToLowerHexString(byte[] bytes) =>
            ConvertBytes.ToHexString(bytes).ToLower();

        public static string ToUpperHexString(byte[] bytes) =>
            ConvertBytes.ToHexString(bytes).ToUpper();
    }
}
