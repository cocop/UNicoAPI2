namespace UNicoAPI2.APIs.likes.Response
{
    public class Rootobject
    {
        public Meta meta { get; set; }
        public Data data { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
    }

    public class Data
    {
        public string thanksMessage { get; set; }
    }
}