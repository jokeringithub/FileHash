using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using FileHash.Model;
using FileHash.Properties;
using FileHash.View;

namespace FileHash
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 主窗口的刷新定时器。
        /// </summary>
        private readonly Timer RefreshTimer;
        /// <summary>
        /// 计算文件散列的线程队列。
        /// </summary>
        private readonly FileInfoAndHashList FileList;

        /// <summary>
        /// 初始化 <see cref="MainWindow"/> 的实例。
        /// </summary>
        public MainWindow()
        {
            // 初始化部分参数。
            this.FileList = new FileInfoAndHashList();
            this.FileList.CurrentCompleted += this.FileList_CurrentCompleted;
            this.FileList.ListCompleted += this.FileList_ListCompleted;
            this.ResultFlags = FileInfoAndHashFlags.Create();
            this.HashFormat = BinaryFormat.Create();
            this.HashFormat.IsUpperHexFormat = true;
            this.FileListProgress = ListProgress.Create();
            this.LocalizedResult = new LocalizedFileInfoAndHash();

            // 初始化用户控件。
            this.LocalizedResources = new LocalizedResources();
            this.InitializeComponent();
            
            // 初始化定时器。
            const double fps = 30;
            this.RefreshTimer = new Timer(1000 / fps);
            this.RefreshTimer.Elapsed += this.RefreshTimer_Elapsed;
        }

        /// <summary>
        /// 本地化用户控件。
        /// </summary>
        public LocalizedResources LocalizedResources { get; }
        /// <summary>
        /// 将计算结果本地化。
        /// </summary>
        public LocalizedFileInfoAndHash LocalizedResult { get; }
        /// <summary>
        /// 文件信息和散列值标志向量。
        /// </summary>
        public FileInfoAndHashFlags ResultFlags { get; }
        /// <summary>
        /// 指示散列值的格式。
        /// </summary>
        public BinaryFormat HashFormat { get; }
        /// <summary>
        /// 文件散列计算进度。
        /// </summary>
        public ListProgress FileListProgress { get; }

        /// <summary>
        /// 拖放完成，传递拖放的文件路径字符串。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultTextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) != null)
            {
                e.Effects = DragDropEffects.Copy;
            }
            e.Handled = true;
        }

        /// <summary>
        /// 拖放释放，取出文件路径，启动计算。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultTextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] dragedStrings = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dragedStrings != null)
            {
                var pathList = new List<string>();
                foreach (string path in dragedStrings)
                {
                    if (File.Exists(path))
                    {
                        pathList.Add(Path.GetFullPath(path));
                    }
                    else if (Directory.Exists(path))
                    {
                        pathList.AddRange(Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories));
                    }
                }
                this.AddToFileInfoAndHashList(pathList.ToArray());
                this.RefreshTimer.Start();
            }
        }

        /// <summary>
        /// 复制按钮按下的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.LocalizedResult.Value != string.Empty)
            {
                Clipboard.SetText(this.LocalizedResult.Value);
            }
        }

        /// <summary>
        /// 清除按钮按下的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.LocalizedResult.Value = string.Empty;
        }

        /// <summary>
        /// 跳过按钮按下的时间处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            this.FileList.Skip();
        }

        /// <summary>
        /// 取消计算按钮按下的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.FileList.Cancel();
        }

        /// <summary>
        /// 文本框内容变化时，自动滚动到最底端。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.resultTextBox.SelectionStart = resultTextBox.Text.Length;
            this.resultTextBox.ScrollToEnd();
        }

        /// <summary>
        /// 定时器到达间隔的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.RefreshTimer.Enabled)
            {
                this.UpadateComputeProgress();
            }
        }

        /// <summary>
        /// 当前文件散列计算完成的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileList_CurrentCompleted(
            object sender, NullableResultEventArgs<FileInfoAndHash> e)
        {
            this.UpdateComputeResult(e.Result);

            try
            {
                this.FileList.StartNext();
            }
            catch (FileNotFoundException exception)
            {
                this.LocalizedResult.AppendFileError(exception.FileName);
            }
        }

        /// <summary>
        /// 全部文件散列计算完成的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileList_ListCompleted(
            object sender, NullableResultEventArgs<FileInfoAndHashList> e)
        {
            this.RefreshTimer.Stop();
            this.FileList.Clear();
            this.FileListProgress.Current = 1;
            this.FileListProgress.All = 1;
        }

        /// <summary>
        /// 将新文件添加到计算线程列表，并启动第一个计算线程
        /// </summary>
        /// <param name="filePaths">文件路径</param>
        private void AddToFileInfoAndHashList(string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                try
                {
                    this.FileList.Add(filePath, this.ResultFlags.Flags);
                }
                catch (FileNotFoundException)
                {
                    this.LocalizedResult.AppendFileError(filePath);
                    continue;
                }
            }

            try
            {
                this.FileList.StartNext();
            }
            catch (FileNotFoundException exception)
            {
                this.LocalizedResult.AppendFileError(exception.FileName);
            }
        }

        /// <summary>
        /// 更新计算结果。
        /// </summary>
        /// <param name="result">计算结果参数传递。</param>
        private void UpdateComputeResult(FileInfoAndHash result)
        {
            // 取出带格式的结果，转化为本地化结果。
            try
            {
                if (this.HashFormat.IsLowerHexFormat)
                {
                    this.LocalizedResult.Append(result.ToLowerHexStrings());
                }
                else if (this.HashFormat.IsUpperHexFormat)
                {
                    this.LocalizedResult.Append(result.ToUpperHexStrings());
                }
                else if (this.HashFormat.IsBase64Format)
                {
                    this.LocalizedResult.Append(result.ToBase64Strings());
                }
            }
            // 文件散列计算未完成。
            catch (NotImplementedException)
            {
                this.LocalizedResult.AppendCancelled(result.FilePath);
            }
        }

        /// <summary>
        /// 更新计算进度。
        /// </summary>
        private void UpadateComputeProgress()
        {
            // 当前文件
            if (this.FileList.Current != null)
            {
                this.FileListProgress.Current =
                    this.FileList.Current.ComputeProgress;
            }

            // 所有文件。
            if ((this.FileList != null) && (this.FileList.Count != 0))
            {
                // 获取完成计算的文件数量。
                int computedFileCount;
                try
                {
                    computedFileCount = this.FileList.IndexOf(FileList.Current);
                }
                catch (Exception)
                {
                    computedFileCount = this.FileList.Count;
                }

                // 避免在计算完成是因线程不同步导致结果为无穷大的情况。
                double allFileProgress;
                if (this.FileList.Count != 0)
                {
                    allFileProgress = (double)computedFileCount / this.FileList.Count;
                }
                else
                {
                    allFileProgress = 1;
                }

                // 加上当前文件进度。
                if (this.FileList.Current != null)
                {
                    allFileProgress += 
                        this.FileList.Current.ComputeProgress / this.FileList.Count;
                }

                // 避免在计算完成是因线程不同步导致结果为无穷大的情况。
                if (allFileProgress > 1)
                {
                    allFileProgress = 1;
                }

                this.FileListProgress.All = allFileProgress;
            }
        }
    }
}
