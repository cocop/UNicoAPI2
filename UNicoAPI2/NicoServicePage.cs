using System;
using UNicoAPI2.Connect;

namespace UNicoAPI2
{
    /******************************************/
    /// <summary>
    /// ドワンゴサービスAPIへアクセス
    /// </summary>
    /******************************************/
    public class NicoServicePage
    {
        Context context = new Context();

        VideoService.VideoServicePage videoServicePage;

        /// <summary>
        /// ID管理
        /// </summary>
        public IDContainer IDContainer { get { return context.IDContainer; } }

        /// <summary>ログインする</summary>
        /// <param name="MailAddress">メールアドレス</param>
        /// <param name="Password">パスワード</param>
        public Session<bool> Login(string MailAddress, string Password)
        {
            var session = new Session<bool>();

            session.SetAccessers(new Func<byte[], APIs.IAccesser>[]
            {
                (data) =>
                {
                    var accesser = new APIs.login.Accesser();
                    accesser.Setting(context.CookieContainer, MailAddress, Password);

                    return accesser;
                }
            },
            (data) =>
                new APIs.login.Parser().Parse(data));

            return session;
        }

        /// <summary>
        /// ニコニコ動画アクセスAPIを取得する
        /// </summary>
        public VideoService.VideoServicePage GetVideoServicePage()
        {
            if (videoServicePage == null)
                videoServicePage = new VideoService.VideoServicePage(context);

            return videoServicePage;
        }
    }
}
