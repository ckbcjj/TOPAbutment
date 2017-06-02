using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Tool;
using Taobao.Top2.Application;
using Taobao.Top2.Application.Implement;
using Taobao.Top2.Entity;
using Taobao.Top2.Models;
using WebGrease.Css.Extensions;

namespace Taobao.Top2.Controllers
{
    public class RoomController : Controller
    {
        private IHotelUpload hotelUpload = new HotelUpload();
        private IRoomUpload roomUpload = new RoomUpload();
        private IProductUpload productUpload = new ProductUpload();
        //
        // GET: /Room/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult HotelRoomInfo(string hotelIds, string roomIds, int status)
        {
            var hotelList = hotelIds.NoSqlHackStringToIntArr();
            var roomList = roomIds.NoSqlHackStringToIntArr();
            var dt = roomUpload.GetHotelRoomInfo(hotelList, roomList, status);
            var result = new BaseResult();
            if (dt == null || dt.Rows.Count == 0)
            {
                result.Status = false;
                result.ErrorMessage = "未找到相关数据";
            }
            else
            {
                result.Status = true;
                var list = new List<object>();
                dt.AsEnumerable()
                    .GroupBy(p => new
                    {
                        hotelid = p.Field<int>("hotelid"),
                        hotelName = p.Field<string>("hotelname"),
                        thid = p.Field<string>("tbhid")
                    })
                    .ForEach(g =>
                    {
                        dynamic temp1;
                        if (string.IsNullOrEmpty(g.Key.thid))
                        {
                            temp1 = new
                            {
                                id = "h" + g.Key.hotelid,
                                name = g.Key.hotelName,
                                g.Key.hotelid,
                                status = "酒店未上传"
                            };
                            list.Add(temp1);
                            return;
                        }
                        else
                        {
                            temp1 = new
                            {
                                id = "h" + g.Key.hotelid,
                                name = g.Key.hotelName,
                                hid = g.Key.thid,
                                g.Key.hotelid,
                                status = "酒店已上传"
                            };
                            list.Add(temp1);
                        }

                        foreach (var item in g)
                        {
                            var hid = (item.Field<string>("hid") ?? "").Trim();
                            var rid = (item.Field<string>("rid") ?? "").Trim();
                            var rpid = (item.Field<string>("rpid") ?? "").Trim();
                            var gid = (item.Field<string>("gid") ?? "").Trim();
                            string roomStatus;
                            if (string.IsNullOrEmpty(rid))
                            {
                                roomStatus = "房型未上传";
                            }
                            else if (string.IsNullOrEmpty(rpid))
                            {
                                roomStatus = "价格计划未上传";
                            }
                            else if (string.IsNullOrEmpty(gid))
                            {
                                roomStatus = "价格策略未上传";
                            }
                            else
                            {
                                roomStatus = "商品已发布";
                            }
                            var temp = new
                            {
                                id = item.Field<int>("room"),
                                _parentId = temp1.id,
                                name = item.Field<string>("roomname"),
                                room = item.Field<int>("room"),
                                status = roomStatus,
                                hid,
                                rid,
                                rpid,
                                gid,
                                lijian = item.Field<int?>("lijian")
                            };
                            list.Add(temp);
                        }
                    });
                result.Result = new { total = list.Count, rows = list };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult RoomDetail(int hotelid)
        {
            var hotelids = new List<int>();
            hotelids.Add(hotelid);
            string hid = hotelUpload.GetTaobaoHotel(hotelid);
            var dt = roomUpload.GetHotelRoomInfo(hotelids, null, -1);
            if (dt == null || dt.Rows.Count == 0)
            {
                ViewBag.Rooms = null;
            }
            else
            {
                ViewBag.HotelId = hotelid;
                ViewBag.Hid = hid;
                ViewBag.HotelName = dt.Rows[0]["hotelname"].ToString();
                ViewBag.Rooms = dt.Select();
            }
            return PartialView("_RoomBinding");
        }

        public JsonResult MappingSave(QmgTaobaoIdMapping rooms)
        {
            BaseResult result = new BaseResult();
            if (string.IsNullOrEmpty(rooms.hid) || string.IsNullOrEmpty(rooms.rid) || string.IsNullOrEmpty(rooms.rpid) ||
                rooms.roomid == 0 || rooms.hotelid == 0)
            {
                result.Status = false;
                result.ErrorMessage = "参数不能为null";
            }
            else
            {
                rooms.gid = productUpload.GetGidByRpid(rooms.rpid);
                if (string.IsNullOrEmpty(rooms.gid))
                {
                    result.Status = false;
                    result.ErrorMessage = "淘宝信息获取错误";
                }
                else
                {
                    result.Status = true;
                    result.Result = rooms.hotelid;
                    productUpload.UpdataProductHis(rooms);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RoomUpload(List<QmgTaobaoIdMapping> rooms)
        {

            BaseResult result = new BaseResult();
            foreach (var r in rooms)
            {
                if (string.IsNullOrEmpty(r.rpid) == false)
                {
                    result.Status = false;
                    result.ErrorMessage = "RPID必须为空";
                }
                roomUpload.CheckUpdate(r.roomid, r.rid);
                productUpload.UpdataProductHis(r);
            }
            try
            {
                var roomList = rooms.Select(q => q.roomid).ToList();
                roomUpload.RoomUpLoadAll(rooms);
                //roomUpload.UpLoad(roomList);
                //var l = productUpload.UpLoadByRooms(roomList);

                result.Status = true;
                result.Result = 1;
                //} 
                //else
                //{
                //    result.Status = false;
                //    result.Result = "上传失败";
                //}
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Result = ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RoomUploadMain(List<int> rooms)
        {
            BaseResult result = new BaseResult();
            try
            {
                roomUpload.UpLoad(rooms);
                foreach (var room in rooms)
                {
                    productUpload.UpLoadByRoom(room);
                }
                result.Status = true;
                result.Result = string.Join(",", rooms);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Result = ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HotelCheck(int hotelid, string hid)
        {
            BaseResult result = new BaseResult();
            result.Result = string.Empty;
            DataRow tbHotelRow = hotelUpload.CheckTaobaoHotelByHid(hid);
            DataRow qmgHotelRow = hotelUpload.CheckQmgHotelByHotelid(hotelid);
            if ((tbHotelRow == null || (int)tbHotelRow["qmg_hotelid"] <= 0) && (qmgHotelRow == null || (int)qmgHotelRow["qmg_hotelid"] <= 0))
            {
                //hid和hotelid都无法查到
                result.Status = true;
                result.Result = 0;
                //直接保存mapping
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (tbHotelRow != null && qmgHotelRow != null)
            {
                if ((int)tbHotelRow["qmg_hotelid"] == (int)qmgHotelRow["qmg_hotelid"] && tbHotelRow["hid"].ToString() == qmgHotelRow["hid"].ToString())
                {
                    //已经正确绑定。直接跳过
                    result.Status = false;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            if (tbHotelRow != null && (int)tbHotelRow["qmg_hotelid"] > 0)
            {
                result.Status = true;
                result.Result = -1;
                result.ErrorMessage += string.Format("<div>[淘宝]酒店: {0}({1}) 已经与[青芒果]酒店id: {2} 绑定</div><br/>",
                       tbHotelRow["name"], tbHotelRow["hid"], tbHotelRow["qmg_hotelid"]);
            }
            if (qmgHotelRow != null && (int)qmgHotelRow["qmg_hotelid"] > 0)
            {
                result.Status = true;
                result.Result = -1;
                result.ErrorMessage += string.Format("<div>[淘宝]酒店: {0}({1}) 已经与[青芒果]酒店id: {2} 绑定</div><br/>",
                       qmgHotelRow["name"], qmgHotelRow["hid"], qmgHotelRow["qmg_hotelid"]);
            }

            result.ErrorMessage = "<br/><br/>" + result.ErrorMessage + "<br/> 是否继续?";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetLijian(int lijian, int[] rooms)
        {
            if (lijian >= 0 && rooms != null && rooms.Length > 0)
            {
                productUpload.SetLijian(lijian, rooms);
            }
            return Json(new object());
        }
    }
}