using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateRelationshipwithroomGetResponse.
    /// </summary>
    public class XhotelRateRelationshipwithroomGetResponse : TopResponse
    {
        /// <summary>
        /// 返回值
        /// </summary>
        [XmlArray("gids")]
        [XmlArrayItem("string")]
        public List<string> Gids { get; set; }

        /// <summary>
        /// 根据条件所查询的所有结果的总数量
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

    }
}
