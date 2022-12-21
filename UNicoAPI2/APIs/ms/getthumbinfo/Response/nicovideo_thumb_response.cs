using System.Xml.Serialization;
using UNicoAPI2.APIs.Response;

namespace UNicoAPI2.APIs.ms.getthumbinfo.Response
{
    /******************************************/
    /// <summary>
    /// 動画情報レスポンス
    /// </summary>
    /******************************************/
    [XmlRoot]
    public class nicovideo_thumb_response
    {
        /// <summary>
        /// 動画情報
        /// </summary>
        [XmlElement]
        public thumb thumb;

        /// <summary>
        /// エラーコード
        /// </summary>
        [XmlElement]
        public error error;

        /// <summary>
        /// 成功か失敗か
        /// </summary>
        [XmlAttribute]
        public string status;
    }
}
