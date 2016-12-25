using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.search
{
    public class Accesser : IAccesser
    {
        public AccesserType Type
        {
            get
            {
                return AccesserType.Download;
            }
        }

        CookieContainer cookieContainer;
        string type = "";
        string word = "";
        string page = "";
        string order = "";
        string sort = "";

        public void Setting(CookieContainer CookieContainer, string type, string word, string page, string order, string sort)
        {
            cookieContainer = CookieContainer;
            this.type = type;
            this.word = word;
            this.page = page;
            this.order = order;
            this.sort = sort;
        }

        public byte[] GetUploadData()
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetUploadStreamAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                "http://ext.nicovideo.jp/api/search/" + type
                    + "/" + word
                    + "?mode&page=" + page
                    + "&order=" + order
                    + "&sort=" + sort);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
