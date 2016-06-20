using Horsesoft.Procgame.AssetModel;

namespace Horsesoft.Procgame.Manager.Base.Interfaces
{
    public interface IAssetRepo
    {
        Animation Animation { get; set; }

        Audio Audio { get; set; }

        Font Font { get; set; }

        Lamp Lamp { get; set; }

        FontStyle Style { get; set; }

        void DeserializeAssets(string assetsJsonPath);

        void SerializeAsset(string assetJson, object assetType);
    }
}
