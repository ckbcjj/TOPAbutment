using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeMemoAddResponse.
    /// </summary>
    public class TradeMemoAddResponse : TopResponse
    {
        /// <summary>
        /// 对一笔交易添加备注后返回其对应的Trade，Trade中可用的返回字段有tid和created
        /// </summary>
        [XmlElement("trade")]
        public Top.Api.Domain.Trade Trade { get; set; }

    }
}
