using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRateDeleteResponse.
    /// </summary>
    public class XhotelRateDeleteResponse : TopResponse
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
	        /// rateid-房型id-房价id
	        /// </summary>
	        [XmlElement("rateid_gid_rpid")]
	        public string RateidGidRpid { get; set; }
}

    }
}
