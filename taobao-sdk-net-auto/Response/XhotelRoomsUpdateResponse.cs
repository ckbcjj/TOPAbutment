using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRoomsUpdateResponse.
    /// </summary>
    public class XhotelRoomsUpdateResponse : TopResponse
    {
        /// <summary>
        /// 成功的gids LIST
        /// </summary>
        [XmlArray("gids")]
        [XmlArrayItem("string")]
        public List<string> Gids { get; set; }

    }
}
