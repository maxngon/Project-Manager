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
    public class MauSacController : Controller
    {
        //
        dBanGiayDataContext data = new dBanGiayDataContext();
        // GET: /Admin/MauSac/
        public ActionResult Index(int ? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var mausac = from tt in data.MauSacs
                         select tt;
            return View(mausac.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Createmau()
        {

            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Createmau(FormCollection collection, MauSac ms)
        {

            var CB_Mamau = collection["MaMau"];
            var CB_Tenmau = collection["TenMau"];
            //Nếu CB_Loaitin có giá trị == null ( để trống )             
            if (string.IsNullOrEmpty(CB_Mamau))
            {
                ViewData["Loi"] = "Mã màu không được để trống";
            }
            else if (string.IsNullOrEmpty(CB_Tenmau))
            {
                ViewData["Loi1"] = " Tên màu không được để trống ";
            }
            else
            {
                ms.MaMau = CB_Mamau;
                ms.TenMau = CB_Tenmau;
                data.MauSacs.InsertOnSubmit(ms);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Createmau();
        }
        public ActionResult Deletemau(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_mau = data.MauSacs.First(m => m.MaMau == id);
            return View(D_mau);
        }
        [HttpPost]
        public ActionResult Deletemau(string id, FormCollection collection)
        {

            var D_mau = data.MauSacs.First(m => m.MaMau == id);
            data.MauSacs.DeleteOnSubmit(D_mau);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Editmau(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_mau = data.MauSacs.First(m => m.MaMau == id);
            return View(E_mau);
        }
        [HttpPost]
        public ActionResult Editmau(string id, FormCollection collection)
        {
            var CB_Tenmau = collection["TenMau"];
            var E_mau = data.MauSacs.First(m => m.MaMau == id);         
            if (String.IsNullOrEmpty(CB_Tenmau))
            {
                ViewData["Loi1"] = "Tên màu không được để trống";
            }

            else
            {

                E_mau.TenMau = CB_Tenmau;
                UpdateModel(E_mau);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editmau(id);
        }   
	}
}