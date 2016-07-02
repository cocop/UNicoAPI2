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
            var session = new Session<Response<User>>();
            var accessorList = new List<Func<byte[], APIs.IAccesser>>();

            if (!htmlCache.IsAvailab)
                accessorList.Add(
                    (data) =>
                    {
                        var accesser = new APIs.user_page_html.Accesser();
                        accesser.Setting(
                            context.CookieContainer,
                            target.ID);

                        return accesser;
                    });

            session.SetAccessers(
                accessorList.ToArray(),
                (data) =>
                {
                    var parser = new APIs.user_page_html.Parser();

                    if (data != null)
                        htmlCache.Value = parser.Parse(data);

                    return Converter.UserResponse(context, parser.Parse(htmlCache));
                });

            return session;
        }

        /// <summary>
        /// 公開マイリスト一覧を取得する
        /// </summary>
        public Session<Response<Mylist.Mylist[]>> DownloadPublicMylistList()
        {
            var session = new Session<Response<Mylist.Mylist[]>>();
            var accessorList = new List<Func<byte[], APIs.IAccesser>>();

            if (!htmlMylistCache.IsAvailab)
                accessorList.Add(
                    (data) =>
                    {
                        var accesser = new APIs.user_mylist_page_html.Accesser();
                        accesser.Setting(
                            context.CookieContainer,
                            target.ID);

                        return accesser;
                    });

            session.SetAccessers(
                accessorList.ToArray(),
                (data) =>
                {
                    var parser = new APIs.user_mylist_page_html.Parser();

                    if (data != null)
                        htmlMylistCache.Value = parser.Parse(data);

                    return Converter.PublicMylistListResponse(context, parser.Parse(htmlMylistCache));
                });

            return session;
        }
    }
}
