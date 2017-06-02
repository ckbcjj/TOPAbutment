using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Taobao.Top2.Entity.TaobaoEntity
{
    /// <summary>
    /// 淘宝订单对应实体
    /// </summary>
    public class x_hotel_order
    {
        /// <summary>
        /// 酒店订单ID
        /// </summary>
        [XmlElement("oid")]
        public string oid { get; set; }
        /// <summary>
        /// 酒店ID
        /// </summary>
        public string hid { get; set; }
        /// <summary>
        /// 房型ID
        /// </summary>
        public string rid { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string gid { get; set; }
        /// <summary>
        /// RatePlan 中的 rpid
        /// </summary>
        public string rpid { get; set; }
        /// <summary>
        /// tid
        /// </summary>
        public string tid { get; set; }
        /// <summary>
        /// 房间数
        /// </summary>
        public int room_number { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int nights { get; set; }
        /// <summary>
        /// 支付类型 可选值 1：预付 5：前台面付
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime checkin_date { get; set; }
        /// <summary>
        /// 离店时间
        /// </summary>
        public DateTime checkout_date { get; set; }
        /// <summary>
        /// 总房价（分）
        /// </summary>
        public int total_room_price { get; set; }
        /// <summary>
        /// 实付款（分）
        /// </summary>
        public int payment { get; set; }
        /// <summary>
        /// 卖家淘宝账号
        /// </summary>
        public string seller_nick { get; set; }
        /// <summary>
        /// 买家淘宝账号
        /// </summary>
        public string buyer_nick { get; set; }
        /// <summary>
        /// 交易状态。 
        /// WAIT_BUYER_PAY:预订中/等待买家付款, 
        /// WAIT_SELLER_SEND_GOODS:预订中/等待卖家发货(确认), 
        /// TRADE_CLOSED:结束/预订失败/交易关闭, 
        /// TRADE_FINISHED:结束/交易成功, 
        /// TRADE_NO_CREATE_PAY:结束/预订失败/没有创建支付宝交易, 
        /// TRADE_CLOSED_BY_TAOBAO:结束/预订失败/预订被卖家关闭, 
        /// TRADE_SUCCESS:交易中/预订成功/卖家已确认, 
        /// TRADE_CHECKIN:交易中/预定成功/买家入住, 
        /// TRADE_CHECKOUT:交易中/预定成功/买家离店, 
        /// TRADE_SETTLEING:交易中/预定成功/结账中, 
        /// TRADE_SETTLE_SUCCESS:结束/预定成功/结账成功
        /// </summary>
        public string trade_status { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string contact_name { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string contact_phone { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime created { get; set; }
        /// <summary>
        /// 订单修改时间
        /// </summary>
        public DateTime modified { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime pay_time { get; set; }
        /// <summary>
        /// 入住人信息
        /// </summary>
        public x_order_guest[] guests { get; set; }
        /// <summary>
        /// 买家留言
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 支付宝交易号，28位字符
        /// </summary>
        public string alipay_trade_no { get; set; }
        /// <summary>
        /// 合作方订单号,最长250个字符
        /// </summary>
        public string out_oid { get; set; }
        /// <summary>
        /// 买家最早到达时间
        /// </summary>
        public DateTime arrive_early { get; set; }
        /// <summary>
        /// 买家最晚到达时间
        /// </summary>
        public DateTime arrive_late { get; set; }
        /// <summary>
        /// 下单时每间夜的价格（分）
        /// </summary>
        [XmlArrayItem("number")]
        public int[] prices { get; set; }
    }

    public class x_order_guest
    {
        /// <summary>
        /// 房间序号
        /// </summary>
        public int room_pos { get; set; }
        /// <summary>
        /// 入住人序号
        /// </summary>
        public int person_pos { get; set; }
        /// <summary>
        /// 入住人姓名
        /// </summary>
        public string name { get; set; }
    }
}
