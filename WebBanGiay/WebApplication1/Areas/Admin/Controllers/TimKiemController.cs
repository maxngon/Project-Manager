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
    public class TimKiemController : Controller
    {
        dBanGiayDataContext data = new dBanGiayDataContext();
        // GET: Admin/KetQuaTimKiem

        [HttpPost]
        public ActionResult KetQuaTimKiem(string txttimkiem, int? page, string Stukhoa)
        {
            ViewBag.TuKhoa = txttimkiem;
            List<Giay> listKQ = data.Giays.Where(n => n.TenGiay.Contains(txttimkiem)).ToList();
            int pagesize = 5;
            int pageNum = (page ?? 1);
            if (listKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có kết quả";
            }
            return View(listKQ.OrderBy(n => n.TenGiay).ToPagedList(pageNum, pagesize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(int? page, string txttimkiem)
        {
            ViewBag.TuKhoa = txttimkiem;
            List<Giay> listKQ = data.Giays.Where(n => n.TenGiay.Contains(txttimkiem)).ToList();
            int pagesize = 5;
            int pageNum = (page ?? 1);
            if (listKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có kết quả";
                return View(data.Giays.OrderBy(n => n.TenGiay).ToPagedList(pageNum, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy" + listKQ.Count + "Kết Quả";
            return View(listKQ.OrderBy(n => n.TenGiay).ToPagedList(pageNum, pagesize));
        }
        [HttpPost]
        public ActionResult KetQuaTimKiemGia(string txtgiatu, string txtgiaden, int? page, string Stukhoa1, string Stukhoa2)
        {
            ViewBag.TuKhoa = txtgiatu;
            ViewBag.TuKhoa1 = txtgiaden;
            string giatu = txtgiatu;
            string giaden = txtgiaden;
            decimal giaTu = decimal.Parse(giatu);
            decimal giaDen = decimal.Parse(giaden);
            int pagesize = 5;
            int pageNum = (page ?? 1);
            List<ChiTietDat> listKQ = data.ChiTietDats.Where(l => l.DonGia >= giaTu && l.DonGia <= giaDen).ToList();
            if (listKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có kết quả";
            }
            return View(listKQ.OrderBy(n => n.DonGia).ToPagedList(pageNum, pagesize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiemGia(string txtgiatu, string txtgiaden,int? page, string txttimkiem)
        {
            ViewBag.TuKhoa = txtgiatu;
            ViewBag.TuKhoa1 = txtgiaden;
            string giatu = txtgiatu;
            string giaden = txtgiaden;
            decimal giaTu = decimal.Parse(giatu);
            decimal giaDen = decimal.Parse(giaden);
            List<ChiTietDat> listKQ = data.ChiTietDats.Where(l => l.DonGia >= giaTu && l.DonGia <= giaDen).ToList();
            int pagesize = 5;
            int pageNum = (page ?? 1);
            if (listKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có kết quả";
                return View(data.Giays.OrderBy(n => n.TenGiay).ToPagedList(pageNum, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy" + listKQ.Count + "Kết Quả";
            return View(listKQ.OrderBy(n => n.DonGia).ToPagedList(pageNum, pagesize));
        }
        [HttpPost]
        public ActionResult KetQuaTimKiemKH(string txttimkiem, int? page, string Stukhoa)
        {
            ViewBag.TuKhoa = txttimkiem;
            List<KhachHang> listKQ = data.KhachHangs.Where(n => n.TenKhachHang.Contains(txttimkiem)).ToList();
            int pagesize = 5;
            int pageNum = (page ?? 1);
            if (listKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có kết quả";
            }
            return View(listKQ.OrderBy(n => n.TenKhachHang).ToPagedList(pageNum, pagesize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiemKH(int? page, string txttimkiem)
        {
            ViewBag.TuKhoa = txttimkiem;
            List<KhachHang> listKQ = data.KhachHangs.Where(n => n.TenKhachHang.Contains(txttimkiem)).ToList();
            int pagesize = 5;
            int pageNum = (page ?? 1);
            if (listKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có kết quả";
                return View(listKQ.OrderBy(n => n.TenKhachHang).ToPagedList(pageNum, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy" + listKQ.Count + "Kết Quả";
            return View(listKQ.OrderBy(n => n.TenKhachHang).ToPagedList(pageNum, pagesize));
        }
    }

}