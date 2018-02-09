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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileHash
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 使用默认方法实例化此类
        /// </summary>
        public MainWindow()
        {
            InitializeParameter();
            LocalizeComponent();
            InitializeComponent();
            InitializeComponentProperty();
            InitializeTimer();
        }

        /// <summary>
        /// 主窗口所使用的定时器，当前仅用于进度条更新
        /// </summary>
        private Timer mainWindowTimer;
        /// <summary>
        /// 计算文件散列的线程队列
        /// </summary>
        private FileInfoAndHashList fileInfoAndHashList;

        #region 数据绑定相关

        /// <summary>
        /// 本地化用户控件
        /// </summary>
        private ComponentContenLocalized localizedComponentContent;
        /// <summary>
        /// 本地化用户控件
        /// </summary>
        public ComponentContenLocalized LocalizedComponentContent { get => localizedComponentContent; }
        /// <summary>
        /// 文件信息和散列值标志向量
        /// </summary>
        private FileInfoAndHashEnables fileInfoAndHashEnables;
        /// <summary>
        /// 文件信息和散列值标志向量
        /// </summary>
        public FileInfoAndHashEnables MainWindowFileInfoAndHashEnables { get => fileInfoAndHashEnables; set => fileInfoAndHashEnables = value; }
        /// <summary>
        /// 指示散列值的格式
        /// </summary>
        private HashFormat hashFormat;
        /// <summary>
        /// 指示散列值的格式
        /// </summary>
        public HashFormat MainWindowHashFormat { get => hashFormat; set => hashFormat = value; }
        /// <summary>
        /// 用于将计算结果本地化
        /// </summary>
        private FileInfoAndHashLocalized localizedInfo;
        /// <summary>
        /// 用于将计算结果本地化
        /// </summary>
        public FileInfoAndHashLocalized LocalizedInfo { get => localizedInfo; }
        /// <summary>
        /// 文件散列计算进度
        /// </summary>
        private ComputeProgress computeProgress;
        /// <summary>
        /// 文件散列计算进度
        /// </summary>
        public ComputeProgress MainWindowComputeProgress { get => computeProgress; }

        #endregion 数据绑定相关

        /// <summary>
        /// 本地化用户控件
        /// </summary>
        private void LocalizeComponent()
        {
            localizedComponentContent = new ComponentContenLocalized(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// 初始化部分参数
        /// </summary>
        private void InitializeParameter()
        {
            localizedInfo = new FileInfoAndHashLocalized(CultureInfo.CurrentUICulture);
            fileInfoAndHashList = new FileInfoAndHashList();
            fileInfoAndHashList.CurrentCompleted += FileInfoAndHashList_CurrentCompleted;
            fileInfoAndHashList.ListCompleted += FileInfoAndHashList_ListCompleted;
            fileInfoAndHashEnables = new FileInfoAndHashEnables();
            hashFormat = new HashFormat();
            computeProgress = new ComputeProgress();
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimer()
        {
            // 更新帧数
            double fps = 30;
            mainWindowTimer = new Timer(1000 / fps);
            mainWindowTimer.Elapsed += MainWindowTimer_Elapsed;
        }

        /// <summary>
        /// 初始化部分用户控件的属性
        /// </summary>
        private void InitializeComponentProperty()
        {
            // 始终显示文件名
            fileInfoAndHashEnables.FileName = true;
            // 文件路径复选框
            filePathCheckBox.IsChecked = false;
            // 文件大小复选框
            fileSizeCheckBox.IsChecked = true;
            // 修改时间复选框
            fileUpdateTimeCheckBox.IsChecked = true;

            // CRC32复选框
            fileCRC32CheckBox.IsChecked = false;
            // MD5复选框
            fileMD5CheckBox.IsChecked = true;
            // SHA1复选框
            fileSHA1CheckBox.IsChecked = true;
            // SHA256复选框
            fileSHA256CheckBox.IsChecked = false;
            // SHA384复选框
            fileSHA384CheckBox.IsChecked = false;
            // SHA512复选框
            fileSHA512CheckBox.IsChecked = false;

            // 小写十六进制选框
            lowerHexFormatRadioButton.IsChecked = false;
            // 大写十六进制选框
            upperHexFormatRadioButton.IsChecked = true;
            // Base64选框
            base64FormatRadioButton.IsChecked = false;
        }

        /// <summary>
        /// 拖放完成，传递拖放的文件路径字符串
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
        /// 拖放释放，取出文件路径，启动计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultTextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] dragedStrings = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dragedStrings != null)
            {
                AddToFileInfoAndHashList(AllFilePaths.GetAllFilePaths(dragedStrings));
                mainWindowTimer.Start();
            }
        }

        /// <summary>
        /// 复制按钮按下的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (localizedInfo.Result != string.Empty)
            {
                Clipboard.SetText(localizedInfo.Result);
            }
        }

        /// <summary>
        /// 清除按钮按下的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            localizedInfo.Result = string.Empty;
        }

        /// <summary>
        /// 跳过按钮按下的时间处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            fileInfoAndHashList.Skip();
        }

        /// <summary>
        /// 取消计算按钮按下的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            fileInfoAndHashList.Cancel();
        }

        /// <summary>
        /// 文本框内容变化时，自动滚动到最底端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            resultTextBox.SelectionStart = resultTextBox.Text.Length;
            resultTextBox.ScrollToEnd();
        }

        /// <summary>
        /// 定时器到达间隔的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (mainWindowTimer.Enabled)
            {
                UpadateComputeProgress();
            }
        }

        /// <summary>
        /// 当前文件散列计算完成的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileInfoAndHashList_CurrentCompleted(object sender, FileInfoAndHash.CompletedEventArgs e)
        {
            UpdateComputeResult(e);

            try
            {
                fileInfoAndHashList.StartNext();
            }
            catch (FileNotFoundException exception)
            {
                localizedInfo.FileNotFoundResultAppend(exception.FileName);
            }
        }

        /// <summary>
        /// 全部文件散列计算完成的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileInfoAndHashList_ListCompleted(object sender, EventArgs e)
        {
            mainWindowTimer.Stop();
            fileInfoAndHashList.Clear();
            computeProgress.CurrentFileProgress = 1;
            computeProgress.AllFileProgress = 1;
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
                    fileInfoAndHashList.Add(filePath, fileInfoAndHashEnables.AllEnables);
                }
                catch (FileNotFoundException)
                {
                    localizedInfo.FileNotFoundResultAppend(filePath);
                    continue;
                }
            }

            try
            {
                fileInfoAndHashList.StartNext();
            }
            catch (FileNotFoundException exception)
            {
                localizedInfo.FileNotFoundResultAppend(exception.FileName);
            }
        }

        /// <summary>
        /// 更新计算结果
        /// </summary>
        /// <param name="e">计算结果参数传递</param>
        private void UpdateComputeResult(FileInfoAndHash.CompletedEventArgs e)
        {
            // 取出带格式的结果，转化为本地化结果
            try
            {
                if (hashFormat.IsLowerHexFormat)
                {
                    localizedInfo.ResultAppend(e.Result.ToLowerHexStrings());
                }
                else if (hashFormat.IsUpperHexFormat)
                {
                    localizedInfo.ResultAppend(e.Result.ToUpperHexStrings());
                }
                else if (hashFormat.IsBase64Format)
                {
                    localizedInfo.ResultAppend(e.Result.ToBase64Strings());
                }
            }
            // 文件散列计算未完成
            catch (NotImplementedException)
            {
                localizedInfo.CancelledResultAppend(e.Result.FilePath);
            }
        }

        /// <summary>
        /// 更新计算进度
        /// </summary>
        private void UpadateComputeProgress()
        {
            // 当前文件
            if (fileInfoAndHashList.Current != null)
            {
                computeProgress.CurrentFileProgress = fileInfoAndHashList.Current.ComputeProgress;
            }

            // 所有文件
            if ((fileInfoAndHashList != null) && (fileInfoAndHashList.Count != 0))
            {
                // 获取完成计算的文件数量
                int computedFileCount;
                try
                {
                    computedFileCount = fileInfoAndHashList.IndexOf(fileInfoAndHashList.Current);
                }
                catch (Exception)
                {
                    computedFileCount = fileInfoAndHashList.Count;
                }

                // 避免在计算完成是因线程不同步导致结果为无穷大的情况
                double allFileProgress;
                if (fileInfoAndHashList.Count != 0)
                {
                    allFileProgress = (double)computedFileCount / fileInfoAndHashList.Count;
                }
                else
                {
                    allFileProgress = 1;
                }

                // 加上当前文件进度
                if (fileInfoAndHashList.Current != null)
                {
                    allFileProgress += fileInfoAndHashList.Current.ComputeProgress / fileInfoAndHashList.Count;
                }

                // 避免在计算完成是因线程不同步导致结果为无穷大的情况
                if (allFileProgress > 1)
                {
                    allFileProgress = 1;
                }

                computeProgress.AllFileProgress = allFileProgress;
            }
        }
    }
}
