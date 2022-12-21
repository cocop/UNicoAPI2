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
        Cache<string> htmlCache = new Cache<string>();
        Cache<string> htmlMylistCache = new Cache<string>();

        internal UserPage(User Target, Context Context)
        {
            target = Target;
            context = Context;
        }

        /// <summary>
        /// ユーザー情報を取得する
        /// </summary>
        public Session<Response<User>> DownloadUser()
        {
            return new Session<Response<User>>((flow) =>
            {
                if (!htmlCache.IsAvailab)
                {
                    flow.Return(new APIs.html.user_page.Accessor
                    {
                        CookieContainer = context.CookieContainer,
                        UserId = target.ID
                    });
                }

                var parser = new APIs.html.my_page.Parser();

                if (flow.GetResult() != null)
                    htmlCache.Value = parser.Parse(flow.GetResult());

                return Converter.UserPage.DownloadUser.From(context, parser.Parse(htmlCache));
            });
        }

        /// <summary>
        /// 公開マイリスト一覧を取得する
        /// </summary>
        public Session<Response<Mylist.Mylist[]>> DownloadPublicMylistList()
        {
            return new Session<Response<Mylist.Mylist[]>>((flow) =>
            {
                var parser = new APIs.html.user_mylist_page.Parser();

                if (!htmlMylistCache.IsAvailab)
                {
                    flow.Return(new APIs.html.user_mylist_page.Accessor
                    {
                        CookieContainer = context.CookieContainer,
                        UserId = target.ID
                    });

                    htmlMylistCache.Value = parser.Parse(flow.GetResult());
                }

                return Converter.UserPage.DownloadPublicMylistList.From(context, parser.Parse(htmlMylistCache));
            });
        }
    }
}
