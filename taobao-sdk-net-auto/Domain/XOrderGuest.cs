using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// XOrderGuest Data Structure.
    /// </summary>
    [Serializable]
    public class XOrderGuest : TopObject
    {
        /// <summary>
        /// 入住人姓名
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 入住人序号
        /// </summary>
        [XmlElement("person_pos")]
        public long PersonPos { get; set; }

        /// <summary>
        /// 房间序号
        /// </summary>
        [XmlElement("room_pos")]
        public long RoomPos { get; set; }
    }
}
