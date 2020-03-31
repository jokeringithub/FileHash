using System.Runtime.CompilerServices;
using XstarS.ComponentModel;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示要输出的文件信息的字段。
    /// </summary>
    public class FileInfoFieldsView : ObservableDataObject
    {
        /// <summary>
        /// 表示当前视图对应的 <see cref="FileInfoFields"/>。
        /// </summary>
        private FileInfoFields _Value;

        /// <summary>
        /// 初始化 <see cref="FileInfoFieldsView"/> 类的新实例。
        /// </summary>
        public FileInfoFieldsView() { }

        /// <summary>
        /// 获取当前视图对应的 <see cref="FileInfoFields"/>。
        /// </summary>
        public FileInfoFields Value => this._Value;

        /// <summary>
        /// 获取或设置是否包含文件名。
        /// </summary>
        public bool HasName
        {
            get => this.HasFlag(FileInfoFields.Name);
            set => this.SetFlag(FileInfoFields.Name, value);
        }

        /// <summary>
        /// 获取或设置是否包含文件路径。
        /// </summary>
        public bool HasFullName
        {
            get => this.HasFlag(FileInfoFields.FullName);
            set => this.SetFlag(FileInfoFields.FullName, value);
        }

        /// <summary>
        /// 获取或设置是否包含文件大小。
        /// </summary>
        public bool HasLength
        {
            get => this.HasFlag(FileInfoFields.Length);
            set => this.SetFlag(FileInfoFields.Length, value);
        }

        /// <summary>
        /// 获取或设置是否包含文件修改日期。
        /// </summary>
        public bool HasLastWriteTime
        {
            get => this.HasFlag(FileInfoFields.LastWriteTime);
            set => this.SetFlag(FileInfoFields.LastWriteTime, value);
        }

        /// <summary>
        /// 获取当前视图对应的枚举值是否包含指定的位域。
        /// </summary>
        /// <param name="flag">要确定是否包含的位域。</param>
        protected bool HasFlag(FileInfoFields flag)
        {
            return (this._Value | flag) == this._Value;
        }

        /// <summary>
        /// 根据指示设置当前视图对应的枚举值的指定的位域。
        /// </summary>
        /// <param name="flag">要设置的位域。</param>
        /// <param name="value">指示是添加位域还是移除位域。</param>
        /// <param name="propertyName">更改的属性名称，由编译器自动获取。</param>
        protected void SetFlag(FileInfoFields flag, bool value,
            [CallerMemberName] string propertyName = null)
        {
            var _value = value ? (this._Value | flag) : (this._Value & ~flag);
            this.SetProperty(ref this._Value, _value);
        }
    }
}
