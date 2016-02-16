using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Threading;
using System.Threading.Tasks;
using UNicoAPI2;
using UNicoAPI2.VideoService;
using UNicoAPI2.VideoService.Video;

namespace Tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            NicoServicePage nsp = new NicoServicePage();
            bool lr = await (Task<bool>)nsp.Login("rmecila@gmail.com", "asdfghjkl");
            var vsp = nsp.GetVideoServicePage();
            var vp = vsp.GetVideoPage(nsp.IDContainer.GetVideoInfo("sm15216282"));

            var dc = vp.DownloadComment();
            
        }
    }
}
