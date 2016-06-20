using System.Collections.Generic;

namespace Horsesoft.Procgame.AssetModel
{
    public class Font
    {
        public List<FontSdl> SdlFonts { get; set; }        

        public Font()
        {
            SdlFonts = new List<FontSdl>();                   
        }
    }

    public class FontStyle
    {
        public List<Style> FontStyles { get; set; }

        public FontStyle()
        {
            FontStyles = new List<Style>();
        }
    }

    public class FontSdl
    {
        public string Alias { get; set; }
        public string Font { get; set; }
        public int Size { get; set; }
    }

    public class Style
    {
        public string Name { get; set; }
        public string InteriorColor { get; set; }
        public string LineColor { get; set; }
        public int LineWidth { get; set; }
    }

}
