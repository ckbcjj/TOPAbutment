using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Common.Tool;
using Taobao.Top2.Application.Common;
using Taobao.Top2.DataAccess;
using Taobao.Top2.DataAccess.Implement;
using Taobao.Top2.Entity;
using Taobao.Top2.Entity.TaobaoEntity;
using Top.Api;
using Top.Api.Request;
using System.Data.SqlClient;

namespace Taobao.Top2.Application.Implement
{
    public class ProductUpload : IProductUpload
    {
        private readonly IProductData dal = new ProductData();
        private IRoomData _roomData = DataFactory.CreateRoomData();
        private IHotelData _hotelData = DataFactory.CreateHotelData();
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void UpdataProductHis(TaobaoProduct product)
        {
            dal.UpdataProductHis(product);
        }

        public string GetGidByRpid(string rpid)
        {
            var req = new XhotelRateRelationshipwithroomGetRequest();
            req.RpId = long.Parse(rpid);
            var response = TopConfig.Execute(req);
            return XDocument.Parse(response.Body).XPathSelectElement(".//string").Value;
        }

        /// <summary>
        /// 全量更新价格
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="hotelIds"></param>
        public void PriceUpdate(int[] hotelIds = null)
        {
            try
            {
                UpdateProducts(hotelIds);
            }
            catch (Exception e)
            {
                log.Error("ProductUpload:PriceUpdate 异常", e);
            }
        }

        public void SetLijian(int lijian, int[] hotelid)
        {
            SqlHelper.ExecuteNonQuery(
                string.Format("update Taobao_new_Product_His set lijian={0} where qmg_hotelid in({1})", lijian,
                    string.Join(",", hotelid)), null,
                SqlHelper.conStr);
        }

        private void GetIds()
        {
            var request = new XhotelRoomsIdsGetRequest();
            var time = DateTime.Parse("2015-01-01");
            request.StartDate = time;
            var time2 = DateTime.Parse("2015-04-25");
            request.EndDate = time2;
            request.PageNo = 8L;
        }

        /// <summary>
        /// 得到房价房态（单房型） 获取价格计划
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="lijian"></param>
        /// <returns></returns>
        public string GetRoomStatus(string roomid, double lijianRate, double zengfuRate)
        {
            var table = dal.GetRoomStatus(roomid, lijianRate, zengfuRate);
            return GetRoomStatusIncr(table.Select().ToList());
        }

        /// <summary>
        /// 获取价格，数量全部为0
        /// </summary>
        /// <returns></returns>
        private string SetRoomStatus(int days)
        {
            //房态集合，淘宝要每 天的房态，防止数据源中某天房态信息丢失，（90天内房态）
            Dictionary<DateTime, string[]> dic = new Dictionary<DateTime, string[]>();
            for (int j = 0; j < days; j++)
            {
                dic.Add(DateTime.Now.Date.AddDays(j), null);
            }
            var str = "";
            foreach (var d in dic)
            {
                str = string.Concat(str, "{'date':",
                   d.Key.ToString("yyyy-MM-dd"),
                   ",'price':", 50000, ",'quota':", 0, "},");
            }
            str = str.Substring(0, str.Length - 1);
            return ("[" + str + "]");
        }

        /// <summary>
        /// 判断酒店类型
        /// </summary>
        /// <param name="hotelid"></param>
        /// <returns>true:香港  false:非香港</returns>
        private bool GetHotelType(int hotelid)
        {
            //判断城市是否为香港
            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@hotelId", SqlDbType.Int);
            sp[0].Value = hotelid;
            string sql = @"SELECT province
                           FROM hotels 
                           WHERE hotelid=@hotelId";
            DataTable dt = SqlHelper.ExcuteDataTable(sql, sp, SqlHelper.conStrRead);

            //自签香港酒店
            if (dt != null && dt.Rows.Count > 0)
            {
                //香港
                if (dt.Rows[0].ItemArray[0].ToString() == "32")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断酒店是不是某种酒店
        /// </summary>
        /// <param name="hotelid">酒店ID</param>
        /// <param name="resourceid">resourceid</param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool GetHotelType(int hotelid, int resourceid, string name)
        {
            //判断城市是否为香港
            SqlParameter[] sp = new SqlParameter[3];

            string sql = @"select hotelid from hotels where hotelid=" + hotelid + " and resourceid=" + resourceid + " and  hotelname like '%" + name + "%'";
            DataTable dt = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);

            //自签香港酒店
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 得到房价房态（多房型）
        /// </summary>
        /// <param name="drs"></param>
        /// <returns></returns>
        private string GetRoomStatusIncr(List<DataRow> drs)
        {
            Dictionary<DateTime, string[]> dic = new Dictionary<DateTime, string[]>();
            for (int j = 0; j < 60; j++)
            {
                dic.Add(DateTime.Now.Date.AddDays(j), null);
            }
            if (drs != null && drs.Count != 0)
            {
                foreach (DataRow r in drs)
                {
                    var effectdate = ((DateTime)r["effectdate"]).Date;
                    double originalPrice = double.Parse(r["eprice"].ToString());
                    if (!dic.ContainsKey(effectdate))
                    {
                        continue;
                    }
                    double lijianRate = double.Parse(r["lijian"].ToString());
                    double zengfuRate = double.Parse(r["zengfu"].ToString());
                    double commission = double.Parse(r["commission"].ToString());
                    string numstr = r["num"].ToString();
                    double newPrice = 50000;
                    if (originalPrice == 0)
                    {
                        numstr = "0";
                    }
                    if (numstr != "0")
                    {
                        if (lijianRate != 0)
                        {
                            newPrice = Math.Ceiling(originalPrice - commission * lijianRate / 100) * 100;
                        }
                        else if (zengfuRate != 0)
                        {
                            newPrice = Math.Ceiling((originalPrice - commission) * (1 + zengfuRate / 100)) * 100;
                        }
                        else
                        {
                            newPrice = originalPrice * 100;
                        }
                    }
                    if (effectdate == DateTime.Now.Date && (((int)r["resourceid"] == 113 && r["hotelname"].ToString().Contains("汉庭")) || (int)r["resourceid"] == 113 || (string)r["huserid"] == "botaojituan"))
                    {
                        dic[effectdate] = new[] { "0", newPrice.ToString() };
                    }
                    else
                    {
                        dic[effectdate] = new[] { numstr, newPrice.ToString() };
                    }
                }
                var str = string.Empty;
                foreach (var d in dic)
                {
                    if (d.Value != null)
                    {
                        str = string.Concat(str, "{'date':",
                            d.Key.ToString("yyyy-MM-dd"), ",'price':", d.Value[1], ",'quota':", d.Value[0].StartsWith("-") ? "0" : d.Value[0], "},");
                    }
                }
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Substring(0, str.Length - 1);
                    return ("[" + str + "]");
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// 上传价格及房态
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns></returns>
        private string RateAdd(TaobaoRoom room, TaobaoProduct product)
        {
            try
            {
                var roomId = product.qmg_roomid;
                var zengfu = product.zengfu;
                var lijian = product.lijian;
                 var req = new XhotelRateUpdateRequest
                {
                    //Gid = 100000L,
                    //Rpid = 100000L,
                    RateplanCode = product.rateplan_code,
                    OutRid = room.outer_id,
                    //CurrencyCode = 1L,
                    //ShijiaTag = 1L,
                    //RateSwitchCal = ""
                };
                string priceandstatus = this.GetRoomStatus(roomId, lijian, zengfu);
                if (string.IsNullOrEmpty(priceandstatus))
                {
                    priceandstatus = SetRoomStatus(60);//默认
                }
                req.InventoryPrice = "{\"use_room_inventory\":false,\"inventory_price\":" + priceandstatus + "}";
                var respose = TopConfig.Execute(req);
                if (respose.IsError)
                {
                    log.Error("上传价格及房态失败,roomid:" + roomId + ",ErrCode:" + respose.ErrCode + "  ErrMsg:" + respose.ErrMsg + "  SubErrCode:" + respose.SubErrCode + " SubErrMsg:" + respose.SubErrMsg);
                }
                try
                {
                    return
                        XDocument.Parse(respose.Body).Element("xhotel_rate_update_response").Element("gid_and_rpid").Value;
                }
                catch
                {
                    return respose.Body;
                }
            }
            catch (Exception e)
            {
                log.Error("ProductUpload:RateAdd 异常", e);
                return null;
            }
        }


        /// <summary>
        /// 上传价格计划（早餐，模式，名称等）
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string RatePlanAdd(DataRow dr, TaobaoProduct product)
        {
            var top = TopConfig.GetClient();
            var num = Convert.ToInt64(dr["breakfasts"]);
            var req = new XhotelRateplanAddRequest
            {
                RateplanCode = product.rateplan_code,
                Name = "全额预付+" + ((num > 0L) ? "含早" : "无早"),
                PaymentType = 1L,
                BreakfastCount = Convert.ToInt64(dr["breakfasts"]),
                CancelPolicy = "{\"cancelPolicyType\":2}",
                Status = 1L
            };
            var response = TopConfig.Execute(req);
            if (response.IsError)
            {
                log.Error("上传价格计划失败,rpcode:"+product.rateplan_code+",ErrCode:" + response.ErrCode + "  ErrMsg:" + response.ErrMsg + "  SubErrCode:" + response.SubErrCode + " SubErrMsg:" + response.SubErrMsg);
                return null;
            }
            try
            {
                return XDocument.Parse(response.Body).Element("xhotel_rateplan_add_response").Element("rpid").Value;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 房型商品加入
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string RoomAdd(DataRow dr, TaobaoRoom room)
        {
            var hid = Convert.ToInt64(room.hid);
            var rid = Convert.ToInt64(room.rid);
            var req = new XhotelRoomUpdateRequest
            {
                OutRid = room.outer_id,
                Title =
                    string.Concat(dr["provincename"], "|", dr["hotelname"], "|",
                        dr["roomname"].ToString().Replace("限量团购", ""))
            };
            if (dr["provincename"].ToString().IndexOf("香港") != -1)
            {
                req.Guide =
                    "<p>青芒果旅行网提醒各位预订香港/澳门酒店的买家注意：港澳酒店的星级是公众根据酒店规模、设施、服务及客人入住口碑对该酒店的综合评价，和国内星级酒店情况相差甚远。多数房间面积在7－10平方，双人床宽度在1.2M－1.5M之间，单人房宽度在70CM-90CM相对于国内的酒店来说房间面积狭小，价格高，老牌酒店装修不够内地酒店新颖，双人房限制入住人数，超人需加人头费等。提供的酒店展示图片及数据均源自酒店/宾馆，仅供参考。也请买家自行多方面、多角度了解酒店实际情况，决定是否预订，感谢支持。</p>";
            }
            else
            {
                req.Guide =
                    "<p>通过淘宝旺旺可以联络到青芒果客服，由客服人员提供报价、房态咨询等服务。因无法完全排除买家'拍'的时候有房，等待酒店确认的时候已无房的情况，故请买家下单前务必向客服咨询是否有房。（逢周末、黄金周、节假日或大型会展期间房间价格浮动，请以客服最终报价为准）</p>";
            }
            if (dr["roomdesc"] == null || dr["roomdesc"] == DBNull.Value || string.IsNullOrEmpty(dr["roomdesc"].ToString()))
            {
                req.Desc = dr["roomname"].ToString().Trim();
            }
            else
            {
                req.Desc = dr["roomdesc"].ToString().Trim();
            }
            req.HasReceipt = true;
            req.ReceiptOtherTypeDesc = "可以提供各种类型发票";
            req.ReceiptType = "A";
            var response = TopConfig.Execute(req);
            if (response.IsError)
            {
                log.Error("上传房型商品失败,rid:"+rid+",ErrCode:" + response.ErrCode + "  ErrMsg:" + response.ErrMsg + "  SubErrCode:" + response.SubErrCode + " SubErrMsg:" + response.SubErrMsg);
                return null;
            }
            else
            {
                return response.Gid.ToString();
            }
        }


        /// <summary>
        /// 全量更新（房型列）
        /// </summary>
        /// <param name="rooms"></param>
        public void UpdateProducts(int[] hotelIds = null)
        {
            try
            {
                DataTable updateList = dal.GetUpdateList(hotelIds);
                int dtCount = updateList.Rows.Count;
                Console.Write("数据总计：" + dtCount);
                var jsonFomat = "{{\"out_rid\":\"{0}\",\"rateplan_code\":\"{1}\",\"data\":{2}}}";
                var num = 0;
                var count = 10;
                var source = updateList.Select().Skip((count * num)).Take(count).ToList<DataRow>();
                var list2 = new List<List<DataRow>>();
                while (source.Any())
                {
                    list2.Add(source);
                    num++;
                    source = updateList.Select().Skip((count * num)).Take(count).ToList<DataRow>();
                }
                Parallel.ForEach(list2, new ParallelOptions { MaxDegreeOfParallelism = 3 }, delegate(List<DataRow> drs)
                {
                    try
                    {
                        var values = new List<string>();
                        var info = string.Empty;
                        foreach (var row in drs)
                        {
                            try
                            {
                                var str2 = row["outer_id"].ToString();
                                var str3 = row["rateplan_code"];
                                string priceandstatus = this.GetRoomStatus(row["qmg_roomid"].ToString(), Convert.ToInt32(row["lijian"]), Convert.ToInt32(row["zengfu"]));
                                if (string.IsNullOrEmpty(priceandstatus))
                                {
                                    continue;
                                }
                                var str4 = "{\"use_room_inventory\":false,\"inventory_price\":" + priceandstatus + "}";
                                values.Add(string.Format(jsonFomat, str2, str3, str4));
                                info = info + str3 + "," + str3 + "\n";
                            }
                            catch (Exception e)
                            {
                                log.Error("全量更新数据异常 Count:" + row.ItemArray.Count() + " gid:" + row["gid"].ToString().Trim() + " rpid:" + row["rpid"].ToString().Trim(), e);
                            }
                        }
                        if (values.Count == 0)
                        {
                            return;
                        }
                        var req = new XhotelRatesIncrementRequest
                        {
                            RateInventoryPriceMap = string.Format("[{0}]", string.Join(",", values))
                        };
                        var res = TopConfig.Execute(req);
                        if (res.IsError)
                        {
                            log.Error(string.Format("全量更新失败:\r\n请求:{0}\r\n返回:{1}", req.RateInventoryPriceMap, res.Body));
                        }
                        else
                        {
                            log.Info(res.Body);
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error("TMD", e);
                    }
                });
            }
            catch (Exception e)
            {
                log.Error("ProductUpload:UpdateProducts 异常", e);
            }
        }

        /// <summary>
        /// 增量更新接口（单一）
        /// </summary>
        /// <param name="rooms"></param>
        public void UpdateProductsIncr(DateTime begintime, DateTime endtime)
        {
            try
            {
                var incrTable = dal.GetIncrPrice(begintime, endtime);
                if (incrTable != null)   //不为空则进行更新
                {
                    var jsonFomat = "{{\"out_rid\":\"{0}\",\"rateplan_code\":\"{1}\",\"data\":{2}}}";
                    var updateList = incrTable.AsEnumerable()
                        .GroupBy(q => new { gid = q.Field<long>("gid"), rpid = q.Field<long>("rpid") })
                        .Select(p => new
                        {
                            p.Key.gid,
                            p.Key.rpid,
                            price = p.Select(r => r).ToList()
                        }).ToList();
                    log.Info("增量获取房型数目:" + updateList.Count);
                    var num = 0;
                    var list2 = new List<dynamic>();
                    while (true)
                    {
                        var source = updateList.Skip((10 * num)).Take(10).ToList();
                        if (source == null || source.Count == 0)
                        {
                            break;
                        }
                        list2.Add(source);
                        num++;
                    }
                    Parallel.ForEach(list2, new ParallelOptions { MaxDegreeOfParallelism = 3 }, l =>
                    {
                        var values = new List<string>();
                        var info = string.Empty;
                        foreach (var row in l)
                        {
                            try
                            {
                                var drs = (List<DataRow>)row.price;
                                var str2 = row.price[0]["outer_id"];
                                var str3 = row.price[0]["rateplan_code"];
                                string priceandstatus = this.GetRoomStatusIncr(drs);
                                if (string.IsNullOrEmpty(priceandstatus))
                                {
                                    continue;
                                }
                                var str4 = "{\"use_room_inventory\":false,\"inventory_price\":" + priceandstatus + "}";
                                values.Add(string.Format(jsonFomat, str2, str3, str4));
                                var str6 = info;
                                info = str6 + str2 + "," + str3 + "\n";
                            }
                            catch (Exception e)
                            {
                                log.Error("增量更新数据异常 Count:" + row.price.Count + " out_rid:" + row.price[0]["outer_id"], e);
                            }
                        }
                        if (values.Count==0)
                        {
                            return;
                        }
                        var req = new XhotelRatesIncrementRequest
                        {
                            RateInventoryPriceMap = string.Format("[{0}]", string.Join(",", values))
                        };
                        var res = TopConfig.Execute(req);
                        if (res.IsError)
                        {
                            log.Error(string.Format("增量更新失败:\r\n请求:{0}\r\n返回:{1}", req.RateInventoryPriceMap, res.Body));
                        }
                        else
                        {
                            log.Info(res.Body);
                        }
                    });
                }
            }
            catch (Exception e)
            {
                log.Error("ProductUpload:UpdateProductsIncr 异常", e);
            }
        }

        /// <summary>
        /// 通过房型ID 上传房价房态
        /// </summary>
        /// <param name="roomid">roomID</param>
        public void UpLoadByRoom(TaobaoRoom room)
        {
            var dt = dal.GetRoomInfoByRoom(room.qmg_roomid);
            if (dt != null && dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                try
                {
                    string gid;
                    string rpid;
                    DataTable hostRoom = dal.GetTaobaoRoomTypeByRid(room.rid);
                    if (hostRoom == null || hostRoom.Rows.Count != 1)
                    {
                        log.Error("根据rid取房型数据匹配出错");
                        return;
                    }
                    if (hostRoom.Rows[0]["qmg_roomid"].ToString() == room.qmg_roomid.ToString()) //自己为多价格宿主房型
                    {
                        gid = RoomAdd(dr, room);//只有宿主房型才有一个gift
                    }
                    else
                    {
                        gid = hostRoom.Rows[0]["gid"].ToString();
                    }
                    TaobaoProduct product = InitProduct(room,Convert.ToInt32(dr["resourceid"]));
                    rpid = RatePlanAdd(dr, product);//多价格
                    product.rpid = rpid;
                    product.gid = gid;
                    if (!string.IsNullOrEmpty(rpid) && !string.IsNullOrEmpty(gid))
                    {
                        UpdataProductHis(product);
                        RateAdd(room, product);
                    }
                }
                catch (Exception e)
                {
                    log.Error("ProductUpload:UpLoadByRoom 异常", e);
                }
            }
        }



        /// <summary>
        /// 删除淘宝价格计划
        /// </summary>
        /// <param name="roomid">roomID</param>
        public bool DelRatePlan(long rpid)
        {
            XhotelRateplanGetRequest req1 = new XhotelRateplanGetRequest();
            req1.Rpid = rpid;
            var va = TopConfig.Execute(req1);
            if (!va.IsError)
            {
                return true;
            }
            else
            {
                log.Error("删除失败");
                return false;
            }
        }

        private bool IsRoomTypeExsit(string outerid)
        {
            XhotelRoomtypeGetRequest req = new XhotelRoomtypeGetRequest();
            req.OuterId = outerid;
            var vas = TopConfig.Execute(req);
            if (!vas.IsError)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新GID
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateGid(DataTable dt)
        {
            string gid = null;
            var rpid = long.Parse(dt.Rows[0]["rpid"].ToString());
            var hid = dt.Rows[0]["hid"].ToString();
            var rid = dt.Rows[0]["rid"].ToString();
            var top2 = TopConfig.GetClient();
            var req = new XhotelRateRelationshipwithroomGetRequest();
            req.RpId = rpid;
            var response = TopConfig.Execute(req);
            if (response.IsError == false)
            {
                try
                {
                    gid = XDocument.Parse(response.Body).XPathSelectElement(".//string").Value;
                }
                catch
                {
                }
            }
            if (string.IsNullOrEmpty(gid) == false)
            {
                dal.UpdateGId(hid, rid, rpid, gid);
            }
        }

        /// <summary>
        /// 商品初始化
        /// </summary>
        private TaobaoProduct InitProduct(TaobaoRoom room, int resourceid)
        {
            TaobaoProduct product = new TaobaoProduct
            {
                hid = room.hid,
                rid = room.rid,
                qmg_roomid = room.qmg_roomid.ToString(),
                qmg_hotelid = room.qmg_hotelid.ToString(),
                rateplan_code = room.qmg_hotelid.ToString() + "_" + room.qmg_roomid.ToString()
            };
            if (resourceid == 110)
            {
                product.zengfu = 5; //发现假期再加5个点
            }
            else if (resourceid == 120)
            {
                product.zengfu = 10;
            }
            return product;
        }

        private bool IsRatePlanExsit(string rpid)
        {
            if (string.IsNullOrEmpty(rpid))
            {
                return false;
            }
            XhotelRateplanGetRequest req1 = new XhotelRateplanGetRequest();
            req1.Rpid = long.Parse(rpid);
            var va = TopConfig.Execute(req1);
            if (!va.IsError)
            {
                return true;
            }
            else
            {
                dal.CleanOffLineData(rpid);
                return false;
            }
        }

        //上下线
        public void ProductStatusUpateByHotelid(List<int> hotelidList, bool isup)
        {
            string hotelMsg = isup ? "上线" : "下线";
            foreach (var hotelid in hotelidList)
            {
                log.Info(string.Format("{0}酒店开始，id:{1}", hotelMsg, hotelid));
                DataTable rpidList = dal.GetRpidListByHotelId(hotelid);
                foreach (DataRow item in rpidList.Rows)
                {
                    object obj = item["rpid"];
                    if (obj == null || obj == DBNull.Value || string.IsNullOrEmpty(obj.ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        XhotelRateplanUpdateRequest req = new XhotelRateplanUpdateRequest();
                        req.Rpid = long.Parse(obj.ToString());
                        req.Status = isup ? 1 : 2; //1开启 2关闭
                        string msg = isup ? "开启" : "关闭";
                        var res = TopConfig.Execute(req);
                        if (res.IsError)
                        {
                            string errmsg = "ErrCode:" + res.ErrCode + "  ErrMsg: " + res.ErrMsg + "  SubErrCode: " + res.SubErrCode + " SubErrMsg: " + res.SubErrMsg;
                            log.Error(string.Format("{0}rateplan失败,rpid:{1}\r\n{2}", msg, req.Rpid, errmsg));
                        }
                        else
                        {
                            log.Info(string.Format("{0}rateplan成功,rpid:{1}", msg, req.Rpid));
                        }
                    }
                }
                dal.UpdateProductStatusByHotelId(hotelid, isup);
                log.Info(string.Format("{0}酒店完成，id:{1}", hotelMsg, hotelid));
            }
        }

        //删除酒店
        public void DeleteHotel(List<long> hidList)
        {
            foreach (var hid in hidList)
            {
                log.Info("删除酒店开始,hid:" + hid);
                XhotelGetRequest req = new XhotelGetRequest();
                req.Hid = hid;
                var res = TopConfig.Execute(req);
                if (!res.IsError)
                {
                    XhotelUpdateRequest delReq = new XhotelUpdateRequest();
                    delReq.Hid = hid;
                    delReq.Tel = res.Xhotel.Tel;
                    delReq.Address = res.Xhotel.Address;
                    delReq.Status = "-1";//删除
                    var delRes = TopConfig.Execute(delReq);
                    if (!delRes.IsError)
                    {
                        log.Info("删除酒店成功");
                        _hotelData.CleanOffLineData(hid.ToString());
                    }
                    else
                    {
                        log.Info("删除酒店失败," + delRes.Body);
                    }
                }
                log.Info("删除酒店完成,hid:" + hid);
            }
        }
    }
}