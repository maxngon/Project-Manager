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
    public class GioHangController : Controller
    {
       dBanGiayDataContext data = new dBanGiayDataContext();
        //
        // GET: /GioHang/
        public List<GioHang>laygiohang()
        {
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if (listgiohang==null)
            {
                listgiohang = new List<GioHang>();
                Session["GioHang"] = listgiohang;
            }

            return listgiohang;
        }
        public ActionResult ThemGioHang(string imagiay,string strUrl)
        {
            List<GioHang> listgiohang = laygiohang();
            GioHang sanpham = listgiohang.Find(n => n.iMaGiay == imagiay);
            if(sanpham==null)
            {
                sanpham = new GioHang(imagiay);
                listgiohang.Add(sanpham);
                return Redirect(strUrl);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strUrl);
            }
        }
        //tong so luong
        private int tongsoluong()
        {
            int iTongsoluong = 0;
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if(listgiohang!=null)
            {
                iTongsoluong=listgiohang.Sum(n => n.isoluong);
            }
            return iTongsoluong;
        }
        private double tongtien()
        {
           double itongtien = 0;
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if (listgiohang != null)
            {
                itongtien = listgiohang.Sum(n => n.thanhtien);
            }
            return itongtien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> listgiohang = laygiohang();
            if(listgiohang.Count ==0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = tongsoluong();
            ViewBag.Tongtien = tongtien();
            return View(listgiohang);
        }
        public ActionResult GioHangPartinal()
        {
            ViewBag.Tongsoluong = tongsoluong();
            ViewBag.Tongtien = tongtien();
            return PartialView();
        }
        public ActionResult XoaGioHang(string iMaSP)
        {
            List<GioHang> listgiohang = laygiohang();
            GioHang sanpham = listgiohang.SingleOrDefault(n => n.iMaGiay == iMaSP);
            if(sanpham!=null)
            {
                listgiohang.RemoveAll(n => n.iMaGiay == iMaSP);
                return RedirectToAction("GioHang");
            }
            if(listgiohang.Count==0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(String iMaSP ,FormCollection f)
        {
            List<GioHang> listgiohang = laygiohang();
            GioHang sanpham = listgiohang.SingleOrDefault(n => n.iMaGiay == iMaSP);
            if(sanpham!=null)
            {
                sanpham.isoluong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaHang()
        {
            List<GioHang> listgiohang = laygiohang();
            listgiohang.Clear();
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Nguoidung");
            }
            if(Session["GioHang"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> listgiohang = laygiohang();
            ViewBag.Tongsoluong = tongsoluong();
            ViewBag.Tongtien = tongtien();
            return View(listgiohang);
        }
      public ActionResult DatHang(FormCollection collection)
        {
            DatHang dh = new DatHang();
            KhachHang kh = (KhachHang)Session["ID"];
            List<GioHang> gh = laygiohang();
            dh.NgayDatHang = DateTime.Now;
            dh.DaGiao = false;
            var ngaygiao = DateTime.Parse(collection["NgayGiaoHang"]);
            dh.NgayGiaoHang = ngaygiao;
            dh.TenNguoiNhan = kh.TenKhachHang;
            dh.DiaChiNhan = kh.DiaChi;
            dh.DienThoaiNhan = kh.SDT;
            dh.HTThanhToan = false;
            dh.HTGiaoHang = false;
            dh.MaKhachHang = kh.MaKhachHang;
            data.DatHangs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach(var item in gh)
            {
                ChiTietDat ctdh = new ChiTietDat();
                ctdh.SoDH = dh.SoDH;
                ctdh.MaGiay = item.iMaGiay;
                ctdh.SoLuong = item.isoluong;
                ctdh.DonGia = (decimal)item.idongia;
                data.ChiTietDats.InsertOnSubmit(ctdh);

            }
            data.SubmitChanges();
            Session["GioHang"] =null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
      {
          return View();
      }
	}
}