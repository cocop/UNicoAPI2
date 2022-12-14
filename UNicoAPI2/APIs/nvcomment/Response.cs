namespace UNicoAPI2.APIs.nvcomment
{
    public class Response<Data>
    {
        public Meta meta { get; set; }
        public Data data { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
        public string message { get; set; }
    }
}
