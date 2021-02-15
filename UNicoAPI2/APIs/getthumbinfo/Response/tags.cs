using System.Xml.Serialization;

namespace UNicoAPI2.APIs.getthumbinfo.Response
{
    /******************************************/
    /// <summary>タグリスト</summary>
    /******************************************/
    public class tags
    {
        /// <summary>言語</summary>
        [XmlElement]
        public string domain;

        /// <summary>タグ</summary>
        [XmlElement]
        public tag[] tag;
    }
}