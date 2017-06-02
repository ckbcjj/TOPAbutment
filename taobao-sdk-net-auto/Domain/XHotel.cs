using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// XHotel Data Structure.
    /// </summary>
    [Serializable]
    public class XHotel : TopObject
    {
        /// <summary>
        /// 酒店地址
        /// </summary>
        [XmlElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// 商圈信息
        /// </summary>
        [XmlElement("business")]
        public string Business { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        [XmlElement("city")]
        public long City { get; set; }

        /// <summary>
        /// 国家编码
        /// </summary>
        [XmlElement("country")]
        public string Country { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// 逗号分隔的字符串 1visa；2万事达卡；3美国运通卡；4发现卡；5大来卡；6JCB卡；7银联卡；110未知卡类型
        /// </summary>
        [XmlElement("credit_card_types")]
        public string CreditCardTypes { get; set; }

        /// <summary>
        /// 酒店数据状态：匹配成功；待匹配；待审核；审核失败；疑似错误；请注意：只有状态为&ldquo;匹配成功&rdquo;; 才是正常状态。其他状态都不会上架商品。
        /// </summary>
        [XmlElement("data_confirm_str")]
        public string DataConfirmStr { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        [XmlElement("district")]
        public long District { get; set; }

        /// <summary>
        /// 0:国内;1:国外
        /// </summary>
        [XmlElement("domestic")]
        public long Domestic { get; set; }

        /// <summary>
        /// 未通过时的拒绝原因等。
        /// </summary>
        [XmlElement("error_info")]
        public string ErrorInfo { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [XmlElement("extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 酒店ID
        /// </summary>
        [XmlElement("hid")]
        public long Hid { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [XmlElement("latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [XmlElement("longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// hotel匹配状态: 0：待系统匹配 1：已系统匹配，匹配成功，待卖家确认 2：已系统匹配，匹配失败，待人工匹配 3：已人工匹配，匹配成功，待卖家确认 4：已人工匹配，匹配失败 5：卖家已确认，确认&ldquo;YES&rdquo; 6：卖家已确认，确认&ldquo;NO&rdquo; 7:已系统匹配，但是匹配重复，待人工确认
        /// </summary>
        [XmlElement("match_status")]
        public long MatchStatus { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 卖家自己系统的id
        /// </summary>
        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        /// <summary>
        /// 坐标类型
        /// </summary>
        [XmlElement("position_type")]
        public string PositionType { get; set; }

        /// <summary>
        /// 省份编码
        /// </summary>
        [XmlElement("province")]
        public long Province { get; set; }

        /// <summary>
        /// 淘宝标准酒店信息
        /// </summary>
        [XmlElement("s_hotel")]
        public Top.Api.Domain.SHotel SHotel { get; set; }

        /// <summary>
        /// 酒店状态：0: 正常;-2:停售；-1：删除
        /// </summary>
        [XmlElement("status")]
        public long Status { get; set; }

        /// <summary>
        /// 酒店电话
        /// </summary>
        [XmlElement("tel")]
        public string Tel { get; set; }

        /// <summary>
        /// 曾用名
        /// </summary>
        [XmlElement("used_name")]
        public string UsedName { get; set; }
    }
}
