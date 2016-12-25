using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.VideoService
{
    /******************************************/
    /// <summary>画像データ</summary>
    /******************************************/
    public class Picture
    {
        /// <summary>画像データのURL</summary>
        public string Url { get; private set; }

        CookieContainer cookieContainer;

        /// <summary>自動生成される画像データ</summary>
        /// <param name="Url">画像データのURL</param>
        /// <param name="CookieContainer">クッキーを持っているClient</param>
        internal Picture(string Url, CookieContainer CookieContainer)
        {
            this.Url = Url;
            cookieContainer = CookieContainer;
        }

        /// <summary>画像ダウンロード用ストリームの取得</summary>
        public Task<WebResponse> GetStreamAsync(PictureSize PictureSize = PictureSize.None)
        {
            var request = (HttpWebRequest)WebRequest.Create(Url + PictureSize.ToKey());

            request.Method = ContentMethod.Get;
            request.CookieContainer = cookieContainer;

            return request.GetResponseAsync();
        }
    }
}
