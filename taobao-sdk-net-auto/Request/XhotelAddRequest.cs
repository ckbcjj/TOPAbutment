using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.add
    /// </summary>
    public class XhotelAddRequest : BaseTopRequest<Top.Api.Response.XhotelAddResponse>
    {
        /// <summary>
        /// 酒店地址。长度不能超过120。不填入会导致不能自动匹配。
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 预订须知。json字段描述：hotelInMountaintop 酒店位于山顶 1在山顶、0不在；needBoat 酒店需要坐船前往 1需要、0不需要；酒店位于景区内 1在景区、0不在；extraBed 加床收费；extraCharge 额外收费；arrivalTime 到店时间；extend 其他补充项
        /// </summary>
        public string BookingNotice { get; set; }

        /// <summary>
        /// 酒店品牌。取值为数字。枚举如下（只给出top30，如果不满足，请联系去啊接口人）：    ruJia("1", "rujiakuaijie", "如家快捷", 1),    qiTian("2", "7 days", "7天连锁", 1),    hanTing("3", "Hanting Inns & Hotels", "汉庭酒店", 1),    geLinHaoTai("4", "Green Tree Inn", "格林豪泰", 1),    jinJiang("5", "Jinjiang Inn", "锦江之星", 1),    su8("6", "Super 8", "速8", 1),    moTai("7", "Motel", "莫泰", 1),    zhouji("8", "InterContinental", "洲际", 4),    budint("9", "Pod Inn", "布丁", 1),    jiuJiu("10", "jiujiuliansuo", "99连锁", 1),    piaoHome("11", "Piao Home Inn", "飘HOME", 1),    juzi("12", "Orange Hotels", "桔子酒店", 1),    yibai("13", "yibai", "易佰", 1),    weiyena("14","weiyena","维也纳",2),    huangguanjiari("15", "huangguanjiari", "皇冠假日", 4),    xidawu("16", "xidawu", "喜达屋", 3),    chengshiBJ("17", "chengshibianjie", "城市便捷", 1),    shagnKeYou("18", "shagnkeyou", "尚客优", 1),    jinjiang("19", "jinjiang", "锦江酒店", 3),    wendemu("20", "Hawthorn Suites", "温德姆", 4),    yibisi("21", "Ibis Hotels", "宜必思", 1),    wanhao("22", "JM Hoteles", "万豪", 4),    yijia365("23", "yijia365", "驿家365", 1),    shoulv("24", "shoulvjituan", "首旅建国", 3),    kaiyuan("25", "New Century Hotel", "开元大酒店", 4),    yagao("26", "yagao", "雅高", 3),    daisi("27", "daisi", "戴斯", 3),    jinling("28", "jinlingliansuo", "金陵", 4),    xianggelila("29", "Shangri-La City Hotels", "香格里拉", 4),    xierdun("30", "Hilton", "希尔顿", 4),
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 商业区（圈）长度不超过20字
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        /// 城市编码。参见：http://hotel.alitrip.com/area.htm，domestic为false时，输入对应国家的海外城市编码，可调用海外城市查询接口获取；（更新酒店时为可选）
        /// </summary>
        public Nullable<long> City { get; set; }

        /// <summary>
        /// domestic为0时，固定China； domestic为1时，必须传定义的海外国家编码值。参见：http://hotel.alitrip.com/area.htm
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 逗号分隔的字符串 1visa；2万事达卡；3美国运通卡；4发现卡；5大来卡；6JCB卡；7银联卡
        /// </summary>
        public string CreditCardTypes { get; set; }

        /// <summary>
        /// 装修时间，格式为2015-01-01装修时间
        /// </summary>
        public string DecorateTime { get; set; }

        /// <summary>
        /// 酒店描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 区域（县级市）编码。参见：http://hotel.alitrip.com/area.htm
        /// </summary>
        public Nullable<long> District { get; set; }

        /// <summary>
        /// 是否国内酒店。0:国内;1:国外。默认是国内
        /// </summary>
        public Nullable<long> Domestic { get; set; }

        /// <summary>
        /// 扩展信息的JSON。注：此字段的值需要ISV在接入前与淘宝沟通，且确认能解析
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        /// 楼层信息。
        /// </summary>
        public string Floors { get; set; }

        /// <summary>
        /// 酒店设施。json格式示例值：{"free Wi-Fi in all rooms":"true","massage":"true","meetingRoom":"true"}目前支持维护的设施枚举有：free Wi-Fi in all rooms 所有房间设有免费无线网络;meetingRoom 会议室;massage  按摩室;fitnessClub 健身房;bar 酒吧;cafe 咖啡厅;frontDeskSafe 前台贵重物品保险柜wifi 无线上网公共区域;casino 娱乐场/棋牌室;restaurant 餐厅;smoking area 吸烟区;Business Facilities 商务设施
        /// </summary>
        public string HotelFacilities { get; set; }

        /// <summary>
        /// 酒店入住政策(针对国际酒店，儿童及加床信息)格式：{"children_age_from":"2","children_age_to":"3","children_stay_free":"True","infant_age":"1","min_guest_age":"4"}
        /// </summary>
        public string HotelPolicies { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开业时间，格式为2015-01-01
        /// </summary>
        public string OpeningTime { get; set; }

        /// <summary>
        /// 扩展信息的JSON。 orbitTrack 业务字段是指从飞猪到酒店说经过平台名以及方式的一个数组，按顺序，从飞猪，再经过若干平台，最后到酒店， platform是指定当前平台名，ways 是指通过哪种方式到该平台 其中，飞猪到下一个平台里, ways 字段只能是【直连】、【人工】两个方式之一； 从最后一个平台到酒店的ways字段只能是【电话】、【传真】、【人工】、【系统】之一； 第一个 飞猪平台 和 最后具体酒店是至少得填的
        /// </summary>
        public string OrbitTrack { get; set; }

        /// <summary>
        /// 外部酒店ID, 这是卖家自己系统中的ID
        /// </summary>
        public string OuterId { get; set; }

        /// <summary>
        /// 酒店图片只支持远程图片，格式如下：[{"url":"http://123.jpg","ismain":"false","type":"大堂","attribute":"普通图"},{"url":"http://456.jpg","ismain":"true","type":"公共区域","attribute":"全景图"},{"url":"http://789.jpg","ismain":"false","type":"大堂","attribute":"普通图"}] 其中url是远程图片的访问地址（URL地址必须是合法的，否则会报错），main是是否为主图（主图只能有一个，如果有多个或者没有，则会报错）,attribute表示图片属性，取值范围只能是：[普通图, 平面图, 全景图] ,type表示图片类型，取值范围只能是：[周边, 外观, 商务中心, 健身房, 其他, 会议室, 餐厅, 浴室, 客房, 公共区域, 娱乐设施, 大堂, 泳池]，图片数量最多是能是10张。
        /// </summary>
        public string Pics { get; set; }

        /// <summary>
        /// 坐标类型，现在支持：G – GoogleB – 百度A – 高德M – MapbarL – 灵图
        /// </summary>
        public string PositionType { get; set; }

        /// <summary>
        /// 邮政编码。
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 省份编码。选填，不填入的时候已city字段为准.参见：http://hotel.alitrip.com/area.htm，domestic为false时默认为0
        /// </summary>
        public Nullable<long> Province { get; set; }

        /// <summary>
        /// 房间的基础设施。json格式示例值：{"bathtub":"true","bathPub":"true"}目前支持维护的设施枚举有：bathtub 独立卫浴;bathPub 公共卫浴
        /// </summary>
        public string RoomFacilities { get; set; }

        /// <summary>
        /// 房间数 0~9999之内的数字
        /// </summary>
        public Nullable<long> Rooms { get; set; }

        /// <summary>
        /// 酒店基础服务。json格式示例值：{"receiveForeignGuests":"true","morningCall":"true","breakfast":"true"}目前支持维护的设施枚举有：receiveForeignGuests 接待外宾;morningCall 叫醒服务; breakfast  早餐服务; airportShuttle 接机服务; luggageClaim 行李寄存; rentCar 租车; HourRoomService24 24小时客房服务; airportTransfer 酒店/机场接送; dryCleaning 干洗; expressCheckInCheckOut 快速入住/退房登记; custodyServices 保管服务
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// 该字段只有确定的时候，才允许填入。用于标示和淘宝酒店的匹配关系。目前尚未启动该字段。
        /// </summary>
        public Nullable<long> Shid { get; set; }

        /// <summary>
        /// 酒店档次，星级。取值范围为1,1.5,2,2.5,3,3.5,4,4.5,5
        /// </summary>
        public string Star { get; set; }

        /// <summary>
        /// 酒店电话。格式：国家代码（最长6位）#区号（最长4位）#电话（最长20位）。国家代码提示：中国大陆0086、香港00852、澳门00853、台湾00886
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 酒店曾用名
        /// </summary>
        public string UsedName { get; set; }

        /// <summary>
        /// 对接系统商名称：可为空不要乱填，需要申请后使用
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("address", this.Address);
            parameters.Add("booking_notice", this.BookingNotice);
            parameters.Add("brand", this.Brand);
            parameters.Add("business", this.Business);
            parameters.Add("city", this.City);
            parameters.Add("country", this.Country);
            parameters.Add("credit_card_types", this.CreditCardTypes);
            parameters.Add("decorate_time", this.DecorateTime);
            parameters.Add("description", this.Description);
            parameters.Add("district", this.District);
            parameters.Add("domestic", this.Domestic);
            parameters.Add("extend", this.Extend);
            parameters.Add("floors", this.Floors);
            parameters.Add("hotel_facilities", this.HotelFacilities);
            parameters.Add("hotel_policies", this.HotelPolicies);
            parameters.Add("latitude", this.Latitude);
            parameters.Add("longitude", this.Longitude);
            parameters.Add("name", this.Name);
            parameters.Add("opening_time", this.OpeningTime);
            parameters.Add("orbit_track", this.OrbitTrack);
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("pics", this.Pics);
            parameters.Add("position_type", this.PositionType);
            parameters.Add("postal_code", this.PostalCode);
            parameters.Add("province", this.Province);
            parameters.Add("room_facilities", this.RoomFacilities);
            parameters.Add("rooms", this.Rooms);
            parameters.Add("service", this.Service);
            parameters.Add("shid", this.Shid);
            parameters.Add("star", this.Star);
            parameters.Add("tel", this.Tel);
            parameters.Add("used_name", this.UsedName);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("address", this.Address);
            RequestValidator.ValidateMaxLength("address", this.Address, 120);
            RequestValidator.ValidateMaxLength("booking_notice", this.BookingNotice, 2000);
            RequestValidator.ValidateMaxLength("business", this.Business, 20);
            RequestValidator.ValidateRequired("city", this.City);
            RequestValidator.ValidateMaxLength("country", this.Country, 30);
            RequestValidator.ValidateMaxLength("extend", this.Extend, 500);
            RequestValidator.ValidateMaxLength("floors", this.Floors, 32);
            RequestValidator.ValidateMaxLength("latitude", this.Latitude, 10);
            RequestValidator.ValidateMaxLength("longitude", this.Longitude, 10);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 128);
            RequestValidator.ValidateRequired("outer_id", this.OuterId);
            RequestValidator.ValidateMaxLength("outer_id", this.OuterId, 64);
            RequestValidator.ValidateMaxLength("postal_code", this.PostalCode, 20);
            RequestValidator.ValidateMaxValue("rooms", this.Rooms, 9999);
            RequestValidator.ValidateMinValue("rooms", this.Rooms, 1);
            RequestValidator.ValidateMaxLength("star", this.Star, 3);
            RequestValidator.ValidateRequired("tel", this.Tel);
            RequestValidator.ValidateMaxLength("tel", this.Tel, 30);
            RequestValidator.ValidateMaxLength("used_name", this.UsedName, 60);
            RequestValidator.ValidateMaxLength("vendor", this.Vendor, 50);
        }

        #endregion
    }
}
