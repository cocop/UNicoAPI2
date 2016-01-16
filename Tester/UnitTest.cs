using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading;
using System.Threading.Tasks;
using UNicoAPI2.VideoService;

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
            var ls = nsp.Login("nagatoyukiz@yahoo.co.jp", "asdfghjkl");
            var lr = await ls.RunAsync(ls.UntreatedCount, t.Token);

            var nvsp = nsp.GetVideoServicePage();
            var ss = nvsp.Search(
                "ゆっくり実況プレイ",
                1,
                SearchType.Tag,
                new SearchOption()
                {
                    SortTarget = SortTarget.PostTime,
                    SortOrder = SortOrder.Up,
                });

            var sr = await ss.RunAsync(ss.UntreatedCount, t.Token);

            t.Cancel();
        }
    }
}
