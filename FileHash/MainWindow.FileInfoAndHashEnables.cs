namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 文件信息和散列值标志向量类
        /// </summary>
        public class FileInfoAndHashEnables
        {
            private bool fileName;
            private bool fileFullName;
            private bool fileLength;
            private bool fileLastWriteTime;

            private bool fileCRC32;
            private bool fileMD5;
            private bool fileSHA1;
            private bool fileSHA256;
            private bool fileSHA384;
            private bool fileSHA512;

            /// <summary>
            /// 文件名
            /// </summary>
            public bool FileName { get => fileName; set => fileName = value; }
            /// <summary>
            /// 文件路径
            /// </summary>
            public bool FileFullName { get => fileFullName; set => fileFullName = value; }
            /// <summary>
            /// 文件大小
            /// </summary>
            public bool FileLength { get => fileLength; set => fileLength = value; }
            /// <summary>
            /// 修改日期
            /// </summary>
            public bool FileLastWriteTime { get => fileLastWriteTime; set => fileLastWriteTime = value; }

            /// <summary>
            /// CRC32
            /// </summary>
            public bool FileCRC32 { get => fileCRC32; set => fileCRC32 = value; }
            /// <summary>
            /// MD5
            /// </summary>
            public bool FileMD5 { get => fileMD5; set => fileMD5 = value; }
            /// <summary>
            /// SHA1
            /// </summary>
            public bool FileSHA1 { get => fileSHA1; set => fileSHA1 = value; }
            /// <summary>
            /// SHA256
            /// </summary>
            public bool FileSHA256 { get => fileSHA256; set => fileSHA256 = value; }
            /// <summary>
            /// SHA384
            /// </summary>
            public bool FileSHA384 { get => fileSHA384; set => fileSHA384 = value; }
            /// <summary>
            /// SHA512
            /// </summary>
            public bool FileSHA512 { get => fileSHA512; set => fileSHA512 = value; }

            /// <summary>
            /// 所有标志位组成的标志向量
            /// </summary>
            public bool[] AllEnables
            {
                get
                {
                    bool[] allEnables = new bool[10];

                    allEnables[0] = fileName;
                    allEnables[1] = fileFullName;
                    allEnables[2] = fileLength;
                    allEnables[3] = fileLastWriteTime;

                    allEnables[4] = fileCRC32;
                    allEnables[5] = fileMD5;
                    allEnables[6] = fileSHA1;
                    allEnables[7] = fileSHA256;
                    allEnables[8] = fileSHA384;
                    allEnables[9] = fileSHA512;

                    return allEnables;
                }
            }
        }
    }
}
