using System;
using System.Collections.Generic;
using System.Text;

namespace DandyBox.Core.DataModels
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public List<MediaFile> MediaFiles { get; set; }
        public List<ActorMovie> ActorMovies { get; set; }
    }
}
