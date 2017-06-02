using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.shop.get
    /// </summary>
    public class ShopGetRequest : BaseTopRequest<Top.Api.Response.ShopGetResponse>
    {
        /// <summary>
        /// 需返回的字段列表。可选值：Shop 结构中的所有字段；多个字段之间用逗号(,)分隔
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        public string Nick { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.shop.get";
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
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("nick", this.Nick);
        }

        #endregion
    }
}
