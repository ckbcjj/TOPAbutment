using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateplanDeleteResponse.
    /// </summary>
    public class XhotelRateplanDeleteResponse : TopResponse
    {
        /// <summary>
        /// result
        /// </summary>
        [XmlElement("result")]
        public ResultSetDomain Result { get; set; }

	/// <summary>
/// ResultSetDomain Data Structure.
/// </summary>
[Serializable]
public class ResultSetDomain : TopObject
{
	        /// <summary>
	        /// errorCode
	        /// </summary>
	        [XmlElement("error_code")]
	        public string ErrorCode { get; set; }
	
	        /// <summary>
	        /// errorMsg
	        /// </summary>
	        [XmlElement("error_msg")]
	        public string ErrorMsg { get; set; }
	
	        /// <summary>
	        /// results
	        /// </summary>
	        [XmlArray("results")]
	        [XmlArrayItem("json")]
	        public List<string> Results { get; set; }
	
	        /// <summary>
	        /// 房价id
	        /// </summary>
	        [XmlElement("rpid")]
	        public string Rpid { get; set; }
}

    }
}
