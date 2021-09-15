using System;

namespace UNicoAPI2.APIs.mylitv2.Response
{
    public class Rootobject
    {
        public Meta meta { get; set; }
        public Data data { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
        public string errorCode { get; set; }
    }

    public class Data
    {
        public Mylist mylist { get; set; }
    }

    public class Mylist
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string defaultSortKey { get; set; }
        public string defaultSortOrder { get; set; }
        public Item[] items { get; set; }
        public int totalItemCount { get; set; }
        public bool hasNext { get; set; }
        public bool isPublic { get; set; }
        public Owner owner { get; set; }
        public bool hasInvisibleItems { get; set; }
        public int followerCount { get; set; }
        public bool isFollowing { get; set; }
    }

    public class Owner
    {
        public string ownerType { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string iconUrl { get; set; }
    }

    public class Item
    {
        public int itemId { get; set; }
        public string watchId { get; set; }
        public string description { get; set; }
        public DateTime addedAt { get; set; }
        public string status { get; set; }
        public Video video { get; set; }
    }

    public class Video
    {
        public string type { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public DateTime registeredAt { get; set; }
        public Count count { get; set; }
        public Thumbnail thumbnail { get; set; }
        public int duration { get; set; }
        public string shortDescription { get; set; }
        public string latestCommentSummary { get; set; }
        public bool isChannelVideo { get; set; }
        public bool isPaymentRequired { get; set; }
        public int? playbackPosition { get; set; }
        public Owner1 owner { get; set; }
        public bool requireSensitiveMasking { get; set; }
        public object videoLive { get; set; }
    }

    public class Count
    {
        public int view { get; set; }
        public int comment { get; set; }
        public int mylist { get; set; }
        public int like { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public string middleUrl { get; set; }
        public string largeUrl { get; set; }
        public string listingUrl { get; set; }
        public string nHdUrl { get; set; }
    }

    public class Owner1
    {
        public string ownerType { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string iconUrl { get; set; }
    }
}