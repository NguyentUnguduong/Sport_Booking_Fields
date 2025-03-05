using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTemplate.Models;
using PagedList;

namespace TestTemplate.Areas.Admin.Controllers
{
    [Authorize(Roles = "XemDanhSach")]
    public class CoSoController : Controller
    {
        // GET: Admin/CoSo
        QLDSEntities db = new QLDSEntities();

        public ActionResult DanhSachCoSo(int? page)
        {
            List<CoSo> danhSachCoSo = db.CoSoes.ToList();
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // Tạo biến số trang hiện tại
            int PageNumber = (page ?? 1);
            return View(danhSachCoSo.OrderBy(n => n.MaCS).ToPagedList(PageNumber, PageSize));
        }

        [Authorize(Roles = "Them")]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(CoSo model, HttpPostedFileBase fileAnh)
        {
            if (fileAnh?.ContentLength > 0)
            {
                string rootFolder = Server.MapPath("/Content/img/");
                string pathImage = rootFolder + fileAnh.FileName;
                fileAnh.SaveAs(pathImage);
                model.HinhAnh = fileAnh.FileName;
            }

            if (string.IsNullOrEmpty(model.MaCS) || model.MaCS.Length > 10)
            {
                ModelState.AddModelError("MaCS", "Bạn đã nhập thiếu mã cơ sở và không được vượt quá 10 kí tự.");
            }
            if (string.IsNullOrEmpty(model.TenCS))
            {
                ModelState.AddModelError("TenCS", "Bạn đã nhập thiếu tên cơ sở.");
            }
            if (string.IsNullOrEmpty(model.HinhAnh))
            {
                ModelState.AddModelError("HinhAnh", "Bạn đã nhập thiếu hình ảnh.");
            }
            if (string.IsNullOrEmpty(model.LinkMap))
            {
                ModelState.AddModelError("LinkMap", "Bạn đã nhập thiếu liên kết bản đồ.");
            }
            if (string.IsNullOrEmpty(model.MucGia))
            {
                ModelState.AddModelError("MucGia", "Bạn đã nhập thiếu mức giá.");
            }
            if (string.IsNullOrEmpty(model.DiaChi))
            {
                ModelState.AddModelError("DiaChi", "Bạn đã nhập thiếu địa chỉ.");
            }

            // Nếu có lỗi thì trả về view với model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                db.CoSoes.Add(model);
                db.SaveChanges();
                return RedirectToAction("DanhSachCoSo");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [Authorize(Roles = "Sua")]
        public ActionResult CapNhat(string id)
        {
            var model_Edit = db.CoSoes.Find(id);
            return View(model_Edit);
        }

        [HttpPost]
        public ActionResult CapNhat(CoSo model_Edit, HttpPostedFileBase fileAnh)
        {
            if (fileAnh.ContentLength > 0)
            {
                string rootFolder = Server.MapPath("/Content/img/");
                string pathImage = rootFolder + fileAnh.FileName;
                fileAnh.SaveAs(pathImage);
                model_Edit.HinhAnh = fileAnh.FileName;
            }

            if (string.IsNullOrEmpty(model_Edit.TenCS) == true ||
                string.IsNullOrEmpty(model_Edit.HinhAnh) == true || string.IsNullOrEmpty(model_Edit.LinkMap) == true ||
                    string.IsNullOrEmpty(model_Edit.MucGia) == true || string.IsNullOrEmpty(model_Edit.DiaChi) == true)
            {
                ModelState.AddModelError("", "Thiếu thông tin");
                return View(model_Edit);
            }

            var updateCoSo = db.CoSoes.Find(model_Edit.MaCS);
            try
            {
                updateCoSo.TenCS = model_Edit.TenCS;
                updateCoSo.HinhAnh = model_Edit.HinhAnh;
                updateCoSo.DiaChi = model_Edit.DiaChi;
                updateCoSo.LinkMap = model_Edit.LinkMap;
                updateCoSo.MucGia = model_Edit.MucGia;
                updateCoSo.MaLoaiCS = model_Edit.MaLoaiCS;

                db.SaveChanges();
                return RedirectToAction("DanhSachCoSo");
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
            var model = db.CoSoes.Find(id);
            db.CoSoes.Remove(model);
            db.SaveChanges();
            return RedirectToAction("DanhSachCoSo");
        }
    }
}