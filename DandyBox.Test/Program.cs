using System;
using System.IO;
using DandyBox.Core;
using DandyBox.Core.DataModels;
using Microsoft.EntityFrameworkCore;


namespace DandyBox.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DandyBox\\");
            //Console.WriteLine(path);

            //var files = FileManager.GetMediaFiles(@"K:\Porn");
            //foreach (var file in files)
            //{
            //    FileManager.ParseProductCode(file);
            //}

            using (var dbContext = new DataContext())
            {
                dbContext.Database.Migrate();
            }

            Console.WriteLine("END OF INFO");
            Console.ReadLine();
        }
    }
}
