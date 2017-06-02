using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trade.confirmfee.get
    /// </summary>
    public class TradeConfirmfeeGetRequest : BaseTopRequest<Top.Api.Response.TradeConfirmfeeGetResponse>
    {
        /// <summary>
        /// 交易主订单或子订单ID
        /// </summary>
        public Nullable<long> Tid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trade.confirmfee.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("tid", this.Tid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        #endregion
    }
}
