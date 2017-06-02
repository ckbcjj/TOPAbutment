using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelBaseinfoGetResponse.
    /// </summary>
    public class XhotelBaseinfoGetResponse : TopResponse
    {
        /// <summary>
        /// result
        /// </summary>
        [XmlElement("result")]
        public ResultSetDomain Result { get; set; }

	/// <summary>
/// RoomTypeDomain Data Structure.
/// </summary>
[Serializable]
public class RoomTypeDomain : TopObject
{
	        /// <summary>
	        /// 房型名称
	        /// </summary>
	        [XmlElement("name")]
	        public string Name { get; set; }
	
	        /// <summary>
	        /// 商家房型ID
	        /// </summary>
	        [XmlElement("outer_id")]
	        public string OuterId { get; set; }
	
	        /// <summary>
	        /// 阿里房型id
	        /// </summary>
	        [XmlElement("rid")]
	        public long Rid { get; set; }
	
	
	        /// <summary>
	        /// 系统商，一般不填写，使用须申请
	        /// </summary>
	        [XmlElement("vendor")]
	        public string Vendor { get; set; }
}

	/// <summary>
/// RatePlanDomain Data Structure.
/// </summary>
[Serializable]
public class RatePlanDomain : TopObject
{
	        /// <summary>
	        /// 房价名称
	        /// </summary>
	        [XmlElement("name")]
	        public string Name { get; set; }
	
	        /// <summary>
	        /// 卖家自己系统的Code，简称RateCode
	        /// </summary>
	        [XmlElement("rate_plan_code")]
	        public string RatePlanCode { get; set; }
	
	        /// <summary>
	        /// 阿里房价id
	        /// </summary>
	        [XmlElement("rate_plan_id")]
	        public long RatePlanId { get; set; }
	
	        /// <summary>
	        /// 1：开启2：关闭。
	        /// </summary>
	        [XmlElement("status")]
	        public long Status { get; set; }
	
	        /// <summary>
	        /// 系统商，一般不填写，使用须申请
	        /// </summary>
	        [XmlElement("vendor")]
	        public string Vendor { get; set; }
}

	/// <summary>
/// XHotelBaseInfoDomain Data Structure.
/// </summary>
[Serializable]
public class XHotelBaseInfoDomain : TopObject
{
	        /// <summary>
	        /// 房价基础信息(需要新增rp时绑定酒店)
	        /// </summary>
	        [XmlArray("rate_plan_list")]
	        [XmlArrayItem("rate_plan")]
	        public List<RatePlanDomain> RatePlanList { get; set; }
	
	        /// <summary>
	        /// 房型基础信息
	        /// </summary>
	        [XmlArray("room_type_list")]
	        [XmlArrayItem("room_type")]
	        public List<RoomTypeDomain> RoomTypeList { get; set; }
}

	/// <summary>
/// ResultSetDomain Data Structure.
/// </summary>
[Serializable]
public class ResultSetDomain : TopObject
{
	        /// <summary>
	        /// errorCode
	        /// </summary>
	        [XmlElement("error_code")]
	        public string ErrorCode { get; set; }
	
	        /// <summary>
	        /// errorMsg
	        /// </summary>
	        [XmlElement("error_msg")]
	        public string ErrorMsg { get; set; }
	
	        /// <summary>
	        /// success
	        /// </summary>
	        [XmlElement("success")]
	        public bool Success { get; set; }
	
	        /// <summary>
	        /// 酒店基础信息
	        /// </summary>
	        [XmlElement("xhotel_base_info")]
	        public XHotelBaseInfoDomain XhotelBaseInfo { get; set; }
}

    }
}
