using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.sellercenter.role.info.get
    /// </summary>
    public class SellercenterRoleInfoGetRequest : BaseTopRequest<Top.Api.Response.SellercenterRoleInfoGetResponse>
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public Nullable<long> RoleId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.sellercenter.role.info.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("role_id", this.RoleId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("role_id", this.RoleId);
        }

        #endregion
    }
}
