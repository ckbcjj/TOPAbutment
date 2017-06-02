using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateAddResponse.
    /// </summary>
    public class XhotelRateAddResponse : TopResponse
    {
        /// <summary>
        /// 酒店商品id-酒店rpID
        /// </summary>
        [XmlElement("gid_and_rpid")]
        public string GidAndRpid { get; set; }

    }
}
