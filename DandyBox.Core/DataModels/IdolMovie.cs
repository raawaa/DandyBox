using System;
using System.Collections.Generic;
using System.Text;

namespace DandyBox.Core.DataModels
{
    public class IdolMovie
    {
        public int IdolId { get; set; }
        public Idol Idol { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
