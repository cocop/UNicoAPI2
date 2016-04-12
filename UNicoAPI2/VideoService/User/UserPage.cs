using System;
using UNicoAPI2.Connect;

namespace UNicoAPI2.VideoService.User
{
    /******************************************/
    /// <summary>
    /// ユーザーページへアクセスする
    /// </summary>
    /******************************************/
    public class UserPage
    {
        User target;
        Context context;

        internal UserPage(User Target, Context Context)
        {
            target = Target;
            context = Context;
        }

        /// <summary>
        /// ユーザー情報を取得するストリームを取得する
        /// </summary>
        public Session<Response<User>> UserDownload()
        {
            var session = new Session<Response<User>>();

            session.SetAccessers(
                new Func<byte[], APIs.IAccesser>[]
                {
                    (data) =>
                    {
                        var accesser = new APIs.user_page_html.Accesser();
                        accesser.Setting(
                            context.CookieContainer,
                            target.ID);

                        return accesser;
                    }
                },
                (data) =>
                {
                    var parser = new APIs.user_page_html.Parser();
                    var result = parser.Parse(parser.Parse(data));

                    return null;
                });

            return session;
        }
    }
}
