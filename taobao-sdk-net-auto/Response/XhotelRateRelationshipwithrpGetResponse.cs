using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateRelationshipwithrpGetResponse.
    /// </summary>
    public class XhotelRateRelationshipwithrpGetResponse : TopResponse
    {
        /// <summary>
        /// 所查询出的结果，是一个字符串数组
        /// </summary>
        [XmlArray("rp_ids")]
        [XmlArrayItem("string")]
        public List<string> RpIds { get; set; }

        /// <summary>
        /// 根据条件所查询的所有结果的总数量
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

    }
}
