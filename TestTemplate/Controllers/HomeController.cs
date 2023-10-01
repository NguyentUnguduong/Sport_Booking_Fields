﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTemplate.Models;

namespace TestTemplate.Controllers
{
    public class HomeController : Controller
    {
        QuanLyDatSanEntities db = new QuanLyDatSanEntities();
        public ActionResult Index()
        {

            // Lần lượt tạo ViewBag để lấy list cơ sở từ sql

            //List bóng đá
            var listCsBongDa = db.cosoes.Where(n => n.mals == "f");
            // Gán vào ViewBag
            ViewBag.CsBongDa = listCsBongDa;

            //List bóng rổ
            var listCsBongRo = db.cosoes.Where(n => n.mals == "br");
            // Gán vào ViewBag
            ViewBag.CsBongRo = listCsBongRo;

            //List bóng cầu lông
            var listCsCauLong = db.cosoes.Where(n => n.mals == "bmt");
            // Gán vào ViewBag
            ViewBag.CsCauLong = listCsCauLong;

            //List bóng tennis
            var listCsTennis = db.cosoes.Where(n => n.mals == "tennis");
            // Gán vào ViewBag
            ViewBag.CsTennis = listCsTennis;

            return View();
            //return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
        }

        
        
    }
}