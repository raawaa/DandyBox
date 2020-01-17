using DandyBox.Core.DataModels;
using Flurl;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace DandyBox.Core
{
    public static class Crawler
    {
        public static MovieInfo GetMovieInfo(string fanhao)
        {
            var domainUrl = @"https://www.javbus.icu/";
            var fanhao_url = Url.Combine(domainUrl, fanhao);
            var web = new HtmlWeb();
            var htmlDoc = web.Load(fanhao_url);

            string title = htmlDoc.DocumentNode
                .InnerText;

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
            if (genreNodes != null)
            {
                foreach (var node in genreNodes)
                {
                    genres.Add(new Genre() { Name = node.FirstChild.InnerText });
                }
            }

            HtmlNodeCollection idolNods = htmlDoc.DocumentNode
                .SelectNodes("/html/body/div[5]/div[1]/div[2]/p[11]/span");
            List<Idol> idols = new List<Idol>();
            if (idolNods != null)
            {
                foreach (var node in idolNods)
                {
                    idols.Add(new Idol() { Name = node.FirstChild.InnerText });
                }
            }

            return new MovieInfo
            {
                ProductId = productId,
                Length = length,
                Studio = studio,
                Label = label,
                ReleaseDate = releaseDate,
                Genres = genres,
                Idols = idols,
                Title = title
            };
        }

        private static string LoadHtml(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            return htmlDoc.Text;
        }
    }

    public class MovieInfo
    {
        public List<Genre> Genres { get; set; }
        public List<Idol> Idols { get; set; }
        public string Label { get; set; }
        public int Length { get; set; }
        public string ProductId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Studio { get; set; }
        public string Title { get; set; }
    }
}