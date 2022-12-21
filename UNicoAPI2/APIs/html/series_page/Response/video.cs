namespace UNicoAPI2.APIs.html.series_page.Response
{
    public class Video
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ViewCount { get; set; }
        public string CommentCount { get; set; }
        public string MylistCount { get; set; }
        public string ThumbnailUrl { get; internal set; }
    }
}
