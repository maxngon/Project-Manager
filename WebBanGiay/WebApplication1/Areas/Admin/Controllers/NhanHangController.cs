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
    public class NhanHangController : Controller
    {
        dBanGiayDataContext data = new dBanGiayDataContext();
        // GET: /Admin/NhanHang/
        //
        public ActionResult Index(int ? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var nhanhang = from tt in data.DatHangs    
                           where tt.DaGiao == false
                                 && tt.HTGiaoHang == false
                                 && tt.HTThanhToan == false
                               select tt;
            return View(nhanhang.ToPagedList(pageNum, pagesize));
        }
	}
}