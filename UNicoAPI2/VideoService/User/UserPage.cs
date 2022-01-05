using System;
using System.Collections.Generic;
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
                    var accessor = new APIs.user_page_html.Accessor();
                    accessor.Setting(
                        context.CookieContainer,
                        target.ID);

                    flow.Return(accessor);
                }

                var parser = new APIs.user_page_html.Parser();

                if (flow.GetResult() != null)
                    htmlCache.Value = parser.Parse(flow.GetResult());

                return Converter.UserResponse(context, parser.Parse(htmlCache));
            });
        }

        /// <summary>
        /// 公開マイリスト一覧を取得する
        /// </summary>
        public Session<Response<Mylist.Mylist[]>> DownloadPublicMylistList()
        {
            return new Session<Response<Mylist.Mylist[]>>((flow) =>
            {
                var parser = new APIs.user_mylist_page_html.Parser();

                if (!htmlMylistCache.IsAvailab)
                {
                    var accessor = new APIs.user_mylist_page_html.Accessor();
                    accessor.Setting(
                        context.CookieContainer,
                        target.ID);
                    flow.Return(accessor);

                    htmlMylistCache.Value = parser.Parse(flow.GetResult());
                }

                return Converter.PublicMylistListResponse(context, parser.Parse(htmlMylistCache));
            });
        }
    }
}
