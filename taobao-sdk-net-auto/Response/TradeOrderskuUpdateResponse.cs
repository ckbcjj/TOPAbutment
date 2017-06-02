using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeOrderskuUpdateResponse.
    /// </summary>
    public class TradeOrderskuUpdateResponse : TopResponse
    {
        /// <summary>
        /// 只返回oid和modified
        /// </summary>
        [XmlElement("order")]
        public Top.Api.Domain.Order Order { get; set; }

    }
}
