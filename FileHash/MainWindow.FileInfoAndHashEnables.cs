namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 文件信息和散列值标志向量。
        /// </summary>
        public class FileInfoAndHashEnables
        {
            /// <summary>
            /// 文件名。
            /// </summary>
            public bool Name { get; set; }
            /// <summary>
            /// 文件路径。
            /// </summary>
            public bool FullName { get; set; }
            /// <summary>
            /// 文件大小。
            /// </summary>
            public bool Length { get; set; }
            /// <summary>
            /// 修改日期。
            /// </summary>
            public bool LastWriteTime { get; set; }
            /// <summary>
            /// CRC32。
            /// </summary>
            public bool CRC32 { get; set; }
            /// <summary>
            /// MD5。
            /// </summary>
            public bool MD5 { get; set; }
            /// <summary>
            /// SHA-1。
            /// </summary>
            public bool SHA1 { get; set; }
            /// <summary>
            /// SHA-256。
            /// </summary>
            public bool SHA256 { get; set; }
            /// <summary>
            /// SHA-384。
            /// </summary>
            public bool SHA384 { get; set; }
            /// <summary>
            /// SHA-512。
            /// </summary>
            public bool SHA512 { get; set; }

            /// <summary>
            /// 所有标志位组成的标志向量。
            /// </summary>
            public bool[] AllEnables
            {
                get => new bool[]
                {
                    this.Name,
                    this.FullName,
                    this.Length,
                    this.LastWriteTime,
                    this.CRC32,
                    this.MD5,
                    this.SHA1,
                    this.SHA256,
                    this.SHA384,
                    this.SHA512
                };
            }
        }
    }
}
