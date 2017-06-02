using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelBaseinfoRoomGetResponse.
    /// </summary>
    public class XhotelBaseinfoRoomGetResponse : TopResponse
    {
        /// <summary>
        /// result
        /// </summary>
        [XmlElement("result")]
        public ResultSetDomain Result { get; set; }

	/// <summary>
/// RatepPlanDomain Data Structure.
/// </summary>
[Serializable]
public class RatepPlanDomain : TopObject
{
	        /// <summary>
	        /// 房价名称
	        /// </summary>
	        [XmlElement("name")]
	        public string Name { get; set; }
	
	        /// <summary>
	        /// ratePlanCode
	        /// </summary>
	        [XmlElement("rate_plan_code")]
	        public string RatePlanCode { get; set; }
	
	        /// <summary>
	        /// 1：开启2：关闭。
	        /// </summary>
	        [XmlElement("status")]
	        public long Status { get; set; }
	
	        /// <summary>
	        /// 系统商
	        /// </summary>
	        [XmlElement("vendor")]
	        public string Vendor { get; set; }
}

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
	        /// outerId
	        /// </summary>
	        [XmlElement("outer_id")]
	        public string OuterId { get; set; }
	
	        /// <summary>
	        /// 房价列表
	        /// </summary>
	        [XmlArray("rate_plan_list")]
	        [XmlArrayItem("ratep_plan")]
	        public List<RatepPlanDomain> RatePlanList { get; set; }
	
	        /// <summary>
	        /// 房型状态。0:正常，-1:删除，-2:停售
	        /// </summary>
	        [XmlElement("status")]
	        public long Status { get; set; }
	
	        /// <summary>
	        /// 系统商
	        /// </summary>
	        [XmlElement("vendor")]
	        public string Vendor { get; set; }
}

	/// <summary>
/// XHotelInfoWithRoomDomain Data Structure.
/// </summary>
[Serializable]
public class XHotelInfoWithRoomDomain : TopObject
{
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
	        public XHotelInfoWithRoomDomain XhotelBaseInfo { get; set; }
}

    }
}
