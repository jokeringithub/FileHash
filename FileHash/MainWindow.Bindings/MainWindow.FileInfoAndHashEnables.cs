using System;
using System.Collections.Generic;
using System.Linq;
using XstarS.ComponentModel;

namespace FileHash
{
    public partial class MainWindow
    {
        /// <summary>
        /// 文件信息和散列值标志向量。
        /// </summary>
        public class FileInfoAndHashEnables : BindableObject
        {
            /// <summary>
            /// 文件名。
            /// </summary>
            private bool name;
            /// <summary>
            /// 文件路径。
            /// </summary>
            private bool fullName;
            /// <summary>
            /// 文件大小。
            /// </summary>
            private bool length;
            /// <summary>
            /// 修改日期。
            /// </summary>
            private bool lastWriteTime;
            /// <summary>
            /// CRC32。
            /// </summary>
            private bool crc32;
            /// <summary>
            /// MD5。
            /// </summary>
            private bool md5;
            /// <summary>
            /// SHA-1。
            /// </summary>
            private bool sha1;
            /// <summary>
            /// SHA-256。
            /// </summary>
            private bool sha256;
            /// <summary>
            /// SHA-384。
            /// </summary>
            private bool sha384;
            /// <summary>
            /// SHA-512。
            /// </summary>
            private bool sha512;

            /// <summary>
            /// 文件名。
            /// </summary>
            public bool Name
            {
                get => this.name;
                set => this.SetProperty(ref this.name, value);
            }
            /// <summary>
            /// 文件路径。
            /// </summary>
            public bool FullName
            {
                get => this.fullName;
                set => this.SetProperty(ref this.fullName, value);
            }
            /// <summary>
            /// 文件大小。
            /// </summary>
            public bool Length
            {
                get => this.length;
                set => this.SetProperty(ref this.length, value);
            }
            /// <summary>
            /// 修改日期。
            /// </summary>
            public bool LastWriteTime
            {
                get => this.lastWriteTime;
                set => this.SetProperty(ref this.lastWriteTime, value);
            }
            /// <summary>
            /// CRC32。
            /// </summary>
            public bool CRC32
            {
                get => this.crc32;
                set => this.SetProperty(ref this.crc32, value);
            }
            /// <summary>
            /// MD5。
            /// </summary>
            public bool MD5
            {
                get => this.md5;
                set => this.SetProperty(ref this.md5, value);
            }
            /// <summary>
            /// SHA-1。
            /// </summary>
            public bool SHA1
            {
                get => this.sha1;
                set => this.SetProperty(ref this.sha1, value);
            }
            /// <summary>
            /// SHA-256。
            /// </summary>
            public bool SHA256
            {
                get => this.sha256;
                set => this.SetProperty(ref this.sha256, value);
            }
            /// <summary>
            /// SHA-384。
            /// </summary>
            public bool SHA384
            {
                get => this.sha384;
                set => this.SetProperty(ref this.sha384, value);
            }
            /// <summary>
            /// SHA-512。
            /// </summary>
            public bool SHA512
            {
                get => this.sha512;
                set => this.SetProperty(ref this.sha512, value);
            }

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
