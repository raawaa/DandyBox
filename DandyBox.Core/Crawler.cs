using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
