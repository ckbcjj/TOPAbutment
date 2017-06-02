using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// RefundMessageAddResponse.
    /// </summary>
    public class RefundMessageAddResponse : TopResponse
    {
        /// <summary>
        /// 退款信息。包含id和created
        /// </summary>
        [XmlElement("refund_message")]
        public Top.Api.Domain.RefundMessage RefundMessage { get; set; }

    }
}
