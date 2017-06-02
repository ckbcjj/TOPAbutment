using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRoomtypeGetResponse.
    /// </summary>
    public class XhotelRoomtypeGetResponse : TopResponse
    {
        /// <summary>
        /// 查询得到的RoomType
        /// </summary>
        [XmlElement("xroomtype")]
        public Top.Api.Domain.XRoomType Xroomtype { get; set; }

    }
}
