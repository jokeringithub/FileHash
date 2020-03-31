using System.Security.Cryptography;

namespace XstarS.Security.Cryptography
{
    /// <summary>
    /// 提供 CRC32 算法的实现。
    /// </summary>s
    public class CRC32 : HashAlgorithm
    {
        /// <summary>
        /// 默认多项式。
        /// </summary>
        public const uint DefaultPolynomial = 0xedb88320;

        /// <summary>
        /// 默认种子。
        /// </summary>
        public const uint DefaultSeed = 0xffffffff;

        /// <summary>
        /// 默认哈希表。
        /// </summary>
        protected static uint[] DefaultTable;

        /// <summary>
        /// 当前哈希值。
        /// </summary>
        protected uint Current;

        /// <summary>
        /// 种子。
        /// </summary>
        protected readonly uint Seed;

        /// <summary>
        /// 哈希表。
        /// </summary>
        protected readonly uint[] Table;

        /// <summary>
        /// 初始化 <see cref="CRC32"/> 类的新实例。
        /// </summary>
        protected CRC32()
            : this(CRC32.DefaultPolynomial, CRC32.DefaultSeed)
        {
        }

        /// <summary>
        /// 根据指定的多项式和种子初始化 <see cref="CRC32"/> 类的新实例。
        /// </summary>
        /// <param name="polynomial">多项式。</param>
        /// <param name="seed">种子。</param>
        protected CRC32(uint polynomial, uint seed)
        {
            this.HashSizeValue = 32;
            this.Table = CRC32.CreateTable(polynomial);
            this.Seed = seed;
            this.Initialize();
        }

        /// <summary>
        /// 创建一个默认的实例实现 CRC32。
        /// </summary>
        /// <returns>CRC32 实例。</returns>
        new public static CRC32 Create() => new CRC32();

        /// <summary>
        /// 根据所给的多项式和种子创建实例实现 CRC32。
        /// </summary>
        /// <param name="polynomial">所给的多项式。</param>
        /// <param name="seed">所给的种子。</param>
        /// <returns>CRC32 实例。</returns>
        public static CRC32 Create(uint polynomial, uint seed)
        {
            return new CRC32(polynomial, seed);
        }

        /// <summary>
        /// 初始化 CRC32 实例。
        /// </summary>
        public override void Initialize()
        {
            this.Current = this.Seed;
        }

        /// <summary>
        /// 将写入对象的数据路由到哈希算法以计算哈希值。
        /// </summary>
        /// <param name="array">要计算其哈希代码的输入。</param>
        /// <param name="ibStart">字节数组中的偏移量，从该位置开始使用数据。</param>
        /// <param name="cbSize">字节数组中用作数据的字节数。</param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            this.Current = CRC32.CalculateHash(this.Current, this.Table, array, ibStart, cbSize);
        }

        /// <summary>
        /// 在加密流对象处理完最后的数据后完成哈希计算。
        /// </summary>
        /// <returns>计算所得的哈希代码。</returns>
        protected override byte[] HashFinal()
        {
            byte[] hashValue = CRC32.GetBytesBigEndian(~this.Current);
            this.HashValue = hashValue;
            return hashValue;
        }

        /// <summary>
        /// 根据指定多项式初始化哈希表。
        /// </summary>
        /// <param name="polynomial">多项式。</param>
        /// <returns>哈希表。</returns>
        protected static uint[] CreateTable(uint polynomial)
        {
            if ((polynomial == CRC32.DefaultPolynomial) && !(CRC32.DefaultTable is null))
            {
                return CRC32.DefaultTable;
            }

            uint[] table = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint entry = (uint)i;
                for (int j = 0; j < 8; j++)
                {
                    entry >>= 1;
                    if ((entry & 1) == 1) { entry ^= polynomial; }
                }
                table[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
            {
                CRC32.DefaultTable = table;
            }

            return table;
        }

        /// <summary>
        /// 根据指定哈希表和种子计算指定字节数组的 CRC32。
        /// </summary>
        /// <param name="seed">种子。</param>
        /// <param name="table">哈希表。</param>
        /// <param name="array">要计算其哈希代码的输入。</param>
        /// <param name="ibStart">字节数组中的偏移量，从该位置开始使用数据。</param>
        /// <param name="cbSize">字节数组中用作数据的字节数。</param>
        /// <returns>32 位无符号整数 CRC32。</returns>
        protected static uint CalculateHash(uint seed, uint[] table,
            byte[] array, int ibStart, int cbSize)
        {
            uint crc32 = seed;
            for (int i = ibStart; i < cbSize; i++)
            {
                unchecked
                {
                    crc32 = (crc32 >> 8) ^ table[array[i] ^ crc32 & 0xff];
                }
            }
            return crc32;
        }

        /// <summary>
        /// 将 32 位无符号整数转换为大端序 8 位无符号整数数组。
        /// </summary>
        /// <param name="value">要转换的 32 为无符号整数。</param>
        /// <returns>与 <paramref name="value"/> 等效的大端序 8 位无符号整数数组。</returns>
        private static byte[] GetBytesBigEndian(uint value)
        {
            return new byte[]
            {
                (byte)((value >> 24) & 0xff),
                (byte)((value >> 16) & 0xff),
                (byte)((value >> 8) & 0xff),
                (byte)(value & 0xff)
            };
        }
    }
}
