namespace Horsesoft.Procgame.Config
{
    public class GameConfig
    {
        public int DotsW { get; set; }
        public int DotsH { get; set; }
        public int DisplayScale { get; set; }
        public int ScreenPositionX { get; set; }
        public int ScreenPositionY { get; set; }
        public bool Borderless { get; set; }
        public int DisplayFlip { get; set; }
        public double FrameRate { get; set; }
        public bool DisplayFilter { get; set; }
        public bool FullScreen { get; set; }

        public string ImagesPath { get; set; }
        public string FontPath { get; set; }
        public string SoundPath { get; set; }
        public string VoicePath { get; set; }
        public string SfxPath { get; set; }
        public string MusicPath { get; set; }

        #region OldProps
        public string pinproc_class { get; set; }
        public string PYSDL2_DLL_PATH { get; set; }
        #endregion     
    }
}
