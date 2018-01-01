using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers


{
    public class GiayNuController : Controller
    {
        //
        // GET: /GiayNu/
        dBanGiayDataContext data = new dBanGiayDataContext();
        public ActionResult Spnhasxnu(int? page, string id, string Stukhoa,string t)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where g.MaNhaSanXuat == id
                           && g.MaLoai == "2"
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }
    [HttpGet]
        public ActionResult Spnhasxnu(int? page, string id,string Stukhoa)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where g.MaNhaSanXuat == id
                           && g.MaLoai == "2"
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Nhasxnu()
        {
            var chude = from cd in data.NhaSanXuats

                        select cd;
            return PartialView(chude);
        }
        public ActionResult Spgiaynu(int? page, string id, string Stukhoa)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 9;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where
                            g.MaLoai == "2"
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }
    }
}