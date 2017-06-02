using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// XRoom Data Structure.
    /// </summary>
    [Serializable]
    public class XRoom : TopObject
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// 宝贝描述
        /// </summary>
        [XmlElement("desc")]
        public string Desc { get; set; }

        /// <summary>
        /// 商品下架原因
        /// </summary>
        [XmlElement("down_reason")]
        public string DownReason { get; set; }

        /// <summary>
        /// extend_info1
        /// </summary>
        [XmlElement("extend_info1")]
        public string ExtendInfo1 { get; set; }

        /// <summary>
        /// extend_info2
        /// </summary>
        [XmlElement("extend_info2")]
        public string ExtendInfo2 { get; set; }

        /// <summary>
        /// extend_info3
        /// </summary>
        [XmlElement("extend_info3")]
        public string ExtendInfo3 { get; set; }

        /// <summary>
        /// gid酒店商品id
        /// </summary>
        [XmlElement("gid")]
        public long Gid { get; set; }

        /// <summary>
        /// 购买须知
        /// </summary>
        [XmlElement("guide")]
        public string Guide { get; set; }

        /// <summary>
        /// 酒店商品是否提供发票
        /// </summary>
        [XmlElement("has_receipt")]
        public bool HasReceipt { get; set; }

        /// <summary>
        /// hid酒店id
        /// </summary>
        [XmlElement("hid")]
        public long Hid { get; set; }

        /// <summary>
        /// iid淘宝商品id
        /// </summary>
        [XmlElement("iid")]
        public long Iid { get; set; }

        /// <summary>
        /// 库存日历
        /// </summary>
        [XmlElement("inventory")]
        public string Inventory { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        /// <summary>
        /// out_rid
        /// </summary>
        [XmlElement("out_rid")]
        public string OutRid { get; set; }

        /// <summary>
        /// 酒店商品图片Url。多个url用逗号隔开
        /// </summary>
        [XmlElement("pic_urls")]
        public string PicUrls { get; set; }

        /// <summary>
        /// 发票说明，不能超过100个汉字,200个字符。
        /// </summary>
        [XmlElement("receipt_info")]
        public string ReceiptInfo { get; set; }

        /// <summary>
        /// 发票类型为其他时的发票描述,不能超过30个汉字，60个字符
        /// </summary>
        [XmlElement("receipt_other_type_desc")]
        public string ReceiptOtherTypeDesc { get; set; }

        /// <summary>
        /// 发票类型。A,B。分别代表： A:酒店住宿发票,B:其他
        /// </summary>
        [XmlElement("receipt_type")]
        public string ReceiptType { get; set; }

        /// <summary>
        /// 橱窗推荐
        /// </summary>
        [XmlElement("recommend")]
        public bool Recommend { get; set; }

        /// <summary>
        /// rid房型id
        /// </summary>
        [XmlElement("rid")]
        public long Rid { get; set; }

        /// <summary>
        /// 宝贝状态。1：上架。2：下架。3：删除
        /// </summary>
        [XmlElement("status")]
        public long Status { get; set; }

        /// <summary>
        /// 宝贝名称
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [XmlElement("vendor")]
        public string Vendor { get; set; }
    }
}
