using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class NguoidungController : Controller
    {
        //
        // GET: /Nguoidung/
        dBanGiayDataContext data = new dBanGiayDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
           [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var CB_TenKH = collection["TenKhachHang"];     
            var CB_Email = collection["Email"];
            var CB_SDT = collection["SDT"];
            var CB_NgaySinh = DateTime.Parse(collection["NgaySinh"]);
            var CB_DiaChi = collection["DiaChi"];
            var CB_ID = collection["ID"];
            var CB_Pasword = collection["Pasword"];
            var CB_MatKhaunlai = collection["matkhaunhaplai"];  
            //Nếu CB_Loaitin có giá trị == null ( để trống )             
            if (string.IsNullOrEmpty(CB_TenKH))
            {
                ViewData["Loi1"] = " Tên  khách hàng không được để trống ";
            }
            else if (string.IsNullOrEmpty(CB_DiaChi))
            {
                ViewData["Loi5"] = " Địa chỉ không được để trống ";
            }
            else if(string.IsNullOrEmpty(CB_ID))
            {
                ViewData["Loi6"] = " Tên đăng nhập không được để trống ";
            }  
            else if (string.IsNullOrEmpty(CB_Pasword))
            {
                ViewData["Loi7"] = " Mật Khẩu không được để trống ";
            }
            else if (string.IsNullOrEmpty(CB_MatKhaunlai))
            {
                ViewData["Loi8"] = " Mật Khẩu nhập lại không được để trống ";
            }
            else if (CB_MatKhaunlai != CB_Pasword)
            {
                ViewData["Loi9"] = " Mật Khẩu nhập lại Không trùng khớp ";
            }
            else
            {
                kh.TenKhachHang = CB_TenKH;            
                kh.Email = CB_Email;
                kh.SDT = CB_SDT;
                kh.NgaySinh = CB_NgaySinh;
                kh.DiaChi = CB_DiaChi;
                kh.ID = CB_ID;
                kh.Pasword = CB_Pasword;
                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        public ActionResult DangNhap()
           {
               return View();
           }
          [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["taikhoan"];
            var matkhau = collection["matkhau"];
              if(String.IsNullOrEmpty(tendangnhap))
              {
                  ViewData["Loi1"] = "Phải nhập tài khoản";
              }
              else if(String.IsNullOrEmpty(matkhau))
              {
                  ViewData["Loi2"] = "Phải nhập mật khẩu";
              }
              else
              {
                  KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.ID== tendangnhap && n.Pasword == matkhau);
                  if(kh !=null)
                  {
                      ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                      Session["ID"] = kh;
                      return RedirectToAction("Index","Home");
                  }
                  else
                      ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                  
              }
              return View();
        }
	}
}