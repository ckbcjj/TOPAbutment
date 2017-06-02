using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateplanAddResponse.
    /// </summary>
    public class XhotelRateplanAddResponse : TopResponse
    {
        /// <summary>
        /// 生成的rp id
        /// </summary>
        [XmlElement("rpid")]
        public long Rpid { get; set; }

    }
}
