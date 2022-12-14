namespace UNicoAPI2.APIs.nvapi
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
