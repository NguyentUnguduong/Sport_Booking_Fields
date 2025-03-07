using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTemplate.Models;

namespace TestTemplate.Areas.Admin.Controllers
{
    [Authorize(Roles = "XemDanhSach")]
    public class SanController : Controller
    {
        QLDSEntities db = new QLDSEntities();

        public ActionResult DanhSachSan(int? page)
        {
            List<San> danhSachSan = db.Sans.ToList();
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // Tạo biến số trang hiện tại
            int PageNumber = (page ?? 1);
            return View(danhSachSan.OrderBy(n => n.MaSan).ToPagedList(PageNumber, PageSize));
        }

        [Authorize(Roles = "Them")]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(San san)
        {
            if(san.MaSan.Length >= 10)
            {
                ModelState.AddModelError("", "Mã sân không được vượt quá 10 kí tự");
                return View(san);
            }

            if((san.SoSan == null) || (san.SoSan <= 0) || san.GiaSan == null || string.IsNullOrEmpty(san.MaSan))
            {
                ModelState.AddModelError("", "Thông tin không đúng");
                return View(san);
            }

            //var checkMaSosan = db.Sans.Any(s => s.MaSan == san.MaSan);

            //if(checkMaSosan)
            //{
            //    ModelState.AddModelError("", "Mã số sân mà bạn đặt đã bị trùng.");
            //    return View(san);
            //}

            

            try
            {
                db.Sans.Add(san);
                db.SaveChanges();
                return RedirectToAction("DanhSachSan");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(san);
            }

        }

        [Authorize(Roles = "Sua")]
        public ActionResult CapNhat(string id)
        {
            var model_Edit = db.Sans.Find(id);
            return View(model_Edit);
        }

        [HttpPost]
        public ActionResult CapNhat(San model_Edit)
        {
            if (model_Edit.GiaSan == null || (model_Edit.SoSan == null) || (model_Edit.SoSan <= 0))
            {
                ModelState.AddModelError("", "Thiếu thông tin");
                return View(model_Edit);
            }

            var updateSan = db.Sans.Find(model_Edit.MaSan);
            try
            {
                updateSan.SoSan = model_Edit.SoSan;
                updateSan.GiaSan = model_Edit.GiaSan;
                updateSan.MaDanhMuc = model_Edit.MaDanhMuc;

                db.SaveChanges();
                return RedirectToAction("DanhSachSan");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model_Edit);
            }
        }

        [Authorize(Roles = "Xoa")]
        public ActionResult Xoa(string id)
        {
            var model = db.Sans.Find(id);
            db.Sans.Remove(model);
            db.SaveChanges();
            return RedirectToAction("DanhSachSan");
        }
    }
}