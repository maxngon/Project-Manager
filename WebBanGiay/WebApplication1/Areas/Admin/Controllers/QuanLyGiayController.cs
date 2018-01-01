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
    public class QuanLyGiayController : Controller
    {
        //
        // GET: /Admin/QuanLyGiay/
        dBanGiayDataContext data = new dBanGiayDataContext();

        public ActionResult giaynam(int ? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login","Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var All_hoanghoa = from tt in data.Giays 
            where tt.MaLoai =="1"         
                               select tt;
            return View(All_hoanghoa.ToPagedList(pageNum, pagesize));
        }
        public ActionResult giaynu(int? page)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var All_hoanghoa = from tt in data.Giays
                               where tt.MaLoai == "2"
                               select tt;
            return View(All_hoanghoa.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Creategiaynu()
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var CB_MaMau = from mm in data.MauSacs select mm;
            ViewData["MaMau"] = new SelectList(data.MauSacs, "MaMau", "TenMau");
            var CB_MaKT = from kt in data.KichThuocs select kt;
            ViewData["MaKichThuoc"] = new SelectList(data.KichThuocs, "MaKichThuoc", "MaKichThuoc");
            var CB_Manhasx = from sx in data.NhaSanXuats select sx;
            ViewData["MaNhaSanXuat"] = new SelectList(data.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");         
            return View();
        }
        [HttpPost]
        public ActionResult Creategiaynu(FormCollection collection, Giay lgiay)
        {
            var giaynu =2;
            var CB_MaGiay = collection["MaGiay"];
            var CB_MaMau = collection["MaMau"];
            var CB_MaKichThuoc = collection["MaKichThuoc"];
            var CB_MaNhaSanXuat = collection["MaNhaSanXuat"];    
            var CB_Name = collection["TenGiay"];
            var CB_anh = collection["HinhAnh"];
            var CB_motamh = collection["MoTa"];
            var CB_Giaban = decimal.Parse(collection["GiaBan"]);
            var CB_ngaycapnhat = DateTime.Parse(collection["NgayCapNhat"]);
            if (string.IsNullOrEmpty(CB_MaGiay))
            {
                ViewData["Loi"] = " Mã giày không được để trống ";
            }

            else if (string.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi"] = " Tên giày không được để trống ";
            }
            else
            {
                lgiay.MaGiay = CB_MaGiay;
                lgiay.MaLoai = CB_MaMau;
                lgiay.MaKichThuoc = CB_MaKichThuoc;
                lgiay.MaNhaSanXuat = CB_MaNhaSanXuat;
                lgiay.MaLoai = giaynu.ToString();
                lgiay.TenGiay = CB_Name;
                lgiay.HinhAnh = CB_anh;
                lgiay.MoTa = CB_motamh;
                lgiay.GiaBan = CB_Giaban;
                lgiay.NgayCapNhat = CB_ngaycapnhat;
                data.Giays.InsertOnSubmit(lgiay);
                data.SubmitChanges();
                return RedirectToAction("giaynu");
            }
            return this.Creategiaynam();
        }
        public ActionResult Detailsgiaynu(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var Details_tin = data.Giays.Where(m => m.MaGiay == id).First();
            return View(Details_tin);
        }


        public ActionResult Creategiaynam()
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var CB_MaMau = from mm in data.MauSacs select mm;
            ViewData["MaMau"] = new SelectList(data.MauSacs, "MaMau","TenMau");
            var CB_MaKT = from kt in data.KichThuocs select kt;
            ViewData["MaKichThuoc"] = new SelectList(data.KichThuocs, "MaKichThuoc","MaKichThuoc");
            var CB_Manhasx = from sx in data.NhaSanXuats select sx;
            ViewData["MaNhaSanXuat"] = new SelectList(data.NhaSanXuats, "MaNhaSanXuat","TenNhaSanXuat");
    
            return View();
        }

        public ActionResult Detailsgiaynam(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var Details_tin = data.Giays.Where(m => m.MaGiay == id).First();
            return View(Details_tin);
        }


        [HttpPost]
        public ActionResult Creategiaynam(FormCollection collection, Giay lgiay)
        {
            var giaynam = 1;
            var CB_MaGiay = collection["MaGiay"];
            var CB_MaMau = collection["MaMau"];
            var CB_MaKichThuoc = collection["MaKichThuoc"];
            var CB_MaNhaSanXuat = collection["MaNhaSanXuat"];  
            var CB_Name = collection["TenGiay"];
            var CB_anh = collection["HinhAnh"];
            var CB_motamh = collection["MoTa"];
            var CB_Giaban = decimal.Parse(collection["GiaBan"]);
            var CB_ngaycapnhat = DateTime.Parse(collection["NgayCapNhat"]);
            if (string.IsNullOrEmpty(CB_MaGiay))
            {
                ViewData["Loi"] = " Mã giày không được để trống ";
            }            
            else  if (string.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi1"] = " Tên giày không được để trống ";
            }          
            else
            {
                lgiay.MaGiay = CB_MaGiay;
                lgiay.MaLoai = CB_MaMau;
                lgiay.MaKichThuoc = CB_MaKichThuoc;
                lgiay.MaNhaSanXuat = CB_MaNhaSanXuat;
                lgiay.MaLoai = giaynam.ToString();
                lgiay.TenGiay = CB_Name;
                lgiay.HinhAnh = CB_anh;
                lgiay.MoTa = CB_motamh;
                lgiay.GiaBan = CB_Giaban;
                lgiay.NgayCapNhat = CB_ngaycapnhat;
                data.Giays.InsertOnSubmit(lgiay);
                data.SubmitChanges();
                return RedirectToAction("giaynam");
            }
            return this.Creategiaynam();
        }
        public ActionResult Deletegiaynu(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_tin = data.Giays.First(m => m.MaGiay == id);
            return View(D_tin);
        }
        [HttpPost]
        public ActionResult Deletegiaynu(string id, FormCollection collection)
        {

            var D_tin = data.Giays.Where(m => m.MaGiay == id).First();
            data.Giays.DeleteOnSubmit(D_tin);
            data.SubmitChanges();
            return RedirectToAction("giaynu");
        }
        public ActionResult Deletegiaynam(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_tin = data.Giays.First(m => m.MaGiay == id);
            return View(D_tin);
        }
        [HttpPost]
        public ActionResult Deletegiaynam(string id, FormCollection collection)
        {
           
            var D_tin = data.Giays.Where(m => m.MaGiay == id).First();
            data.Giays.DeleteOnSubmit(D_tin);
            data.SubmitChanges();
            return RedirectToAction("giaynam");
        }
        public ActionResult Editgiaynam(string id) 
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_giay = data.Giays.First(m =>m.MaGiay == id); 
            var mamau = from lt in data.LoaiGiays   select lt;
            ViewData["MaMau"] = new SelectList(data.MauSacs, "MaMau", "TenMau");
            var makt = from lt in data.KichThuocs select lt;
            ViewData["MaKichThuoc"] = new SelectList(data.KichThuocs, "MaKichThuoc", "MaKichThuoc");
            var Manhasx = from sx in data.NhaSanXuats select sx;
            ViewData["MaNhaSanXuat"] = new SelectList(data.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            return View(E_giay);
        }
        [HttpPost]
        public ActionResult Editgiaynam(string id, FormCollection collection) 
        {
            var giaynam = 1;
            var CB_MaMau = collection["MaMau"];
            var CB_MaKichThuoc = collection["MaKichThuoc"];
            var CB_MaNhaSanXuat = collection["MaNhaSanXuat"];
            var CB_Name = collection["TenGiay"];
            var CB_anh = collection["HinhAnh"];
            var CB_motamh = collection["MoTa"];
            var CB_Giaban = decimal.Parse(collection["GiaBan"]);
            var CB_ngaycapnhat = DateTime.Parse(collection["ngaycapnhat"]);
            var Etin = data.Giays.First(m => m.MaGiay == id);
            if (String.IsNullOrEmpty(CB_Name)) 
            { 
                ViewData["Loi"] = "Tên giày không được để trống";
            }
           
            else 
            {

                Etin.MaLoai = CB_MaMau;
                Etin.MaKichThuoc = CB_MaKichThuoc;
                Etin.MaNhaSanXuat = CB_MaNhaSanXuat;
                Etin.MaLoai =giaynam.ToString();
                Etin.TenGiay = CB_Name;
                Etin.HinhAnh = CB_anh;
                Etin.MoTa = CB_motamh;
                Etin.GiaBan = CB_Giaban;
                Etin.NgayCapNhat = CB_ngaycapnhat;
                UpdateModel(Etin); 
                data.SubmitChanges();
                return RedirectToAction("giaynam");
            }
            return this.Editgiaynam(id); 
        }

        public ActionResult Editgiaynu(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_giay = data.Giays.First(m => m.MaGiay == id);
            var mamau = from lt in data.LoaiGiays select lt;
            ViewData["MaMau"] = new SelectList(data.MauSacs, "MaMau", "TenMau");
            var makt = from lt in data.KichThuocs select lt;
            ViewData["MaKichThuoc"] = new SelectList(data.KichThuocs, "MaKichThuoc", "MaKichThuoc");
            var Manhasx = from sx in data.NhaSanXuats select sx;
            ViewData["MaNhaSanXuat"] = new SelectList(data.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");        
            return View(E_giay);
        }
        [HttpPost]
        public ActionResult Editgiaynu(string id, FormCollection collection)
        {
            var giaynu = 2;
            var CB_MaMau = collection["MaMau"];
            var CB_MaKichThuoc = collection["MaKichThuoc"];
            var CB_MaNhaSanXuat = collection["MaNhaSanXuat"];
            var CB_Name = collection["TenGiay"];
            var CB_anh = collection["HinhAnh"];
            var CB_motamh = collection["MoTa"];
            var CB_Giaban = decimal.Parse(collection["GiaBan"]);
            var CB_ngaycapnhat = DateTime.Parse(collection["ngaycapnhat"]);
            var Etin = data.Giays.First(m => m.MaGiay == id);
            if (String.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi"] = "Tên giày không được để trống";
            }

            else
            {

                Etin.MaLoai = CB_MaMau;
                Etin.MaKichThuoc = CB_MaKichThuoc;
                Etin.MaNhaSanXuat = CB_MaNhaSanXuat;
                Etin.MaLoai = giaynu.ToString();
                Etin.TenGiay = CB_Name;
                Etin.HinhAnh = CB_anh;
                Etin.MoTa = CB_motamh;
                Etin.GiaBan = CB_Giaban;
                Etin.NgayCapNhat = CB_ngaycapnhat;
                UpdateModel(Etin);
                data.SubmitChanges();
                return RedirectToAction("giaynu");
            }
            return this.Editgiaynu(id);
        }   

    }
}
