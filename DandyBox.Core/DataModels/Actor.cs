using System;
using System.Collections.Generic;
using System.Text;

namespace DandyBox.Core.DataModels
{
    class Actor
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public List<MovieInfo> Movies { get; set; }

    }
}
