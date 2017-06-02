using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// XRoomIds Data Structure.
    /// </summary>
    [Serializable]
    public class XRoomIds : TopObject
    {
        /// <summary>
        /// 宝贝的gid
        /// </summary>
        [XmlElement("gid")]
        public long Gid { get; set; }

        /// <summary>
        /// 宝贝对应的酒店ID
        /// </summary>
        [XmlElement("hid")]
        public long Hid { get; set; }

        /// <summary>
        /// 宝贝对应的iid
        /// </summary>
        [XmlElement("iid")]
        public long Iid { get; set; }

        /// <summary>
        /// 宝贝对应的roomId
        /// </summary>
        [XmlElement("rid")]
        public long Rid { get; set; }

        /// <summary>
        /// 宝贝对应的上下架状态，1为上架，2为下架
        /// </summary>
        [XmlElement("status")]
        public long Status { get; set; }

        /// <summary>
        /// 宝贝对应的中文名称
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }
    }
}
