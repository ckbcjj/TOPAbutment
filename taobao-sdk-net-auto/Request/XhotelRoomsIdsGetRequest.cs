using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rooms.ids.get
    /// </summary>
    public class XhotelRoomsIdsGetRequest : BaseTopRequest<Top.Api.Response.XhotelRoomsIdsGetResponse>
    {
        /// <summary>
        /// 创建宝贝截止时间
        /// </summary>
        public Nullable<DateTime> EndDate { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        /// <summary>
        /// 创建宝贝起始时间
        /// </summary>
        public Nullable<DateTime> StartDate { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rooms.ids.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("end_date", this.EndDate);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("start_date", this.StartDate);
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
