using System;

namespace UNicoAPI2.APIs.nvapi.user_series.Response
{
    public class Rootobject : Response<Data> { }

    public class Data
    {
        public int totalCount { get; set; }
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public Owner owner { get; set; }
        public string title { get; set; }
        public bool isListed { get; set; }
        public string description { get; set; }
        public string thumbnailUrl { get; set; }
        public int itemsCount { get; set; }
    }

    public class Owner
    {
        public string type { get; set; }
        public string id { get; set; }
    }
}