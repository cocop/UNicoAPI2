using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Threading;
using System.Threading.Tasks;
using UNicoAPI2;
using UNicoAPI2.Connect;
using UNicoAPI2.VideoService;
using UNicoAPI2.VideoService.Mylist;
using UNicoAPI2.VideoService.User;

namespace Tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            NicoServicePage nsp = new NicoServicePage();
            var lr = await (Task<User>)nsp.Login("cypsf18611@yahoo.co.jp","asdfghjkl");
            var vsp = nsp.GetVideoServicePage();
            var up = vsp.GetUserPage(new User("5194412"));
            var u = await (Task<Response<User>>)up.DownloadUser();
            var r = u.Result;
        }
    }
}
