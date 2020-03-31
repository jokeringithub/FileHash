using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;

namespace XstarS.FileHash.Models
{
    /// <summary>
    /// 表示 <see cref="FileInfoAndHash"/> 的队列。
    /// 仅支持添加、清空和获取项目操作，不支持插入、删除和设置项目操作。
    /// </summary>
    public class FileInfoAndHashCollection : Collection<FileInfoAndHash>
    {
        /// <summary>
        /// 表示计算文件哈希值的任务是否正在取消中。
        /// </summary>
        private bool IsCancelling = false;

        /// <summary>
        /// 表示更新计算进度的定时器。
        /// </summary>
        private readonly Timer ProgressTimer;

        /// <summary>
        /// 初始化 <see cref="FileInfoAndHashCollection"/> 类的新实例。
        /// </summary>
        public FileInfoAndHashCollection()
        {
            this.CurrentIndex = -1;
            this.Progress = new ListProgressView();
            this.ProgressTimer = new Timer();
            this.ProgressTimer.Elapsed += this.ProgressTimer_Elapsed;
            this.ProgressTimer.Start();
        }

        /// <summary>
        /// 获取当前正在在运行的项目的索引。
        /// </summary>
        protected int CurrentIndex { get; private set; }

        /// <summary>
        /// 获取当前正在在运行的项目。
        /// </summary>
        public FileInfoAndHash Current =>
            (this.CurrentIndex >= 0) ? this[this.CurrentIndex] : null;

        /// <summary>
        /// 获取计算文件哈希值的异步任务。
        /// </summary>
        protected Task HashingTask { get; private set; }

        /// <summary>
        /// 获取当前计算哈希值的进度。
        /// </summary>
        public ListProgressView Progress { get; }

        /// <summary>
        /// 当前项目计算哈希值完成时发生。
        /// </summary>
        public event EventHandler CurrentComplete;

        /// <summary>
        /// 当前列表计算哈希值完成时发生。
        /// </summary>
        public event EventHandler AllComplete;

        /// <summary>
        /// 将 <see cref="FileInfoAndHashCollection.Current"/> 移动到下一项目。
        /// </summary>
        /// <returns>是否成功移动到下一项目。</returns>
        private bool MoveNext()
        {
            if (this.CurrentIndex < this.Count - 1)
            {
                this.CurrentIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 逐个异步计算整个集合的文件哈希值。
        /// </summary>
        /// <returns>计算文件哈希值的异步任务。</returns>
        public Task ComputeAsync()
        {
            if (this.HashingTask is null)
            {
                this.HashingTask = Task.Run(() =>
                {
                    while (this.MoveNext())
                    {
                        if (this.IsCancelling) { break; }
                        Task.WaitAll(this.Current.ComputeAsync());
                        this.OnCurrentComplete();
                    }
                    if (this.IsCancelling) { this.IsCancelling = false; }
                    this.OnAllComplete();
                });
            }
            return this.HashingTask;
        }

        /// <summary>
        /// 取消计算当前文件的哈希值。
        /// </summary>
        public void CancelCurrent() => this.Current?.Cancel();

        /// <summary>
        /// 取消计算所有文件的哈希值。
        /// </summary>
        public void Cancel()
        {
            this.IsCancelling = true;
            this.CancelCurrent();
        }

        /// <inheritdoc/>
        protected override void ClearItems()
        {
            this.Current?.Cancel();
            this.HashingTask = null;
            this.Progress.ResetProgress();
            foreach (var item in this) { item?.Dispose(); }
            base.ClearItems();
            this.CurrentIndex = -1;
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">插入位置不为尾部。</exception>
        protected override void InsertItem(int index, FileInfoAndHash item)
        {
            if (index == this.Count)
            {
                base.InsertItem(index, item);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">不支持此方法调用。</exception>
        protected override void RemoveItem(int index)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">不支持此方法调用。</exception>
        protected override void SetItem(int index, FileInfoAndHash item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 引发 <see cref="FileInfoAndHashCollection.CurrentComplete"/> 事件。
        /// </summary>
        protected virtual void OnCurrentComplete()
        {
            this.CurrentComplete?.Invoke(this, EventArgs.Empty);
            this.Current?.Dispose();
        }

        /// <summary>
        /// 引发 <see cref="FileInfoAndHashCollection.AllComplete"/> 事件。
        /// </summary>
        protected virtual void OnAllComplete()
        {
            this.AllComplete?.Invoke(this, EventArgs.Empty);
            this.Progress.SetProgressComplete();
            this.HashingTask = null;
        }

        /// <summary>
        /// <see cref="FileInfoAndHashCollection.ProgressTimer"/> 达到时间间隔的事件处理。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">提供事件数据的对象。</param>
        private void ProgressTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var index = this.CurrentIndex;
            var current = this.Current;
            if (!(current is null))
            {
                this.Progress.CurrentProgress = current.Progress;
                this.Progress.AllProgress =
                    (double)index / this.Count + current.Progress / this.Count;
            }
        }
    }
}
