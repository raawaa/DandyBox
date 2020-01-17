using DandyBox.Core.DataModels;
using System;
using System.Collections.Generic;

namespace DandyBox.Core
{
    public class MovieInfo
    {
        public List<Genre> Genres { get; set; }
        public List<Idol> Idols { get; set; }
        public string Label { get; set; }
        public string Director { get; set; }
        public int Length { get; set; }
        public string ProductId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Studio { get; set; }
        public string Title { get; set; }
        public string Series { get; set; }
    }
}