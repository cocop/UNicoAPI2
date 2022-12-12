using System;

namespace UNicoAPI2.APIs.nvcomment.get.Response
{
    public class Rootobject : Response<Data> { }

    public class Data
    {
        public Globalcomment[] globalComments { get; set; }
        public Thread[] threads { get; set; }
    }

    public class Globalcomment
    {
        public string id { get; set; }
        public int count { get; set; }
    }

    public class Thread
    {
        public string id { get; set; }
        public string fork { get; set; }
        public int commentCount { get; set; }
        public Comment[] comments { get; set; }
    }

    public class Comment
    {
        public string id { get; set; }
        public int no { get; set; }
        public int vposMs { get; set; }
        public string body { get; set; }
        public string[] commands { get; set; }
        public string userId { get; set; }
        public bool isPremium { get; set; }
        public int score { get; set; }
        public DateTime postedAt { get; set; }
        public int nicoruCount { get; set; }
        public object nicoruId { get; set; }
        public string source { get; set; }
        public bool isMyPost { get; set; }
    }
}