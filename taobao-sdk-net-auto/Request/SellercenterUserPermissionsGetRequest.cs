using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.sellercenter.user.permissions.get
    /// </summary>
    public class SellercenterUserPermissionsGetRequest : BaseTopRequest<Top.Api.Response.SellercenterUserPermissionsGetResponse>
    {
        /// <summary>
        /// 用户标识，次入参必须为子账号比如zhangsan:cool。如果只输入主账号zhangsan，将报错。
        /// </summary>
        public string Nick { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.sellercenter.user.permissions.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("nick", this.Nick);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("nick", this.Nick);
        }

        #endregion
    }
}
