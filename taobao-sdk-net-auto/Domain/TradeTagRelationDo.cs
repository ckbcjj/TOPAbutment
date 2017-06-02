using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Domain
{
    /// <summary>
    /// TradeTagRelationDo Data Structure.
    /// </summary>
    [Serializable]
    public class TradeTagRelationDo : TopObject
    {
        /// <summary>
        /// 记录的创建时间
        /// </summary>
        [XmlElement("gmt_created")]
        public string GmtCreated { get; set; }

        /// <summary>
        /// 记录的最新修改时间
        /// </summary>
        [XmlElement("gmt_modified")]
        public string GmtModified { get; set; }

        /// <summary>
        /// 该标签操作的历史记录
        /// </summary>
        [XmlArray("history_trade_tag_relations")]
        [XmlArrayItem("history_trade_relation_do")]
        public List<Top.Api.Domain.HistoryTradeRelationDo> HistoryTradeTagRelations { get; set; }

        /// <summary>
        /// 订单标签记录id
        /// </summary>
        [XmlElement("id")]
        public long Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [XmlElement("tag_name")]
        public string TagName { get; set; }

        /// <summary>
        /// 标签类型       1：官方标签      2：自定义标签     3：主站只读标签
        /// </summary>
        [XmlElement("tag_type")]
        public long TagType { get; set; }

        /// <summary>
        /// 标签值，json格式
        /// </summary>
        [XmlElement("tag_value")]
        public string TagValue { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        [XmlElement("tid")]
        public long Tid { get; set; }

        /// <summary>
        /// 该标签在消费者端是否显示,0:不显示,1：显示
        /// </summary>
        [XmlElement("visible")]
        public long Visible { get; set; }
    }
}
