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

namespace FileHash
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 主窗口所使用的定时器，当前仅用于进度条更新。
        /// </summary>
        private readonly Timer mainWindowTimer;
        /// <summary>
        /// 计算文件散列的线程队列。
        /// </summary>
        private readonly FileInfoAndHashList fileInfoAndHashList;

        /// <summary>
        /// 初始化 <see cref="MainWindow"/> 的实例。
        /// </summary>
        public MainWindow()
        {
            // 初始化部分参数。
            this.LocalizedInfo = new FileInfoAndHashLocalized(CultureInfo.CurrentUICulture);
            this.fileInfoAndHashList = new FileInfoAndHashList();
            this.fileInfoAndHashList.CurrentCompleted += this.FileInfoAndHashList_CurrentCompleted;
            this.fileInfoAndHashList.ListCompleted += this.FileInfoAndHashList_ListCompleted;
            this.MainWindowFileInfoAndHashEnables = new FileInfoAndHashEnables();
            this.MainWindowHashFormat = new HashFormat();
            this.MainWindowComputeProgress = new ComputeProgress();

            // 本地化用户控件。
            this.LocalizedComponentContent = new ComponentContentLocalized(CultureInfo.CurrentUICulture);

            // 初始化用户控件。
            this.InitializeComponent();

            // 设定部分用户控件的初值。
            this.InitializeComponentProperty();

            // 初始化定时器。
            double fps = 30;
            this.mainWindowTimer = new Timer(1000 / fps);
            this.mainWindowTimer.Elapsed += this.MainWindowTimer_Elapsed;
        }

        /// <summary>
        /// 本地化用户控件。
        /// </summary>
        public ComponentContentLocalized LocalizedComponentContent { get; }
        /// <summary>
        /// 将计算结果本地化。
        /// </summary>
        public FileInfoAndHashLocalized LocalizedInfo { get; }
        /// <summary>
        /// 文件信息和散列值标志向量。
        /// </summary>
        public FileInfoAndHashEnables MainWindowFileInfoAndHashEnables { get; }
        /// <summary>
        /// 指示散列值的格式。
        /// </summary>
        public HashFormat MainWindowHashFormat { get; }
        /// <summary>
        /// 文件散列计算进度。
        /// </summary>
        public ComputeProgress MainWindowComputeProgress { get; }

        /// <summary>
        /// 初始化部分用户控件的属性。
        /// </summary>
        private void InitializeComponentProperty()
        {
            // 始终显示文件名。
            this.MainWindowFileInfoAndHashEnables.Name = true;
            // 文件路径复选框。
            this.filePathCheckBox.IsChecked = false;
            // 文件大小复选框。
            this.fileSizeCheckBox.IsChecked = true;
            // 修改时间复选框。
            this.fileUpdateTimeCheckBox.IsChecked = true;

            // CRC32 复选框。
            this.fileCRC32CheckBox.IsChecked = false;
            // MD5 复选框。
            this.fileMD5CheckBox.IsChecked = true;
            // SHA-1 复选框。
            this.fileSHA1CheckBox.IsChecked = true;
            // SHA-256 复选框。
            this.fileSHA256CheckBox.IsChecked = false;
            // SHA-384 复选框。
            this.fileSHA384CheckBox.IsChecked = false;
            // SHA-512 复选框。
            this.fileSHA512CheckBox.IsChecked = false;

            // 小写十六进制选框。
            this.lowerHexFormatRadioButton.IsChecked = false;
            // 大写十六进制选框。
            this.upperHexFormatRadioButton.IsChecked = true;
            // Base64 选框。
            this.base64FormatRadioButton.IsChecked = false;
        }

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
                this.mainWindowTimer.Start();
            }
        }

        /// <summary>
        /// 复制按钮按下的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.LocalizedInfo.Result != string.Empty)
            {
                Clipboard.SetText(this.LocalizedInfo.Result);
            }
        }

        /// <summary>
        /// 清除按钮按下的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.LocalizedInfo.Result = string.Empty;
        }

        /// <summary>
        /// 跳过按钮按下的时间处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            this.fileInfoAndHashList.Skip();
        }

        /// <summary>
        /// 取消计算按钮按下的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.fileInfoAndHashList.Cancel();
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
        private void MainWindowTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.mainWindowTimer.Enabled)
            {
                this.UpadateComputeProgress();
            }
        }

        /// <summary>
        /// 当前文件散列计算完成的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileInfoAndHashList_CurrentCompleted(object sender, FileInfoAndHash.CompletedEventArgs e)
        {
            this.UpdateComputeResult(e.Result);

            try
            {
                this.fileInfoAndHashList.StartNext();
            }
            catch (FileNotFoundException exception)
            {
                this.LocalizedInfo.FileNotFoundResultAppend(exception.FileName);
            }
        }

        /// <summary>
        /// 全部文件散列计算完成的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileInfoAndHashList_ListCompleted(object sender, EventArgs e)
        {
            this.mainWindowTimer.Stop();
            this.fileInfoAndHashList.Clear();
            this.MainWindowComputeProgress.CurrentFileProgress = 1;
            this.MainWindowComputeProgress.AllFileProgress = 1;
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
                    this.fileInfoAndHashList.Add(filePath, this.MainWindowFileInfoAndHashEnables.AllEnables);
                }
                catch (FileNotFoundException)
                {
                    this.LocalizedInfo.FileNotFoundResultAppend(filePath);
                    continue;
                }
            }

            try
            {
                this.fileInfoAndHashList.StartNext();
            }
            catch (FileNotFoundException exception)
            {
                this.LocalizedInfo.FileNotFoundResultAppend(exception.FileName);
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
                if (this.MainWindowHashFormat.IsLowerHexFormat)
                {
                    this.LocalizedInfo.ResultAppend(result.ToLowerHexStrings());
                }
                else if (this.MainWindowHashFormat.IsUpperHexFormat)
                {
                    this.LocalizedInfo.ResultAppend(result.ToUpperHexStrings());
                }
                else if (this.MainWindowHashFormat.IsBase64Format)
                {
                    this.LocalizedInfo.ResultAppend(result.ToBase64Strings());
                }
            }
            // 文件散列计算未完成。
            catch (NotImplementedException)
            {
                this.LocalizedInfo.CancelledResultAppend(result.FilePath);
            }
        }

        /// <summary>
        /// 更新计算进度。
        /// </summary>
        private void UpadateComputeProgress()
        {
            // 当前文件
            if (this.fileInfoAndHashList.Current != null)
            {
                this.MainWindowComputeProgress.CurrentFileProgress = this.fileInfoAndHashList.Current.ComputeProgress;
            }

            // 所有文件。
            if ((this.fileInfoAndHashList != null) && (this.fileInfoAndHashList.Count != 0))
            {
                // 获取完成计算的文件数量。
                int computedFileCount;
                try
                {
                    computedFileCount = this.fileInfoAndHashList.IndexOf(fileInfoAndHashList.Current);
                }
                catch (Exception)
                {
                    computedFileCount = this.fileInfoAndHashList.Count;
                }

                // 避免在计算完成是因线程不同步导致结果为无穷大的情况。
                double allFileProgress;
                if (this.fileInfoAndHashList.Count != 0)
                {
                    allFileProgress = (double)computedFileCount / this.fileInfoAndHashList.Count;
                }
                else
                {
                    allFileProgress = 1;
                }

                // 加上当前文件进度。
                if (this.fileInfoAndHashList.Current != null)
                {
                    allFileProgress += this.fileInfoAndHashList.Current.ComputeProgress / this.fileInfoAndHashList.Count;
                }

                // 避免在计算完成是因线程不同步导致结果为无穷大的情况。
                if (allFileProgress > 1)
                {
                    allFileProgress = 1;
                }

                this.MainWindowComputeProgress.AllFileProgress = allFileProgress;
            }
        }
    }
}
