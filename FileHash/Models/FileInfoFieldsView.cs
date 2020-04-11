using XstarS.ComponentModel;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示要输出的文件信息的字段。
    /// </summary>
    public sealed class FileInfoFieldsView : EnumFlagsVectorView<FileInfoFields>
    {
        /// <summary>
        /// 初始化 <see cref="FileInfoFieldsView"/> 类的新实例。
        /// </summary>
        public FileInfoFieldsView() { }

        /// <summary>
        /// 获取或设置是否包含文件名。
        /// </summary>
        public bool Name { get => this.HasFlag(); set => this.SetFlag(value); }

        /// <summary>
        /// 获取或设置是否包含文件路径。
        /// </summary>
        public bool FullName { get => this.HasFlag(); set => this.SetFlag(value); }

        /// <summary>
        /// 获取或设置是否包含文件大小。
        /// </summary>
        public bool Length { get => this.HasFlag(); set => this.SetFlag(value); }

        /// <summary>
        /// 获取或设置是否包含文件修改日期。
        /// </summary>
        public bool LastWriteTime { get => this.HasFlag(); set => this.SetFlag(value); }
    }
}
