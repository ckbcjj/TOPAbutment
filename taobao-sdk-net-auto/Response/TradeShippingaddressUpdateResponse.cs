using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeShippingaddressUpdateResponse.
    /// </summary>
    public class TradeShippingaddressUpdateResponse : TopResponse
    {
        /// <summary>
        /// 交易结构
        /// </summary>
        [XmlElement("trade")]
        public Top.Api.Domain.Trade Trade { get; set; }

    }
}
