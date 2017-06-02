using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRatesIncrementResponse.
    /// </summary>
    public class XhotelRatesIncrementResponse : TopResponse
    {
        /// <summary>
        /// gid和rpid组合数组  gid_rpid
        /// </summary>
        [XmlArray("gid_and_rpids")]
        [XmlArrayItem("string")]
        public List<string> GidAndRpids { get; set; }

    }
}
