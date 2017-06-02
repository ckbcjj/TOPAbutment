using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.fuwu.sale.link.gen
    /// </summary>
    public class FuwuSaleLinkGenRequest : BaseTopRequest<Top.Api.Response.FuwuSaleLinkGenResponse>
    {
        /// <summary>
        /// 用户需要营销的目标人群中的用户nick
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 从服务商后台，营销链接功能中生成的参数串直接复制使用。不要修改，否则抛错。
        /// </summary>
        public string ParamStr { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.fuwu.sale.link.gen";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("nick", this.Nick);
            parameters.Add("param_str", this.ParamStr);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("param_str", this.ParamStr);
        }

        #endregion
    }
}
