using System.Collections.Generic;

namespace DandyBox.Core.DataModels
{
    public class Idol
    {
        public int IdolId { get; set; }
        public string Name { get; set; }
        public List<IdolMovie> ActorMovies { get; set; }
    }
}