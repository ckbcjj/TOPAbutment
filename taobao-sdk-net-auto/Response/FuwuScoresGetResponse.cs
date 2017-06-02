using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// FuwuScoresGetResponse.
    /// </summary>
    public class FuwuScoresGetResponse : TopResponse
    {
        /// <summary>
        /// 评价流水记录
        /// </summary>
        [XmlArray("score_result")]
        [XmlArrayItem("score_result")]
        public List<Top.Api.Domain.ScoreResult> ScoreResult { get; set; }

    }
}
