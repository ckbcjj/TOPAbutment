using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.shopcats.list.get
    /// </summary>
    public class ShopcatsListGetRequest : BaseTopRequest<Top.Api.Response.ShopcatsListGetResponse>
    {
        /// <summary>
        /// 需要返回的字段列表，见ShopCat，默认返回：cid,parent_cid,name,is_parent
        /// </summary>
        public string Fields { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.shopcats.list.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxListSize("fields", this.Fields, 20);
        }

        #endregion
    }
}
