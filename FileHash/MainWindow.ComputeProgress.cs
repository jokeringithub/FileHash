using System.ComponentModel;

namespace FileHash
{
    partial class MainWindow
    {
        /// <summary>
        /// 文件散列计算进度类
        /// </summary>
        public class ComputeProgress : BindableObject
        {
            private double currenFileProgress;
            private double allFileProgress;

            /// <summary>
            /// 当前文件计算进度
            /// </summary>
            public double CurrentFileProgress { get => currenFileProgress; set => SetProperty(ref currenFileProgress, value); }
            /// <summary>
            /// 所有文件计算进度
            /// </summary>
            public double AllFileProgress { get => allFileProgress; set => SetProperty(ref allFileProgress, value); }
        }
    }
}
