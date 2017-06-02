using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trade.postage.update
    /// </summary>
    public class TradePostageUpdateRequest : BaseTopRequest<Top.Api.Response.TradePostageUpdateResponse>
    {
        /// <summary>
        /// 邮费价格(邮费单位是元）
        /// </summary>
        public string PostFee { get; set; }

        /// <summary>
        /// 主订单编号
        /// </summary>
        public Nullable<long> Tid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trade.postage.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("post_fee", this.PostFee);
            parameters.Add("tid", this.Tid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("post_fee", this.PostFee);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        #endregion
    }
}
