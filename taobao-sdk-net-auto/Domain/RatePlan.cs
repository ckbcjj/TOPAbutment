using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// RatePlan Data Structure.
    /// </summary>
    [Serializable]
    public class RatePlan : TopObject
    {
        /// <summary>
        /// 是否是协议价。1代表是
        /// </summary>
        [XmlElement("agreement")]
        public long Agreement { get; set; }

        /// <summary>
        /// 早餐日历,如果没有日历，表示没有日历化。则以RP上的早餐为准
        /// </summary>
        [XmlElement("breakfast_cal")]
        public string BreakfastCal { get; set; }

        /// <summary>
        /// 早餐数量
        /// </summary>
        [XmlElement("breakfast_count")]
        public long BreakfastCount { get; set; }

        /// <summary>
        /// 可入住的最晚时间（小时房相关字段）
        /// </summary>
        [XmlElement("can_checkin_end")]
        public string CanCheckinEnd { get; set; }

        /// <summary>
        /// 可入住的最早时间（小时房相关字段）
        /// </summary>
        [XmlElement("can_checkin_start")]
        public string CanCheckinStart { get; set; }

        /// <summary>
        /// 退订政策
        /// </summary>
        [XmlElement("cancel_policy")]
        public string CancelPolicy { get; set; }

        /// <summary>
        /// 销售渠道，目前制定一了一种A-集团协议
        /// </summary>
        [XmlElement("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// rateplan生效截止时间
        /// </summary>
        [XmlElement("deadline_time")]
        public string DeadlineTime { get; set; }

        /// <summary>
        /// 价格计划名称name通过加工处理以后的值
        /// </summary>
        [XmlElement("display_name")]
        public long DisplayName { get; set; }

        /// <summary>
        /// rateplan生效开始时间
        /// </summary>
        [XmlElement("effective_time")]
        public string EffectiveTime { get; set; }

        /// <summary>
        /// 每日结束时间默认24:00:00。生效时间＜结束时间
        /// </summary>
        [XmlElement("end_time")]
        public string EndTime { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [XmlElement("english_name")]
        public string EnglishName { get; set; }

        /// <summary>
        /// extend
        /// </summary>
        [XmlElement("extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 额外服务的扩展，是一段JSON
        /// </summary>
        [XmlElement("extend_fee")]
        public string ExtendFee { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [XmlElement("extend_info1")]
        public string ExtendInfo1 { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [XmlElement("extend_info2")]
        public string ExtendInfo2 { get; set; }

        /// <summary>
        /// 扩展字段3
        /// </summary>
        [XmlElement("extend_info3")]
        public string ExtendInfo3 { get; set; }

        /// <summary>
        /// 另加早餐金额
        /// </summary>
        [XmlElement("fee_breakfast_amount")]
        public long FeeBreakfastAmount { get; set; }

        /// <summary>
        /// 另加早餐数量
        /// </summary>
        [XmlElement("fee_breakfast_count")]
        public long FeeBreakfastCount { get; set; }

        /// <summary>
        /// 额外服务-政府税-金额（1-9999）
        /// </summary>
        [XmlElement("fee_gov_tax_amount")]
        public long FeeGovTaxAmount { get; set; }

        /// <summary>
        /// 额外服务-政府税-百分比（0%-99%）
        /// </summary>
        [XmlElement("fee_gov_tax_percent")]
        public long FeeGovTaxPercent { get; set; }

        /// <summary>
        /// 额外服务-服务费-金额（0-9999）
        /// </summary>
        [XmlElement("fee_service_amount")]
        public long FeeServiceAmount { get; set; }

        /// <summary>
        /// 额外服务-服务费-百分比（0%-99%）
        /// </summary>
        [XmlElement("fee_service_percent")]
        public long FeeServicePercent { get; set; }

        /// <summary>
        /// 是否是首住优惠rp。1代表是
        /// </summary>
        [XmlElement("first_stay")]
        public long FirstStay { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("gmt_create")]
        public string GmtCreate { get; set; }



        /// <summary>
        /// 担保金额，只有担保类型为5，该字段才有意义
        /// </summary>
        [XmlElement("guarantee_amount")]
        public long GuaranteeAmount { get; set; }

        /// <summary>
        /// 担保日历  如果没有日历，说明没有日历化，以RP上的担保为准
        /// </summary>
        [XmlElement("guarantee_cal")]
        public string GuaranteeCal { get; set; }

        /// <summary>
        /// guarantee_mode
        /// </summary>
        [XmlElement("guarantee_mode")]
        public long GuaranteeMode { get; set; }

        /// <summary>
        /// 每日开始担保时间
        /// </summary>
        [XmlElement("guarantee_start_time")]
        public string GuaranteeStartTime { get; set; }

        /// <summary>
        /// 担保类型，只支持： 0 无担保 1 首晚担保
        /// </summary>
        [XmlElement("guarantee_type")]
        public long GuaranteeType { get; set; }

        /// <summary>
        /// hid
        /// </summary>
        [XmlElement("hid")]
        public long Hid { get; set; }

        /// <summary>
        /// 入住的开始跨度（小时房相关字段）
        /// </summary>
        [XmlElement("hourage")]
        public string Hourage { get; set; }

        /// <summary>
        /// 最大提前预订小时按入住时间的23:59:59(一般认为24点)来计算
        /// </summary>
        [XmlElement("max_adv_hours")]
        public long MaxAdvHours { get; set; }

        /// <summary>
        /// 最大入住天数（1-365）
        /// </summary>
        [XmlElement("max_days")]
        public long MaxDays { get; set; }

        /// <summary>
        /// 会员等级。支持多个等级","分隔
        /// </summary>
        [XmlElement("member_level")]
        public string MemberLevel { get; set; }

        /// <summary>
        /// 最小提前预订小时按入住时间的23:59:59(一般认为24点)来计算
        /// </summary>
        [XmlElement("min_adv_hours")]
        public long MinAdvHours { get; set; }

        /// <summary>
        /// 首日入住房间数（1-99）
        /// </summary>
        [XmlElement("min_amount")]
        public long MinAmount { get; set; }

        /// <summary>
        /// 最小入住天数（1-365）
        /// </summary>
        [XmlElement("min_days")]
        public long MinDays { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        /// <summary>
        /// RP名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 入住人数
        /// </summary>
        [XmlElement("occupancy")]
        public long Occupancy { get; set; }

        /// <summary>
        /// outHid
        /// </summary>
        [XmlElement("out_hid")]
        public string OutHid { get; set; }

        /// <summary>
        /// outRid
        /// </summary>
        [XmlElement("out_rid")]
        public string OutRid { get; set; }

        /// <summary>
        /// 支付类型 可选值 1：预付 5：前台面付
        /// </summary>
        [XmlElement("payment_type")]
        public long PaymentType { get; set; }

        /// <summary>
        /// 卖家系统的编码或ID
        /// </summary>
        [XmlElement("rate_plan_code")]
        public string RatePlanCode { get; set; }

        /// <summary>
        /// 房价id
        /// </summary>
        [XmlElement("rate_plan_id")]
        public long RatePlanId { get; set; }

        /// <summary>
        /// 卖家自己系统的Code，简称RateCode
        /// </summary>
        [XmlElement("rateplan_code")]
        public string RateplanCode { get; set; }

        /// <summary>
        /// rid
        /// </summary>
        [XmlElement("rid")]
        public long Rid { get; set; }

        /// <summary>
        /// rateplan类型 1为小时房
        /// </summary>
        [XmlElement("rp_type")]
        public string RpType { get; set; }

        /// <summary>
        /// rateplan_id
        /// </summary>
        [XmlElement("rpid")]
        public long Rpid { get; set; }

        /// <summary>
        /// 卖家id
        /// </summary>
        [XmlElement("seller_id")]
        public long SellerId { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        /// <summary>
        /// 每日生效时间默认00:00:00。生效时间＜结束时间
        /// </summary>
        [XmlElement("start_time")]
        public string StartTime { get; set; }

        /// <summary>
        /// 每日生效开始时间（仅时分秒有效）
        /// </summary>
        [XmlElement("start_time_daily")]
        public string StartTimeDaily { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("status")]
        public long Status { get; set; }

        /// <summary>
        /// 卖家。
        /// </summary>
        [XmlElement("vendor")]
        public string Vendor { get; set; }
    }
}
