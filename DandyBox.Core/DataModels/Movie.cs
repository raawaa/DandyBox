using System;
using System.Collections.Generic;

namespace DandyBox.Core.DataModels
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string ProductId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Length { get; set; }
        public string Studio { get; set; }
        public string Label { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }
        public string Title { get; set; }
        public List<MediaFile> MediaFiles { get; set; }
        public List<IdolMovie> IdolMovies { get; set; }
    }
}