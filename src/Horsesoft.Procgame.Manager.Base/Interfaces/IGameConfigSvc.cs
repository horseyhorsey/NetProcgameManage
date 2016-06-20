using Horsesoft.Procgame.Config;

namespace Horsesoft.Procgame.Manager.Base.Interfaces
{
    public interface IGameConfigSvc
    {
        GameConfig Config { get; set; }

        void LoadGameConfig(string configYaml);

        void SaveGameConfig(GameConfig config);
    }
}
