using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Common.Tool;
using Taobao.Top2.Application.Common;
using Taobao.Top2.DataAccess;
using Taobao.Top2.Entity.TaobaoEntity;
using Top.Api.Request;
using Top.Api.Domain;


namespace Taobao.Top2.Application.Implement
{
    public class HotelUpload : IHotelUpload
    {
        public IHotelData HotelData = DataFactory.CreateHotelData();
        private ProductUpload _productUpload = new ProductUpload();
        private IRoomUpload _roomUpload = new RoomUpload();
        private IProductData _productData = DataFactory.CreateProductData();
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 多酒店上传
        /// </summary>
        /// <param name="hotelIds">酒店ID</param>
        /// <returns></returns>
        public List<TaobaoHotel> UpLoad(List<int> hotelIds)
        {
            //获取酒店列表
            DataTable dt = GetHotelsByIds(hotelIds);
            List<TaobaoHotel> taobaoList = new List<TaobaoHotel>();
            //多线程酒店上传开始
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    TaobaoHotel taobao = InitHotel(dr);
                    int hotel_parentid = Convert.ToInt32(dr["hotel_parentid"]);
                    int qmg_hotelid = dr["qmg_hotelid"] == null || dr["qmg_hotelid"] == DBNull.Value || string.IsNullOrEmpty(dr["qmg_hotelid"].ToString()) ? 0 : Convert.ToInt32(dr["qmg_hotelid"].ToString());
                    if (qmg_hotelid != 0)
                    {
                        //自我更新
                        taobao.hid = dr["hid"].ToString();
                        taobao.outer_id = dr["outer_id"].ToString();
                        taobao = taobaohotelupload(taobao);
                        if (taobao == null) continue;
                        _roomUpload.Upload(taobao, 0);
                    }
                    else
                    {
                        //判断该酒店是否是多供应商来源且是否已上线过
                        XHotel hotel = CheckIsOnLine(hotel_parentid);
                        if (hotel != null)
                        {
                            //聚合
                            taobao.hid = hotel.Hid.ToString();
                            taobao.outer_id = hotel.OuterId;
                            _roomUpload.Upload(taobao, 1);
                        }
                        else //新增
                        {
                            taobao = taobaohotelupload(taobao);
                            if (taobao == null) continue;
                            _roomUpload.Upload(taobao, 2);
                        }
                    }
                    //非宿主酒店状态变更上线
                    if (taobao.status.HasValue && taobao.status.Value == -2)//停售
                    {
                        _productUpload.ProductStatusUpateByHotelid(new List<int> { taobao.qmg_HotelId }, true);//上线
                    }
                    taobaoList.Add(taobao);
                }
                catch (Exception ex)
                {
                    log.Error("HotelUpload:UpLoad  酒店ID：" + dr["hotelid"], ex);
                }
            }
            return taobaoList;
        }

        private TaobaoHotel taobaohotelupload(TaobaoHotel hotel)
        {
            TaobaoHotel taobao = UpLoadToTaobao(hotel);
            if (taobao != null)
            {
                HotelData.UpDateTaobaoHotel(taobao);
            }
            return taobao;
        }

        //检查该酒店是否是多供应商酒店并已上线
        private XHotel CheckIsOnLine(int hotelparentid)
        {
            if (hotelparentid == 0)
            {
                return null;
            }
            else
            {
                string sql = "select outer_id,hid from taobao_new_hotel where qmg_hotelid in(select hotelid from hotels where hotel_parentid =" + hotelparentid + ")";
                DataTable db = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
                if (db == null || db.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    List<XHotel> lstXhotel = new List<XHotel>();
                    foreach (DataRow item in db.Rows)
                    {
                        XHotel hotel = IsHotelExist(item["outer_id"].ToString(), item["hid"].ToString());
                        if (hotel != null)
                        {
                            lstXhotel.Add(hotel);
                        }
                    }
                    if (lstXhotel.Count == 0)
                    {
                        return null;
                    }
                    else if (lstXhotel.Count == 1)
                    {
                        return lstXhotel[0];
                    }
                    else
                    {
                        log.Warning(string.Format("匹配到多酒店,outerids:{0}", string.Join(",", lstXhotel.Select(t => t.OuterId))));
                        return lstXhotel[0];
                    }
                }
            }
        }

        //判断酒店是否存在，清除脏数据
        private XHotel IsHotelExist(string outerid, string hid)
        {
            XhotelGetRequest req = new XhotelGetRequest();
            req.OuterId = outerid;
            var res = TopConfig.Execute(req);
            if (!res.IsError)
            {
                if (res.Xhotel.Status == -1) //因为自有删除功能，此处考虑数据问题。
                {
                    log.Info("酒店[outerid:" + outerid + "]已删除,清理数据");
                    HotelData.CleanOffLineData(hid);
                    return null;
                }
                else
                {
                    return res.Xhotel;
                }
            }
            else
            {
                if (res.SubErrCode == "isv.invalid-parameter:HOTEL_NOT_EXIST")
                {
                    log.Info("酒店[outerid:" + outerid + "]不存在,清理数据");
                    HotelData.CleanOffLineData(hid);
                    return null;
                }
                else
                {
                    log.Info("查询酒店出错,outerid:" + outerid + "。" + res.Body);
                    throw new Exception("酒店上线失败,请人工处理");
                }
            }
        }

        public DataTable GetHotelsByCondition(Dictionary<string, object> dic)
        {
            return HotelData.GetHotelsByCondition(dic);
        }

        public DataRow CheckTaobaoHotelByHid(string hid)
        {
            var dt = HotelData.TaoBaoHotelSearch(hid);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt.Rows[0];
            }
        }

        public DataRow CheckQmgHotelByHotelid(int hotelid)
        {
            var dt = HotelData.TaoBaoHotelSearch(hotelid);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt.Rows[0];
            }
        }

        public TaobaoHotel UpdateFromTaobao(TaobaoHotel hotel)
        {
            try
            {
                XhotelGetRequest req = new XhotelGetRequest();
                req.Hid = long.Parse(hotel.hid);
                //req.OuterId = hotel.qmg_HotelId.ToString().Trim();
                var res = TopConfig.Execute(req);

                XmlHelper.XmlToObject(
                        XDocument.Parse(TopConfig.Execute(req).Body).Element("xhotel_get_response").Elements("xhotel").Elements().ToList(),
                        hotel);
                return hotel;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 酒店初始化
        /// </summary>
        private TaobaoHotel InitHotel(DataRow dr)
        {
            TaobaoHotel hotel = new TaobaoHotel
            {
                qmg_HotelId = Convert.ToInt32(dr["hotelid"]),
                name = dr["hotelname"].ToString(),
                province = dr["p2"].ToString().Trim(),
                city = dr["city2"].ToString().Trim(),
                country = "China",
                tel = dr["mobile"].ToString().Trim(),
                outer_id = Guid.NewGuid().ToString("N"),
                address = dr["address"].ToString()
            };
            return hotel;
        }

        /// <summary>
        /// 酒店入库
        /// </summary>
        private bool Save(TaobaoHotel taobao)
        {
            return HotelData.SaveHotels(taobao);
        }

        /// <summary>
        /// 获取酒店信息（多）
        /// </summary>
        private DataTable GetHotelsByIds(List<int> hotelIds)
        {
            DataTable dt = HotelData.GetHotelById(hotelIds);
            return dt;
        }

        /// <summary>
        /// 获取酒店信息（单）
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        private DataRow GetHotelById(string hotelId)
        {
            DataTable dt = HotelData.GetHotelById(hotelId);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            return null;
        }

        /// <summary>
        /// 将酒店的信息上传到淘宝
        /// </summary>
        private TaobaoHotel UpLoadToTaobao(TaobaoHotel hotel)
        {
            log.Info("酒店上传 酒店ID：" + hotel.qmg_HotelId);
            XhotelAddRequest req = new XhotelAddRequest();
            req.Address = hotel.address;
            try
            {
                req.Province = long.Parse(hotel.province);
                req.City = long.Parse(hotel.city);
            }
            catch
            {
                req.Province = 0;
                req.City = 0;
            }
            if (req.City == 0 || req.Province == 0)
            {
                log.Error("上传酒店错误,城市或省份数据有问题");
                return null;
            }
            req.Country = "China";
            req.District = 0L;
            req.Domestic = 0L;
            req.Tel = hotel.tel;
            req.Name = hotel.name;
            req.OuterId = hotel.outer_id;
            var va = TopConfig.Execute(req);
            if (va.IsError == false)
            {
                try
                {
                    XmlHelper.XmlToObject(
                        XDocument.Parse(va.Body).Element("xhotel_add_response").Elements("xhotel").Elements().ToList(),
                        hotel);
                    return hotel;
                }
                catch
                {
                    log.Error("上传酒店错误,ErrCode:" + va.ErrCode + "  ErrMsg:" + va.ErrMsg + "  SubErrCode:" + va.SubErrCode + " SubErrMsg:" + va.SubErrMsg);
                    return null;
                }
            }
            return null;
        }

        public DataTable GetHotelInfo(int hotelid)
        {
            return HotelData.TaoBaoHotelSearch(hotelid);
        }

        public void SynHotelsStatus()
        {
            try
            {
                DataTable dt = HotelData.GetHotelStatus();
                log.Info("同步酒店数量:" + dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    int hotelid = Convert.ToInt32(dr["hotelid"].ToString());
                    log.Info("同步酒店开始，id=" + hotelid);
                    bool isup = dr["after"].ToString() == "-1" ? false : true;
                    DataTable rpidList = _productData.GetRpidListByHotelId(hotelid);
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
                                log.Error(string.Format("{0}rateplan失败,rpid:{1}\r\n{2}", msg, req.Rpid,errmsg));
                            }
                            else
                            {
                                log.Info(string.Format("{0}rateplan成功,rpid:{1}", msg, req.Rpid));
                            }
                        }
                    }
                    _productData.UpdateProductStatusByHotelId(hotelid, isup);
                    HotelData.UpdateTaobaoHotelLogStatus(hotelid);
                    log.Info("同步酒店完成，id=" + hotelid);
                }
            }
            catch (Exception err)
            {
                log.Error("同步酒店状态出错," + err.Message);
            }
        }
    }
}
