using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// OcTradetagsGetResponse.
    /// </summary>
    public class OcTradetagsGetResponse : TopResponse
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        [XmlArray("trade_tags")]
        [XmlArrayItem("trade_tag_relation_do")]
        public List<Top.Api.Domain.TradeTagRelationDo> TradeTags { get; set; }

    }
}
