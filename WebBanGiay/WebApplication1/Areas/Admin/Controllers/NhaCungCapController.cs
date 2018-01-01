﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class NhaCungCapController : Controller
    {
        //
        dBanGiayDataContext data = new dBanGiayDataContext();
        // GET: /Admin/NhaCungCap/
        public ActionResult Index(int ? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var nhancc = from tt in data.NhaSanXuats
                           select tt;
            return View(nhancc.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Creatensx()
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }         
            return View();
        }
        [HttpPost]
        public ActionResult Creatensx(FormCollection collection, NhaSanXuat nsx)
        {

            var CB_Mansx = collection["MaNhaSanXuat"];
            var CB_Tennsx = collection["TenNhaSanXuat"];      
            //Nếu CB_Loaitin có giá trị == null ( để trống )             
            if (string.IsNullOrEmpty(CB_Mansx))
            {
                ViewData["Loi"] = " Mã nhà sản xuất không được để trống ";
            }
            else if (string.IsNullOrEmpty(CB_Tennsx))
            {
                ViewData["Loi1"] = " Tên nhà sản xuất không được để trống ";
            }
            else
            {
                nsx.MaNhaSanXuat = CB_Mansx;
                nsx.TenNhaSanXuat = CB_Tennsx;
                data.NhaSanXuats.InsertOnSubmit(nsx);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Deletenhasx(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_nsx = data.NhaSanXuats.First(m => m.MaNhaSanXuat == id);
            return View(D_nsx);
        }
        [HttpPost]
        public ActionResult Deletenhasx(string id, FormCollection collection)
        {

            var D_nsx = data.NhaSanXuats.First(m => m.MaNhaSanXuat == id);
            data.NhaSanXuats.DeleteOnSubmit(D_nsx);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Editnhasx(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_nhasx = data.NhaSanXuats.First(m => m.MaNhaSanXuat == id);
            return View(E_nhasx);
        }
        [HttpPost]
        public ActionResult Editnhasx(string id, FormCollection collection)
        {
            var CB_Tennsx = collection["TenNhaSanXuat"];
            var E_nhasx = data.NhaSanXuats.First(m => m.MaNhaSanXuat == id);        
            if (String.IsNullOrEmpty(CB_Tennsx))
            {
                ViewData["Loi1"] = "Tên nhà sản xuất không được để trống";
            }

            else
            {
                E_nhasx.TenNhaSanXuat = CB_Tennsx;
                UpdateModel(E_nhasx);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editnhasx(id);
        }   
	}
}