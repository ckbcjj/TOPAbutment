using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity.OrderEntity;
using Taobao.Top2.DataAccess.Implement;
using Taobao.Top2.DataAccess;
using Common.Tool;
using Top.Api.Request;
using Taobao.Top2.Application.Common;
using System.Xml.Linq;
using Taobao.Top2.Entity.TaobaoEntity;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Threading.Tasks;

namespace Taobao.Top2.Application.Implement
{
    public class OrderUpload : IOrderUpload
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IOrderData OrderData = DataFactory.CreateOrderData();

        /// <summary>
        /// 获取酒店信息（多）
        /// </summary>
        private DataTable GetHotelsByIds(List<string> hid)
        {
            DataTable dt = OrderData.GetHotelById(hid);
            return dt;
        }

        /// <summary>
        /// 获取在售酒店信息（多）
        /// </summary>
        private DataTable GetSaleHotelsByIds(List<string> hotelid)
        {
            DataTable dt = OrderData.GetSaleHotelById(hotelid);
            return dt;
        }

        /// <summary>
        /// 获取房型信息（多）
        /// </summary>
        private DataTable GetRoomsByIds(List<string> rpid)
        {
            DataTable dt = OrderData.GetRoomsById(rpid);
            return dt;
        }

        /// <summary>
        /// 获取在售房型（多）
        /// </summary>
        private DataTable GetSaleRoomsByIds(List<string> hotelid, List<string> roomid)
        {
            DataTable dt = OrderData.GetSaleRoomById(hotelid, roomid);
            return dt;
        }


        /// <summary>
        /// 酒店初始化
        /// </summary>
        private TaobaoHotel InitHotel(DataRow dr)
        {
            TaobaoHotel hotel = new TaobaoHotel
            {
                qmg_HotelId = Convert.ToInt32(dr["qmg_HotelId"]),
                hid = dr["hid"].ToString()
            };
            return hotel;
        }

        /// <summary>
        /// 房型初始化
        /// </summary>
        private TaobaoRoom InitRoom(DataRow dr)
        {
            TaobaoRoom hotel = new TaobaoRoom
            {
                qmg_hotelid = dr["qmg_hotelid"].ToString() == "" ? 0 : Convert.ToInt32(dr["qmg_hotelid"]),
                qmg_roomid = dr["qmg_roomid"].ToString() == "" ? 0 : Convert.ToInt32(dr["qmg_roomid"]),
                rid = dr["rid"].ToString(),
                hid = dr["hid"].ToString()
            };
            return hotel;
        }

        /// <summary>
        /// 获取淘宝订单信息
        /// </summary>
        /// <returns></returns>
        public List<HotelOrderInfo> GetTaobaoOrders()
        {
            List<x_hotel_order> taobaoOrders = new List<x_hotel_order>();
            List<HotelOrderInfo> hotelOrders = new List<HotelOrderInfo>();
            int pages = 1;
            // System.Configuration.ConfigurationSettings ss = new System.Configuration.ConfigurationSettings();  // ConfigurationSettings["datetime"].ConnectionString.ToString();


            while (true)
            {
                XhotelOrderSearchRequest req = new XhotelOrderSearchRequest();
                //获取前30分钟的订单信息
                req.CreatedStart = DateTime.Now.AddMinutes(-30);
                //req.CreatedStart = DateTime.Now.AddDays(-4);
                req.CreatedEnd = DateTime.Now;
                req.PageNo = pages++;
                var va = TopConfig.Execute(req);
                if (va.IsError == false)
                {
                    try
                    {
                        foreach (XElement x in XDocument.Parse(va.Body).Element("xhotel_order_search_response").Elements("hotel_orders").Elements())
                        {
                            if (x.Element("arrive_early") != null)
                            {
                                x.Element("arrive_early").SetValue(x.Element("arrive_early").Value.Replace(" ", "T"));
                            }
                            if (x.Element("arrive_late") != null)
                            {
                                x.Element("arrive_late").SetValue(x.Element("arrive_late").Value.Replace(" ", "T"));
                            }
                            if (x.Element("checkin_date") != null)
                            {
                                x.Element("checkin_date").SetValue(x.Element("checkin_date").Value.Replace(" ", "T"));
                            }
                            if (x.Element("checkout_date") != null)
                            {
                                x.Element("checkout_date").SetValue(x.Element("checkout_date").Value.Replace(" ", "T"));
                            }
                            if (x.Element("created") != null)
                            {
                                x.Element("created").SetValue(x.Element("created").Value.Replace(" ", "T"));
                            }
                            if (x.Element("pay_time") != null)
                            {
                                x.Element("pay_time").SetValue(x.Element("pay_time").Value.Replace(" ", "T"));
                            }
                            if (x.Element("end_time") != null)
                            {
                                x.Element("end_time").SetValue(x.Element("end_time").Value.Replace(" ", "T"));
                            }
                            if (x.Element("modified") != null)
                            {
                                x.Element("modified").SetValue(x.Element("modified").Value.Replace(" ", "T"));
                            }
                            taobaoOrders.Add((x_hotel_order)XmlHelper.Deserialize(typeof(x_hotel_order), x.ToString().Replace("[", "").Replace("]", "")));
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error("订单序列化错误", e);
                    }
                }
                else
                {
                    try
                    {
                        
                        //上线后用第一行代码  过滤已付款，未发货数据
                        List<x_hotel_order> sendTaobaoOrders = taobaoOrders.Where(model => model.trade_status == "WAIT_SELLER_SEND_GOODS").ToList();

                        //调试代码
                        //string[] s = { "3156411113484983" };//, "3006450810713041", "3029728682178177", "3032553304296261", "2500989379132328", "3032379518658797", "3032785703952492", "3006701015826437", "3032818704574790" };
                        //List<x_hotel_order> sendTaobaoOrders = taobaoOrders.Where(model => model.trade_status == "WAIT_SELLER_SEND_GOODS" && s.Contains(model.tid)).Select(model => model).ToList();
                        //List<x_hotel_order> sendTaobaoOrders = taobaoOrders.Where(model => s.Contains(model.tid)).Select(model => model).ToList();

                        List<string> allSendOrderIdList = sendTaobaoOrders.Select(model => model.tid).ToList();
                        log.Info("全部已付款订单列表：" + string.Join(",", allSendOrderIdList));

                        List<TaobaoHotel> taobaoHotel = new List<TaobaoHotel>();
                        List<TaobaoRoom> taobaoRoom = new List<TaobaoRoom>();

                        //获取上传到淘宝的酒店与房型信息
                        DataTable roomList = GetRoomsByIds(sendTaobaoOrders.Select(model => model.rpid).ToList());

                        Parallel.ForEach(roomList.Select(), new ParallelOptions { MaxDegreeOfParallelism = 10 }, dr =>
                        {
                            taobaoRoom.Add(InitRoom(dr));
                        });

                        hotelOrders = (from model in sendTaobaoOrders
                                       join r in taobaoRoom
                                       on new { rid = model.rid, hid = model.hid }
                                       equals new { rid = r.rid, hid = r.hid }
                                       where r.qmg_hotelid != 0 || r.qmg_roomid != 0
                                       select new HotelOrderInfo
                                       {
                                           OrderId = 0,
                                           PartnerOrderId = model.tid,
                                           HotelId = r.qmg_hotelid,
                                           RoomId = r.qmg_roomid,
                                           Gid = model.gid,
                                           RPid = model.rpid,
                                           Tid = model.tid,
                                           RoomNum = model.room_number,
                                           Days = model.nights,
                                           Type = model.type,
                                           Sdate = model.checkin_date,
                                           Edate = model.checkout_date,
                                           Price = model.total_room_price,
                                           Payment = model.payment,
                                           SellerNick = model.seller_nick,
                                           BuyerNick = model.buyer_nick,
                                           Status = 0,
                                           Contactor = model.contact_name,
                                           Mobile = model.contact_phone,
                                           iDate = model.created,
                                           Modified = model.modified,
                                           PayTime = model.pay_time,
                                           guests = model.guests,
                                           hotelDesc = model.message,
                                           AlipayTradeNO = model.alipay_trade_no,
                                           OutOid = model.out_oid,
                                           Lastarrtime = model.arrive_late.Hour,
                                           prices = model.prices,
                                           Webfrom = "taobao"
                                           ,Restcard = 80018
                                       }).ToList();

                        var hotelIds = (from model in hotelOrders
                                        select new
                                        {
                                            model.HotelId
                                        }).ToList();


                        //获取在售酒店信息
                        List<string> saleHotel = new List<string>();
                        DataTable saleHotelIdList = GetSaleHotelsByIds(hotelOrders.Select(model => model.HotelId.ToString()).ToList());

                        Parallel.ForEach(saleHotelIdList.Select(), new ParallelOptions { MaxDegreeOfParallelism = 10 }, dr =>
                        {
                            saleHotel.Add(dr.ItemArray[0].ToString());
                        });


                        //获取在售房型信息
                        List<string> saleRooms = new List<string>();
                        DataTable saleRoomIdList = GetSaleRoomsByIds(saleHotel, hotelOrders.Select(model => model.RoomId.ToString()).ToList());

                        Parallel.ForEach(saleRoomIdList.Select(), new ParallelOptions { MaxDegreeOfParallelism = 10 }, dr =>
                        {
                            saleRooms.Add(dr.ItemArray[0].ToString());
                        });


                        hotelOrders = hotelOrders.Where(model => saleRooms.Contains(model.RoomId.ToString())).ToList();


                        return hotelOrders;
                    }
                    catch (Exception e)
                    {
                        log.Error("订单数据规整错误", e);
                        return hotelOrders;
                    }
                }
            }
        }


        /// <summary>
        /// 酒店订单发货
        /// </summary>
        /// <param name="hotelOrderList"></param>
        public void SetTaobaoOrderStart(List<HotelOrderInfo> hotelOrderList)
        {

            foreach (HotelOrderInfo order in hotelOrderList)
            {
                string sql = @"select count(*) from hotelorders where torderid='" + order.PartnerOrderId + "' and ProcStatus in (1,4,5,6,7,8)";
                DataTable roomdatatable = null;
                try
                {
                    roomdatatable = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStr);
                    //规避重复订单问题
                    if (int.Parse(roomdatatable.Rows[0].ItemArray[0].ToString()) > 0)
                    //if (true)
                    {
                        XhotelOrderUpdateRequest req = new XhotelOrderUpdateRequest();
                        req.Tid = long.Parse(order.Tid);
                        req.OptType = 2;
                        var va = TopConfig.Execute(req);
                        if (va.IsError)
                        {
                            log.Error("更新订单错误 ErrCode:" + va.ErrCode + "  ErrMsg:" + va.ErrMsg + "  SubErrCode:" + va.SubErrCode + " SubErrMsg:" + va.SubErrMsg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("获取订单信息异常 订单号："+order.PartnerOrderId, ex);
                }
            }
        }

        /// <summary>
        /// 生成新订单(返回订单列表)
        /// </summary>
        /// <param name="hotelOrderInfo"></param>
        /// <returns>返回执行成功的订单信息</returns>
        public List<HotelOrderInfo> AddNewOrder(List<HotelOrderInfo> hotelOrderInfoList)
        {
            List<HotelOrderInfo> modeList = new List<HotelOrderInfo>();
            try
            {
                foreach (HotelOrderInfo order in hotelOrderInfoList)
                {
                    string sql = @"select count(*) from hotelorders where torderid='" + order.PartnerOrderId + "'";
                    DataTable roomdatatable = null;

                    roomdatatable = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStr);
                    if (roomdatatable != null && int.Parse(roomdatatable.Rows[0].ItemArray[0].ToString()) == 0)
                    //if (true)
                    {
                        int orderId = OrderData.AddNewOrder(order);
                        if (orderId != 0)
                        {
                            order.OrderId = orderId;
                            modeList.Add(order);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("生成新订单异常", e);
            }
            return modeList;
        }

        /// <summary>
        /// 生成房型数据并更新订单数据
        /// </summary>
        /// <param name="hotelOrderInfo"></param>
        /// <returns>返回执行成功的订单信息</returns>
        public List<HotelOrderInfo> AddOrderRoom(List<HotelOrderInfo> hotelOrderInfoList)
        {
            List<HotelOrderInfo> modeList = new List<HotelOrderInfo>();
            try
            {
                foreach (HotelOrderInfo order in hotelOrderInfoList)
                {
                    modeList.Add(OrderData.AddOrderRoom(order));
                }
            }
            catch (Exception e)
            {
                log.Error("生成房型数据并更新订单数据异常", e);
            }
            return modeList;
        }
    }
}
