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
    public class GiaoHangController : Controller
    {
        //
        dBanGiayDataContext data = new dBanGiayDataContext();
        // GET: /Admin/GiaoHang/
        public ActionResult Index(int ? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var giaohang = from tt in data.DatHangs
                           where tt.DaGiao == false
                            || tt.HTGiaoHang == false
                          ||   tt.HTThanhToan == false
                           select tt;
            return View(giaohang.ToPagedList(pageNum, pagesize));        
        }
        public ActionResult giaohang(int id, string strUrl)
        {
            var Egiao = data.DatHangs.First(m => m.SoDH == id);
            Egiao.DaGiao =true;     
            UpdateModel(Egiao);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
        public ActionResult thanhtoan(int id, string strUrl)
        {
            var Etoan = data.DatHangs.First(m => m.SoDH == id);
            Etoan.HTThanhToan = true;
            UpdateModel(Etoan);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
        public ActionResult htgiaohang(int id, string strUrl)
        {
            var Ehang = data.DatHangs.First(m => m.SoDH == id);
            Ehang.HTGiaoHang = true;
            UpdateModel(Ehang);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
        public ActionResult chinhsua(int id, string strUrl)
        {
            var Ehang = data.DatHangs.First(m => m.SoDH == id);
            Ehang.HTGiaoHang = false;
            Ehang.DaGiao = false;
            Ehang.HTThanhToan = false;
            UpdateModel(Ehang);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
	}
}