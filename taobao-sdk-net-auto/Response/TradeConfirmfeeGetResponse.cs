using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeConfirmfeeGetResponse.
    /// </summary>
    public class TradeConfirmfeeGetResponse : TopResponse
    {
        /// <summary>
        /// 获取到的交易确认收货费用
        /// </summary>
        [XmlElement("trade_confirm_fee")]
        public Top.Api.Domain.TradeConfirmFee TradeConfirmFee { get; set; }

    }
}
