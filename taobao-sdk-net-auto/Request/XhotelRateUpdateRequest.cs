using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rate.update
    /// </summary>
    public class XhotelRateUpdateRequest : BaseTopRequest<Top.Api.Response.XhotelRateUpdateResponse>
    {
        /// <summary>
        /// 废弃
        /// </summary>
        public Nullable<long> AddBed { get; set; }

        /// <summary>
        /// 废弃
        /// </summary>
        public Nullable<long> AddBedPrice { get; set; }

        /// <summary>
        /// 废弃（仅支持CNY）
        /// </summary>
        public Nullable<long> CurrencyCode { get; set; }

        /// <summary>
        /// 废弃，请使用out_rid
        /// </summary>
        public Nullable<long> Gid { get; set; }

        /// <summary>
        /// 每日价格和房价专有库存信息。A:use_room_inventory:是否使用room级别共享库存，可选值 true false 1、true时：使用room级别共享库存（即使用gid对应的XRoom中的inventory），rate_quota_map 的json 数据中不需要录入库存信息,录入的库存信息会忽略 2、false时：使用rate级别私有库存，此时要求价格和库存必填。B:date  日期必须为 T---T+180 日内的日期（T为当天），且不能重复C:price 价格 int类型 取值范围1-99999999 单位为分D:quota 库存 int 类型 取值范围  0-999（数量库存）  60000(状态库存关) 61000(状态库存开)
        /// </summary>
        public string InventoryPrice { get; set; }

        /// <summary>
        /// 废弃
        /// </summary>
        public Nullable<long> JishiquerenTag { get; set; }

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
        /// 商家房型ID
        /// </summary>
        public string OutRid { get; set; }

        /// <summary>
        /// 日历价格开关， date：开关状态控制的是那一天 rate_status：开关状态。0，关闭；1，打开
        /// </summary>
        public string RateSwitchCal { get; set; }

        /// <summary>
        /// 商家价格计划编码
        /// </summary>
        public string RateplanCode { get; set; }

        /// <summary>
        /// 废弃，请使用rateplan_code
        /// </summary>
        public Nullable<long> Rpid { get; set; }

        /// <summary>
        /// 废弃
        /// </summary>
        public Nullable<long> ShijiaTag { get; set; }

        /// <summary>
        /// 系统商，一般不用填写，使用需要申请
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rate.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("add_bed", this.AddBed);
            parameters.Add("add_bed_price", this.AddBedPrice);
            parameters.Add("currency_code", this.CurrencyCode);
            parameters.Add("gid", this.Gid);
            parameters.Add("inventory_price", this.InventoryPrice);
            parameters.Add("jishiqueren_tag", this.JishiquerenTag);
            parameters.Add("lock_end_time", this.LockEndTime);
            parameters.Add("lock_start_time", this.LockStartTime);
            parameters.Add("name", this.Name);
            parameters.Add("out_rid", this.OutRid);
            parameters.Add("rate_switch_cal", this.RateSwitchCal);
            parameters.Add("rateplan_code", this.RateplanCode);
            parameters.Add("rpid", this.Rpid);
            parameters.Add("shijia_tag", this.ShijiaTag);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxValue("add_bed", this.AddBed, 2);
            RequestValidator.ValidateMinValue("add_bed", this.AddBed, 1);
            RequestValidator.ValidateMaxLength("name", this.Name, 60);
            RequestValidator.ValidateMaxLength("out_rid", this.OutRid, 128);
            RequestValidator.ValidateMaxLength("rateplan_code", this.RateplanCode, 50);
            RequestValidator.ValidateMaxLength("vendor", this.Vendor, 50);
        }

        #endregion
    }
}
