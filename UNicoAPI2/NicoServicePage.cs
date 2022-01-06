using System;
using System.Net;
using UNicoAPI2.Connect;
using UNicoAPI2.VideoService.User;
using Windows.Foundation;

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
        /// <param name="CheckLogin">すでにログインしているかチェックする、していた場合はそのユーザーを返す</param>
        /// <param name="MultiAuthRequest">二段階認証の要求に答える、nullの場合に要求が来た場合はログイン失敗する</param>
        /// <returns>nullの場合はログイン失敗</returns>
        public Session<User> Login(string MailAddress, string Password, bool CheckLogin = true, Func<MultiAuthParameter> MultiAuthRequest = null)
        {
            return new Session<User>((flow) =>
            {
                var parser = new APIs.login.Parser();

                if (CheckLogin)
                {
                    flow.Return(new APIs.UrlGet.Accessor(context.CookieContainer, "https://account.nicovideo.jp/my/account?ref=pc_mypage_top"));
                    var rslt = parser.Parse(flow.GetResult());
                    if (rslt != null)
                    {
                        return rslt;
                    }
                }

                {
                    var accessor = new APIs.login_page_html.Accessor();
                    accessor.Setting(context.CookieContainer);
                    flow.Return(accessor);
                }

                {
                    var accessor = new APIs.login.Accessor();
                    accessor.Setting(context.CookieContainer, MailAddress, Password);
                    flow.Return(accessor);
                }

                if (parser.IsMultiAuth(flow.GetResult()))
                {
                    if (MultiAuthRequest == null)
                        return null;

                    var param = MultiAuthRequest();
                    flow.Return(new APIs.login.MultiAuthAccessor(
                        context.CookieContainer,
                        parser.GetCsrfToken(flow.GetResult()),
                        param.Code,
                        param.DeviceName,
                        param.IsTrustedDevice));

                    flow.Return(new APIs.UrlGet.Accessor(context.CookieContainer, "https://account.nicovideo.jp/my/account?ref=pc_mypage_top"));
                }

                return parser.Parse(flow.GetResult());
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
