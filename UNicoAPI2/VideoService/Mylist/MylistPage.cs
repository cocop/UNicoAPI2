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
                flow.Return(new APIs.nvapi.mylitv2.Accessor
                {
                    CcookieContainer = context.CookieContainer,
                    Id = target.ID,
                    Index = index,
                    Count = 100
                });

                return Converter.MylistPage.DownloadMylist.From(
                    context,
                    new APIs.nvapi.mylitv2.Parser().Parse(flow.GetResult()),
                    target.ID);
            });
        }
    }
}