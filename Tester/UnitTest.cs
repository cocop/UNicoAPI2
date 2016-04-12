using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Threading;
using System.Threading.Tasks;
using UNicoAPI2;
using UNicoAPI2.VideoService;
using UNicoAPI2.VideoService.User;

namespace Tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            var nsp = new NicoServicePage();
            var id = ((Task<string>)nsp.Login("rmecila@gmail.com", "asdfghjkl")).Result;

            var up = nsp
                .GetVideoServicePage()
                .GetUserPage(new User(id));

            ((Task<Response<User>>)up.UserDownload()).Wait();
        }
    }
}
