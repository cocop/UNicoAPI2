using System.Net;

namespace UNicoAPI2
{
    /******************************************/
    /// <summary>
    /// 受け渡しオブジェクト
    /// </summary>
    /******************************************/
    public class Context
    {
        /// <summary>
        /// クライアント通信処理
        /// </summary>
        public CookieContainer CookieContainer = new CookieContainer();

        /// <summary>
        /// インスタンスの管理
        /// </summary>
        public IDContainer IDContainer = new IDContainer();
    }
}