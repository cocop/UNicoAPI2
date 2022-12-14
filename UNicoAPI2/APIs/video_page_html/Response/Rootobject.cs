
using System.Runtime.Serialization;

namespace UNicoAPI2.APIs.video_page_html.Response
{

    public class Rootobject
    {
        public Ads ads { get; set; }
        public object category { get; set; }
        public object channel { get; set; }
        public Client client { get; set; }
        public Comment comment { get; set; }
        public object community { get; set; }
        public Easycomment easyComment { get; set; }
        public External external { get; set; }
        public Genre genre { get; set; }
        public Marquee marquee { get; set; }
        public Media media { get; set; }
        public string okReason { get; set; }
        public Owner owner { get; set; }
        public Pcwatchpage pcWatchPage { get; set; }
        public Player player { get; set; }
        public object ppv { get; set; }
        public Ranking ranking { get; set; }
        public Series series { get; set; }
        public object smartphone { get; set; }
        public System system { get; set; }
        public Tag tag { get; set; }
        public Video video { get; set; }
        public Videoads videoAds { get; set; }
        public Viewer viewer { get; set; }
        public Waku waku { get; set; }

        public class Ads
        {
            public bool isAvailable { get; set; }
        }

        public class Client
        {
            public string nicosid { get; set; }
            public string watchId { get; set; }
            public string watchTrackId { get; set; }
        }

        public class Comment
        {
            public Server server { get; set; }
            public Keys keys { get; set; }
            public Layer[] layers { get; set; }
            public Thread[] threads { get; set; }
            public Ng ng { get; set; }
            public bool isAttentionRequired { get; set; }
            public Nvcomment nvComment { get; set; }

            public class Server
            {
                public string url { get; set; }
            }

            public class Keys
            {
                public string userKey { get; set; }
            }

            public class Layer
            {
                public int index { get; set; }
                public bool isTranslucent { get; set; }
                public Threadid[] threadIds { get; set; }

                public class Threadid
                {
                    public int id { get; set; }
                    public int fork { get; set; }
                }
            }

            public class Thread
            {
                public int id { get; set; }
                public int fork { get; set; }
                public string forkLabel { get; set; }
                public bool isActive { get; set; }
                public bool isDefaultPostTarget { get; set; }
                public bool isEasyCommentPostTarget { get; set; }
                public bool isLeafRequired { get; set; }
                public bool isOwnerThread { get; set; }
                public bool isThreadkeyRequired { get; set; }
                public object threadkey { get; set; }
                public bool is184Forced { get; set; }
                public bool hasNicoscript { get; set; }
                public string label { get; set; }
                public int postkeyStatus { get; set; }
                public string server { get; set; }
            }

            public class Ng
            {
                public Ngscore ngScore { get; set; }
                public object[] channel { get; set; }
                public object[] owner { get; set; }
                public Viewer viewer { get; set; }

                public class Ngscore
                {
                    public bool isDisabled { get; set; }
                }

                public class Viewer
                {
                    public int revision { get; set; }
                    public int count { get; set; }
                    public Item[] items { get; set; }

                    public class Item
                    {
                        public string type { get; set; }
                        public string source { get; set; }
                        public string registeredAt { get; set; }
                    }
                }

            }

            [DataContract]
            public class Nvcomment
            {
                [DataMember]
                public string threadKey { get; set; }
                [DataMember]
                public string server { get; set; }
                [DataMember(Name = "params")]
                public Params _params { get; set; }

                public class Params
                {
                    public Target[] targets { get; set; }
                    public string language { get; set; }
                }

                public class Target
                {
                    public string id { get; set; }
                    public string fork { get; set; }
                }
            }
        }

        public class Easycomment
        {
            public Phras[] phrases { get; set; }

            public class Phras
            {
                public string text { get; set; }
                public Nicodic nicodic { get; set; }

                public class Nicodic
                {
                    public string title { get; set; }
                    public string viewTitle { get; set; }
                    public string summary { get; set; }
                    public string link { get; set; }
                }
            }
        }

        public class External
        {
            public Commons commons { get; set; }
            public Ichiba ichiba { get; set; }

            public class Commons
            {
                public bool hasContentTree { get; set; }
            }

            public class Ichiba
            {
                public bool isEnabled { get; set; }
            }
        }

        public class Genre
        {
            public string key { get; set; }
            public string label { get; set; }
            public bool isImmoral { get; set; }
            public bool isDisabled { get; set; }
            public bool isNotSet { get; set; }
        }

        public class Marquee
        {
            public bool isDisabled { get; set; }
            public object tagRelatedLead { get; set; }
        }

        public class Media
        {
            public Delivery delivery { get; set; }
            public object deliveryLegacy { get; set; }

            public class Delivery
            {
                public string recipeId { get; set; }
                public object encryption { get; set; }
                public Movie movie { get; set; }
                public object storyboard { get; set; }
                public string trackingId { get; set; }

                public class Movie
                {
                    public string contentId { get; set; }
                    public Audio[] audios { get; set; }
                    public Video[] videos { get; set; }
                    public Session session { get; set; }

                    public class Session
                    {
                        public string recipeId { get; set; }
                        public string playerId { get; set; }
                        public string[] videos { get; set; }
                        public string[] audios { get; set; }
                        public object[] movies { get; set; }
                        public string[] protocols { get; set; }
                        public Authtypes authTypes { get; set; }
                        public string serviceUserId { get; set; }
                        public string token { get; set; }
                        public string signature { get; set; }
                        public string contentId { get; set; }
                        public int heartbeatLifetime { get; set; }
                        public int contentKeyTimeout { get; set; }
                        public float priority { get; set; }
                        public object[] transferPresets { get; set; }
                        public Url[] urls { get; set; }

                        public class Authtypes
                        {
                            public string http { get; set; }
                            public string hls { get; set; }
                        }

                        public class Url
                        {
                            public string url { get; set; }
                            public bool isWellKnownPort { get; set; }
                            public bool isSsl { get; set; }
                        }
                    }

                    public class Audio
                    {
                        public string id { get; set; }
                        public bool isAvailable { get; set; }
                        public Metadata metadata { get; set; }

                        public class Metadata
                        {
                            public int bitrate { get; set; }
                            public int samplingRate { get; set; }
                            public Loudness loudness { get; set; }
                            public int levelIndex { get; set; }
                            public Loudnesscollection[] loudnessCollection { get; set; }

                            public class Loudness
                            {
                                public float integratedLoudness { get; set; }
                                public float truePeak { get; set; }
                            }

                            public class Loudnesscollection
                            {
                                public string type { get; set; }
                                public float value { get; set; }
                            }
                        }
                    }

                    public class Video
                    {
                        public string id { get; set; }
                        public bool isAvailable { get; set; }
                        public Metadata metadata { get; set; }

                        public class Metadata
                        {
                            public string label { get; set; }
                            public int bitrate { get; set; }
                            public Resolution resolution { get; set; }
                            public int levelIndex { get; set; }
                            public int recommendedHighestAudioLevelIndex { get; set; }

                            public class Resolution
                            {
                                public int width { get; set; }
                                public int height { get; set; }
                            }
                        }
                    }
                }
            }

        }

        public class Owner
        {
            public int id { get; set; }
            public string nickname { get; set; }
            public string iconUrl { get; set; }
            public object channel { get; set; }
            public object live { get; set; }
            public bool isVideosPublic { get; set; }
            public bool isMylistsPublic { get; set; }
            public Viewer viewer { get; set; }

            public class Viewer
            {
                public bool isFollowing { get; set; }
            }
        }

        public class Pcwatchpage
        {
            public object tagRelatedBanner { get; set; }
            public Videoend videoEnd { get; set; }
            public bool showOwnerMenu { get; set; }
            public bool showOwnerThreadCoEditingLink { get; set; }
            public bool showMymemoryEditingLink { get; set; }

            public class Videoend
            {
                public object bannerIn { get; set; }
                public object overlay { get; set; }
            }
        }

        public class Player
        {
            public Initialplayback initialPlayback { get; set; }
            public Comment comment { get; set; }
            public int layerMode { get; set; }

            public class Initialplayback
            {
                public string type { get; set; }
                public object positionSec { get; set; }
            }

            public class Comment
            {
                public bool isDefaultInvisible { get; set; }
            }
        }

        public class Ranking
        {
            public object genre { get; set; }
            public object[] popularTag { get; set; }
        }

        public class Series
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string thumbnailUrl { get; set; }
            public Video video { get; set; }

            public class Video
            {
                public Item prev { get; set; }
                public Item next { get; set; }
                public Item first { get; set; }

                public class Item
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
                    public object playbackPosition { get; set; }
                    public Owner owner { get; set; }
                    public bool requireSensitiveMasking { get; set; }

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

                    public class Owner
                    {
                        public string ownerType { get; set; }
                        public string id { get; set; }
                        public string name { get; set; }
                        public string iconUrl { get; set; }
                    }
                }
            }
        }

        public class System
        {
            public string serverTime { get; set; }
            public bool isPeakTime { get; set; }
        }

        public class Tag
        {
            public Item[] items { get; set; }
            public bool hasR18Tag { get; set; }
            public bool isPublishedNicoscript { get; set; }
            public Edit edit { get; set; }
            public Viewer viewer { get; set; }

            public class Item
            {
                public string name { get; set; }
                public bool isCategory { get; set; }
                public bool isCategoryCandidate { get; set; }
                public bool isNicodicArticleExists { get; set; }
                public bool isLocked { get; set; }
            }

            public class Edit
            {
                public bool isEditable { get; set; }
                public object uneditableReason { get; set; }
                public string editKey { get; set; }
            }

            public class Viewer
            {
                public bool isEditable { get; set; }
                public object uneditableReason { get; set; }
                public string editKey { get; set; }
            }
        }

        public class Video
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public Count count { get; set; }
            public int duration { get; set; }
            public Thumbnail thumbnail { get; set; }
            public Rating rating { get; set; }
            public string registeredAt { get; set; }
            public bool isPrivate { get; set; }
            public bool isDeleted { get; set; }
            public bool isNoBanner { get; set; }
            public bool isAuthenticationRequired { get; set; }
            public bool isEmbedPlayerAllowed { get; set; }
            public Viewer viewer { get; set; }
            public string watchableUserTypeForPayment { get; set; }
            public string commentableUserTypeForPayment { get; set; }


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
                public string player { get; set; }
                public string ogp { get; set; }
            }

            public class Rating
            {
                public bool isAdult { get; set; }
            }

            public class Viewer
            {
                public bool isOwner { get; set; }
                public Like like { get; set; }

                public class Like
                {
                    public bool isLiked { get; set; }
                    public object count { get; set; }
                }
            }
        }

        public class Videoads
        {
            public Additionalparams additionalParams { get; set; }
            public Item[] items { get; set; }
            public string reason { get; set; }

            public class Additionalparams
            {
                public string videoId { get; set; }
                public int videoDuration { get; set; }
                public bool isAdultRatingNG { get; set; }
                public bool isAuthenticationRequired { get; set; }
                public bool isR18 { get; set; }
                public string nicosid { get; set; }
                public string lang { get; set; }
                public string watchTrackId { get; set; }
                public string genre { get; set; }
                public string gender { get; set; }
                public int age { get; set; }
            }

            public class Item
            {
                public string type { get; set; }
                public object timingMs { get; set; }
                public Additionalparams additionalParams { get; set; }

                public class Additionalparams
                {
                    public string linearType { get; set; }
                    public int adIdx { get; set; }
                    public int skipType { get; set; }
                    public int skippableType { get; set; }
                    public int pod { get; set; }
                }
            }
        }

        public class Viewer
        {
            public int id { get; set; }
            public string nickname { get; set; }
            public bool isPremium { get; set; }
            public Existence existence { get; set; }

            public class Existence
            {
                public int age { get; set; }
                public string prefecture { get; set; }
                public string sex { get; set; }
            }
        }

        public class Waku
        {
            public object information { get; set; }
            public object[] bgImages { get; set; }
            public object addContents { get; set; }
            public object addVideo { get; set; }
            public object tagRelatedBanner { get; set; }
            public object tagRelatedMarquee { get; set; }
        }
    }
}