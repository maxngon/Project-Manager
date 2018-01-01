using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class TimKiemController : Controller
    {
        //
        // GET: /TimKiem/
        dBanGiayDataContext data = new dBanGiayDataContext();


       
    [HttpPost]
        public ActionResult KetQuaTimKiem(string txttimkiem, int? page, string Stukhoa)
        {
            ViewBag.TuKhoa = txttimkiem;
            List<Giay> listKQ = data.Giays.Where(n => n.TenGiay.Contains(txttimkiem)).ToList();
            int pagesize = 9;
            int pageNum = (page ?? 1);
           if(listKQ.Count ==0)
           {
               ViewBag.Thongbao = "Không có kết quả";
           }
           return View(listKQ.OrderBy(n => n.TenGiay).ToPagedList(pageNum, pagesize));
        }
    [HttpGet]
    public ActionResult KetQuaTimKiem(int? page,string txttimkiem)
    {
        ViewBag.TuKhoa = txttimkiem;
        List<Giay> listKQ = data.Giays.Where(n => n.TenGiay.Contains(txttimkiem)).ToList();
        int pagesize = 9;
        int pageNum = (page ?? 1);
        if (listKQ.Count == 0)
        {
            ViewBag.Thongbao = "Không có kết quả";
            return View(data.Giays.OrderBy(n => n.TenGiay).ToPagedList(pageNum, pagesize));
        }
        ViewBag.Thongbao = "Đã tìm thấy"+ listKQ.Count +"Kết Quả";
        return View(listKQ.OrderBy(n => n.TenGiay).ToPagedList(pageNum, pagesize));
    }
                
       
	}
}