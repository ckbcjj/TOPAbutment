using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRoomtypeAddResponse.
    /// </summary>
    public class XhotelRoomtypeAddResponse : TopResponse
    {
        /// <summary>
        /// 房型信息
        /// </summary>
        [XmlElement("xroomtype")]
        public Top.Api.Domain.XRoomType Xroomtype { get; set; }

    }
}
