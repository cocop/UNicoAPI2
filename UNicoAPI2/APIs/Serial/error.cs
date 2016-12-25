using System.Xml.Serialization;

namespace UNicoAPI2.APIs.Serial
{
    /// <summary>
    /// エラーコード
    /// </summary>
    public class error
    {
        /// <summary>
        /// エラーコード
        /// </summary>
        [XmlElement]
        public string code;

        /// <summary>
        /// エラーの説明
        /// </summary>
        [XmlElement]
        public string description;
    }
}
