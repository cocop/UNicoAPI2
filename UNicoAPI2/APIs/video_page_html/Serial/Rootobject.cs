namespace UNicoAPI2.APIs.video_page_html.Serial
{

    public class Rootobject
    {
        public Video video { get; set; }
        public Player player { get; set; }
        public Thread thread { get; set; }
        public Commentcomposite commentComposite { get; set; }
        public Tag[] tags { get; set; }
        public Playlist playlist { get; set; }
        public Owner owner { get; set; }
        public Viewer viewer { get; set; }
        public object community { get; set; }
        public object mainCommunity { get; set; }
        public object channel { get; set; }
        public Ad ad { get; set; }
        public Lead lead { get; set; }
        public object maintenance { get; set; }
        public Context context { get; set; }
        public Livetopics liveTopics { get; set; }
        public object[] dataLayer { get; set; }
        public string watchRecommendationRecipe { get; set; }
        public Series series { get; set; }
        public object wakutkool { get; set; }

        public class Video
        {
            public string id { get; set; }
            public string title { get; set; }
            public string originalTitle { get; set; }
            public string description { get; set; }
            public string originalDescription { get; set; }
            public string thumbnailURL { get; set; }
            public string largeThumbnailURL { get; set; }
            public string postedDateTime { get; set; }
            public object originalPostedDateTime { get; set; }
            public object width { get; set; }
            public object height { get; set; }
            public int duration { get; set; }
            public int viewCount { get; set; }
            public int mylistCount { get; set; }
            public bool translation { get; set; }
            public object translator { get; set; }
            public string movieType { get; set; }
            public object badges { get; set; }
            public object mainCommunityId { get; set; }
            public Dmcinfo dmcInfo { get; set; }
            public object backCommentType { get; set; }
            public object channelId { get; set; }
            public bool isCommentExpired { get; set; }
            public object isWide { get; set; }
            public object isOfficialAnime { get; set; }
            public bool isDeleted { get; set; }
            public bool isTranslated { get; set; }
            public bool isR18 { get; set; }
            public bool isAdult { get; set; }
            public object isNicowari { get; set; }
            public bool isPublic { get; set; }
            public object isPublishedNicoscript { get; set; }
            public object isNoNGS { get; set; }
            public string isCommunityMemberOnly { get; set; }
            public object isCommonsTreeExists { get; set; }
            public bool isNoIchiba { get; set; }
            public bool isOfficial { get; set; }
            public bool isMonetized { get; set; }
            public Smileinfo smileInfo { get; set; }

            public class Dmcinfo
            {
                public int time { get; set; }
                public long time_ms { get; set; }
                public Video video { get; set; }
                public Thread thread { get; set; }
                public User user { get; set; }
                public int import_version { get; set; }
                public object error { get; set; }
                public string tracking_id { get; set; }
                public Session_Api session_api { get; set; }
                public object storyboard_session_api { get; set; }
                public Quality quality { get; set; }

                public class Video
                {
                    public string video_id { get; set; }
                    public int length_seconds { get; set; }
                    public int deleted { get; set; }
                }

                public class Thread
                {
                    public string server_url { get; set; }
                    public string sub_server_url { get; set; }
                    public int thread_id { get; set; }
                    public object nicos_thread_id { get; set; }
                    public object optional_thread_id { get; set; }
                    public bool thread_key_required { get; set; }
                    public object[] channel_ng_words { get; set; }
                    public object[] owner_ng_words { get; set; }
                    public bool maintenances_ng { get; set; }
                    public bool postkey_available { get; set; }
                    public int ng_revision { get; set; }
                }

                public class User
                {
                    public int user_id { get; set; }
                    public bool is_premium { get; set; }
                    public string nickname { get; set; }
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
                    public int heartbeat_lifetime { get; set; }
                    public int content_key_timeout { get; set; }
                    public float priority { get; set; }
                    public object[] transfer_presets { get; set; }
                    public Url[] urls { get; set; }

                    public class Auth_Types
                    {
                        public string http { get; set; }
                        public string hls { get; set; }
                    }

                    public class Url
                    {
                        public string url { get; set; }
                        public bool is_well_known_port { get; set; }
                        public bool is_ssl { get; set; }
                    }
                }

                public class Quality
                {
                    public Video[] videos { get; set; }
                    public Audio[] audios { get; set; }

                    public class Video
                    {
                        public string id { get; set; }
                        public int level_index { get; set; }
                        public bool available { get; set; }
                        public int bitrate { get; set; }
                        public Resolution resolution { get; set; }
                        public string label { get; set; }

                        public class Resolution
                        {
                            public int width { get; set; }
                            public int height { get; set; }
                        }

                    }

                    public class Audio
                    {
                        public string id { get; set; }
                        public bool available { get; set; }
                        public int bitrate { get; set; }
                        public int sampling_rate { get; set; }
                        public Loudness loudness { get; set; }
                        public Loudness_Correction_Value[] loudness_correction_value { get; set; }

                        public class Loudness
                        {
                            public float integratedLoudness { get; set; }
                            public float truePeak { get; set; }
                        }

                        public class Loudness_Correction_Value
                        {
                            public string type { get; set; }
                            public float value { get; set; }
                        }
                    }
                }
            }

            public class Smileinfo
            {
                public string url { get; set; }
                public bool isSlowLine { get; set; }
                public string currentQualityId { get; set; }
                public string[] qualityIds { get; set; }
                public Loudnesscorrectionvalue[] loudnessCorrectionValue { get; set; }

                public class Loudnesscorrectionvalue
                {
                    public string type { get; set; }
                    public float value { get; set; }
                }
            }
        }

        public class Player
        {
            public int playerInfoXMLUpdateTIme { get; set; }
            public bool isContinuous { get; set; }
        }

        public class Thread
        {
            public int commentCount { get; set; }
            public object hasOwnerThread { get; set; }
            public object mymemoryLanguage { get; set; }
            public string serverUrl { get; set; }
            public string subServerUrl { get; set; }
            public Ids ids { get; set; }

            public class Ids
            {
                public string _default { get; set; }
                public object nicos { get; set; }
                public object community { get; set; }
            }
        }

        public class Tag
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool? isCategory { get; set; }
            public bool? isCategoryCandidate { get; set; }
            public bool? isDictionaryExists { get; set; }
            public bool? isLocked { get; set; }
            public bool? isRepresentedTag { get; set; }
        }

        public class Commentcomposite
        {
            public Thread[] threads { get; set; }
            public Layer[] layers { get; set; }


            public class Thread
            {
                public int id { get; set; }
                public int fork { get; set; }
                public bool isActive { get; set; }
                public int postkeyStatus { get; set; }
                public bool isDefaultPostTarget { get; set; }
                public bool isThreadkeyRequired { get; set; }
                public bool isLeafRequired { get; set; }
                public string label { get; set; }
                public bool isOwnerThread { get; set; }
                public bool hasNicoscript { get; set; }
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
        }

        public class Playlist
        {
            public string recipe { get; set; }
        }

        public class Owner
        {
            public string id { get; set; }
            public string nickname { get; set; }
            public string iconURL { get; set; }
            public object nicoliveInfo { get; set; }
            public object channelInfo { get; set; }
            public bool isUserVideoPublic { get; set; }
            public bool isUserMyVideoPublic { get; set; }
            public bool isUserOpenListPublic { get; set; }
        }

        public class Viewer
        {
            public int id { get; set; }
            public string nickname { get; set; }
            public int prefecture { get; set; }
            public int sex { get; set; }
            public int age { get; set; }
            public bool isPremium { get; set; }
            public bool isPostLocked { get; set; }
            public bool isHtrzm { get; set; }
            public bool isTwitterConnection { get; set; }
            public bool isBicentennial { get; set; }
            public string nicosid { get; set; }
        }

        public class Ad
        {
            public Vastmetadata[] vastMetaData { get; set; }
            public string vastReason { get; set; }

            public class Vastmetadata
            {
                public int id { get; set; }
                public int adIdx { get; set; }
                public string type { get; set; }
                public object timingMS { get; set; }
                public int skippableType { get; set; }
                public int pod { get; set; }
            }
        }

        public class Lead
        {
            public object tagRelatedMarquee { get; set; }
            public object tagRelatedBanner { get; set; }
            public object videoEndBannerIn { get; set; }
            public object videoEndOverlay { get; set; }
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
            public string isNoRelatedVideo { get; set; }
            public object isDownloadCompleteWait { get; set; }
            public object isNoNicotic { get; set; }
            public bool isNeedPayment { get; set; }
            public bool isNeedAdmission { get; set; }
            public bool isPPV { get; set; }
            public bool isPremiumOnly { get; set; }
            public bool isAdultRatingNG { get; set; }
            public object isPlayable { get; set; }
            public bool isTranslatable { get; set; }
            public bool isTagUneditable { get; set; }
            public object tagUneditableReason { get; set; }
            public bool isVideoOwner { get; set; }
            public bool isThreadOwner { get; set; }
            public bool userCanShowEditorMenu { get; set; }
            public object isOwnerThreadEditable { get; set; }
            public object useChecklistCache { get; set; }
            public object isDisabledMarquee { get; set; }
            public bool isDictionaryDisplayable { get; set; }
            public bool isDefaultCommentInvisible { get; set; }
            public object accessFrom { get; set; }
            public string csrfToken { get; set; }
            public int translationVersionJsonUpdateTime { get; set; }
            public string userkey { get; set; }
            public string watchAuthKey { get; set; }
            public string watchTrackId { get; set; }
            public long watchPageServerTime { get; set; }
            public bool isAuthenticationRequired { get; set; }
            public object isPeakTime { get; set; }
            public int ngRevision { get; set; }
            public object yesterdayRank { get; set; }
            public object highestRank { get; set; }
            public bool isMyMemory { get; set; }
            public Ownernglist[] ownerNGList { get; set; }
            public object linkedChannelVideos { get; set; }
            public bool isAllowEmbedPlayer { get; set; }
            public string genreName { get; set; }
            public string genreKey { get; set; }
            public string[] representedTags { get; set; }
            public object[] highestRepresentedTagRanking { get; set; }
            public object highestGenreRanking { get; set; }

            public class Ownernglist
            {
                public string source { get; set; }
                public string destination { get; set; }
            }
        }

        public class Livetopics
        {
            public Item[] items { get; set; }

            public class Item
            {
                public string id { get; set; }
                public string title { get; set; }
                public string thumbnailURL { get; set; }
                public int point { get; set; }
                public bool isHigh { get; set; }
                public int elapsedTimeM { get; set; }
                public string communityId { get; set; }
                public string communityName { get; set; }
            }
        }

        public class Series
        {
            public int id { get; set; }
            public string title { get; set; }
            public string thumbnailUrl { get; set; }
            public string createdAt { get; set; }
            public string updatedAt { get; set; }
            public Video prevVideo { get; set; }
            public Video nextVideo { get; set; }
            public Video firstVideo { get; set; }

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
                public object playbackPosition { get; set; }
                public Owner owner { get; set; }

                public class Count
                {
                    public int view { get; set; }
                    public int comment { get; set; }
                    public int mylist { get; set; }
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
}
