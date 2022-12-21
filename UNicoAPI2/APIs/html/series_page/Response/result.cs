namespace UNicoAPI2.APIs.html.series_page.Response
{
    public class Result
    {
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public User PostUser { get; set; }
        public Series[] OtherSeriesList { get; set; }
        public Video[] VideoList { get; set; }
    }
}
