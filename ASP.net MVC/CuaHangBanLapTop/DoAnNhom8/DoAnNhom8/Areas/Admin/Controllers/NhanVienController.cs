using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DoAnNhom8.Models;

namespace DoAnNhom8.Areas.Admin.Controllers
{
    public class NhanVienController : ApiController
    {
        [HttpGet]
        public List<NHANVIEN> GetNhanViens()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            return db.NHANVIENs.ToList();
        }

        [HttpGet]
        public NHANVIEN GetNhanVien(string id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            return db.NHANVIENs.FirstOrDefault(s => s.MaNV == id);
        }
    }
}
