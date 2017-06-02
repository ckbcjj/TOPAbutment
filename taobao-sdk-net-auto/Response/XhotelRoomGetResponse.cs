using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRoomGetResponse.
    /// </summary>
    public class XhotelRoomGetResponse : TopResponse
    {
        /// <summary>
        /// 房间信息
        /// </summary>
        [XmlElement("room")]
        public Top.Api.Domain.XRoom Room { get; set; }

    }
}
