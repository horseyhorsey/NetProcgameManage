using Horsesoft.Procgame.Config;
using Horsesoft.Procgame.Manager.Base.Interfaces;
using System.IO;
using YamlDotNet.Serialization;

namespace Horsesoft.Procgame.Manager.Base.Services
{
    public class GameConfigSvc : IGameConfigSvc
    {           
        public GameConfig Config { get; set; }

        /// <summary>
        /// Deserialize config.yaml
        /// </summary>
        /// <param name="configYaml"></param>
        /// <param name="configObject"></param>
        /// <returns></returns>
        public void LoadGameConfig(string configYaml)
        {
            var yaml = new Deserializer();

            if (!File.Exists(Path.GetFullPath(configYaml)))
            {                
                Config = new GameConfig()
                {
                    DotsW = 128,
                    DotsH = 28,
                    FrameRate = 25,
                    DisplayFlip = 0,
                    FullScreen = false,
                    DisplayFilter = false,
                    DisplayScale = 1,
                    ScreenPositionX = 50,
                    ScreenPositionY = 50,
                    Borderless = true,                    
                    ImagesPath = "asset\\gfx",
                    FontPath = "asset\\fonts",
                    VoicePath = "asset\\voice",
                    SfxPath = "asset\\sfx",
                    MusicPath = "asset\\music",
                };

                SaveGameConfig(Config);
                
            }            

            using (TextReader reader = File.OpenText(configYaml))
            {
                Config = yaml.Deserialize<GameConfig>(reader);               
            }
        }

        public void SaveGameConfig(GameConfig config)
        {
            var yaml = new Serializer();

            using (TextWriter writer = File.CreateText("config.yaml"))
            {
                Config = config;

                yaml.Serialize(writer, Config);
                
            }
        }
    }
}
