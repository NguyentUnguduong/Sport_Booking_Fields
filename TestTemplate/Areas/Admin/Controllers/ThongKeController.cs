using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTemplate.Models;

namespace TestTemplate.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        QLDSEntities db = new QLDSEntities();
        public ActionResult Index()
        {
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.SLKH = ThongKeKhachHang();
            ViewBag.sanDaDat = ThongKeSoSan();
            ViewBag.SoLuotTruyCap = HttpContext.Application["LuotTruyCap"];
            return View();
        }

        public double ThongKeDoanhThu()
        {
            var lstCTHD = db.CTHDs.ToList();
            double TongDoanhThu = 0;
            foreach(var item in lstCTHD)
            {
                TongDoanhThu = (double)(TongDoanhThu + item.GiaTien);
            }
            return TongDoanhThu;
        }

        public double ThongKeKhachHang()
        {
            double soLuongKH = db.user_KhachHang.Count();
            return soLuongKH;
        }

        public double ThongKeSoSan()
        {
            double sanDaDat = db.LichDats.Count();
            return sanDaDat;
        }
    }
}