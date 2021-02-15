namespace UNicoAPI2.APIs.user_page_html.Response
{

    public class Rootobject
    {
        public Userdetails userDetails { get; set; }

        public class Userdetails
        {
            public Userdetails1 userDetails { get; set; }

            public class Userdetails1
            {
                public string type { get; set; }
                public User user { get; set; }

                public class User
                {
                    public string description { get; set; }
                    public string strippedDescription { get; set; }
                    public bool isPremium { get; set; }
                    public string registeredVersion { get; set; }
                    public int followeeCount { get; set; }
                    public int followerCount { get; set; }
                    public Userlevel userLevel { get; set; }
                    public int niconicoPoint { get; set; }
                    public string language { get; set; }
                    public object premiumTicketExpireTime { get; set; }
                    public int creatorPatronizingScore { get; set; }
                    public object userChannel { get; set; }
                    public bool isMailBounced { get; set; }
                    public bool isNicorepoReadable { get; set; }
                    public object[] sns { get; set; }
                    public string id { get; set; }
                    public string nickname { get; set; }
                    public Icons icons { get; set; }

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
                }
            }
        }
    }
}
