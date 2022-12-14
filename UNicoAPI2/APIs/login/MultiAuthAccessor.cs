using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.APIs.login
{
    public class MultiAuthAccessor : IAccessorWithUploadData
    {
        public CookieContainer CookieContainer { get; set; }
        public string CsrfToken { get; set; }
        public string Otp { get; set; }
        string deviceName;
        /// <summary>
        /// 上限64文字
        /// </summary>
        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                if (value.Length >= 64)
                {
                    throw new ArgumentOutOfRangeException("64文字以上の DeviceName を指定出来ません");
                }
                deviceName = value;
            }
        }
        public bool IsMfaTrustedDevice { get; set; }

        HttpWebRequest request;

        public byte[] GetUploadData()
        {
            return Encoding.UTF8.GetBytes(
                "otp=" + Otp +
                "&loginBtn=" + Uri.EscapeDataString("ログイン") +
                "&device_name=" + Uri.EscapeDataString(DeviceName) +
                (IsMfaTrustedDevice ? "&is_mfa_trusted_device=true" : ""));
        }

        public Task<Stream> GetUploadStreamAsync(int DataLength)
        {
            request = (HttpWebRequest)WebRequest.Create(
                "https://account.nicovideo.jp/mfa?site=niconico&continue=" +
                Uri.EscapeDataString($"https://account.nicovideo.jp/login/mfa/callback?site=niconico&sec=header_pc&next_url=/my&csrf_token={CsrfToken}&facebook=1&twitter=1"));

            request.Method = ContentMethod.Post;
            request.ContentType = ContentType.Form;
            request.Headers["Cache-Control"] = "max-age=0";
            request.Headers["Content-Length"] = DataLength.ToString();
            request.CookieContainer = CookieContainer;

            return request.GetRequestStreamAsync();
        }

        public Task<WebResponse> GetDownloadStreamAsync()
        {
            return request.GetResponseAsync();
        }
    }
}
