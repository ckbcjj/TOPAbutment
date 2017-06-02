using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRatesUpdateResponse.
    /// </summary>
    public class XhotelRatesUpdateResponse : TopResponse
    {
        /// <summary>
        /// gid_and_rateplan_ids
        /// </summary>
        [XmlArray("gid_and_rpids")]
        [XmlArrayItem("string")]
        public List<string> GidAndRpids { get; set; }

    }
}
