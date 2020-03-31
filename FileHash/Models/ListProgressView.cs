using XstarS.ComponentModel;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示一个列表的进度。
    /// </summary>
    public class ListProgressView : ObservableDataObject
    {
        /// <summary>
        /// 初始化 <see cref="ListProgressView"/> 类的新实例。
        /// </summary>
        public ListProgressView() { }

        /// <summary>
        /// 获取或设置当前进度。
        /// </summary>
        public double CurrentProgress
        {
            get => this.GetProperty<double>();
            set => this.SetProperty(value);
        }

        /// <summary>
        /// 获取或设置整体进度。
        /// </summary>
        public double AllProgress
        {
            get => this.GetProperty<double>();
            set => this.SetProperty(value);
        }

        /// <summary>
        /// 重设当前和整体进度。
        /// </summary>
        public void ResetProgress()
        {
            this.CurrentProgress = 0.0;
            this.AllProgress = 0.0;
        }

        /// <summary>
        /// 设定当前和整体进度已完成。
        /// </summary>
        public void SetProgressComplete()
        {
            this.CurrentProgress = 1.0;
            this.AllProgress = 1.0;
        }
    }
}
