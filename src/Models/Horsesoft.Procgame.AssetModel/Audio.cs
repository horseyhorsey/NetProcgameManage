using System.Collections.Generic;

namespace Horsesoft.Procgame.AssetModel
{
    public class Audio
    {
        public List<Music> Music { get; set; }
        public List<Voice> Voice { get; set; }
        public List<Sfx> Sfx { get; set; }

        public Audio()
        {
            Music = new List<Music>();
            Voice = new List<Voice>();
            Sfx = new List<Sfx>();
        }
    }

    public class SoundFile
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public double Volume { get; set; } = 0.8;
    }
    
    public class Sfx : SoundFile { }

    public class Voice : SoundFile { }

    public class Music : SoundFile { }
}
