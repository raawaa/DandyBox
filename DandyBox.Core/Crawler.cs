using DandyBox.Core.DataModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using Flurl;

namespace DandyBox.Core
{
    public static class Crawler
    {
        public static string LoadHtml(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            return htmlDoc.Text;
        }
        public static Movie GetMovieInfo(string fanhao)
        {
            var domainUrl = @"https://www.javbus.icu/";
            var fanhao_url = Url.Combine(domainUrl, fanhao);
            var web = new HtmlWeb();
            var htmlDoc = web.Load(fanhao_url);
            string productId = htmlDoc.DocumentNode
                .SelectSingleNode("/html/body/div[5]/div[1]/div[2]/p[1]/span[2]")
                .InnerText;
            string releaseDateText = htmlDoc.DocumentNode
                .SelectSingleNode("/html/body/div[5]/div[1]/div[2]/p[2]")
                .LastChild.InnerText.Trim();
            DateTime releaseDate = DateTime.ParseExact(
                releaseDateText,
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture);
            string lengthText = htmlDoc.DocumentNode
                .SelectSingleNode("/html/body/div[5]/div[1]/div[2]/p[3]")
                .LastChild.InnerText;
            var lengthRegex = new System.Text.RegularExpressions.Regex(@"\d+");
            int length = int.Parse(lengthRegex.Match(lengthText).Value);

            string studio = htmlDoc.DocumentNode
                .SelectSingleNode("/html/body/div[5]/div[1]/div[2]/p[4]/a")
                .InnerText;
            string label = htmlDoc.DocumentNode
                .SelectSingleNode("/html/body/div[5]/div[1]/div[2]/p[5]/a")
                .InnerText;

            HtmlNodeCollection genreNodes = htmlDoc.DocumentNode
                .SelectNodes("/html/body/div[5]/div[1]/div[2]/p[7]/span");
            List<Genre> genres = new List<Genre>();

            var idolNods = htmlDoc.DocumentNode
                .SelectNodes("/html/body/div[5]/div[1]/div[2]/p[11]/span");
            var idols = new List<Idol>();
            foreach (var node in idolNods)
            {
                idols.Add(new Idol() { Name = node.FirstChild.InnerText });
            }
            foreach (var node in genreNodes)
            {
                genres.Add(new Genre() { Name = node.FirstChild.InnerText });
            }

            return new Movie()
            {
                ProductId = productId,
                Length = length,
                Studio = studio,
                Label = label,


            };

        }
    }
}
