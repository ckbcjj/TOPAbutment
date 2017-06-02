using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeCloseResponse.
    /// </summary>
    public class TradeCloseResponse : TopResponse
    {
        /// <summary>
        /// 关闭交易时返回的Trade信息，可用字段有tid和modified
        /// </summary>
        [XmlElement("trade")]
        public Top.Api.Domain.Trade Trade { get; set; }

    }
}
