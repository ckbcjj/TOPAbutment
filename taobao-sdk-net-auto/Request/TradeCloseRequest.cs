using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trade.close
    /// </summary>
    public class TradeCloseRequest : BaseTopRequest<Top.Api.Response.TradeCloseResponse>
    {
        /// <summary>
        /// 交易关闭原因。可以选择的理由有：1.未及时付款2、买家不想买了3、买家信息填写错误，重新拍4、恶意买家/同行捣乱5、缺货6、买家拍错了7、同城见面交易
        /// </summary>
        public string CloseReason { get; set; }

        /// <summary>
        /// 主订单或子订单编号。
        /// </summary>
        public Nullable<long> Tid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trade.close";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("close_reason", this.CloseReason);
            parameters.Add("tid", this.Tid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("close_reason", this.CloseReason);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        #endregion
    }
}
