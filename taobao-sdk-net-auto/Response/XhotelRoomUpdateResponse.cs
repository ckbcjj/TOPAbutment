using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRoomUpdateResponse.
    /// </summary>
    public class XhotelRoomUpdateResponse : TopResponse
    {
        /// <summary>
        /// gid酒店商品id
        /// </summary>
        [XmlElement("gid")]
        public long Gid { get; set; }

    }
}
