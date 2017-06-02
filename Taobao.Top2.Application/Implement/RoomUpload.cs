using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common.Tool;
using Taobao.Top2.Application.Common;
using Taobao.Top2.DataAccess;
using Taobao.Top2.Entity;
using Taobao.Top2.Entity.TaobaoEntity;
using Top.Api.Request;
using Top.Api.Domain;

namespace Taobao.Top2.Application.Implement
{
    public class RoomUpload : IRoomUpload
    {
        private IRoomData _roomUpload = DataFactory.CreateRoomData();
        private IProductData _productData = DataFactory.CreateProductData();
        private IProductUpload _productUpload = new ProductUpload();
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //----------------------------------------------------------------------
        //----------------------私有方法----------------------------------------
        private bool Save(TaobaoRoom room)
        {
            return _roomUpload.SaveTaobaoRoom(room);
        }

        private DataTable GetRoomInfoByHotel(string hotel)
        {
            return _roomUpload.GetRoomInfoByHotel(hotel);
        }

        /// <summary>
        /// 房型上传
        /// </summary>
        /// <param name="dr">房型数据</param>
        /// <param name="room"></param>
        /// <returns></returns>
        private TaobaoRoom UploadToTaobao(TaobaoRoom room)
        {
            log.Info("房型数据上传中 酒店ID:" + room.qmg_hotelid + " 房型ID:" + room.qmg_roomid);
            var top2 = TopConfig.GetClient();
            XhotelRoomtypeAddRequest req = new XhotelRoomtypeAddRequest
            {
                Name = room.name.Replace("限量团购", ""),
                OuterId = room.outer_id,
                OutHid = room.outHid,
                MaxOccupancy = room.max_occupancy,
                BedType = "标准",
                Service = room.service
            };
            var res = TopConfig.Execute(req);
            if (res.IsError == false)
            {
                string va = res.Body;
                room = XmlHelper.XmlToObject(XElement.Parse(va).Element("xroomtype").Elements().ToList(),
                      room);
            }
            else
            {
                log.Error("房型上传失败,ErrCode:" + res.ErrCode + "  ErrMsg:" + res.ErrMsg + "  SubErrCode:" + res.SubErrCode + " SubErrMsg:" + res.SubErrMsg);
                return null;
            }
            return room;
        }

        private DataRow GetRoomInfo(string roomId)
        {
            DataTable dt = _roomUpload.GetRoomInfoById(roomId);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            return null;
        }

        /// <summary>
        /// 房型上传
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private TaobaoRoom TaobaoRoomUpLoad(TaobaoRoom room)
        {
            TaobaoRoom room2 = UploadToTaobao(room);
            if (room2 != null)
            {
                UpdataRoom(room2);
            }
            return room2;
        }

        private void UpdataRoom(TaobaoRoom room2)
        {
            _roomUpload.SaveToRoomType(room2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taobao"></param>
        /// <param name="flag">0酒店自我更新，1酒店聚合，2酒店新增</param>
        public void Upload(TaobaoHotel taobao, int flag)
        {
            log.Info("房型数据开始上传");
            DataTable rooms = _roomUpload.GetRoomInfoByHotelId(taobao.qmg_HotelId);
            log.Info("房型条数:" + rooms.Rows.Count);
            Parallel.ForEach(rooms.Select(), new ParallelOptions { MaxDegreeOfParallelism = 3 }, dr =>
             {
                 try
                 {
                     TaobaoRoom hotelroom = InitRoom(dr);
                     hotelroom.hid = taobao.hid;
                     hotelroom.outHid = taobao.outer_id;
                     int hotelroomParentid = Convert.ToInt32(dr["hotelroom_parentid"].ToString());
                     int qmg_roomid = dr["qmg_roomid"] == null || dr["qmg_roomid"] == DBNull.Value || string.IsNullOrEmpty(dr["qmg_roomid"].ToString()) ? 0 : Convert.ToInt32(dr["qmg_roomid"].ToString());
                     if (flag == 0)
                     {
                         if (qmg_roomid != 0) //更新
                         {
                             hotelroom.rid = dr["rid"].ToString();
                             hotelroom.outer_id = dr["outer_id"].ToString();
                         }
                         hotelroom = TaobaoRoomUpLoad(hotelroom); //新增
                         if (hotelroom == null) return;
                     }
                     else if (flag == 1)
                     {
                         XRoomType roomtype = CheckIsOnLine(hotelroomParentid, taobao.outer_id, hotelroom.qmg_hotelid);
                         if (roomtype != null) //聚合(多价格聚合)
                         {
                             hotelroom.rid = roomtype.Rid.ToString();
                             hotelroom.outer_id = roomtype.OuterId;
                         }
                         else //(多房型聚合)
                         {
                             if (qmg_roomid != 0) //更新
                             {
                                 hotelroom.rid = dr["rid"].ToString();
                                 hotelroom.outer_id = dr["outer_id"].ToString();
                             }
                             hotelroom = TaobaoRoomUpLoad(hotelroom);//新增
                             if (hotelroom == null) return;
                         }
                     }
                     else  //新增
                     {
                         hotelroom = TaobaoRoomUpLoad(hotelroom);
                         if (hotelroom == null) return;
                     }
                     _productUpload.UpLoadByRoom(hotelroom);
                 }
                 catch (Exception ex)
                 {
                     log.Error("房型对应数据上传异常：RoomId:" + dr["qmg_roomid"], ex);
                 }
             });
            log.Info("房型数据上传结束");
        }

        /// <summary>
        /// 房型初始化
        /// </summary>
        private TaobaoRoom InitRoom(DataRow dr)
        {
            TaobaoRoom room = new TaobaoRoom
            {
                outer_id = Guid.NewGuid().ToString("N"),
                qmg_hotelid = Convert.ToInt32(dr["qmg_hotelid"]),
                name = dr["roomname"].ToString(),
                qmg_roomid = Convert.ToInt32(dr["roomid"]),
                max_occupancy = Convert.ToInt32(dr["persons"]),
                service =
                    string.Format("{{\"bar\":{0},\"catv\":{1},\"ddd\":{2},\"idd\":{3},\"pubtoilet\":{4},\"toilet\":{5}}}",
                        new object[]
                        {
                            "false", (dr["istv"].ToString() == "有") ? "true" : "false",
                            (dr["istel"].ToString() == "有") ? "true" : "false",
                            (dr["istel"].ToString() == "有") ? "true" : "false", "true",
                            (dr["hasDlwy"].ToString() == "T") ? "true" : "false"
                        })
            };
            return room;
        }

        //检查多渠道房型是否已上线
        private XRoomType CheckIsOnLine(int hotelroomParentid, string outer_hid,int hotelid)
        {
            if (hotelroomParentid == 0)
            {
                return null;
            }
            else
            {
                string sql = "select distinct tnr.outer_id,tnr.rid,tnr.qmg_roomid from taobao_new_roomtype tnr" +
    " left join hotelroom hr on tnr.qmg_roomid = hr.room " +
     " where tnr.outer_hid='" + outer_hid + "' and hr.hotelroom_parentid = " + hotelroomParentid + " and tnr.qmg_hotelid<>" + hotelid;
                DataTable db = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
                if (db == null || db.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    List<XRoomType> lstroomtype = new List<XRoomType>();
                    foreach (DataRow item in db.Rows)
                    {
                        XRoomType roomtype = IsRoomExist(item["outer_id"].ToString(), item["rid"].ToString());
                        if (roomtype != null)
                        {
                            lstroomtype.Add(roomtype);
                        }
                    }
                    if (lstroomtype.Count == 0)
                    {
                        return null;
                    }
                    else if (lstroomtype.Count == 1)
                    {
                        return lstroomtype[0];
                    }
                    else
                    {
                        log.Warning(string.Format("匹配到多条房型,outerids:{0}", string.Join(",", lstroomtype.Select(t => t.OuterId))));
                        return lstroomtype[0]; //不同于酒店重复，多价格取第一条即可。
                    }
                }
            }
        }

        //判断房型是否存在，清除脏数据
        private XRoomType IsRoomExist(string outer_id, string rid)
        {
            XhotelRoomtypeGetRequest req = new XhotelRoomtypeGetRequest();
            req.OuterId = outer_id;
            var res = TopConfig.Execute(req);
            if (res.IsError && res.ErrCode == "15" && res.SubErrCode == "isv.invalid-parameter:ROOM_TYPE_NOT_EXIST")
            {
                log.Info("房型[outerid:" + outer_id + "]不存在,清理数据");
                _roomUpload.CleanOffLineData(rid);
                return null;
            }
            else
            {
                return res.Xroomtype;
            }
        }

        public void SynRoomTypeStatus()
        {
            try
            {
                DataTable dt = _roomUpload.GetRoomTypeStatus();
                log.Info("同步房型数量:" + dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    int roomid = Convert.ToInt32(dr["roomid"].ToString());
                    log.Info("同步房型开始，id=" + roomid);
                    bool isup = dr["after"].ToString() == "-1" ? false : true;
                    DataTable rpidList = _productData.GetRpidListByRoomId(roomid);
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
                    _productData.UpdateProductStatusByRoomId(roomid, isup);
                    _roomUpload.UpdateTaobaoRoomLogStatus(roomid);
                    log.Info("同步房型完成，id=" + roomid);
                }
            }
            catch (Exception err)
            {
                log.Error("同步房型状态出错," + err.Message);
            }
        }
    }
}
