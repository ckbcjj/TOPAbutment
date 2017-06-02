using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelMultiplerateUpdateResponse.
    /// </summary>
    public class XhotelMultiplerateUpdateResponse : TopResponse
    {
        /// <summary>
        /// gid-rpid-occupancy-lengthofstay  商品ID-房价ID-入住人数-连住天数
        /// </summary>
        [XmlElement("gid_and_rpid_occupancy_lengthofstay")]
        public string GidAndRpidOccupancyLengthofstay { get; set; }

    }
}
