using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.search_page_html
{
    public class Accessor : IAccessor
    {
        public AccessorType Type
        {
            get
            {
                return AccessorType.Download;
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

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            throw new NotImplementedException();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                "https://www.nicovideo.jp/" + type
                + "/" + word
                + "?page=" + page
                + "&sort=" + sort
                + "&order=" + order);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
