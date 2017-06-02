using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.shop.remainshowcase.get
    /// </summary>
    public class ShopRemainshowcaseGetRequest : BaseTopRequest<Top.Api.Response.ShopRemainshowcaseGetResponse>
    {
        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.shop.remainshowcase.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
