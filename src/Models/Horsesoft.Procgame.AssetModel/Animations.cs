
namespace Horsesoft.Procgame.AssetModel
{
    public class Animations
    {
        public string Alias { get; set; }
        public string FileName { get; set; }
        public byte FrameTime { get; } = 1;
        public bool Repeat { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string ColorKey { get; set; }
    }
}
