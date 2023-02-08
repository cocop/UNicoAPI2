using System;
using System.Collections.Generic;
using System.Text;

namespace UNicoAPI2.APIs.html.series_page.Response
{
    public class Rootobject
    {
        public State state { get; set; }
        public Nvapi[] nvapi { get; set; }
    }

    public class State
    {
        public Userdetails userDetails { get; set; }
    }

    public class Userdetails
    {
        public Userdetails1 userDetails { get; set; }
    }

    public class Userdetails1
    {
        public string type { get; set; }
        public User user { get; set; }
        public Followstatus followStatus { get; set; }
    }

    public class User
    {
        public string description { get; set; }
        public string decoratedDescriptionHtml { get; set; }
        public string strippedDescription { get; set; }
        public bool isPremium { get; set; }
        public string registeredVersion { get; set; }
        public int followeeCount { get; set; }
        public int followerCount { get; set; }
        public Userlevel userLevel { get; set; }
        public object userChannel { get; set; }
        public bool isNicorepoReadable { get; set; }
        public Sn[] sns { get; set; }
        public object coverImage { get; set; }
        public string id { get; set; }
        public string nickname { get; set; }
        public Icons icons { get; set; }
    }

    public class Userlevel
    {
        public int currentLevel { get; set; }
        public int nextLevelThresholdExperience { get; set; }
        public int nextLevelExperience { get; set; }
        public int currentLevelExperience { get; set; }
    }

    public class Icons
    {
        public string small { get; set; }
        public string large { get; set; }
    }

    public class Sn
    {
        public string type { get; set; }
        public string label { get; set; }
        public string iconUrl { get; set; }
        public string screenName { get; set; }
        public string url { get; set; }
    }

    public class Followstatus
    {
        public bool isFollowing { get; set; }
    }

    public class Nvapi
    {
        public string method { get; set; }
        public string path { get; set; }
        public string templatePath { get; set; }
        public Query query { get; set; }
        public Body body { get; set; }
    }

    public class Query
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string sensitiveContents { get; set; }
    }

    public class Body
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
        public Detail detail { get; set; }
        public int totalCount { get; set; }
        public Item[] items { get; set; }
    }

    public class Detail
    {
        public int id { get; set; }
        public Owner owner { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string decoratedDescriptionHtml { get; set; }
        public string thumbnailUrl { get; set; }
        public bool isListed { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }

    public class Owner
    {
        public string type { get; set; }
        public string id { get; set; }
        public User1 user { get; set; }
    }

    public class User1
    {
        public string type { get; set; }
        public bool isPremium { get; set; }
        public string description { get; set; }
        public string strippedDescription { get; set; }
        public string shortDescription { get; set; }
        public int id { get; set; }
        public string nickname { get; set; }
        public Icons1 icons { get; set; }
    }

    public class Icons1
    {
        public string small { get; set; }
        public string large { get; set; }
    }

    public class Item
    {
        public Meta1 meta { get; set; }
        public Video video { get; set; }
    }

    public class Meta1
    {
        public string id { get; set; }
        public int order { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }

    public class Video
    {
        public string type { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string registeredAt { get; set; }
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
        public bool isMuted { get; set; }
        public bool _9d091f87 { get; set; }
        public bool acf68865 { get; set; }
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
        public string type { get; set; }
        public string visibility { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string iconUrl { get; set; }
    }
}
