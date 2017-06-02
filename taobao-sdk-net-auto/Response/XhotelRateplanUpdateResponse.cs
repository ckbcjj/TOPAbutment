using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateplanUpdateResponse.
    /// </summary>
    public class XhotelRateplanUpdateResponse : TopResponse
    {
        /// <summary>
        /// 修改的rp id
        /// </summary>
        [XmlElement("rpid")]
        public long Rpid { get; set; }

    }
}
