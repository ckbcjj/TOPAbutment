using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeMemoUpdateResponse.
    /// </summary>
    public class TradeMemoUpdateResponse : TopResponse
    {
        /// <summary>
        /// 更新交易的备注信息后返回的Trade，其中可用字段为tid和modified
        /// </summary>
        [XmlElement("trade")]
        public Top.Api.Domain.Trade Trade { get; set; }

    }
}
