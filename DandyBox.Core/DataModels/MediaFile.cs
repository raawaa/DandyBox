using System;
using System.IO;
using System.Linq;

namespace DandyBox.Core.DataModels
{
    public class MediaFile
    {
        public int MediaFileId { get; set; }
        public string FilePath { get; set; }

        public string ParsedCode { get; set; }

        //public int MovieId { get; set; }
        public Movie Movie { get; set; }

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
}