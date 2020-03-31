using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace XstarS.FileHash.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 初始化 <see cref="MainWindow"/> 类。
        /// </summary>
        static MainWindow()
        {
            MainWindow.InitializeCommandBindings();
        }

        /// <summary>
        /// 初始化 <see cref="MainWindow"/> 类的新实例。
        /// </summary>
        public MainWindow()
        {
            this.DataContext = new MainWindowModel();
            this.Model.PropertyChanged += this.Model_PropertyChanged;
            this.InitializeComponent();
        }

        /// <summary>
        /// 获取当前窗口的数据逻辑模型。
        /// </summary>
        public MainWindowModel Model => (MainWindowModel)this.DataContext;

        /// <summary>
        /// 初始化 <see cref="MainWindow"/> 的命令绑定。
        /// </summary>
        private static void InitializeCommandBindings()
        {
            var commandBindings = new[]
            {
                new CommandBinding(ApplicationCommands.Copy,
                    (sender, e) => Clipboard.SetText(((MainWindow)sender).Model.ResultText),
                    (sender, e) => e.CanExecute = ((MainWindow)sender).Model.HasResult),
                new CommandBinding(EditingCommands.Delete,
                    (sender, e) => ((MainWindow)sender).Model.ClearResultText(),
                    (sender, e) => e.CanExecute = ((MainWindow)sender).Model.HasResult),
                new CommandBinding(NavigationCommands.NextPage,
                    (sender, e) => ((MainWindow)sender).Model.SkipCurrentHashing(),
                    (sender, e) => e.CanExecute = ((MainWindow)sender).Model.CanCancelHashing),
                new CommandBinding(ApplicationCommands.Stop,
                    (sender, e) => ((MainWindow)sender).Model.CancelHashing(),
                    (sender, e) => e.CanExecute = ((MainWindow)sender).Model.CanCancelHashing),
            };
            foreach (var commandBinding in commandBindings)
            {
                CommandManager.RegisterClassCommandBinding(typeof(MainWindow), commandBinding);
            }
        }

        /// <summary>
        /// 结果文本框文本更改的事件处理。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">提供事件数据的对象。</param>
        private void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).ScrollToEnd();
        }

        /// <summary>
        /// 结果文本框拖拽的事件处理。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">提供事件数据的对象。</param>
        private void ResultTextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[])
            {
                e.Effects = DragDropEffects.Copy;
            }
            e.Handled = true;
        }

        /// <summary>
        /// 结果文本框拖拽放下的事件处理。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">提供事件数据的对象。</param>
        private void ResultTextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            this.Model.AddHashingFiles(e.Data.GetData(DataFormats.FileDrop) as string[]);
        }

        /// <summary>
        /// 当前窗口数据逻辑模型属性更改的事件处理。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">提供事件数据的对象。</param>
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
        }
    }
}
