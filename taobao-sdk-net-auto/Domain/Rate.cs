using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// Rate Data Structure.
    /// </summary>
    [Serializable]
    public class Rate : TopObject
    {
        /// <summary>
        /// 额外服务-是否可以加床，1：不可以，2：可以
        /// </summary>
        [XmlElement("add_bed")]
        public long AddBed { get; set; }

        /// <summary>
        /// 额外服务-加床价格
        /// </summary>
        [XmlElement("add_bed_price")]
        public long AddBedPrice { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// 币种（仅支持CNY）
        /// </summary>
        [XmlElement("currency_code")]
        public long CurrencyCode { get; set; }

        /// <summary>
        /// 酒店商品id
        /// </summary>
        [XmlElement("gid")]
        public long Gid { get; set; }

        /// <summary>
        /// 价格和库存信息。  A:use_room_inventory:是否使用room级别共享库存，可选值 true false 1、true时：使用room级别共享库存（即使用gid对应的XRoom中的inventory），rate_quota_map 的json 数据中不需要录入库存信息,录入的库存信息会忽略 2、false时：使用rate级别私有库存，此时要求价格和库存必填。  B:date  日期必须为 T---T+90 日内的日期（T为当天），且不能重复  C:price 价格 int类型 取值范围1-99999999 单位为分  D:quota 库存 int 类型 取值范围  0-999（数量库存）  60000(状态库存关) 61000(状态库存开)
        /// </summary>
        [XmlElement("inventory_price")]
        public string InventoryPrice { get; set; }

        /// <summary>
        /// 即时确认状态，表示此rate预订后是否可以直接发货。可取范围：0,1。可以为空
        /// </summary>
        [XmlElement("jishiqueren_tag")]
        public long JishiquerenTag { get; set; }

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
        /// 酒店RPID
        /// </summary>
        [XmlElement("rpid")]
        public long Rpid { get; set; }

        /// <summary>
        /// 实价有房标签（RP支付类型为全额支付）
        /// </summary>
        [XmlElement("shijia_tag")]
        public long ShijiaTag { get; set; }
    }
}
