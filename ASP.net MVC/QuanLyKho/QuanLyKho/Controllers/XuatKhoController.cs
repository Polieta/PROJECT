using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models;
using System.Data.Entity.Infrastructure;
using Lib_QRcode;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Net;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;

namespace QuanLyKho.Controllers
{
    public class XuatKhoController : Controller
    {
        QuanLyKho_HongLamEntities db = new QuanLyKho_HongLamEntities();
        [HttpGet]
        public ActionResult PhieuXuatKho()
        {
            ViewBag.kho = new SelectList(db.tblkho.ToList().OrderBy(n => n.id), "id", "ten");
            ViewBag.lydo = new SelectList(db.tbllydoxuat.ToList().OrderBy(n => n.id), "id", "ten");
            return View();
        }
        [HttpPost]
        public ActionResult PhieuXuatKho(tblphieuxuatkho phieuxuatkho, string newReason)
        {
            ViewBag.lydo = new SelectList(db.tbllydoxuat.ToList().OrderBy(n => n.id), "id", "ten");
                bool isNewReason = false; // Biến để xác định xem người dùng đã chọn lựa chọn "Thêm mới" hay không
                if (!string.IsNullOrEmpty(newReason))
                {
                    isNewReason = true;
                    // Tạo một lý do xuất mới
                    var lydo = new tbllydoxuat();
                    int count = db.tbllydoxuat.Count();
                    if (count != 0)
                    {
                        lydo.id = db.tbllydoxuat.Max(n => n.id) + 1;
                    }
                    else
                    {
                        lydo.id = 0;
                    }
                    lydo.ma = phieuxuatkho.ma;
                    lydo.ten = newReason; // Sử dụng giá trị từ trường textbox thêm mới
                    lydo.idcongty = Convert.ToInt32(Session["Congty"]);
                    lydo.userid = Convert.ToInt32(Session["idUser"]);
                    db.tbllydoxuat.Add(lydo);
                    db.SaveChanges();

                    phieuxuatkho.idlydoxuat = lydo.id; // Sét id lý do xuất mới cho phiếu xuất kho
                }
                phieuxuatkho.userid = Convert.ToInt32(Session["idUser"]);
                phieuxuatkho.idcongty = Convert.ToInt32(Session["Congty"]);
                phieuxuatkho.id = GenerateDecimalId();
                phieuxuatkho.hide = 0;
                Session["KhoDangChon"] = phieuxuatkho.idkho;
                Session["IDphieu"] = phieuxuatkho.id;
                phieuxuatkho.ngayud = DateTime.Now.Date;
                db.tblphieuxuatkho.Add(phieuxuatkho);
                db.SaveChanges();
                return RedirectToAction("PhieuXuatKhoCT");
        }
        [HttpGet]
        public ActionResult PhieuXuatKhoCT(int? size, int? page, string searchstring)
        {
            int idkho = Convert.ToInt32(Session["KhoDangChon"]);
            var danhsach = db.tbltonkhoct.OrderBy(x => x.id).Where(m => m.idkho == idkho).ToList();
            // Truy vấn tên hàng từ bảng tblhanghoa
            var tenHangHoaDict = db.tblhanghoa.ToDictionary(h => h.id, h => h.ten);
            ViewBag.TenHangHoaDict = tenHangHoaDict; // Truyền dữ liệu tên hàng vào ViewBag
            return View(danhsach);
        }
        [HttpPost]
        public ActionResult PhieuXuatKhoCT(FormCollection form)
        {
            int idkho = Convert.ToInt32(Session["KhoDangChon"]);
            var itemId = form["submitBtn"];
            var tam = form["mahanghoa_" + itemId];
            int mahanghoa = Convert.ToInt32(tam);
            var soLuong = form["soluong_" + mahanghoa];
            var tonKhoData = db.tbltonkhoth.FirstOrDefault(t => t.mahanghoa == mahanghoa && t.idkho == idkho);
            if (tonKhoData != null)
            {
                var sln = tonKhoData.sln ?? 0;
                var slx = tonKhoData.slx ?? 0;
                tonKhoData.tondau = tonKhoData.tondau ?? 0;
                var soLuongTon = tonKhoData.tondau + sln - slx;
                if (Convert.ToDecimal(soLuong) > soLuongTon)
                {
                    TempData["ErrorMessage"] = "Trong kho không đủ số lượng cho hàng hóa được chọn";
                    return RedirectToAction("PhieuXuatKhoCT");
                }
                var phieuXuatKhoCT = new tblphieuxuatkhoct
                {
                    mahanghoa = Convert.ToInt32(mahanghoa),
                    id = Convert.ToDecimal(Session["IDphieu"]),
                    tenhanghoa = db.tblhanghoa.FirstOrDefault(h => h.id == Convert.ToInt32(mahanghoa)).ten,
                    solo = db.tbltonkhoct.FirstOrDefault(h => h.id == Convert.ToInt32(mahanghoa)).solo,
                    //hsd = db.tbltonkhoct.FirstOrDefault(h => h.id == Convert.ToInt32(mahanghoa)).hsd,
                    dvt = db.tbldvt.FirstOrDefault(h => h.id == Convert.ToInt32(mahanghoa)).ten,
                    soluong = Convert.ToInt32(soLuong),
                    thanhtien = 0
                };
                if (!db.tblphieuxuatkhoct.Any()) // Kiểm tra xem có bản ghi nào trong bảng không
                {
                    phieuXuatKhoCT.stt = 1;
                }
                else
                {
                    phieuXuatKhoCT.stt = db.tblphieuxuatkhoct.Max(t => t.stt) + 1;
                }
                db.tblphieuxuatkhoct.Add(phieuXuatKhoCT);
                db.SaveChanges();
                tbltonkhoth tonkhoth = db.tbltonkhoth.FirstOrDefault(k => k.mahanghoa == phieuXuatKhoCT.mahanghoa);
                tbltonkhoct tonkhoct = db.tbltonkhoct.FirstOrDefault(k => k.mahanghoa == phieuXuatKhoCT.mahanghoa);
                if (tonkhoct != null)
                {
                    if (tonkhoct.sln == null)
                        tonkhoct.sln = 0;
                    tonkhoct.slx += phieuXuatKhoCT.soluong;
                }
                else
                {
                    var tonkhoct1 = new tbltonkhoct();
                    tonkhoct1.id = Convert.ToDecimal(Session["IDphieu"]);
                    tonkhoct1.idkho = idkho;
                    tonkhoct1.idn = Convert.ToDecimal(Session["IDphieu"]);
                    tonkhoct1.idnguoikk = tonkhoct1.idn;
                    tonkhoct1.mahanghoa = phieuXuatKhoCT.mahanghoa;
                    tonkhoct1.solo = phieuXuatKhoCT.solo;
                    tonkhoct1.hsd = phieuXuatKhoCT.hsd;
                    tonkhoct1.sln = phieuXuatKhoCT.soluong;
                    tonkhoct1.ngayud = DateTime.Now.Date;
                    tonkhoct1.idcongty = Convert.ToInt32(Session["Congty"]);
                    db.tbltonkhoct.Add(tonkhoct1);
                    db.SaveChanges();
                }
                if (tonkhoth != null)
                {
                    if (tonkhoth.sln == null)
                        tonkhoth.sln = 0;
                    tonkhoth.sln += phieuXuatKhoCT.soluong;
                }
                else
                {
                    var tonkhoth1 = new tbltonkhoth();
                    tonkhoth1.idkho = Convert.ToInt32(Session["KhoDangChon"]);
                    tonkhoth1.mahanghoa = phieuXuatKhoCT.mahanghoa;
                    tonkhoth1.sln = phieuXuatKhoCT.soluong;
                    tonkhoth1.ngayud = DateTime.Now.Date;
                    tonkhoth1.idcongty = Convert.ToInt32(Session["Congty"]);
                    db.tbltonkhoth.Add(tonkhoth1);
                    db.SaveChanges();
                }
                db.SaveChanges();
                return RedirectToAction("PhieuXuatKhoCT");
            }
            return RedirectToAction("PhieuXuatKho");
        }


        public ActionResult XuatKho()
        {
            ViewBag.kho = new SelectList(db.tblkho.ToList().OrderBy(n => n.id), "id", "ten");
            return View();
        }
        public ActionResult XuLyMaQR(string qrData)
        {
            string[] separatedInfo = qrData.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> extractedData = new Dictionary<string, string>();
            foreach (var info in separatedInfo)
            {
                string[] keyValue = info.Split(':');
                if (keyValue.Length == 2 || keyValue.Length == 4)
                {
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();
                    if (key == "NSX" || key == "HSD")
                    {
                        for (int i = 2; i < keyValue.Length; i++)
                        {
                            value += ":" + keyValue[i].Trim();
                        }
                    }
                    extractedData.Add(key, value);
                }
            }
            string maHang = extractedData.ContainsKey("Mã hàng") ? extractedData["Mã hàng"] : "";
            string loSX = extractedData.ContainsKey("Lô SX") ? extractedData["Lô SX"] : "";
            string nsx = extractedData.ContainsKey("NSX") ? extractedData["NSX"] : "";
            string hsd = extractedData.ContainsKey("HSD") ? extractedData["HSD"] : "";
            var data = new
            {
                MaHang = maHang,
                LoSX = loSX,
                NSX = nsx,
                HSD = hsd
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LuuDuLieu(string maPhieu, string nguoiNhap, string nguoiGiao, string diaChi, string soThue, string dienGiai, string kho)
        {
            try
            {
                // Lưu thông tin phiếu nhập kho vào cơ sở dữ liệu
                tblphieunhapkho nhapkho = new tblphieunhapkho();
                nhapkho.id = GenerateDecimalId();
                nhapkho.idcongty = Convert.ToInt32(Session["Congty"]);
                nhapkho.userid = Convert.ToInt32(Session["idUser"]);
                nhapkho.ma = Convert.ToInt32(maPhieu);
                nhapkho.nguoinhapphieu = nguoiNhap;
                nhapkho.nguoigiaohang = nguoiGiao;
                nhapkho.diachi = diaChi;
                nhapkho.diengiai = dienGiai;
                nhapkho.masothue = soThue;
                nhapkho.ngayud = DateTime.Now.Date;
                nhapkho.idkho = Convert.ToInt32(kho);
                db.tblphieunhapkho.Add(nhapkho);
                // Lưu thông tin chi tiết phiếu nhập kho vào cơ sở dữ liệu
                int stt = 0;
                var dataTam = db.bangtamnhapkho.ToList();
                foreach (var item in dataTam)
                {
                    tblphieunhapkhoct chiTietPhieu = new tblphieunhapkhoct();
                    chiTietPhieu.id = nhapkho.id; // Lấy ID của phiếu nhập kho vừa tạo
                    chiTietPhieu.mahanghoa = item.mahanghoa;
                    chiTietPhieu.lot = Convert.ToString(item.solo);
                    chiTietPhieu.stt = stt + 1;
                    stt++;
                    chiTietPhieu.soluong = Convert.ToDecimal(item.soluong);
                    chiTietPhieu.hsd = item.hsd;
                    chiTietPhieu.nsx = item.nsx;
                    chiTietPhieu.ngayud = nhapkho.ngayud;
                    chiTietPhieu.idcongty = nhapkho.idcongty;
                    chiTietPhieu.maphieu = nhapkho.ma;
                    db.tblphieunhapkhoct.Add(chiTietPhieu);

                    tbltonkhoct tonkhoct = new tbltonkhoct();
                    tonkhoct.id = chiTietPhieu.id;
                    tonkhoct.idkho = nhapkho.idkho;
                    tonkhoct.idn = chiTietPhieu.id;
                    tonkhoct.idx = null;
                    tonkhoct.mahanghoa = chiTietPhieu.mahanghoa;
                    tonkhoct.solo = Convert.ToInt32(chiTietPhieu.lot);
                    tonkhoct.hsd = chiTietPhieu.hsd;
                    tonkhoct.tondau = 0;
                    tonkhoct.sln = chiTietPhieu.soluong;
                    tonkhoct.slx = 0;
                    tonkhoct.ngayud = nhapkho.ngayud;
                    tonkhoct.idcongty = nhapkho.idcongty;
                    tonkhoct.idnguoikk = Convert.ToInt32(Session["idUser"]);
                    db.tbltonkhoct.Add(tonkhoct);
                    db.SaveChanges();
                }
                db.SaveChanges();
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult LuuDuLieuTam(string maphieu, string mahang, string solo, string soluong, string nsx, string hsd)
        {
            try
            {
                int maPhieuInt = int.Parse(maphieu);
                Session["maphieu"] = maPhieuInt;
                int maHangInt = int.Parse(mahang);
                var ktHH = db.tblhanghoa.FirstOrDefault(item => item.ma == mahang);
                if (ktHH != null) //==null Đang nhập từ máy tính, != null thì nhập từ điện thoại
                {
                    maHangInt = ktHH.id;
                }
                int soLoInt = int.Parse(solo);
                int soLuongInt = int.Parse(soluong);
                var existingItem = db.bangtamnhapkho.FirstOrDefault(item => item.mahanghoa == maHangInt && item.solo == soLoInt && item.maphieu == maPhieuInt);
                if (existingItem != null)
                {
                    existingItem.soluong += soLuongInt;
                    db.Entry(existingItem).State = EntityState.Modified;
                }
                else
                {
                    bangtamnhapkho CT_nhapkhotam = new bangtamnhapkho();
                    CT_nhapkhotam.maphieu = maPhieuInt;
                    CT_nhapkhotam.mahanghoa = maHangInt;
                    CT_nhapkhotam.tenhanghoa = LayTen_TEN_HangHoa(maHangInt);
                    CT_nhapkhotam.solo = soLoInt;
                    CT_nhapkhotam.soluong = soLuongInt;
                    CT_nhapkhotam.nsx = string.IsNullOrEmpty(nsx) ? (DateTime?)null : Convert.ToDateTime(nsx);
                    CT_nhapkhotam.hsd = string.IsNullOrEmpty(hsd) ? (DateTime?)null : Convert.ToDateTime(hsd);
                    db.bangtamnhapkho.Add(CT_nhapkhotam);
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        private string LayTen_TEN_HangHoa(int idHangHoa)
        {
            var tenHangHoa = db.tblhanghoa.Where(h => h.id == idHangHoa).Select(h => h.ten).FirstOrDefault();
            return tenHangHoa; // Trả về tên hàng hóa
        }
        [HttpGet]
        public ActionResult LayDuLieuTam()
        {
            try
            {
                int a = Convert.ToInt32(Session["maphieu"]);
                if (a != 0)
                {
                    var data = db.bangtamnhapkho.Where(t => t.maphieu == a).ToList();
                    var jsonData = data.Select(item => new
                    {
                        maHangHoa = item.mahanghoa,
                        tenHangHoa = item.tenhanghoa,
                        soLo = item.solo,
                        soLuong = item.soluong,
                        ngaySanXuat = item.nsx != null ? item.nsx.ToString() : null,
                        hanSuDung = item.hsd != null ? item.hsd.ToString() : null
                    });
                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }

                var data1 = db.bangtamnhapkho.ToList();
                if (data1 == null || data1.Count == 0)
                {
                    return Json(new List<object>(), JsonRequestBehavior.AllowGet);
                }
                var jsonData1 = data1.Select(item => new
                {
                    maHangHoa = item.mahanghoa,
                    tenHangHoa = item.tenhanghoa,
                    soLo = item.solo,
                    soLuong = item.soluong,
                    ngaySanXuat = item.nsx != null ? item.nsx.ToString() : null,
                    hanSuDung = item.hsd != null ? item.hsd.ToString() : null
                });
                return Json(jsonData1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        private int LayTenHangHoa(string tenhh)
        {
            var idhh = db.tblhanghoa.Where(h => h.ten == tenhh).Select(h => h.id).FirstOrDefault();
            return idhh; // Trả về id hàng hóa từ tên
        }
        public ActionResult XoaMatHang(string maHangHoa)
        {
            int mahanghoa = LayTenHangHoa(maHangHoa);
            var hangHoa = db.bangtamnhapkho.FirstOrDefault(h => h.mahanghoa == mahanghoa);
            if (hangHoa != null)
            {
                db.bangtamnhapkho.Remove(hangHoa);
                db.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        public ActionResult LuuSuaMatHang(string maHangHoa, string soLo, string soLuong, string ngaySanXuat, string hanSuDung)
        {
            try
            {
                if (ngaySanXuat == "null")
                {
                    ngaySanXuat = "";
                }
                if (hanSuDung == "null")
                {
                    hanSuDung = "";
                }
                int mahanghoa = LayTenHangHoa(maHangHoa);
                int solo = Convert.ToInt32(soLo);
                int soluong = Convert.ToInt32(soLuong);
                var matHang = db.bangtamnhapkho.SingleOrDefault(m => m.mahanghoa == mahanghoa && m.solo == solo);
                matHang.nsx = string.IsNullOrEmpty(ngaySanXuat) ? (DateTime?)null : Convert.ToDateTime(ngaySanXuat);
                matHang.hsd = string.IsNullOrEmpty(hanSuDung) ? (DateTime?)null : Convert.ToDateTime(hanSuDung);
                matHang.mahanghoa = mahanghoa;
                matHang.solo = solo;
                matHang.soluong = soluong;

                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult XoaDuLieu()
        {
            try
            {
                var maPhieu = Convert.ToInt32(Session["maphieu"]);
                if (maPhieu != 0)
                {
                    // Xây dựng câu lệnh SQL với điều kiện WHERE
                    string sql = "DELETE FROM bangtamnhapkho WHERE maphieu = @maPhieu";
                    // Thực thi câu lệnh SQL với tham số
                    db.Database.ExecuteSqlCommand(sql, new SqlParameter("@maPhieu", maPhieu));
                    // Lưu các thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }
                else
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM bangtamnhapkho");
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public ActionResult TimTenHangHoa(string maHangHoa)
        {
            int id = Convert.ToInt32(maHangHoa);
            var tenHangHoa = db.tblhanghoa.Where(hh => hh.id == id).Select(hh => hh.ten).FirstOrDefault();
            if (!string.IsNullOrEmpty(tenHangHoa))
            {
                return Json(new { success = true, tenHangHoa = tenHangHoa }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult XemPhieu(string IDPhieu)
        {
            decimal id = Convert.ToDecimal(IDPhieu);
            var phieuNhapKho = db.tblphieunhapkho.FirstOrDefault(a => a.id == id);
            if (phieuNhapKho != null)
            {
                tblphieunhapkho phieu = new tblphieunhapkho
                {
                    ma = phieuNhapKho.ma,
                    nguoinhapphieu = phieuNhapKho.nguoinhapphieu,
                    nguoigiaohang = phieuNhapKho.nguoigiaohang,
                    diachi = phieuNhapKho.diachi,
                    masothue = phieuNhapKho.masothue,
                    diengiai = phieuNhapKho.diengiai,
                    idkho = phieuNhapKho.idkho
                };
                return Json(new { success = true, phieuNhapKho = phieu }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult XemDuLieuBang(string IDPhieu)
        {
            decimal id = Convert.ToDecimal(IDPhieu);
            var data = db.tblphieunhapkhoct.Where(t => t.id == id).ToList();
            var jsonData = data.Select(item => new
            {
                maPhieu = item.maphieu,
                maHangHoa = item.mahanghoa,
                tenHangHoa = LayTen_TEN_HangHoa(item.mahanghoa),
                soLo = item.lot,
                soLuong = item.soluong,
                ngaySanXuat = item.nsx != null ? item.nsx.ToString() : null,
                hanSuDung = item.hsd != null ? item.hsd.ToString() : null
            });
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public decimal GenerateDecimalId()
        {
            //sử dụng kết hợp của timestamp và một số ngẫu nhiên
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            Random rnd = new Random();
            int randomNumber = rnd.Next(100, 999); // Sinh một số ngẫu nhiên có 3 chữ số
            string ipPart = GetIPv4Address().Replace(".", ""); // Loại bỏ dấu chấm trong địa chỉ IP
            // Tính toán chiều dài cần cắt bớt để đạt được tổng độ dài 20
            int remainingLength = 20 - (timestamp.Length + randomNumber.ToString().Length);
            string ipPartTrimmed = ipPart.Length > remainingLength ? ipPart.Substring(0, remainingLength) : ipPart;
            string combinedId = timestamp + randomNumber.ToString() + ipPartTrimmed;
            // Chuyển đổi chuỗi kết hợp thành decimal
            decimal generatedId = decimal.Parse(combinedId);
            return generatedId;
        }
        private string GetIPv4Address()
        {
            string hostName = Dns.GetHostName(); // Lấy tên máy tính
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName); // Lấy địa chỉ IP từ tên máy tính

            foreach (IPAddress ipAddress in ipAddresses)
            {
                // Kiểm tra nếu là địa chỉ IPv4
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ipAddress.ToString(); // Trả về địa chỉ IPv4 dưới dạng chuỗi
                }
            }
            return string.Empty; // Trả về chuỗi rỗng nếu không tìm thấy địa chỉ IPv4
        }
	}
}