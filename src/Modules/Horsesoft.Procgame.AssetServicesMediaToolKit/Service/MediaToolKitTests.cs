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
    public class MediaToolKitTests
    {
        [Test()]
        public async Task GetVideoFormatTest()
        {
            var info = await MediaToolKit.GetVideoFormat(@"C:\test.mp4");

            foreach (var item in info.Split(','))
            {
                Console.WriteLine(item);
            }

        }
    }
}