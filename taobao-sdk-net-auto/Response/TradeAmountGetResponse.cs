using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeAmountGetResponse.
    /// </summary>
    public class TradeAmountGetResponse : TopResponse
    {
        /// <summary>
        /// 主订单的财务信息详情
        /// </summary>
        [XmlElement("trade_amount")]
        public Top.Api.Domain.TradeAmount TradeAmount { get; set; }

    }
}
