using System;
using System.Net;
using UNicoAPI2.Connect;
using UNicoAPI2.VideoService.User;

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

        public CookieContainer CookieContainer { get { return context.CookieContainer; } }

        /// <summary>
        /// キャッシュの有効期限
        /// </summary>
        public TimeSpan CacheDeadline { get { return context.CacheDeadline; } set { context.CacheDeadline = value; } }

        /// <summary>ログインする</summary>
        /// <param name="MailAddress">メールアドレス</param>
        /// <param name="Password">パスワード</param>
        public Session<User> Login(string MailAddress, string Password)
        {
            return new Session<User>((flow) =>
            {
                flow.Return(new APIs.login_page_html.Accessor());

                var accessor = new APIs.login.Accessor();
                accessor.Setting(context.CookieContainer, MailAddress, Password);
                flow.Return(accessor);

                return new APIs.login.Parser().Parse(flow.GetResult());
            });
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
