using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading;
using System.Threading.Tasks;
using UNicoAPI2.VideoService;
using System.IO;

namespace Tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            var t = new CancellationTokenSource();
            var nsp = new UNicoAPI2.NicoServicePage();
            var ls = nsp.Login("cypsf18611@yahoo.co.jp", "asdfghjkl");
            var lr = await ls.RunAsync(ls.UntreatedCount, t.Token);

            var nvsp = nsp.GetVideoServicePage();


            var ss = nvsp.Search(
                "ゆっくり実況プレイ",
                1,
                SearchType.Tag,
                new UNicoAPI2.VideoService.SearchOption()
                {
                    SortTarget = SortTarget.PostTime,
                    SortOrder = SortOrder.Up,
                });

            var sr = await ss.RunAsync(ss.UntreatedCount, t.Token);

            var vs = nvsp.GetVideoPage((sr.Result[0])).DownloadVideo();
            var vstream = await vs.RunAsync(vs.UntreatedCount, t.Token);

            

            var str = vstream.GetResponseStream();
            var mstr = new MemoryStream();
            await str.CopyToAsync(mstr, 1024, t.Token);


            //t.Cancel();
        }
    }
}
