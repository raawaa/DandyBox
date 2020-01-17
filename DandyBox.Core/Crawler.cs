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
                .SelectSingleNode("/html/body/div[5]/h3")
                .InnerText;

            string productId = htmlDoc.DocumentNode
                .SelectSingleNode("//span[text()='識別碼:']/following-sibling::span")
                .InnerText;
            string releaseDateText = htmlDoc.DocumentNode
                .SelectSingleNode("//span[text()='發行日期:']/../text()")
                .InnerText.Trim();
            DateTime releaseDate = DateTime.ParseExact(
                releaseDateText,
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture);
            var directorNode = htmlDoc.DocumentNode
                .SelectSingleNode("//span[text()='導演:']/following-sibling::a");
            string director = directorNode == null ? null : directorNode.InnerText;

            var seriesNode = htmlDoc.DocumentNode
                .SelectSingleNode("//span[text()='系列:']/following-sibling::a");
            string series = seriesNode == null ? null : seriesNode.InnerText;

            string lengthText = htmlDoc.DocumentNode
            .SelectSingleNode("//span[text()='長度:']/../text()")
            .InnerText;
            var lengthRegex = new System.Text.RegularExpressions.Regex(@"\d+");
            int length = int.Parse(lengthRegex.Match(lengthText).Value);

            string studio = htmlDoc.DocumentNode
                .SelectSingleNode("//span[text()='製作商:']/following-sibling::a")
                .InnerText;
            string label = htmlDoc.DocumentNode
                .SelectSingleNode("//span[text()='發行商:']/following-sibling::a")
                .InnerText;

            HtmlNodeCollection genreNodes = htmlDoc.DocumentNode
                .SelectNodes("//p[text()='類別:']//following-sibling::p[position()=1]/span[@class='genre']");
            List<Genre> genres = new List<Genre>();
            if (genreNodes != null)
            {
                foreach (var node in genreNodes)
                {
                    genres.Add(new Genre() { Name = node.InnerText });
                }
            }

            HtmlNodeCollection idolNods = htmlDoc.DocumentNode
                .SelectNodes("//div[@class='star-name']/a");
            List<Idol> idols = new List<Idol>();
            if (idolNods != null)
            {
                foreach (var node in idolNods)
                {
                    idols.Add(new Idol() { Name = node.InnerText });
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
                Title = title,
                Director = director,
                Series=series
            };
        }

        private static string LoadHtml(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            return htmlDoc.Text;
        }
    }
}