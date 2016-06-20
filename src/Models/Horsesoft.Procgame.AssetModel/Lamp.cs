using System.Collections.Generic;

namespace Horsesoft.Procgame.AssetModel
{
    public class Lamp
    {
        public List<Lampshow> Lampshows { get; set; }

        public Lamp()
        {
            Lampshows = new List<Lampshow>();
        }
    }

    public class Lampshow
    {
        public string Name { get; set; }
        public string FileName { get; set; }
    }
}
