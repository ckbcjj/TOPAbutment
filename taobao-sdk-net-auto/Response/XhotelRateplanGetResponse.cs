using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateplanGetResponse.
    /// </summary>
    public class XhotelRateplanGetResponse : TopResponse
    {
        /// <summary>
        /// rateplan
        /// </summary>
        [XmlElement("rateplan")]
        public Top.Api.Domain.RatePlan Rateplan { get; set; }

    }
}
