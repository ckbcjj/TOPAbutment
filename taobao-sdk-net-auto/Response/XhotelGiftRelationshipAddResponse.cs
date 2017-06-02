using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelGiftRelationshipAddResponse.
    /// </summary>
    public class XhotelGiftRelationshipAddResponse : TopResponse
    {
        /// <summary>
        /// gift_id
        /// </summary>
        [XmlElement("gift_id")]
        public long GiftId { get; set; }

    }
}
