using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// RefundGetResponse.
    /// </summary>
    public class RefundGetResponse : TopResponse
    {
        /// <summary>
        /// 退款详情
        /// </summary>
        [XmlElement("refund")]
        public Top.Api.Domain.Refund Refund { get; set; }

    }
}
