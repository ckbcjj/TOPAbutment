using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rate.relationshipwithrp.get
    /// </summary>
    public class XhotelRateRelationshipwithrpGetRequest : BaseTopRequest<Top.Api.Response.XhotelRateRelationshipwithrpGetResponse>
    {
        /// <summary>
        /// 宝贝的gid
        /// </summary>
        public Nullable<long> Gid { get; set; }

        /// <summary>
        /// 页数，可根据此值展示某页的数据。不填默认为1
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rate.relationshipwithrp.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("gid", this.Gid);
            parameters.Add("page_no", this.PageNo);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("gid", this.Gid);
        }

        #endregion
    }
}
