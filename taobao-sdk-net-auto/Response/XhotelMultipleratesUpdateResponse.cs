using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelMultipleratesUpdateResponse.
    /// </summary>
    public class XhotelMultipleratesUpdateResponse : TopResponse
    {
        /// <summary>
        /// 商品id,房价id,入住人数，连住天数
        /// </summary>
        [XmlArray("gid_and_rpid_occupancy_lengthofstay")]
        [XmlArrayItem("json")]
        public List<string> GidAndRpidOccupancyLengthofstay { get; set; }

        /// <summary>
        /// 批量更新的时候，如果部分更新失败，会展示部分失败的原因
        /// </summary>
        [XmlElement("warnmessage")]
        public string Warnmessage { get; set; }

    }
}
