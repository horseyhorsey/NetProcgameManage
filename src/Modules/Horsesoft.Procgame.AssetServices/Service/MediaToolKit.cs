using MediaToolkit;
using MediaToolkit.Model;
using System.IO;
using System.Threading.Tasks;

namespace Horsesoft.Procgame.AssetServices.Service
{
    public class MediaToolKit
    {
        public static async Task<string> GetVideoFormat(string videoPath)
        {
            string videoInfo = "";

            await Task.Run(() =>
            {
                if (!File.Exists(videoPath)) return "";

                var inputFile = new MediaFile { Filename = videoPath };
                
                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    videoInfo += inputFile.Metadata.VideoData.FrameSize + ",";
                    videoInfo += inputFile.Metadata.VideoData.Fps + ",";                    
                    videoInfo += inputFile.Metadata.Duration.Duration() + ",";

                    try {videoInfo += inputFile.Metadata.AudioData.Format;}
                    catch (System.Exception) { }
                    
                    engine.Dispose();
                    return videoInfo;
                }                
            });

            return videoInfo;
        }
    }
}
