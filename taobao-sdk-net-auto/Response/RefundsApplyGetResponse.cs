using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// RefundsApplyGetResponse.
    /// </summary>
    public class RefundsApplyGetResponse : TopResponse
    {
        /// <summary>
        /// 搜索到的退款信息列表
        /// </summary>
        [XmlArray("refunds")]
        [XmlArrayItem("refund")]
        public List<Top.Api.Domain.Refund> Refunds { get; set; }

        /// <summary>
        /// 搜索到的交易信息总数
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

    }
}
