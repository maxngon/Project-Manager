using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;
using PagedList.Mvc;
namespace WebApplication1.Controllers
{
  
    public class HomeController : Controller
    {
        dBanGiayDataContext data = new dBanGiayDataContext();
        private List<Giay>laygiaymoi(int count)
        {

            return data.Giays.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giaymoi = laygiaymoi(12);
            return View(giaymoi.ToPagedList(pageNum,pagesize));
        }
        public ActionResult Chitiet(string id)
        {
          var giay=from g in data.Giays 
         where  g.MaGiay == id
           select g;
        
          return View(giay.Single());
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Sptheonhasx(int? page, string id, string Stukhoa,string t)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where g.MaNhaSanXuat==id
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }
    [HttpGet]
        public ActionResult Sptheonhasx(int? page, string id,string Stukhoa)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where g.MaNhaSanXuat == id
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }
    
        public ActionResult NhaSanXuat()
        {
            var chude = from cd in data.NhaSanXuats select cd;
            return PartialView(chude);
        }
        public ActionResult sptt(string id)
        {
            var chude = from cd in data.Giays
                        where cd.MaNhaSanXuat == id
                        select cd;
            return PartialView(chude);
        }
        public ActionResult Spkm(int? page, string id, string Stukhoa)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay_nam = from g in data.Giays
                           where g.KhuyenMai == Boolean.Parse("True")
                           select g;
            return View(giay_nam.ToPagedList(pageNum, pagesize));
        }

    }
}