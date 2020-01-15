using System;
using System.Collections.Generic;
using System.Text;

namespace DandyBox.Core.DataModels
{
    public class MovieInfo
    {
        public int MovieInfoId { get; set; }
        public string Title { get; set; }
        public List<MediaFile> MediaFiles { get; set; }
    }
}
