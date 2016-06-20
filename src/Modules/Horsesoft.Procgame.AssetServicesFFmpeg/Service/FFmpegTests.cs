using NUnit.Framework;
using Horsesoft.Procgame.AssetServices.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horsesoft.Procgame.AssetServices.Service.Tests
{
    [TestFixture()]
    public class FFmpegTests
    {
        const string ffmpeg = @"C:\ffmpeg.exe";
        const string videoFile = @"I:\HyperSpin\Media\Amstrad CPC\Video\2088 (europe).mp4"; 

         [Test()]
        public async Task SoundFromMovieTest()
        {            
            await FFmpeg.SoundFromMovieAsync(ffmpeg, videoFile, @"C:\", "testAudio", "mp3");
        }

        [Test()]
        public async Task ImageFromMovieTest()
        {
            await FFmpeg.ImagesFromMovieAsync(ffmpeg, videoFile, @"C:\", "testimage",23.97, "png");
        }

        string assetPath = @"I:\VS_Projects\!Pinball\ProcgameManager\src\Horsesoft.Procgame.Manager.Shell\bin\Debug\asset\\";

        [Test()]
        public void SerializeAssetTest()
        {
            var assetRepo = new AssetRepo();

            assetRepo.DeserializeAssets(assetPath);

            assetRepo.SerializeAsset(assetPath, assetRepo.Style);
        }
    }
}