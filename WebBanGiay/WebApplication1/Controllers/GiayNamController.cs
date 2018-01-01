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
    public class GiayNamController : Controller
    {
        //
        // GET: /Giay/

        dBanGiayDataContext data = new dBanGiayDataContext();
        public ActionResult Spnhasxnam(int? page, string id, string Stukhoa,string t)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where g.MaNhaSanXuat == id
                           && g.MaLoai=="1"
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }
        [HttpGet]
        public ActionResult Sptheonhasx(int? page, string id, string Stukhoa)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where g.MaNhaSanXuat == id
                           && g.MaLoai == "1"
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Nhasxnam()
        {
            var chude = from cd in data.NhaSanXuats 
                   
                        select cd;
            return PartialView(chude);
        }
        public ActionResult Spgiaynam(int? page, string id, string Stukhoa)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 9;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where 
                            g.MaLoai == "1"
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }
    

    }
}