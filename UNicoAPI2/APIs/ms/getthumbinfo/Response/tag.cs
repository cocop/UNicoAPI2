using System.Xml.Serialization;

namespace UNicoAPI2.APIs.ms.getthumbinfo.Response
{
    /******************************************/
    /// <summary>
    /// タグ
    /// </summary>
    /******************************************/
    public class tag
    {
        /// <summary>
        /// タグ名
        /// </summary>
        [XmlText]
        public string _tag;

        /// <summary>
        /// カテゴリ
        /// </summary>
        [XmlAttribute]
        public int category;

        /// <summary>
        /// タグロック
        /// </summary>
        [XmlAttribute("lock")]
        public int _lock;
    }
}