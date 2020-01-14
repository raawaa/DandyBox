using System;
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

            string[] mediaExtensions = { ".avi", ".avi", ".mp4", ".mkv", ".mpg", ".rmvb", ".rm", ".mov", ".mpeg", ".flv", ".wmv", ".m4v" };


            // 考虑文件夹访问权限。如果用户选择了一个没有访问权限的文件夹……
            IEnumerable<string> files = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
            IEnumerable<string> mediaFiles = from file in files
                                             where mediaExtensions.Contains(Path.GetExtension(file))
                                             select file;
            return mediaFiles;
        }
        public static string ParseProductCode(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            fileName = fileName.ToLower();
            string productCode = null;
            Console.ForegroundColor = ConsoleColor.Red;
            if (fileName.Contains("t28"))
            {
                productCode = FindRegex(fileName, @"T28-?\d\d+");
                Console.WriteLine($"{fileName}\t{productCode}");
            }
            else if (fileName.Contains("sqte"))
            {
                productCode = FindRegex(fileName, @"sqte-?\d\d+");
                Console.WriteLine($"{fileName}\t{productCode}");
            }
            else if (fileName.Contains("heyzo"))
            {
                productCode = FindRegex(fileName, @"heyzo\s?\)?\(?_?(hd|lt)?\+?-?_?\d\d+");
                productCode = productCode.Replace("hd", "").Replace("lt", "");
                Console.WriteLine($"{fileName}\t{productCode}");

            }

            else if (fileName.Contains("heydouga"))
            {
                productCode = FindRegex(fileName, @"heydouga_?-?\d\d+");
                Console.WriteLine($"{fileName}\t{productCode}");

            }
            else if (fileName.Contains("fc2"))
            {
                productCode = FindRegex(fileName, @"\d{5,}");
                if (string.IsNullOrEmpty(productCode))
                    productCode = "";
                else
                    productCode = $"fc2ppv-{productCode}";
                Console.WriteLine($"{fileName}\t{productCode}");

            }
            else if (fileName.Contains("mkd-s"))
            {
                productCode = FindRegex(fileName, @"mkd-s\d\d+"); Console.WriteLine($"{fileName}\t{productCode}");

            }
            else if (fileName.Contains("mkbd-s"))
            {
                productCode = FindRegex(fileName, @"mkbd-s\d\d+"); Console.WriteLine($"{fileName}\t{productCode}");

            }
            else if (fileName.Contains("s2m"))
            {
                productCode = FindRegex(fileName, @"s2m-?\d\d+"); Console.WriteLine($"{fileName}\t{productCode}");

            }
            else if (fileName.Contains("s2mbd"))
            {
                productCode = FindRegex(fileName, @"s2mbd-?\d\d+"); Console.WriteLine($"{fileName}\t{productCode}");

            }
            else if (fileName.Contains("s2mcr"))
            {
                productCode = FindRegex(fileName, @"s2mcr-?\d\d+"); Console.WriteLine($"{fileName}\t{productCode}");

            }
            else if (!string.IsNullOrEmpty(FindRegex(fileName, @"\d{5,}(_|-)\d\d+")))
            {
                productCode = FindRegex(fileName, @"\d{5,}(_|-)\d\d+");
                Console.WriteLine($"{fileName}\t{productCode}");

            }
            else
            {
                productCode = FindRegex(fileName, @"[A-Za-z][A-Za-z]+-?\d\d+");
                if (!string.IsNullOrEmpty(productCode))
                    Console.WriteLine($"{fileName}\t{productCode}");

            }

            return productCode;
        }
        /// <summary>
        /// Return the first match of the regString.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="regString"></param>
        /// <returns></returns>
        private static string FindRegex(string fileName, string regString)
        {
            var regex = new System.Text.RegularExpressions.Regex(regString);
            System.Text.RegularExpressions.MatchCollection matches = regex.Matches(fileName);
            if (matches.Count > 0)
            {
                return matches.First().Value;

            }
            else
            {
                return null;
            }
        }
    }
}
