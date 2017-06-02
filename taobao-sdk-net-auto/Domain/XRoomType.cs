using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// XRoomType Data Structure.
    /// </summary>
    [Serializable]
    public class XRoomType : TopObject
    {
        /// <summary>
        /// 可选值：A,B,C,D。分别代表： A：15平米以下，B：16－30平米，C：31－50平米，D：50平米以上 2）也可以自己定义，比如：40平方米
        /// </summary>
        [XmlElement("area")]
        public string Area { get; set; }

        /// <summary>
        /// 床宽。
        /// </summary>
        [XmlElement("bed_size")]
        public string BedSize { get; set; }

        /// <summary>
        /// 床型。按自己定义存储，比如：高低床、上下床
        /// </summary>
        [XmlElement("bed_type")]
        public string BedType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// 酒店数据状态：匹配成功；待匹配；待审核；审核失败；疑似错误；请注意：只有状态为&ldquo;匹配成功&rdquo;才是正常状态。其他状态都不会上架商品。
        /// </summary>
        [XmlElement("data_confirm_str")]
        public string DataConfirmStr { get; set; }

        /// <summary>
        /// 出错原因,没有匹配上标准房型时，小二拒绝的理由
        /// </summary>
        [XmlElement("error_info")]
        public string ErrorInfo { get; set; }

        /// <summary>
        /// 扩展信息的JSON。 注：此字段的值需要ISV在接入前与淘宝沟通，且确认能解析
        /// </summary>
        [XmlElement("extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 客房在建筑的第几层，隔层为1-2层，4-5层，7-8层
        /// </summary>
        [XmlElement("floor")]
        public string Floor { get; set; }

        /// <summary>
        /// hid
        /// </summary>
        [XmlElement("hid")]
        public long Hid { get; set; }

        /// <summary>
        /// 宽带服务。A,B,C,D。分别代表： A：无宽带，B：免费宽带，C：收费宽带，D：部分收费宽带
        /// </summary>
        [XmlElement("internet")]
        public string Internet { get; set; }

        /// <summary>
        /// 匹配状态: 0：待系统匹配 1：已系统匹配，匹配成功，待卖家确认 2：已系统匹配，匹配失败，待人工匹配 3：已人工匹配，匹配成功，待卖家确认 4：已人工匹配，匹配失败 5：卖家已确认，确认&ldquo;YES&rdquo; 6：卖家已确认，确认&ldquo;NO&rdquo; 7:已系统匹配，但是匹配重复，待人工确认
        /// </summary>
        [XmlElement("match_status")]
        public long MatchStatus { get; set; }

        /// <summary>
        /// 最大入住人数
        /// </summary>
        [XmlElement("max_occupancy")]
        public long MaxOccupancy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        /// <summary>
        /// 房型名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 卖家系统id
        /// </summary>
        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        /// <summary>
        /// rid
        /// </summary>
        [XmlElement("rid")]
        public long Rid { get; set; }

        /// <summary>
        /// 标准房型信息
        /// </summary>
        [XmlElement("s_roomtype")]
        public Top.Api.Domain.SRoomType SRoomtype { get; set; }

        /// <summary>
        /// 设施服务。JSON格式。 value值true有此服务，false没有。 bar：吧台，catv：有线电视，ddd：国内长途电话，idd：国际长途电话，toilet：独立卫生间，pubtoliet：公共卫生间。 如： {&quot;bar&quot;:false,&quot;catv&quot;:false,&quot;ddd&quot;:false,&quot;idd&quot;:false,&quot;pubtoilet&quot;:false,&quot;toilet&quot;:false}
        /// </summary>
        [XmlElement("service")]
        public string Service { get; set; }

        /// <summary>
        /// 房型状态。0:正常，-1:删除，-2:停售
        /// </summary>
        [XmlElement("status")]
        public long Status { get; set; }

        /// <summary>
        /// 窗型,0：无窗/1：有窗
        /// </summary>
        [XmlElement("window_type")]
        public long WindowType { get; set; }
    }
}
