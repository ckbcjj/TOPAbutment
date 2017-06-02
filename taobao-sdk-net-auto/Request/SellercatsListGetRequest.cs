using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.sellercats.list.get
    /// </summary>
    public class SellercatsListGetRequest : BaseTopRequest<Top.Api.Response.SellercatsListGetResponse>
    {
        /// <summary>
        /// fields参数
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        public string Nick { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.sellercats.list.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("nick", this.Nick);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxListSize("fields", this.Fields, 20);
            RequestValidator.ValidateRequired("nick", this.Nick);
        }

        #endregion
    }
}
