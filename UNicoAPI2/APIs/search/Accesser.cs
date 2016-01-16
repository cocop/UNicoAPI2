using System;
using System.IO;
using System.Net;
using System.Text;
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
        string f_range = "";
        string l_range = "";

        public void Setting(CookieContainer CookieContainer, string type, string word, string page, string order, string sort, string f_range, string l_range)
        {
            cookieContainer = CookieContainer;
            this.type = type;
            this.word = word;
            this.page = page;
            this.order = order;
            this.sort = sort;
            this.f_range = f_range;
            this.l_range = l_range;
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
                    + "&sort=" + sort
                    + "&f_range=" + f_range
                    + "&l_range=" + l_range);

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
