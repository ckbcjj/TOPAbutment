using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateUpdateResponse.
    /// </summary>
    public class XhotelRateUpdateResponse : TopResponse
    {
        /// <summary>
        /// 酒店商品ID-酒店RPid
        /// </summary>
        [XmlElement("gid_and_rpid")]
        public string GidAndRpid { get; set; }

    }
}
