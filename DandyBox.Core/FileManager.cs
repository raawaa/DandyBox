using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DandyBox.Core
{
    public static class FileManager
    {
        /// <summary>
        /// 递归获取文件夹下的所有媒体文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetMediaFiles(string path)
        {

            string[] mediaExtensions = { ".avi" };


            // 考虑文件夹访问权限。如果用户选择了一个没有访问权限的文件夹……
            var files = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
            var mediaFiles = from file in files
                             where mediaExtensions.Contains(Path.GetExtension(file))
                             select file;


            return mediaFiles;
        }
    }
}
