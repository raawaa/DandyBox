using System;
using System.IO;
using DandyBox.Core;


namespace DandyBox.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DandyBox\\");
            //Console.WriteLine(path);

            var files = FileManager.GetMediaFiles(@"E:\俞文杰手机微信文件备份\");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("END OF INFO");
            Console.ReadLine();
        }
    }
}
