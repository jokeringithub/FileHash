using System;
using System.Collections.Generic;
using System.IO;

namespace XstarS.FileHash.Helpers
{
    /// <summary>
    /// 提供文件系统路径相关的帮助方法。
    /// </summary>
    internal static class PathHelper
    {
        /// <summary>
        /// 获取指定路径包含的所有文件的完整路径。
        /// </summary>
        /// <param name="path">要获取文件的文件或目录路径。</param>
        /// <param name="recurse">指定对于子目录是否递归搜索。</param>
        /// <returns><paramref name="path"/> 包含的所有文件的完整路径。</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        internal static string[] GetFilePaths(string path, bool recurse)
        {
            var filePaths = new List<string>();
            if (File.Exists(path))
            {
                filePaths.Add(Path.GetFullPath(path));
            }
            else if (Directory.Exists(path))
            {
                try
                {
                    if (recurse)
                    {
                        foreach (var dir in Directory.GetDirectories(path))
                        {
                            filePaths.AddRange(PathHelper.GetFilePaths(dir, recurse));
                        }
                    }
                    filePaths.AddRange(Directory.GetFiles(path));
                }
                catch (Exception) { }
            }
            return filePaths.ToArray();
        }
    }
}
