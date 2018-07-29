using System.ComponentModel;

namespace FileHash
{
    partial class MainWindow
    {
        /// <summary>
        /// 文件散列计算进度。
        /// </summary>
        public class ComputeProgress : BindableObject
        {
            /// <summary>
            /// 当前文件计算进度。
            /// </summary>
            private double currenFileProgress;
            /// <summary>
            /// 所有文件计算进度。
            /// </summary>
            private double allFileProgress;

            /// <summary>
            /// 当前文件计算进度。
            /// </summary>
            public double CurrentFileProgress
            {
                get => this.currenFileProgress;
                set => this.SetProperty(ref this.currenFileProgress, value);
            }
            /// <summary>
            /// 所有文件计算进度。
            /// </summary>
            public double AllFileProgress
            {
                get => this.allFileProgress;
                set => this.SetProperty(ref this.allFileProgress, value);
            }
        }
    }
}
