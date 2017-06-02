using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.vas.subscribe.get
    /// </summary>
    public class VasSubscribeGetRequest : BaseTopRequest<Top.Api.Response.VasSubscribeGetResponse>
    {
        /// <summary>
        /// 商品编码，从合作伙伴后台（my.open.taobao.com）-收费管理-收费项目列表 能够获得该应用的商品代码
        /// </summary>
        public string ArticleCode { get; set; }

        /// <summary>
        /// 淘宝会员名
        /// </summary>
        public string Nick { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.vas.subscribe.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("article_code", this.ArticleCode);
            parameters.Add("nick", this.Nick);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("article_code", this.ArticleCode);
            RequestValidator.ValidateRequired("nick", this.Nick);
        }

        #endregion
    }
}
