using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// MultipleRate Data Structure.
    /// </summary>
    [Serializable]
    public class MultipleRate : TopObject
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [XmlElement("currency_code")]
        public long CurrencyCode { get; set; }

        /// <summary>
        /// 酒店商品id
        /// </summary>
        [XmlElement("gid")]
        public long Gid { get; set; }

        /// <summary>
        /// 价格和库存信息,包括加床价，加人价等信息。date  日期必须为 T---T+90 日内的日期（T为当天），且不能重复price 价格 int类型 取值范围1-99999999 单位为分quota 库存 int 类型 取值范围  0-999（数量库存）  60000(状态库存关) 61000(状态库存开)addPerson 加人价addBed 加床价
        /// </summary>
        [XmlElement("inventory_price")]
        public string InventoryPrice { get; set; }

        /// <summary>
        /// 连住天数
        /// </summary>
        [XmlElement("lengthofstay")]
        public long Lengthofstay { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 入住人数
        /// </summary>
        [XmlElement("occupancy")]
        public long Occupancy { get; set; }

        /// <summary>
        /// 房价id
        /// </summary>
        [XmlElement("rpid")]
        public long Rpid { get; set; }
    }
}
