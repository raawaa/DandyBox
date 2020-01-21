using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DandyBox.Core.DataModels;

namespace DandyBox.Core
{
    public static class FileManager
    {
        /// <summary>
        /// 递归获取文件夹下的所有媒体文件
        /// </summary>
        /// <param name="path">想要获取的文件夹路径</param>
        /// <returns></returns>
        public static IEnumerable<MediaFile> GetMediaFiles(string path)
        {
            string[] mediaExtensions = { ".avi", ".avi", ".mp4", ".mkv", ".mpg", ".rmvb", ".rm", ".mov", ".mpeg", ".flv", ".wmv", ".m4v" };

            // TODO: 考虑文件夹访问权限。如果用户选择了一个没有访问权限的文件夹……
            IEnumerable<string> files = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
            IEnumerable<MediaFile> mediaFiles = from file in files
                                             where mediaExtensions.Contains(Path.GetExtension(file))
                                             select new MediaFile { FilePath=file};
            return mediaFiles;
        }

        

        /// <summary>
        /// Return the first match of the regString.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="regString"></param>
        /// <returns></returns>
        
    }
}