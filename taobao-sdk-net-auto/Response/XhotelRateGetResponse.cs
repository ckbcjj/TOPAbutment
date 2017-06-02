using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateGetResponse.
    /// </summary>
    public class XhotelRateGetResponse : TopResponse
    {
        /// <summary>
        /// rate
        /// </summary>
        [XmlElement("rate")]
        public Top.Api.Domain.Rate Rate { get; set; }

    }
}
