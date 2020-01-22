using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DandyBox.Core.DataModels
{
    public class MediaFile
    {
        public MediaFile(FileInfo fileInfo)
        {
            FilePath = fileInfo.FullName;
            Length = fileInfo.Length / 1024;
        }

        public int MediaFileId { get; set; }
        public string FilePath { get; set; } // file size in mebibyte
        public long Length { get; set; }

        public string ParsedCode { get; set; }

        //public int MovieId { get; set; }
        public Movie Movie { get; set; }

        //public MediaFile(string parsedCode)
        //{
        //    ParsedCode = parsedCode ?? ParseProductCode(FilePath);
        //}

        public string ParseProductCode(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            fileName = fileName.ToLower();
            Console.ForegroundColor = ConsoleColor.Red;
            string productCode;
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

        private string FindRegex(string fileName, string regString)
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

    internal class MediaFileComparer : IEqualityComparer<MediaFile>
    {
        bool IEqualityComparer<MediaFile>.Equals(MediaFile x, MediaFile y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null)) return false;
            return x.FilePath == y.FilePath;
        }

        int IEqualityComparer<MediaFile>.GetHashCode(MediaFile mediaFile)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(mediaFile, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashMediaFilePath = mediaFile.FilePath == null ? 0 : mediaFile.FilePath.GetHashCode();

            return hashMediaFilePath;
        }
    }
}