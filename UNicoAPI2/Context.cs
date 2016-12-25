using System;
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
        /// クッキーコンテナ
        /// </summary>
        public CookieContainer CookieContainer { get; set; } = new CookieContainer();

        /// <summary>
        /// インスタンスの管理
        /// </summary>
        public IDContainer IDContainer { get; set; } = new IDContainer();

        /// <summary>
        /// キャッシュの有効期限
        /// </summary>
        public TimeSpan CacheDeadline
        {
            get
            {
                return Cache.DefaultDeadline;
            }

            set
            {
                Cache.DefaultDeadline = value;
            }
        }
    }
}