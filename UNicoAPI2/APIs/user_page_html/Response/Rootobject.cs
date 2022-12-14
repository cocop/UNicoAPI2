namespace UNicoAPI2.APIs.user_page_html.Response
{

    public class Rootobject
    {
        public int frontendId { get; set; }
        public string frontendVersion { get; set; }
        public Baseurl baseUrl { get; set; }
        public Cssurl cssUrl { get; set; }
        public string pageType { get; set; }
        public int userId { get; set; }
        public Viewer viewer { get; set; }
        public string language { get; set; }
        public string locale { get; set; }
        public object csrfToken { get; set; }
    }

    public class Baseurl
    {
        public string video { get; set; }
        public string uni { get; set; }
        public string uniRes { get; set; }
        public string nvapi { get; set; }
        public string nicoaccount { get; set; }
        public string secure { get; set; }
        public string premium { get; set; }
        public string point { get; set; }
        public string commons { get; set; }
        public string live { get; set; }
        public string live2 { get; set; }
        public string com { get; set; }
        public string seiga { get; set; }
        public string nico3d { get; set; }
        public string app { get; set; }
        public string appicon { get; set; }
        public string game { get; set; }
        public string niconare { get; set; }
        public string publicAPI { get; set; }
        public string follo { get; set; }
        public string nicoad { get; set; }
        public string nicocas { get; set; }
        public string site { get; set; }
        public string smileUpload { get; set; }
        public string ext { get; set; }
        public string dic { get; set; }
        public string channel { get; set; }
        public string q { get; set; }
        public string qa { get; set; }
        public string oshiraseBox { get; set; }
        public string dcdn { get; set; }
        public string koken { get; set; }
        public string wktk { get; set; }
        public string commonMuteAPI { get; set; }
        public string creatorSupport { get; set; }
        public string income { get; set; }
        public string gift { get; set; }
    }

    public class Cssurl
    {
        public string Nicorepo { get; set; }
        public string CreatorSupport { get; set; }
        public string Follow { get; set; }
        public string WatchLater { get; set; }
        public string Mylist { get; set; }
        public string Video { get; set; }
        public string Badge { get; set; }
        public string History { get; set; }
    }

    public class Viewer
    {
        public string type { get; set; }
        public bool isPremium { get; set; }
        public string description { get; set; }
        public string strippedDescription { get; set; }
        public string shortDescription { get; set; }
        public int id { get; set; }
        public string nickname { get; set; }
        public Icons icons { get; set; }
    }

    public class Icons
    {
        public string small { get; set; }
        public string large { get; set; }
    }
}
