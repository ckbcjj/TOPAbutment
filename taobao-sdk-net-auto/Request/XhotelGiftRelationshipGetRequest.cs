using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.gift.relationship.get
    /// </summary>
    public class XhotelGiftRelationshipGetRequest : BaseTopRequest<Top.Api.Response.XhotelGiftRelationshipGetResponse>
    {
        /// <summary>
        /// 礼包ID
        /// </summary>
        public Nullable<long> GiftId { get; set; }

        /// <summary>
        /// 礼包关系类型1：Seller，2：Hotel，3：ROOM，4：RatePlan+ROOM
        /// </summary>
        public Nullable<long> Type { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.gift.relationship.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("gift_id", this.GiftId);
            parameters.Add("type", this.Type);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("gift_id", this.GiftId);
            RequestValidator.ValidateRequired("type", this.Type);
            RequestValidator.ValidateMaxValue("type", this.Type, 4);
            RequestValidator.ValidateMinValue("type", this.Type, 1);
        }

        #endregion
    }
}
