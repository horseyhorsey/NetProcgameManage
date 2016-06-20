using Newtonsoft.Json;
using System.IO;
using Horsesoft.Procgame.Manager.Base.Interfaces;
using Horsesoft.Procgame.AssetModel;
using System;

namespace Horsesoft.Procgame.AssetServices.Service
{
    public class AssetRepo : IAssetRepo
    {
        public Animation Animation { get; set; }
        public Audio Audio { get; set; }
        public Font Font { get; set; }
        public FontStyle Style { get; set; }
        public Lamp Lamp { get; set; }        

        /// <summary>
        /// Asset repositorys
        /// </summary>
        public AssetRepo()
        {
            Animation = new Animation();
            Audio = new Audio();
            Font = new Font();
            Lamp = new Lamp();
            Style = new FontStyle();
        }

        public void DeserializeAssets(string assetsJsonPath)
        {
            if (!Directory.Exists(assetsJsonPath)) return;

            foreach (var json in Directory.EnumerateFiles(assetsJsonPath, "*.json"))
            {
                try
                {
                    switch (Path.GetFileNameWithoutExtension(json).ToLower())
                    {
                        case "lamp":
                            using (var streamReader = new StreamReader(json))
                            {
                                Lamp = JsonConvert.DeserializeObject<Lamp>(streamReader.ReadToEnd());
                            }
                            break;
                        case "font":
                            using (var streamReader = new StreamReader(json))
                            {
                                Font = JsonConvert.DeserializeObject<Font>(streamReader.ReadToEnd());
                            }
                            break;
                        case "fontstyle":
                            using (var streamReader = new StreamReader(json))
                            {
                                Style = JsonConvert.DeserializeObject<FontStyle>(streamReader.ReadToEnd());
                            }
                            break;
                        case "audio":
                            using (var streamReader = new StreamReader(json))
                            {
                                Audio = JsonConvert.DeserializeObject<Audio>(streamReader.ReadToEnd());
                            }
                            break;
                        case "animation":
                            using (var streamReader = new StreamReader(json))
                            {
                                Animation = JsonConvert.DeserializeObject<Animation>(streamReader.ReadToEnd());
                            }
                            break;
                        default:
                            break;
                    }
                }

                catch (System.Exception)
                {

                    throw;
                }
            }
        }

        public void SerializeAsset(string assetJson, object assetType)
        {
            var s = JsonConvert.SerializeObject(assetType,Formatting.Indented);

            Type t = assetType.GetType();

            using (var reader = new StringReader(s))
            {                
                var write = File.CreateText(assetJson + t.Name + ".json");

                string line = reader.ReadLine();
                while (line != null)
                {
                    write.WriteLine(line);
                    line = reader.ReadLine();
                }

                write.Flush();
                write.Close();

            }

        }
    }
}
