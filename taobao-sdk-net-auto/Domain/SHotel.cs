using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// SHotel Data Structure.
    /// </summary>
    [Serializable]
    public class SHotel : TopObject
    {
        /// <summary>
        /// 酒店地址
        /// </summary>
        [XmlElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// brand
        /// </summary>
        [XmlElement("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// business
        /// </summary>
        [XmlElement("business")]
        public string Business { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        [XmlElement("city")]
        public long City { get; set; }

        /// <summary>
        /// 地区标签
        /// </summary>
        [XmlElement("city_tag")]
        public string CityTag { get; set; }

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
        /// 装修年份
        /// </summary>
        [XmlElement("decorate_time")]
        public string DecorateTime { get; set; }

        /// <summary>
        /// 酒店介绍
        /// </summary>
        [XmlElement("desc")]
        public string Desc { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [XmlElement("district")]
        public long District { get; set; }

        /// <summary>
        /// 0:国内;1:国外
        /// </summary>
        [XmlElement("domestic")]
        public long Domestic { get; set; }

        /// <summary>
        /// 扩展信息的JSON
        /// </summary>
        [XmlElement("extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement("fax")]
        public string Fax { get; set; }

        /// <summary>
        /// 酒店设施
        /// </summary>
        [XmlElement("hotel_facilities")]
        public string HotelFacilities { get; set; }

        /// <summary>
        /// latitude
        /// </summary>
        [XmlElement("latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// 酒店级别
        /// </summary>
        [XmlElement("level")]
        public string Level { get; set; }

        /// <summary>
        /// longitude
        /// </summary>
        [XmlElement("longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        /// <summary>
        /// name
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 开业年份
        /// </summary>
        [XmlElement("opening_time")]
        public string OpeningTime { get; set; }

        /// <summary>
        /// 酒店图片url
        /// </summary>
        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        /// <summary>
        /// position_type
        /// </summary>
        [XmlElement("position_type")]
        public long PositionType { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// 省份编码
        /// </summary>
        [XmlElement("province")]
        public long Province { get; set; }

        /// <summary>
        /// 房间设施
        /// </summary>
        [XmlElement("room_facilities")]
        public string RoomFacilities { get; set; }

        /// <summary>
        /// 房间数
        /// </summary>
        [XmlElement("rooms")]
        public long Rooms { get; set; }

        /// <summary>
        /// 交通距离与设施服务。JSON格式。
        /// </summary>
        [XmlElement("service")]
        public string Service { get; set; }

        /// <summary>
        /// 酒店ID
        /// </summary>
        [XmlElement("shid")]
        public long Shid { get; set; }

        /// <summary>
        /// 状态:  0：待系统匹配  1：已系统匹配，匹配成功，待卖家确认  2：已系统匹配，匹配失败，待人工匹配  3：已人工匹配，匹配成功，待卖家确认  4：已人工匹配，匹配失败  5：卖家已确认，确认“YES”  6：卖家已确认，确认“NO”  7：停售
        /// </summary>
        [XmlElement("status")]
        public long Status { get; set; }

        /// <summary>
        /// 楼层数
        /// </summary>
        [XmlElement("storeys")]
        public string Storeys { get; set; }

        /// <summary>
        /// 酒店电话
        /// </summary>
        [XmlElement("tel")]
        public string Tel { get; set; }

        /// <summary>
        /// 酒店类型
        /// </summary>
        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// used_name
        /// </summary>
        [XmlElement("used_name")]
        public string UsedName { get; set; }
    }
}
