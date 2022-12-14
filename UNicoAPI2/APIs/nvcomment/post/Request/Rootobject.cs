namespace UNicoAPI2.APIs.nvcomment.post.Request
{
    public class Rootobject
    {
        public string videoId { get; set; }
        public string[] commands { get; set; }
        public string body { get; set; }
        public int vposMs { get; set; }
        public string postKey { get; set; }
    }
}
