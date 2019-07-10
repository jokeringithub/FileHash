using System.ComponentModel;
using XstarS.ComponentModel;

namespace FileHash.View
{
    /// <summary>
    /// 表示一个列表的进度。
    /// </summary>
    public abstract class ListProgress : INotifyPropertyChanged
    {
        /// <summary>
        /// 初始化 <see cref="ListProgress"/> 的新实例。
        /// </summary>
        public ListProgress() { }

        /// <summary>
        /// 所有进度。
        /// </summary>
        public abstract double All { get; set; }
        /// <summary>
        /// 当前进度。
        /// </summary>
        public abstract double Current { get; set; }

        /// <summary>
        /// 在属性值更改时发生。
        /// </summary>
        public abstract event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 创建一个 <see cref="ListProgress"/> 的实例。
        /// </summary>
        /// <returns>创建的 <see cref="ListProgress"/> 的实例。</returns>
        public static ListProgress Create() =>
            BindableTypeProvider<ListProgress>.Default.CreateInstance();
    }
}
