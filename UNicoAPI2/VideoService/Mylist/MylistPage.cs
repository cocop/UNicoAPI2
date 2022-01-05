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
            var index = 1;
            return new Session<Response<Mylist>>((flow) =>
            {
                var accessor = new APIs.mylitv2.Accessor();
                accessor.Setting(
                    context.CookieContainer,
                    target.ID,
                    index,
                    100);
                flow.Return(accessor);

                return Converter.MylistResponse(
                    context,
                    new APIs.mylitv2.Parser().Parse(flow.GetResult()),
                    target.ID);
            });
        }
    }
}