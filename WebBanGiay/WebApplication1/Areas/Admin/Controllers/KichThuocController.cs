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
    public class KichThuocController : Controller
    {
        //
        dBanGiayDataContext data = new dBanGiayDataContext();
        // GET: /Admin/KichThuoc/
        public ActionResult Index(int ? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var All_kichthuoc = from tt in data.KichThuocs                            
                               select tt;
            return View(All_kichthuoc.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Createkt()
        {

            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Createkt(FormCollection collection, KichThuoc kt)
        {

            var CB_MaSize = collection["MaKichThuoc"];
            var CB_MoTa = collection["MoTaSize"];
            if (string.IsNullOrEmpty(CB_MaSize))
            {
                ViewData["Loi"] = "Mã kích thước không được để trống";
            }
            else if (string.IsNullOrEmpty(CB_MoTa))
            {
                ViewData["Loi1"] = " Mô tả không được để trống ";
            }
            else
            {
                kt.MaKichThuoc = CB_MaSize;
                kt.MoTaSize = CB_MoTa;
                data.KichThuocs.InsertOnSubmit(kt);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Createkt();
        }
        public ActionResult Deletekt(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_kt = data.KichThuocs.First(m => m.MaKichThuoc == id);
            return View(D_kt);
        }
        [HttpPost]
        public ActionResult Deletekt(string id, FormCollection collection)
        {

            var D_kt = data.KichThuocs.First(m => m.MaKichThuoc == id);
            data.KichThuocs.DeleteOnSubmit(D_kt);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Editkt(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_kt = data.KichThuocs.First(m => m.MaKichThuoc == id);
            return View(E_kt);
        }
        [HttpPost]
        public ActionResult Editkt(string id, FormCollection collection)
        {
            var CB_MoTa = collection["MoTaSize"];
             var E_kt = data.KichThuocs.First(m => m.MaKichThuoc == id);
            if (String.IsNullOrEmpty(CB_MoTa))
            {
                ViewData["Loi1"] = "Mô tả không được để trống";
            }

            else
            {

                E_kt.MoTaSize = CB_MoTa;
                UpdateModel(E_kt);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editkt(id);
        }   
	}
}