
using System;
using System.Runtime.Serialization;

namespace UNicoAPI2.APIs.html.search_page.Response
{
    public class Rootobject
    {
        public Meta meta { get; set; }
        public Data data { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
        public string code { get; set; }
    }

    public class Data
    {
        public Metadata metadata { get; set; }
        public Googletagmanager googleTagManager { get; set; }
        public Response response { get; set; }
    }

    public class Metadata
    {
        public string title { get; set; }
        public Linktag[] linkTags { get; set; }
        public Metatag[] metaTags { get; set; }
        public object[] jsonLds { get; set; }
    }

    public class Linktag
    {
        public string rel { get; set; }
        public string href { get; set; }
        public object attrs { get; set; }
    }

    public class Metatag
    {
        public string name { get; set; }
        public string content { get; set; }
        public string property { get; set; }
    }

    public class Googletagmanager
    {
        public User user { get; set; }
    }

    public class User
    {
        public string login_status { get; set; }
        public string user_id { get; set; }
        public string member_status { get; set; }
        public string ui_area { get; set; }
        public string ui_lang { get; set; }
    }

    [DataContract]
    public class Response
    {
        [DataMember(Name = "$getSearchVideoV2")]
        public Getsearchvideov2 getSearchVideoV2 { get; set; }
        [DataMember]
        public Page page { get; set; }
    }

    public class Getsearchvideov2
    {
        public Data1 data { get; set; }
    }

    public class Data1
    {
        public string searchId { get; set; }
        public int totalCount { get; set; }
        public bool hasNext { get; set; }
        public string keyword { get; set; }
        public object tag { get; set; }
        public object lockTag { get; set; }
        public object[] genres { get; set; }
        public Item[] items { get; set; }
        public Additionals additionals { get; set; }
    }

    public class Additionals
    {
        public Tag[] tags { get; set; }
        public Nicoadgroup[] nicoadGroups { get; set; }
        public Waku waku { get; set; }
    }

    public class Waku
    {
        public Tagrelatedbanner tagRelatedBanner { get; set; }
    }

    public class Tagrelatedbanner
    {
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string description { get; set; }
        public bool isEvent { get; set; }
        public string linkUrl { get; set; }
        public string linkType { get; set; }
        public string linkOrigin { get; set; }
        public bool isNewWindow { get; set; }
    }

    public class Tag
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Nicoadgroup
    {
        public int requestId { get; set; }
        public Nicoadnicodic[] nicoadNicodics { get; set; }
    }

    public class Nicoadnicodic
    {
        public string title { get; set; }
        public int activePoint { get; set; }
        public int totalPoint { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string message { get; set; }
        public string redirectUrl { get; set; }
        public string decoration { get; set; }
        public Tracking tracking { get; set; }
    }

    public class Tracking
    {
        public string impression { get; set; }
        public string click { get; set; }
    }

    public class Item
    {
        public string type { get; set; }
        public string id { get; set; }
        public string contentType { get; set; }
        public string title { get; set; }
        public string registeredAt { get; set; }
        public Count count { get; set; }
        public Thumbnail thumbnail { get; set; }
        public int duration { get; set; }
        public string shortDescription { get; set; }
        public string latestCommentSummary { get; set; }
        public bool isChannelVideo { get; set; }
        public bool isPaymentRequired { get; set; }
        public object playbackPosition { get; set; }
        public Owner owner { get; set; }
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
        public object shortUrl { get; set; }
    }

    public class Owner
    {
        public string ownerType { get; set; }
        public string type { get; set; }
        public string visibility { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string iconUrl { get; set; }
    }

    public class Page
    {
        public Common common { get; set; }
        public Nicodic nicodic { get; set; }
        public Nicoadgroupsrequestidmap nicoadGroupsRequestIdMap { get; set; }
        public string playlist { get; set; }
        public object defaultSortABTestVariant { get; set; }
    }

    public class Common
    {
        public string searchWord { get; set; }
        public string[] searchWords { get; set; }
        public string searchType { get; set; }
        public Option option { get; set; }
        public Pagination pagination { get; set; }
        public bool isImmoralSearch { get; set; }
    }

    public class Option
    {
        public Sort sort { get; set; }
        public Presetfilter[] presetFilter { get; set; }
        public Daterangefilter dateRangeFilter { get; set; }
    }

    public class Sort
    {
        public Key[] key { get; set; }
        public Order[] order { get; set; }
    }

    public class Key
    {
        public string label { get; set; }
        public string value { get; set; }
        public bool active { get; set; }
        public bool _default { get; set; }
        public bool orderable { get; set; }
    }

    public class Order
    {
        public string label { get; set; }
        public string value { get; set; }
        public bool active { get; set; }
        public bool _default { get; set; }
    }

    public class Daterangefilter
    {
        public Start start { get; set; }
        public End end { get; set; }
    }

    public class Start
    {
        public string label { get; set; }
        public object value { get; set; }
    }

    public class End
    {
        public string label { get; set; }
        public object value { get; set; }
    }

    public class Presetfilter
    {
        public string label { get; set; }
        public string query { get; set; }
        public Item1[] items { get; set; }
    }

    public class Item1
    {
        public string label { get; set; }
        public object value { get; set; }
        public bool active { get; set; }
        public bool _default { get; set; }
    }

    public class Pagination
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int maxPage { get; set; }
    }

    public class Nicodic
    {
        public string url { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public bool advertisable { get; set; }
    }

    public class Nicoadgroupsrequestidmap
    {
        public int nicodic { get; set; }
    }
}