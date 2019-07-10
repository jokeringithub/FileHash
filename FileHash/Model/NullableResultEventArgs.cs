using System;

namespace FileHash.Model
{
    /// <summary>
    /// 提供结果可为空值的事件数据。
    /// </summary>
    public class NullableResultEventArgs<TResult> : EventArgs
    {
        /// <summary>
        /// 初始化 <see cref="NullableResultEventArgs{TResult}"/> 类的新实例。
        /// </summary>
        public NullableResultEventArgs()
        {
            this.HasResult = false;
        }

        /// <summary>
        /// 以结果初始化 <see cref="NullableResultEventArgs{TResult}"/> 类的新实例。
        /// </summary>
        /// <param name="result">传递到事件的结果。</param>
        public NullableResultEventArgs(TResult result)
        {
            this.HasResult = true;
            this.Result = result;
        }

        /// <summary>
        /// 指示事件是否存在结果。
        /// </summary>
        public bool HasResult { get; }

        /// <summary>
        /// 传递到事件的结果。
        /// </summary>
        public TResult Result { get; }
    }
}
