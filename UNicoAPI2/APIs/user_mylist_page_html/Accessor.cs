﻿using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.user_mylist_page_html
{
    public class Accessor : IAccessor
    {
        public CookieContainer CookieContainer { get; set; }
        public string UserId { get; set; }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(
                $"http://www.nicovideo.jp/user/{UserId}/mylist");

            request.Method = ContentMethod.Get;
            request.CookieContainer = CookieContainer;

            return request.GetResponseAsync();
        }
    }
}
