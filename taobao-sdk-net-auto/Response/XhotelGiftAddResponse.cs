using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelGiftAddResponse.
    /// </summary>
    public class XhotelGiftAddResponse : TopResponse
    {
        /// <summary>
        /// 礼包id
        /// </summary>
        [XmlElement("gift_id")]
        public long GiftId { get; set; }

    }
}
