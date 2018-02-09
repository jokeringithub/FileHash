using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileHash
{
    /// <summary>
    /// 计算文件信息和散列的线程队列类
    /// </summary>
    public partial class FileInfoAndHashList : List<FileInfoAndHash>
    {
        /// <summary>
        /// 使用默认方法实例化此类
        /// </summary>
        public FileInfoAndHashList()
        {
            fileInfoAndHashCurrent = null;
        }

        /// <summary>
        /// 添加一条新的文件到线程列表
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileInfoAndHashEnables">标志向量</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public void Add(string filePath, bool[] fileInfoAndHashEnables)
        {
            FileInfoAndHash fileInfoAndHash = new FileInfoAndHash(filePath, fileInfoAndHashEnables);
            fileInfoAndHash.Completed += FileInfoAndHash_Completed;
            this.Add(fileInfoAndHash);
        }

        /// <summary>
        /// 添加多条新的文件到线程列表
        /// </summary>
        /// <param name="filePaths">文件路径字符串数组</param>
        /// <param name="fileInfoAndHashEnables">标志向量</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public void AddRange(string[] filePaths, bool[] fileInfoAndHashEnables)
        {
            foreach (string filePath in filePaths)
            {
                FileInfoAndHash fileInfoAndHash = new FileInfoAndHash(filePath, fileInfoAndHashEnables);
                fileInfoAndHash.Completed += FileInfoAndHash_Completed;
                this.Add(fileInfoAndHash);
            }
        }

        /// <summary>
        /// 启动下一个计算线程，若队列中已有启动的线程，则不做任何事
        /// </summary>
        public void StartNext()
        {
            // 当队列为不为空且没有任何启动的任务时
            if ((this.Count != 0) && (!this.Any(fileInfoAndHash => fileInfoAndHash.IsComputing)))
            {
                try
                {
                    // 搜寻第一个未启动也未被取消的线程
                    fileInfoAndHashCurrent = this.First(fileInfoAndHash =>
                        (!fileInfoAndHash.IsStarted && !fileInfoAndHash.IsCancelled));
                    fileInfoAndHashCurrent.StartAsync();
                }
                // 无法读取文件
                catch (FileNotFoundException e)
                {
                    fileInfoAndHashCurrent.CancelAsync();
                    StartNext();
                    throw e;
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 跳过正在计算的线程
        /// </summary>
        public void Skip()
        {
            if (fileInfoAndHashCurrent != null)
            {
                fileInfoAndHashCurrent.CancelAsync();
            }
        }

        /// <summary>
        /// 取消当前队列的所有计算线程
        /// </summary>
        public void Cancel()
        {
            foreach (FileInfoAndHash fileInfoAndHash in this)
            {
                // 将未完成计算的全部取消
                if (!fileInfoAndHash.IsCompleted)
                {
                    fileInfoAndHash.CancelAsync();
                }
            }
        }

        /// <summary>
        /// 清空线程队列
        /// </summary>
        public new void Clear()
        {
            fileInfoAndHashCurrent = null;
            base.Clear();
        }

        /// <summary>
        /// 检查队列是否全部完成，完成则会触发ListCompleted事件
        /// </summary>
        private void CheckListCompleted()
        {
            if (this.All(fileInfoAndHash => fileInfoAndHash.IsCompleted))
            {
                OnListCompleted(this, new EventArgs());
            }
        }

        /// <summary>
        /// 单个文件线程完成的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileInfoAndHash_Completed(object sender, FileInfoAndHash.CompletedEventArgs e)
        {
            OnCurrentCompleted(this, e);
            CheckListCompleted();
        }

        /// <summary>
        /// 当前正在计算的线程
        /// </summary>
        private FileInfoAndHash fileInfoAndHashCurrent;
        /// <summary>
        /// 当前正在计算的线程
        /// </summary>
        public FileInfoAndHash Current { get => fileInfoAndHashCurrent; }
    }

    public partial class FileInfoAndHashList
    {
        /// <summary>
        /// 当前文件计算完成事件委托处理
        /// </summary>
        /// <param name="isNormallyCompleted"></param>
        public delegate void CurrentCompletedEventHandler(object sender, FileInfoAndHash.CompletedEventArgs e);

        /// <summary>
        /// 当前文件计算完成事件，文件散列计算完成时发生
        /// </summary>
        public event CurrentCompletedEventHandler CurrentCompleted;

        /// <summary>
        /// 触发当前文件计算完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnCurrentCompleted(object sender, FileInfoAndHash.CompletedEventArgs e)
        {
            CurrentCompleted?.Invoke(sender, e);
        }
    }

    public partial class FileInfoAndHashList
    {
        /// <summary>
        /// 队列计算完成事件委托处理
        /// </summary>
        /// <param name="isNormallyCompleted"></param>
        public delegate void ListCompletedEventHandler(object sender, EventArgs e);

        /// <summary>
        /// 队列计算完成事件，全部文件散列计算完成时发生
        /// </summary>
        public event ListCompletedEventHandler ListCompleted;

        /// <summary>
        /// 触发队列计算完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnListCompleted(object sender, EventArgs e)
        {
            ListCompleted?.Invoke(sender, e);
        }
    }
}
