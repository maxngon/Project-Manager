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
    public class LoaiGiayController : Controller
    {
        //
        dBanGiayDataContext data = new dBanGiayDataContext();
        // GET: /Admin/LoaiGiay/
        public ActionResult Index(int ? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var loai = from tt in data.LoaiGiays
                         select tt;
            return View(loai.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Createloai()
        {

            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Createloai(FormCollection collection, LoaiGiay lg)
        {

            var CB_MaLoai = collection["MaLoai"];
            var CB_TenLoai = collection["TenLoai"];            
            if (string.IsNullOrEmpty(CB_MaLoai))
            {
                ViewData["Loi"] = "Mã loại không được để trống";
            }
            else if (string.IsNullOrEmpty(CB_TenLoai))
            {
                ViewData["Loi1"] = " Tên loại không được để trống ";
            }
            else
            {
                lg.MaLoai = CB_MaLoai;
                lg.TenLoai = CB_TenLoai;
                data.LoaiGiays.InsertOnSubmit(lg);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Createloai();
        }
        public ActionResult Deleteloai(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_loai = data.LoaiGiays.First(m => m.MaLoai == id);
            return View(D_loai);
        }
        [HttpPost]
        public ActionResult Deleteloai(string id, FormCollection collection)
        {

            var D_loai = data.LoaiGiays.First(m => m.MaLoai == id);
            data.LoaiGiays.DeleteOnSubmit(D_loai);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Editloai (string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_loai = data.LoaiGiays.First(m => m.MaLoai == id);
            return View(E_loai);
        }
        [HttpPost]
        public ActionResult Editloai(string id, FormCollection collection)
        {
            var CB_TenLoai = collection["TenLoai"];
            var E_loai = data.LoaiGiays.First(m => m.MaLoai == id);
            if (String.IsNullOrEmpty(CB_TenLoai))
            {
                ViewData["Loi1"] = "Tên loại không được để trống";
            }

            else
            {

               E_loai.TenLoai = CB_TenLoai;
                UpdateModel(E_loai);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editloai(id);
        }   
	}
}