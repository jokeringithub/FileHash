using System.ComponentModel;
using XstarS.ComponentModel;

namespace FileHash.View
{
    /// <summary>
    /// 文件信息和散列值标志向量。
    /// </summary>
    public abstract class FileInfoAndHashFlags : INotifyPropertyChanged
    {
        /// <summary>
        /// 初始化 <see cref="FileInfoAndHashFlags"/> 的新实例。
        /// </summary>
        public FileInfoAndHashFlags()
        {
            this.Name = true;
            this.FullName = false;
            this.Length = true;
            this.LastWriteTime = true;
            this.CRC32 = false;
            this.MD5 = true;
            this.SHA1 = true;
            this.SHA256 = true;
            this.SHA384 = false;
            this.SHA512 = false;
        }

        /// <summary>
        /// 文件名。
        /// </summary>
        public abstract bool Name { get; set; }
        /// <summary>
        /// 文件路径。
        /// </summary>
        public abstract bool FullName { get; set; }
        /// <summary>
        /// 文件大小。
        /// </summary>
        public abstract bool Length { get; set; }
        /// <summary>
        /// 修改日期。
        /// </summary>
        public abstract bool LastWriteTime { get; set; }
        /// <summary>
        /// CRC32。
        /// </summary>
        public abstract bool CRC32 { get; set; }
        /// <summary>
        /// MD5。
        /// </summary>
        public abstract bool MD5 { get; set; }
        /// <summary>
        /// SHA-1。
        /// </summary>
        public abstract bool SHA1 { get; set; }
        /// <summary>
        /// SHA-256。
        /// </summary>
        public abstract bool SHA256 { get; set; }
        /// <summary>
        /// SHA-384。
        /// </summary>
        public abstract bool SHA384 { get; set; }
        /// <summary>
        /// SHA-512。
        /// </summary>
        public abstract bool SHA512 { get; set; }
        /// <summary>
        /// 所有标志位组成的标志向量。
        /// </summary>
        public bool[] Flags => new bool[]
        {
            this.Name, this.FullName, this.Length, this.LastWriteTime,
            this.CRC32, this.MD5, this.SHA1, this.SHA256, this.SHA384, this.SHA512
        };

        /// <summary>
        /// 在属性值更改时发生。
        /// </summary>
        public abstract event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 创建一个 <see cref="FileInfoAndHashFlags"/> 的实例。
        /// </summary>
        /// <returns>创建的 <see cref="FileInfoAndHashFlags"/> 的实例。</returns>
        public static FileInfoAndHashFlags Create() =>
            BindableTypeProvider<FileInfoAndHashFlags>.Default.CreateInstance();
    }
}
