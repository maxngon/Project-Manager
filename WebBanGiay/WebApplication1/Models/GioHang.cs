using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Models
{
   
    public class GioHang
    {
        dBanGiayDataContext data = new dBanGiayDataContext();
        public string iSoDDH { set; get; }
        public string iMaGiay { set; get; }
        public string iTenGiay { set; get; }
        public string anh { set; get; }
        public int isoluong { set; get; }
        public double idongia { set; get; }
        public double thanhtien
        {
            get
            {
                return isoluong * idongia;
            }
        }
        public GioHang(string MaGiay)
        {
            iMaGiay = MaGiay;
            Giay giay = data.Giays.Single(n => n.MaGiay == iMaGiay);
            iTenGiay = giay.TenGiay;
            anh = giay.HinhAnh;
            idongia = double.Parse(giay.GiaBan.ToString());
            isoluong = 1;          
        }
    }
}