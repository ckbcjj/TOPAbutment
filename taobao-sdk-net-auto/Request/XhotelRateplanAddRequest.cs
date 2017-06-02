using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rateplan.add
    /// </summary>
    public class XhotelRateplanAddRequest : BaseTopRequest<Top.Api.Response.XhotelRateplanAddResponse>
    {
        /// <summary>
        /// 废弃。价格类型字段：0.非协议价；1.集采协议价；如果不是协议价，请不要填写该字段。该字段有权限控制，如需使用，请联系阿里旅行运营。 如果不填写或者填写为0，默认是阿里旅行价
        /// </summary>
        public Nullable<long> Agreement { get; set; }

        /// <summary>
        /// base rp标记，1是；0否
        /// </summary>
        public Nullable<long> BaseRpFlag { get; set; }

        /// <summary>
        /// 在添加rateplan时，同时新增早餐日历。date：说明这条记录的早餐政策breakfast_count：这一天早餐的数量。>=-1,<=99。如果date为空，那么会去读取startDate和endDate（格式都为"yyyy-MM-dd"），即早餐正常属于一个时间段。-1为状态早餐，和最终绑定的几人价有关，如果是一人价那么就是我一份早餐，二人价就是两份早餐。请注意，该字段仅能维护从当前时间开始，10年以内的数据，如果超过10年，会报错。
        /// </summary>
        public string BreakfastCal { get; set; }

        /// <summary>
        /// -1：状态早餐,有具体几人价有关系，几人价是几份早餐;0：不含早1：含单早2：含双早N：含N早（-1-99可选）
        /// </summary>
        public Nullable<long> BreakfastCount { get; set; }

        /// <summary>
        /// 最早可选入住时间，小时房特有字段。格式为HH:mm
        /// </summary>
        public string CanCheckinEnd { get; set; }

        /// <summary>
        /// 最晚可选入住时间，小时房特有字段。格式为HH:mm
        /// </summary>
        public string CanCheckinStart { get; set; }

        /// <summary>
        /// 不推荐使用，使用改规则
        /// </summary>
        public Nullable<long> CancelBeforeDay { get; set; }

        /// <summary>
        /// 不推荐使用，使用改规则
        /// </summary>
        public string CancelBeforeHour { get; set; }

        /// <summary>
        /// 退订政策字段，是个json串，参考示例值设置改字段的值。允许变更/取消：在XX年XX月XX日XX时前取消收取Y%的手续费，100>Y>=0允许变更/取消：在入住前X小时前取消收取Y%的手续费，100>Y>=0（不超过10条）。1.表示任意退{"cancelPolicyType":1};2.表示不能退{"cancelPolicyType":2};4.从入住当天24点往前推X小时前取消收取Y%手续费，否则不可取消{"cancelPolicyType":4,"policyInfo":{"48":10,"24":20}}表示，从入住日24点往前推提前至少48小时取消，收取10%的手续费，从入住日24点往前推提前至少24小时取消，收取20%的手续费;5.从24点往前推多少小时可退{"cancelPolicyType":5,"policyInfo":{"timeBefore":6}}表示从入住日24点往前推至少6个小时即入住日18点前可免费取消;6.从入住日24点往前推，至少提前小时数扣取首晚房费{"cancelPolicyType":6,"policyInfo":{"14":1}}表示入住日24点往前推14小时，即入住日10点前取消收取首晚房费。 注意：支付类型为预付，那么可以使用所有的退订类型,但是必须是非担保；支付类型为面付或者信任住并且是无担保，那么只能使用1类型的退订；支付类型为面付或者信任住并且为担保，那么只能使用2,5类型的退订；支付类型为在线预约，那么只能使用1,2,5类型的退改。如果支付类型是面付或者信任住并且为担保，那么如果传了4或者6的退订，那么会强制转成类型5。
        /// </summary>
        public string CancelPolicy { get; set; }

        /// <summary>
        /// 在新增rateplan的同时新增取消政策日历。 json格式。 date：日历的某一天，格式为"yyyy-MM-dd" cancel_policy：日历某一天的价格政策。格式和限制同cancel_policy。 如果date为空，那么会读取startDate和endDate（格式都为"yyyy-MM-dd"），即取消政策属于某一个时间段。 注意：支付类型为预付，那么可以使用所有的退订类型，但是必须是非担保；支付类型为面付或者信任住并且是无担保，那么只能使用1类型的退订；支付类型为面付或者信任住并且为担保，那么只能使用2,5类型的退订；支付类型为在线预约，那么只能使用1,2,5类型的退改。如果支付类型是面付或者信任住并且为担保，那么如果传了4或者6的退订，那么会强制转成类型5。请注意，该字段仅能维护从当前时间开始，10年以内的数据，如果超过10年，会报错。
        /// </summary>
        public string CancelPolicyCal { get; set; }

        /// <summary>
        /// 销售渠道。如需开通，需要申请权限。目前支持的渠道有 H:飞猪 O:钉钉商旅 A:集团内部商旅。如果只投放飞猪，改字段不用填写或者只填H；如果有多个用","分开。如果需要投放其他渠道，请联系飞猪运营或者技术支持。
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 生效截止时间，用来控制此rateplan生效的截止时间，配合字段effective_time一起限定rp的有效期
        /// </summary>
        public Nullable<DateTime> DeadlineTime { get; set; }

        /// <summary>
        /// 餐食描述
        /// </summary>
        public string DinningDesc { get; set; }

        /// <summary>
        /// 生效开始时间，用来控制此rateplan生效的开始时间，配合字段deadline_time一起限定rp的有效期
        /// </summary>
        public Nullable<DateTime> EffectiveTime { get; set; }

        /// <summary>
        /// 产品每日结束销售时间,当end_time<start_time时，表示end_time为第二天，此时附加限制end_time<=06:00:00并且start_time>=12:00:00,表明可售时间从当天12点到次日的凌晨6点（扩展此信息主要为了描述尾房的rp）注意start_time一定是当天的时间。尾房18：00起可售
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// RP的英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 个性化定制扩展信息的JSON。注：此字段的值需要ISV在接入前与淘宝沟通，且确认能解析
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        /// 不推荐使用
        /// </summary>
        public string ExtendFee { get; set; }

        /// <summary>
        /// 不推荐使用
        /// </summary>
        public Nullable<long> FeeBreakfastAmount { get; set; }

        /// <summary>
        /// 废弃
        /// </summary>
        public Nullable<long> FeeBreakfastCount { get; set; }

        /// <summary>
        /// 不推荐使用
        /// </summary>
        public Nullable<long> FeeGovTaxAmount { get; set; }

        /// <summary>
        /// 不推荐使用
        /// </summary>
        public Nullable<long> FeeGovTaxPercent { get; set; }

        /// <summary>
        /// 不推荐使用
        /// </summary>
        public Nullable<long> FeeServiceAmount { get; set; }

        /// <summary>
        /// 不推荐使用
        /// </summary>
        public Nullable<long> FeeServicePercent { get; set; }

        /// <summary>
        /// 需申请会员权限。是否是新用户首住优惠rp。1-代表是。0或者不填写代表否
        /// </summary>
        public Nullable<long> FirstStay { get; set; }

        /// <summary>
        /// 在新增rateplan的同时，新增担保日历。date：担保日历的某一天。guarantee:担保政策。其中有两个属性：guaranteeType,guaranteeStartTime。 guaranteeType的可选值同guaranteeType字段，详见guaranteeType字段。guaranteeStartTime格式为"HH:mm"。如果date为空，那么会读取startDate和endDate（格式都为"yyyy-MM-dd"），即担保政策属于某一个时间段。（如果设置了峰时担保类型，那么峰时担保时间不能为空，并且必须大于等于8点）。请注意，该字段仅能维护从当前时间开始，10年以内的数据，如果超过10年，会报错。
        /// </summary>
        public string GuaranteeCal { get; set; }

        /// <summary>
        /// 0支付宝担保，1 PCI担保
        /// </summary>
        public Nullable<long> GuaranteeMode { get; set; }

        /// <summary>
        /// 分时担保每日开始担保时间。 （如果设置了峰时担保类型，那么峰时担保时间不能为空，并且必须大于等于8点）
        /// </summary>
        public string GuaranteeStartTime { get; set; }

        /// <summary>
        /// 担保类型，只支持： 0  无担保  1  峰时首晚担保 2峰时全额担保 3全天首晚担保 4全天全额担保
        /// </summary>
        public Nullable<long> GuaranteeType { get; set; }

        /// <summary>
        /// 酒店id
        /// </summary>
        public Nullable<long> Hid { get; set; }

        /// <summary>
        /// 小时房入住时间跨度。小时房特有字段。比如4小时的小时房，那么值为4
        /// </summary>
        public string Hourage { get; set; }

        /// <summary>
        /// 是否学生价，0：否；1：是。
        /// </summary>
        public Nullable<long> IsStudent { get; set; }

        /// <summary>
        /// 最大提前预定小时数，从入住当天的24点往前计算。例如如果这个字段设置了48，代表最多提前两天预定，那么如果想预定24号入住，,必须在23号零点以后下单。
        /// </summary>
        public Nullable<long> MaxAdvHours { get; set; }

        /// <summary>
        /// 儿童最大年龄(0-18)
        /// </summary>
        public Nullable<long> MaxChildAge { get; set; }

        /// <summary>
        /// 最大入住天数（1-90）。默认90
        /// </summary>
        public Nullable<long> MaxDays { get; set; }

        /// <summary>
        /// 婴儿最大年龄(0-18)
        /// </summary>
        public Nullable<long> MaxInfantAge { get; set; }

        /// <summary>
        /// 双方映射后的会员等级。如需开通，需要申请权限，取值范围为：1,2,3,4,5,none。比如飞猪F3对应商家V4,则传4.（如果有疑问请联系对接技术支持）
        /// </summary>
        public string MemberLevel { get; set; }

        /// <summary>
        /// 最小提前预定小时数，从入住当天的24点往前计算。例如如果这个字段设置了48，代表必须至少提前两天预定，那么如果想预定24号入住，,必须在23号零点前下单。
        /// </summary>
        public Nullable<long> MinAdvHours { get; set; }

        /// <summary>
        /// 首日入住房间数（1-99）。默认1。不推荐使用
        /// </summary>
        public Nullable<long> MinAmount { get; set; }

        /// <summary>
        /// 儿童最小年龄(0-18)
        /// </summary>
        public Nullable<long> MinChildAge { get; set; }

        /// <summary>
        /// 最小入住天数（1-90）。默认1
        /// </summary>
        public Nullable<long> MinDays { get; set; }

        /// <summary>
        /// 婴儿最小年龄(0-18)
        /// </summary>
        public Nullable<long> MinInfantAge { get; set; }

        /// <summary>
        /// 在淘宝搜索页面展示的房价名称。请注意名称里不要维护早餐信息，如果想设置早餐信息，请设置breakfast_count字段即可
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 不推荐使用
        /// </summary>
        public Nullable<long> Occupancy { get; set; }

        /// <summary>
        /// 外部酒店id
        /// </summary>
        public string OutHid { get; set; }

        /// <summary>
        /// 外部房型id
        /// </summary>
        public string OutRid { get; set; }

        /// <summary>
        /// 支付类型，只支持：1：预付5：现付6: 信用住7:在线预约。其中5,6,7三种类型需要申请权限
        /// </summary>
        public Nullable<long> PaymentType { get; set; }

        /// <summary>
        /// 卖家自己系统的Code，简称RateCode
        /// </summary>
        public string RateplanCode { get; set; }

        /// <summary>
        /// 房型id
        /// </summary>
        public Nullable<long> Rid { get; set; }

        /// <summary>
        /// rp类型，1为小时房。目前只支持小时房。如果不是小时房rateplan,则不要填写。如果想要清空该字段可以传入none
        /// </summary>
        public string RpType { get; set; }

        /// <summary>
        /// 产品每日开始销售时间，start_time一定为当天时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 1：开启（默认）2：关闭。如果没传值那么默认默认值为1
        /// </summary>
        public Nullable<long> Status { get; set; }

        /// <summary>
        /// super rp标记，1是；0否
        /// </summary>
        public Nullable<long> SuperRpFlag { get; set; }

        /// <summary>
        /// 系统商，一般不填写，使用须申请
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rateplan.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("agreement", this.Agreement);
            parameters.Add("base_rp_flag", this.BaseRpFlag);
            parameters.Add("breakfast_cal", this.BreakfastCal);
            parameters.Add("breakfast_count", this.BreakfastCount);
            parameters.Add("can_checkin_end", this.CanCheckinEnd);
            parameters.Add("can_checkin_start", this.CanCheckinStart);
            parameters.Add("cancel_before_day", this.CancelBeforeDay);
            parameters.Add("cancel_before_hour", this.CancelBeforeHour);
            parameters.Add("cancel_policy", this.CancelPolicy);
            parameters.Add("cancel_policy_cal", this.CancelPolicyCal);
            parameters.Add("channel", this.Channel);
            parameters.Add("deadline_time", this.DeadlineTime);
            parameters.Add("dinning_desc", this.DinningDesc);
            parameters.Add("effective_time", this.EffectiveTime);
            parameters.Add("end_time", this.EndTime);
            parameters.Add("english_name", this.EnglishName);
            parameters.Add("extend", this.Extend);
            parameters.Add("extend_fee", this.ExtendFee);
            parameters.Add("fee_breakfast_amount", this.FeeBreakfastAmount);
            parameters.Add("fee_breakfast_count", this.FeeBreakfastCount);
            parameters.Add("fee_gov_tax_amount", this.FeeGovTaxAmount);
            parameters.Add("fee_gov_tax_percent", this.FeeGovTaxPercent);
            parameters.Add("fee_service_amount", this.FeeServiceAmount);
            parameters.Add("fee_service_percent", this.FeeServicePercent);
            parameters.Add("first_stay", this.FirstStay);
            parameters.Add("guarantee_cal", this.GuaranteeCal);
            parameters.Add("guarantee_mode", this.GuaranteeMode);
            parameters.Add("guarantee_start_time", this.GuaranteeStartTime);
            parameters.Add("guarantee_type", this.GuaranteeType);
            parameters.Add("hid", this.Hid);
            parameters.Add("hourage", this.Hourage);
            parameters.Add("is_student", this.IsStudent);
            parameters.Add("max_adv_hours", this.MaxAdvHours);
            parameters.Add("max_child_age", this.MaxChildAge);
            parameters.Add("max_days", this.MaxDays);
            parameters.Add("max_infant_age", this.MaxInfantAge);
            parameters.Add("member_level", this.MemberLevel);
            parameters.Add("min_adv_hours", this.MinAdvHours);
            parameters.Add("min_amount", this.MinAmount);
            parameters.Add("min_child_age", this.MinChildAge);
            parameters.Add("min_days", this.MinDays);
            parameters.Add("min_infant_age", this.MinInfantAge);
            parameters.Add("name", this.Name);
            parameters.Add("occupancy", this.Occupancy);
            parameters.Add("out_hid", this.OutHid);
            parameters.Add("out_rid", this.OutRid);
            parameters.Add("payment_type", this.PaymentType);
            parameters.Add("rateplan_code", this.RateplanCode);
            parameters.Add("rid", this.Rid);
            parameters.Add("rp_type", this.RpType);
            parameters.Add("start_time", this.StartTime);
            parameters.Add("status", this.Status);
            parameters.Add("super_rp_flag", this.SuperRpFlag);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("breakfast_count", this.BreakfastCount);
            RequestValidator.ValidateMaxValue("breakfast_count", this.BreakfastCount, 99);
            RequestValidator.ValidateMinValue("breakfast_count", this.BreakfastCount, -1);
            RequestValidator.ValidateMaxLength("cancel_before_hour", this.CancelBeforeHour, 50);
            RequestValidator.ValidateRequired("cancel_policy", this.CancelPolicy);
            RequestValidator.ValidateMaxLength("cancel_policy", this.CancelPolicy, 500);
            RequestValidator.ValidateMaxLength("channel", this.Channel, 20);
            RequestValidator.ValidateMaxLength("end_time", this.EndTime, 5);
            RequestValidator.ValidateMaxLength("english_name", this.EnglishName, 60);
            RequestValidator.ValidateMaxLength("extend", this.Extend, 500);
            RequestValidator.ValidateMaxLength("extend_fee", this.ExtendFee, 500);
            RequestValidator.ValidateMaxValue("fee_breakfast_amount", this.FeeBreakfastAmount, 999999);
            RequestValidator.ValidateMinValue("fee_breakfast_amount", this.FeeBreakfastAmount, 1);
            RequestValidator.ValidateMaxValue("fee_breakfast_count", this.FeeBreakfastCount, 99);
            RequestValidator.ValidateMinValue("fee_breakfast_count", this.FeeBreakfastCount, 0);
            RequestValidator.ValidateMaxValue("fee_gov_tax_amount", this.FeeGovTaxAmount, 999999);
            RequestValidator.ValidateMinValue("fee_gov_tax_amount", this.FeeGovTaxAmount, 0);
            RequestValidator.ValidateMaxValue("fee_gov_tax_percent", this.FeeGovTaxPercent, 99);
            RequestValidator.ValidateMinValue("fee_gov_tax_percent", this.FeeGovTaxPercent, 0);
            RequestValidator.ValidateMaxValue("fee_service_amount", this.FeeServiceAmount, 9999);
            RequestValidator.ValidateMinValue("fee_service_amount", this.FeeServiceAmount, 0);
            RequestValidator.ValidateMaxValue("fee_service_percent", this.FeeServicePercent, 99);
            RequestValidator.ValidateMinValue("fee_service_percent", this.FeeServicePercent, 0);
            RequestValidator.ValidateMaxValue("max_adv_hours", this.MaxAdvHours, 2160);
            RequestValidator.ValidateMinValue("max_adv_hours", this.MaxAdvHours, 0);
            RequestValidator.ValidateMaxValue("max_child_age", this.MaxChildAge, 18);
            RequestValidator.ValidateMinValue("max_child_age", this.MaxChildAge, 0);
            RequestValidator.ValidateMaxValue("max_days", this.MaxDays, 90);
            RequestValidator.ValidateMinValue("max_days", this.MaxDays, 1);
            RequestValidator.ValidateMaxValue("max_infant_age", this.MaxInfantAge, 18);
            RequestValidator.ValidateMinValue("max_infant_age", this.MaxInfantAge, 0);
            RequestValidator.ValidateMaxLength("member_level", this.MemberLevel, 20);
            RequestValidator.ValidateMaxValue("min_adv_hours", this.MinAdvHours, 2160);
            RequestValidator.ValidateMinValue("min_adv_hours", this.MinAdvHours, 0);
            RequestValidator.ValidateMaxValue("min_amount", this.MinAmount, 99);
            RequestValidator.ValidateMinValue("min_amount", this.MinAmount, 1);
            RequestValidator.ValidateMaxValue("min_child_age", this.MinChildAge, 18);
            RequestValidator.ValidateMinValue("min_child_age", this.MinChildAge, 0);
            RequestValidator.ValidateMaxValue("min_days", this.MinDays, 90);
            RequestValidator.ValidateMinValue("min_days", this.MinDays, 1);
            RequestValidator.ValidateMaxValue("min_infant_age", this.MinInfantAge, 18);
            RequestValidator.ValidateMinValue("min_infant_age", this.MinInfantAge, 0);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 60);
            RequestValidator.ValidateMaxValue("occupancy", this.Occupancy, 10);
            RequestValidator.ValidateMinValue("occupancy", this.Occupancy, 1);
            RequestValidator.ValidateMaxLength("out_rid", this.OutRid, 64);
            RequestValidator.ValidateRequired("payment_type", this.PaymentType);
            RequestValidator.ValidateMaxValue("payment_type", this.PaymentType, 7);
            RequestValidator.ValidateMinValue("payment_type", this.PaymentType, 1);
            RequestValidator.ValidateRequired("rateplan_code", this.RateplanCode);
            RequestValidator.ValidateMaxLength("rateplan_code", this.RateplanCode, 64);
            RequestValidator.ValidateMaxLength("start_time", this.StartTime, 5);
            RequestValidator.ValidateRequired("status", this.Status);
            RequestValidator.ValidateMaxLength("vendor", this.Vendor, 50);
        }

        #endregion
    }
}
