using System;
using UNicoAPI2.Connect;

namespace UNicoAPI2.VideoService.Mylist
{
    /******************************************/
    /// <summary>
    /// マイリストページへアクセスする
    /// </summary>
    /******************************************/
    public class MylistPage
    {
        Mylist target;
        Context context;

        internal MylistPage(Mylist Target, Context Context)
        {
            target = Target;
            context = Context;
        }

        /// <summary>
        /// マイリストを取得する
        /// </summary>
        public Session<Response<Mylist>> DownloadMylist()
        {
            var session = new Session<Response<Mylist>>();

            var index = 1;

            session.SetAccessers(
                new Func<byte[], APIs.IAccessor>[]
                {
                    (data) =>
                    {
                        var accesser = new APIs.mylitv2.Accessor();
                        accesser.Setting(
                            context.CookieContainer,
                            target.ID,
                            index,
                            100);

                        return accesser;
                    }
                },
                (data) =>
                {
                    return Converter.MylistResponse(
                        context,
                        new APIs.mylitv2.Parser().Parse(data),
                        target.ID);
                });

            return session;
        }
    }
}