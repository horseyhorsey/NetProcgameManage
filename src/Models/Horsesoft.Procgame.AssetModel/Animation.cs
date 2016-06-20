using System.Collections.Generic;

namespace Horsesoft.Procgame.AssetModel
{
    public class Animation
    {
        public List<Animations> Animations { get; set; }
        public List<Animations> Videos { get; set; }

        public Animation()
        {
            Animations = new List<Animations>();
            Videos = new List<Animations>();
        }
    }
}
