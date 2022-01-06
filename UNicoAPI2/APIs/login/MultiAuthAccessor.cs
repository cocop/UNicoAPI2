using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.login
{
    public class MultiAuthAccessor : IAccessor
    {
        public AccessorType Type
        {
            get
            {
                return AccessorType.Upload;
            }
        }

        CookieContainer cookieContainer;
        string csrf_token = "";
        string otp = "";
        string device_name = "";
        bool is_mfa_trusted_device = true;

        HttpWebRequest request;

        public MultiAuthAccessor(CookieContainer CookieContainer, string csrf_token, string otp, string device_name, bool is_mfa_trusted_device)
        {
            cookieContainer = CookieContainer;
            this.csrf_token = csrf_token;
            this.otp = otp;
            this.device_name = device_name;
            this.is_mfa_trusted_device = is_mfa_trusted_device;

            if (device_name.Length >= 64)
            {
                throw new Exception();
            }
        }

        public byte[] GetUploadData()
        {
            return Encoding.UTF8.GetBytes(
                "otp=" + otp +
                "&loginBtn=" + Uri.EscapeDataString("ログイン") +
                "&device_name=" + Uri.EscapeDataString(device_name) +
                (is_mfa_trusted_device ? "&is_mfa_trusted_device=true" : ""));
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create(
                "https://account.nicovideo.jp/mfa?site=niconico&continue=" +
                Uri.EscapeDataString($"https://account.nicovideo.jp/login/mfa/callback?site=niconico&sec=header_pc&next_url=%2F&csrf_token={this.csrf_token}&facebook=1&twitter=1"));

            request.Method = ContentMethod.Post;
            request.ContentType = ContentType.Form;
            request.Headers["Cache-Control"] = "max-age=0";
            request.Headers["Content-Length"] = DataLength.ToString();
            request.CookieContainer = cookieContainer;

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
