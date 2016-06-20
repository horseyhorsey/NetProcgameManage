namespace Horsesoft.Procgame.AssetServices.Model
{
    public class ConvertSettings
    {
        public bool ConvertToImages { get; set; } = true;
        public bool ConvertAudio { get; set; } = true;
        public string AudioName { get; set; }
        public string AudioFormat { get; set; }
        public string AnimationName { get; set; }
        public string ImageFormat { get; set; }
        public double Fps { get; set; }
    }
}
