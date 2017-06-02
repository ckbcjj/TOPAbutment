using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.sellercenter.role.add
    /// </summary>
    public class SellercenterRoleAddRequest : BaseTopRequest<Top.Api.Response.SellercenterRoleAddResponse>
    {
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表示卖家昵称
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 需要授权的权限点permission_code列表,以","分割.其code值可以通过调用taobao.sellercenter.user.permissions.get返回，其中permission.is_authorize=1的权限点可以通过本接口授权给对应角色。
        /// </summary>
        public string PermissionCodes { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.sellercenter.role.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("description", this.Description);
            parameters.Add("name", this.Name);
            parameters.Add("nick", this.Nick);
            parameters.Add("permission_codes", this.PermissionCodes);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxLength("description", this.Description, 20);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 20);
            RequestValidator.ValidateRequired("nick", this.Nick);
            RequestValidator.ValidateMaxLength("nick", this.Nick, 500);
            RequestValidator.ValidateMaxListSize("permission_codes", this.PermissionCodes, 2000);
        }

        #endregion
    }
}
