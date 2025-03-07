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
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        QLDSEntities db = new QLDSEntities();

        public ActionResult DanhSachKhachHang(int? page)
        {
            List<user_KhachHang> danhSachKhachHang = db.user_KhachHang.ToList();
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // Tạo biến số trang hiện tại
            int PageNumber = (page ?? 1);
            return View(danhSachKhachHang.OrderBy(n => n.MaKH).ToPagedList(PageNumber, PageSize));
        }

        [Authorize(Roles = "Sua")]
        public ActionResult CapNhat(string id)
        {
            var model_Edit = db.user_KhachHang.Find(id);
            return View(model_Edit);
        }

        [HttpPost]
        public ActionResult CapNhat(user_KhachHang model_Edit, HttpPostedFileBase fileAnh)
        {
            if (string.IsNullOrEmpty(model_Edit.HoTen) == true ||
                    string.IsNullOrEmpty(model_Edit.SoDienThoai) == true || string.IsNullOrEmpty(model_Edit.Email) == true)
            {
                ModelState.AddModelError("", "Thiếu thông tin");
                return View(model_Edit);
            }

            var updateKhachHang = db.user_KhachHang.Find(model_Edit.MaKH);
            try
            {
                updateKhachHang.HoTen = model_Edit.HoTen;
                updateKhachHang.SoDienThoai = model_Edit.SoDienThoai;
                updateKhachHang.Email = model_Edit.Email;

                db.SaveChanges();
                return RedirectToAction("DanhSachKhachHang");
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
            var model = db.user_KhachHang.Find(id);
            db.user_KhachHang.Remove(model);
            db.SaveChanges();
            return RedirectToAction("DanhSachKhachHang");
        }

        public ActionResult HoaDon(string id, int? page)
        {

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("DanhSachKhachHang");
            }
            List<HoaDon> dsHoaDon = db.HoaDons.Where(hd => hd.MaKhachHang == id).ToList();
            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(dsHoaDon.OrderBy(n => n.MaHoaDon).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult CapNhatHD(string id)
        {
            var model = db.HoaDons.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult CapNhatHD(HoaDon hoaDon)
        {
            if(string.IsNullOrEmpty(hoaDon.TrangThai) == true)
            {
                ModelState.AddModelError("", "Nhập trạng thái");
                return View(hoaDon);
            }

            var updateStatus = db.HoaDons.Find(hoaDon.MaHoaDon);
            try
            {
                updateStatus.TrangThai = hoaDon.TrangThai;

                db.SaveChanges();
                return RedirectToAction("HoaDon");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(hoaDon);
            }
        }

        public ActionResult CTHD(string id, int? page)
        {
            List<CTHD> dsCTHDs = db.CTHDs.Where(cthd => cthd.MaHoaDon == id).ToList();
            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(dsCTHDs.OrderBy(n => n.MaCTHD).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult DanhSachLichDat(string id, int? page)
        {

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("DanhSachLichDat");
            }
            List<LichDat> dsLichDat = db.LichDats.Where(ld => ld.MaKhachHang == id).ToList();
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // Tạo biến số trang hiện tại
            int PageNumber = (page ?? 1);
            return View(dsLichDat.OrderBy(n => n.MaLichDat).ToPagedList(PageNumber, PageSize));
        }
    }
}