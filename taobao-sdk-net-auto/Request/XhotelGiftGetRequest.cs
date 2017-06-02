using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.gift.get
    /// </summary>
    public class XhotelGiftGetRequest : BaseTopRequest<Top.Api.Response.XhotelGiftGetResponse>
    {
        /// <summary>
        /// 礼包id
        /// </summary>
        public Nullable<long> GiftId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.gift.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("gift_id", this.GiftId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("gift_id", this.GiftId);
        }

        #endregion
    }
}
