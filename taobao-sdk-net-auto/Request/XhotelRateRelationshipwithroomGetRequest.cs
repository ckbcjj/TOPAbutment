using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rate.relationshipwithroom.get
    /// </summary>
    public class XhotelRateRelationshipwithroomGetRequest : BaseTopRequest<Top.Api.Response.XhotelRateRelationshipwithroomGetResponse>
    {
        /// <summary>
        /// 页数
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        /// <summary>
        /// rpId
        /// </summary>
        public Nullable<long> RpId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rate.relationshipwithroom.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("page_no", this.PageNo);
            parameters.Add("rp_id", this.RpId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("rp_id", this.RpId);
        }

        #endregion
    }
}
