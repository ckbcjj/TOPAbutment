using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelGiftRelationshipGetResponse.
    /// </summary>
    public class XhotelGiftRelationshipGetResponse : TopResponse
    {
        /// <summary>
        /// relate_id 当TYPE=1时，Dataid填写1，表示设置卖家级别礼包 当TYPE=2时，Dataid填HID 当TYPE=3时，Dataid填GID 当TYPE=4时，Dataid填[GID+RPID]
        /// </summary>
        [XmlArray("relate_ids")]
        [XmlArrayItem("string")]
        public List<string> RelateIds { get; set; }

    }
}
