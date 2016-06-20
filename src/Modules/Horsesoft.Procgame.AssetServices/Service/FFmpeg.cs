using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Horsesoft.Procgame.AssetServices.Service
{
    public static class FFmpeg
    {
        public static async Task SoundFromMovieAsync(string ffMpeg, string videoPath,
            string audioPath, string audioFileName,
            string audioFormat = "mp3", int bitrate = 256)
        {            

            if (!Directory.Exists(audioPath))
                Directory.CreateDirectory(audioPath);

            var vidPath = '"' + videoPath + '"';
            var soundPath = '"' + audioPath + @"\" + audioFileName + "." + audioFormat + '"';

            await Task.Run(() => Process.Start(ffMpeg, @" -i " + videoPath + " -vn -ab " + bitrate + "k " + soundPath).WaitForExit());

        }

        public static async Task ImagesFromMovieAsync(
            string ffMpeg, string videoPath,
            string imagesPath, string imageFileName, double fps,
            string imageFormat = "png")
        {

            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var vidPath = '"' + videoPath + '"';

            var newImageFileName = imageFileName + @"_%04d." + imageFormat;

            var imagePath = '"' + imagesPath + @"\" + newImageFileName + '"';
            
            await Task.Run(() => Process.Start(ffMpeg, @" -i " + vidPath + " -qscale:v 10 -vf fps=" + fps + " " + imagePath).WaitForExit());

        }
    }
}
