using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeFullinfoGetResponse.
    /// </summary>
    public class TradeFullinfoGetResponse : TopResponse
    {
        /// <summary>
        /// 交易主订单信息
        /// </summary>
        [XmlElement("trade")]
        public Top.Api.Domain.Trade Trade { get; set; }

    }
}
