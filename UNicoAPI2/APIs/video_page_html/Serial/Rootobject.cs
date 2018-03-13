using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UNicoAPI2.APIs.video_page_html.Serial
{
    [DataContract]
    public class Rootobject
    {
        [DataMember]
        public Video video { get; set; }

        [DataMember]
        public Player player { get; set; }

        [DataMember]
        public Thread1 thread { get; set; }

        [DataMember]
        public Tag[] tags { get; set; }

        [DataMember]
        public Playlist playlist { get; set; }

        [DataMember]
        public Owner owner { get; set; }

        [DataMember]
        public Viewer viewer { get; set; }

        [DataMember]
        public object community { get; set; }

        [DataMember]
        public object channel { get; set; }

        [DataMember]
        public Ad ad { get; set; }

        [DataMember]
        public Lead lead { get; set; }

        [DataMember]
        public object maintenance { get; set; }

        [DataMember]
        public Context context { get; set; }

        [DataMember]
        public Livetopics liveTopics { get; set; }
    }

    public class Video
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string originalTitle { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string originalDescription { get; set; }

        [DataMember]
        public string thumbnailURL { get; set; }

        [DataMember]
        public string postedDateTime { get; set; }

        [DataMember]
        public object originalPostedDateTime { get; set; }

        [DataMember]
        public int? width { get; set; }

        [DataMember]
        public int? height { get; set; }

        [DataMember]
        public int? duration { get; set; }

        [DataMember]
        public int? viewCount { get; set; }

        [DataMember]
        public int? mylistCount { get; set; }

        [DataMember]
        public bool? translation { get; set; }

        [DataMember]
        public object translator { get; set; }

        [DataMember]
        public string movieType { get; set; }

        [DataMember]
        public object badges { get; set; }

        [DataMember]
        public object introducedNicoliveDJInfo { get; set; }

        [DataMember]
        public Dmcinfo dmcInfo { get; set; }

        [DataMember]
        public object backCommentType { get; set; }

        [DataMember]
        public bool? isCommentExpired { get; set; }

        [DataMember]
        public string isWide { get; set; }

        [DataMember]
        public object isOfficialAnime { get; set; }

        [DataMember]
        public object isNoBanner { get; set; }

        [DataMember]
        public bool? isDeleted { get; set; }

        [DataMember]
        public bool? isTranslated { get; set; }

        [DataMember]
        public bool? isR18 { get; set; }

        [DataMember]
        public bool? isAdult { get; set; }

        [DataMember]
        public object isNicowari { get; set; }

        [DataMember]
        public bool? isPublic { get; set; }

        [DataMember]
        public object isPublishedNicoscript { get; set; }

        [DataMember]
        public object isNoNGS { get; set; }

        [DataMember]
        public string isCommunityMemberOnly { get; set; }

        [DataMember]
        public bool? isCommonsTreeExists { get; set; }

        [DataMember]
        public bool? isNoIchiba { get; set; }

        [DataMember]
        public bool? isOfficial { get; set; }

        [DataMember]
        public bool? isMonetized { get; set; }

        [DataMember]
        public Smileinfo smileInfo { get; set; }
    }

    public class Dmcinfo
    {
        public int? time { get; set; }
        public long time_ms { get; set; }
        public Video1 video { get; set; }
        public Thread thread { get; set; }
        public User user { get; set; }
        public Hiroba hiroba { get; set; }
        public object error { get; set; }
        public Session_Api session_api { get; set; }
        public object storyboard_session_api { get; set; }
        public Quality quality { get; set; }
    }

    public class Video1
    {
        public string video_id { get; set; }
        public int? length_seconds { get; set; }
        public int? deleted { get; set; }
    }

    public class Thread
    {
        public string server_url { get; set; }
        public string sub_server_url { get; set; }
        public int? thread_id { get; set; }
        public object nicos_thread_id { get; set; }
        public object optional_thread_id { get; set; }
        public bool? thread_key_required { get; set; }
        public object[] channel_ng_words { get; set; }
        public object[] owner_ng_words { get; set; }
        public bool? maintenances_ng { get; set; }
        public bool? postkey_available { get; set; }
        public int? ng_revision { get; set; }
    }

    public class User
    {
        public int? user_id { get; set; }
        public bool? is_premium { get; set; }
        public string nickname { get; set; }
    }

    public class Hiroba
    {
        public object fms_token { get; set; }
        public string server_url { get; set; }
        public int? server_port { get; set; }
        public int? thread_id { get; set; }
        public string thread_key { get; set; }
    }

    public class Session_Api
    {
        public string recipe_id { get; set; }
        public string player_id { get; set; }
        public string[] videos { get; set; }
        public string[] audios { get; set; }
        public object[] movies { get; set; }
        public string[] protocols { get; set; }
        public Auth_Types auth_types { get; set; }
        public string service_user_id { get; set; }
        public string token { get; set; }
        public string signature { get; set; }
        public string content_id { get; set; }
        public int? heartbeat_lifetime { get; set; }
        public int? content_key_timeout { get; set; }
        public float priority { get; set; }
        public Url[] urls { get; set; }
    }

    public class Auth_Types
    {
        public string http { get; set; }
    }

    public class Url
    {
        public string url { get; set; }
        public bool? is_well_known_port { get; set; }
        public bool? is_ssl { get; set; }
    }

    public class Quality
    {
        public Video2[] videos { get; set; }
        public Audio[] audios { get; set; }
    }

    public class Video2
    {
        public string id { get; set; }
        public bool? available { get; set; }
        public int? bitrate { get; set; }
        public Resolution resolution { get; set; }
    }

    public class Resolution
    {
        public int? width { get; set; }
        public int? height { get; set; }
    }

    public class Audio
    {
        public string id { get; set; }
        public bool? available { get; set; }
        public int? bitrate { get; set; }
        public int? sampling_rate { get; set; }
    }

    public class Smileinfo
    {
        public string url { get; set; }
        public bool? isSlowLine { get; set; }
        public string currentQualityId { get; set; }
        public string[] qualityIds { get; set; }
    }

    public class Player
    {
        public int? playerInfoXMLUpdateTIme { get; set; }
        public bool? isContinuous { get; set; }
    }

    public class Thread1
    {
        public int? commentCount { get; set; }
        public object hasOwnerThread { get; set; }
        public object mymemoryLanguage { get; set; }
        public string serverUrl { get; set; }
        public string subServerUrl { get; set; }
        public Ids ids { get; set; }
    }

    public class Ids
    {
        public string _default { get; set; }
        public object nicos { get; set; }
        public object community { get; set; }
    }

    public class Playlist
    {
        public Item[] items { get; set; }
        public string type { get; set; }
        public string _ref { get; set; }
        public object[] option { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public object requestId { get; set; }
        public string title { get; set; }
        public string thumbnailURL { get; set; }
        public string viewCounter { get; set; }
        public object numRes { get; set; }
        public string mylistCounter { get; set; }
        public string firstRetrieve { get; set; }
        public string lengthSeconds { get; set; }
        public object threadUpdateTime { get; set; }
        public object createTime { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public bool? isTranslated { get; set; }
        public object mylistComment { get; set; }
        public object tkasType { get; set; }
        public bool? hasData { get; set; }
    }

    public class Owner
    {
        public string id { get; set; }
        public string nickname { get; set; }
        public string stampExp { get; set; }
        public string iconURL { get; set; }
        public object nicoliveInfo { get; set; }
        public object channelInfo { get; set; }
        public bool? isUserVideoPublic { get; set; }
        public bool? isUserMyVideoPublic { get; set; }
        public bool? isUserOpenListPublic { get; set; }
    }

    public class Viewer
    {
        public int? id { get; set; }
        public string nickname { get; set; }
        public int? prefecture { get; set; }
        public int? sex { get; set; }
        public int? age { get; set; }
        public bool? isPremium { get; set; }
        public bool? isPrivileged { get; set; }
        public bool? isPostLocked { get; set; }
        public bool? isHtrzm { get; set; }
        public bool? isTwitterConnection { get; set; }
    }

    public class Ad
    {
        public object vastMetaData { get; set; }
    }

    public class Lead
    {
        public Tagrelatedmarquee tagRelatedMarquee { get; set; }
        public Tagrelatedbanner tagRelatedBanner { get; set; }
        public object nicosdkApplicationBanner { get; set; }
        public object videoEndBannerIn { get; set; }
        public object videoEndOverlay { get; set; }
    }

    public class Tagrelatedmarquee
    {
        public string id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
    }

    public class Tagrelatedbanner
    {
        public string id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string thumbnailURL { get; set; }
    }

    public class Context
    {
        public object playFrom { get; set; }
        public object initialPlaybackPosition { get; set; }
        public object initialPlaybackType { get; set; }
        public object playLength { get; set; }
        public object returnId { get; set; }
        public object returnTo { get; set; }
        public object returnMsg { get; set; }
        public string watchId { get; set; }
        public object isNoMovie { get; set; }
        public object isNoRelatedVideo { get; set; }
        public object isDownloadCompleteWait { get; set; }
        public object isNoNicotic { get; set; }
        public bool? isNeedPayment { get; set; }
        public bool? isAdultRatingNG { get; set; }
        public object isPlayable { get; set; }
        public bool? isTranslatable { get; set; }
        public bool? isTagUneditable { get; set; }
        public bool? isVideoOwner { get; set; }
        public bool? isThreadOwner { get; set; }
        public object isOwnerThreadEditable { get; set; }
        public string useChecklistCache { get; set; }
        public object isDisabledMarquee { get; set; }
        public bool? isDictionaryDisplayable { get; set; }
        public bool? isDefaultCommentInvisible { get; set; }
        public object accessFrom { get; set; }
        public string csrfToken { get; set; }
        public int? translationVersionJsonUpdateTime { get; set; }
        public string userkey { get; set; }
        public string watchAuthKey { get; set; }
        public string watchTrackId { get; set; }
        public long watchPageServerTime { get; set; }
        public bool? isAuthenticationRequired { get; set; }
        public bool? isPeakTime { get; set; }
        public int? ngRevision { get; set; }
        public string categoryName { get; set; }
        public string categoryKey { get; set; }
        public string categoryGroupName { get; set; }
        public string categoryGroupKey { get; set; }
        public int? yesterdayRank { get; set; }
        public int? highestRank { get; set; }
        public bool? isMyMemory { get; set; }
        public Ownernglist[] ownerNGList { get; set; }
    }

    public class Ownernglist
    {
        public string source { get; set; }
        public string destination { get; set; }
    }

    public class Livetopics
    {
        public Item1[] items { get; set; }
    }

    public class Item1
    {
        public string id { get; set; }
        public string title { get; set; }
        public string thumbnailURL { get; set; }
        public int? point { get; set; }
        public bool? isHigh { get; set; }
        public int? elapsedTimeM { get; set; }
        public string communityId { get; set; }
        public string communityName { get; set; }
    }

    public class Tag
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool? isCategory { get; set; }
        public object isCategoryCandidate { get; set; }
        public bool? isDictionaryExists { get; set; }
        public bool? isLocked { get; set; }
    }
}
