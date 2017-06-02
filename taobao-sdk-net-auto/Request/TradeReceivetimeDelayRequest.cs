using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trade.receivetime.delay
    /// </summary>
    public class TradeReceivetimeDelayRequest : BaseTopRequest<Top.Api.Response.TradeReceivetimeDelayResponse>
    {
        /// <summary>
        /// 延长收货的天数，可选值为：3, 5, 7, 10。
        /// </summary>
        public Nullable<long> Days { get; set; }

        /// <summary>
        /// 主订单号
        /// </summary>
        public Nullable<long> Tid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trade.receivetime.delay";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("days", this.Days);
            parameters.Add("tid", this.Tid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("days", this.Days);
            RequestValidator.ValidateMaxValue("days", this.Days, 10);
            RequestValidator.ValidateMinValue("days", this.Days, 3);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        #endregion
    }
}
