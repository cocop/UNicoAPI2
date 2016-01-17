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
    }
}