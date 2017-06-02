using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.oc.tradetags.get
    /// </summary>
    public class OcTradetagsGetRequest : BaseTopRequest<Top.Api.Response.OcTradetagsGetResponse>
    {
        /// <summary>
        /// 是否查询历史标签
        /// </summary>
        public Nullable<long> History { get; set; }

        /// <summary>
        /// 不填，则不做标签名称过滤
        /// </summary>
        public string TagNames { get; set; }

        /// <summary>
        /// 不填，则默认只查询1,2。1为官方标，2为自定义标，3为主站只读标签
        /// </summary>
        public string TagTypes { get; set; }

        /// <summary>
        /// 交易主订单id
        /// </summary>
        public Nullable<long> Tid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.oc.tradetags.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("history", this.History);
            parameters.Add("tag_names", this.TagNames);
            parameters.Add("tag_types", this.TagTypes);
            parameters.Add("tid", this.Tid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxListSize("tag_names", this.TagNames, 20);
            RequestValidator.ValidateMaxListSize("tag_types", this.TagTypes, 20);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        #endregion
    }
}
