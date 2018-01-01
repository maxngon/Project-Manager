using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        //
        dBanGiayDataContext data = new dBanGiayDataContext();
        // GET: /Admin/HoaDon/
        public ActionResult Index(int ? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var hd = from tt in data.ChiTietDats
                         select tt;
            return View(hd.ToPagedList(pageNum, pagesize));
        }
	}
}