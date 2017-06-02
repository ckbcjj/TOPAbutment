using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.multiplerate.update
    /// </summary>
    public class XhotelMultiplerateUpdateRequest : BaseTopRequest<Top.Api.Response.XhotelMultiplerateUpdateResponse>
    {
        /// <summary>
        /// 儿童人数
        /// </summary>
        public Nullable<long> Childnum { get; set; }

        /// <summary>
        /// 币种.CNY为人民币
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// 废弃，使用out_rid
        /// </summary>
        public Nullable<long> Gid { get; set; }

        /// <summary>
        /// 婴儿人数
        /// </summary>
        public Nullable<long> Infantnum { get; set; }

        /// <summary>
        /// 价格和库存信息。 A:use_room_inventory:是否使用房型共享库存，可选值 true false ,false时：使用房价专有库存，此时要求价格和库存必填。 B:date 日期必须为 T---T+180 日内的日期（T为当天），且不能重复 C:price 价格 int类型 取值范围1-99999999 单位为分 D:quota 库存 int 类型 取值范围 0-999（数量库存） 60000(状态库存关) 61000(状态库存开) tax为税费，addBed为加床价，addPerson为加人价1
        /// </summary>
        public string InventoryPrice { get; set; }

        /// <summary>
        /// 连住天数(范围1~10)
        /// </summary>
        public Nullable<long> Lengthofstay { get; set; }

        /// <summary>
        /// 锁库存截止时间，如果当前时间是在锁库存开始时间和截止时间之间，那么不允许修改该活动库存（包含开始时间和截止时间）
        /// </summary>
        public string LockEndTime { get; set; }

        /// <summary>
        /// 锁库存开始时间，如果当前时间是在锁库存开始时间和截止时间之间，那么不允许修改该活动库存（包含开始时间和截止时间）
        /// </summary>
        public string LockStartTime { get; set; }

        /// <summary>
        /// 废弃
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 入住人数(范围1~10)
        /// </summary>
        public Nullable<long> Occupancy { get; set; }

        /// <summary>
        /// 卖家房型ID
        /// </summary>
        public string OutRid { get; set; }

        /// <summary>
        /// 卖家自己系统的房价code
        /// </summary>
        public string RatePlanCode { get; set; }

        /// <summary>
        /// 价格开关 date：开关状态控制的那一天；rate_status：开关状态(0，关闭；1，打开); checkin_status：入住开关(0，关闭；1，打开)；checkout_status：离店开关 (0，关闭；1，打开)
        /// </summary>
        public string RateSwitchCal { get; set; }

        /// <summary>
        /// 废弃，使用rate_plan_code
        /// </summary>
        public Nullable<long> Rpid { get; set; }

        /// <summary>
        /// 价格状态。0为不可售；1为可售，默认可售
        /// </summary>
        public Nullable<long> Status { get; set; }

        /// <summary>
        /// 系统商，一般不填写，使用须申请
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.multiplerate.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("childnum", this.Childnum);
            parameters.Add("currency_code", this.CurrencyCode);
            parameters.Add("gid", this.Gid);
            parameters.Add("infantnum", this.Infantnum);
            parameters.Add("inventory_price", this.InventoryPrice);
            parameters.Add("lengthofstay", this.Lengthofstay);
            parameters.Add("lock_end_time", this.LockEndTime);
            parameters.Add("lock_start_time", this.LockStartTime);
            parameters.Add("name", this.Name);
            parameters.Add("occupancy", this.Occupancy);
            parameters.Add("out_rid", this.OutRid);
            parameters.Add("rate_plan_code", this.RatePlanCode);
            parameters.Add("rate_switch_cal", this.RateSwitchCal);
            parameters.Add("rpid", this.Rpid);
            parameters.Add("status", this.Status);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxValue("childnum", this.Childnum, 10);
            RequestValidator.ValidateMinValue("childnum", this.Childnum, 1);
            RequestValidator.ValidateMaxValue("infantnum", this.Infantnum, 10);
            RequestValidator.ValidateMinValue("infantnum", this.Infantnum, 1);
            RequestValidator.ValidateRequired("lengthofstay", this.Lengthofstay);
            RequestValidator.ValidateRequired("occupancy", this.Occupancy);
        }

        #endregion
    }
}
