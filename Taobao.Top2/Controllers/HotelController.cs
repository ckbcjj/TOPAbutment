using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Common.Tool;
using Newtonsoft.Json;
using Taobao.Top2.Application;
using Taobao.Top2.Application.Implement;
using Taobao.Top2.Entity.TaobaoEntity;
using Taobao.Top2.Models;

namespace Taobao.Top2.Controllers
{
    public class HotelController : Controller
    {
        private IHotelUpload hotelUpload = new HotelUpload();
        private IRoomUpload roomUpload = new RoomUpload();
        private IProductUpload productUpload = new ProductUpload();
        //
        // GET: /Hotel/

        public ActionResult Index()
        {
           return View();
        }

        /// <summary>
        /// 搜索酒店
        /// </summary>
        public ContentResult HotelList(string hotelIds, string hotelName, int status)
        {
            BaseResult result = new BaseResult();
            result.Status = true;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("HotelIds", hotelIds.NoSqlHackIn());
            dic.Add("HotelName", hotelName.NoSqlHack());
               dic.Add("Status", status);
            return Content(JsonConvert.SerializeObject(result), "json");
        }

        /// <summary>
        /// 酒店上传
        /// </summary>
        /// <param name="hotels"></param>
        /// <returns></returns>
        public ContentResult UpLoadToTaobao(List<int> hotels)
        {
            List<TaobaoHotel> taobao = hotelUpload.UpLoad(hotels);
            taobao.Select(q => q.qmg_HotelId.ToString()).ToList();
            return HotelList(string.Join(",", hotels), null, -1);
        }

        public ActionResult UpdateAll(List<int> hotels)
        {
            var taobao = hotelUpload.UpLoad(hotels);
            List<string> hotelList = taobao.Select(q => q.qmg_HotelId.ToString()).ToList();
            roomUpload.UpLoadByHotel(hotelList);
            productUpload.UpLoadByHotel(hotelList);
            return HotelList(string.Join(",", hotels), null, -1);
        }

        public JsonResult MappingSave(string hid, int hotelid)
        {

            BaseResult result = new BaseResult();
            try
            {

                var taobao = hotelUpload.UpdateHotelMapping(hid, hotelid);
                if (taobao == null)
                {
                    result.Result = hotelid;
                    result.Status = false;
                    result.ErrorMessage = "保存失败";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    result.Result = hotelid;
                    result.Status = true;
                    result.ErrorMessage = "保存成功";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                result.Result = hotelid;
                result.Status = false;
                result.ErrorMessage = exception.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
