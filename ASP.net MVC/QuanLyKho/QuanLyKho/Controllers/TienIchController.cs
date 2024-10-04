using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity.Infrastructure;

namespace QuanLyKho.Controllers
{
    public class TienIchController : Controller
    {
        //
        // GET: /TienIch/
        QuanLyKho_HongLamEntities db = new QuanLyKho_HongLamEntities();
        [HttpGet]
        public ActionResult DonViTinh(int? size, int? page,string searchstring)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "25", Value = "25" }
            };
            foreach (var item in items)
            {
                if (item.Value == size.ToString())
                {
                    item.Selected = true;
                }
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            page = page ?? 1;
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.PageNumber = pageNumber;
            var links = db.tbldvt.OrderBy(x => x.ma).AsQueryable(); // Đưa truy vấn vào biến links
            if (!string.IsNullOrEmpty(searchstring)) // Kiểm tra xem searchstring có giá trị không
            {
                links = links.Where(s => s.ma.Contains(searchstring)); // Sử dụng Contains để kiểm tra chuỗi tồn tại trong ma
            }
            var pagedLinks = links.ToPagedList(pageNumber, pageSize); // Áp dụng phân trang cho kết quả
            return View(pagedLinks);
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(tbldvt nh)
        {
            if (ModelState.IsValid)
            {
                    var existingKho = db.tbldvt.FirstOrDefault(k => k.ma == nh.ma);
                    var existingKho1 = db.tbldvt.FirstOrDefault(k => k.ten == nh.ten);
                    if (existingKho != null)
                    {
                        TempData["ErrorMessage"] = "Mã đơn vị tính đã tồn tại!";
                        return View(nh);
                    }
                    else if (existingKho1 != null)
                    {
                        TempData["ErrorMessage"] = "Tên đơn vị tính đã tồn tại!";
                        return View(nh);
                    }
                    nh.userid = Convert.ToInt32(Session["idUser"]);
                    if (db.tbldvt.Count() == 0)
                    {
                        nh.id = 0;
                    }
                    else
                    {
                        nh.id = db.tbldvt.Max(n => n.id) + 1;
                    }
                    nh.idcongty = Convert.ToInt32(Session["Congty"]);
                    nh.hide = 0;
                    nh.ngayud = DateTime.Now.Date;
                    db.tbldvt.Add(nh);
                    db.SaveChanges();
                    return RedirectToAction("DonViTinh"); 
            }
            return View(nh);
        }
        public ActionResult Xoa(string ma)
        {
            tbldvt lh = db.tbldvt.FirstOrDefault(m => m.ma == ma);
            if (lh == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã đơn vị tính!";
                return RedirectToAction("DonViTinh", "TienIch");
            }
            
            try
            {
                db.tbldvt.Remove(lh);
                db.SaveChanges();
                return RedirectToAction("DonViTinh", "TienIch");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dữ liệu đang được sử dụng và không thể xóa!";
            }
            return RedirectToAction("DonViTinh", "TienIch");
        }
        [HttpGet]
        public ActionResult Sua(string ma)
        {
            tbldvt kho = db.tbldvt.FirstOrDefault(m => m.ma == ma);
            if (kho == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã đơn vị tính!";
                return RedirectToAction("DonViTinh", "TienIch");
            }
            return View(kho);
        }
        [HttpPost]
        public ActionResult Sua(string ma, string ten)
        {
            tbldvt lh = db.tbldvt.FirstOrDefault(n => n.ma == ma);
            var existingKho1 = db.tbldvt.FirstOrDefault(k => k.ten == ten);
            if (existingKho1 != null)
            {
                TempData["ErrorMessage"] = "Tên đơn vị tính đã tồn tại!";
                return View();
            }
            if (ModelState.IsValid)
            {
                UpdateModel(lh);
                db.SaveChanges();
            }
            return RedirectToAction("DonViTinh", "TienIch");
        }

       [HttpGet]
        public ActionResult LoaiHang(int? size, int? page, string searchstring)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "25", Value = "25" }
            };
            foreach (var item in items)
            {
                if (item.Value == size.ToString())
                {
                    item.Selected = true;
                }
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            page = page ?? 1;
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.PageNumber = pageNumber;
            var links = db.tblloaihang.OrderBy(x => x.ma).AsQueryable(); // Đưa truy vấn vào biến links
            if (!string.IsNullOrEmpty(searchstring)) // Kiểm tra xem searchstring có giá trị không
            {
                links = links.Where(s => s.ma.Contains(searchstring)); // Sử dụng Contains để kiểm tra chuỗi tồn tại trong ma
            }
            var pagedLinks = links.ToPagedList(pageNumber, pageSize); // Áp dụng phân trang cho kết quả
            return View(pagedLinks);
        }
        [HttpGet]
        public ActionResult ThemMoi_LoaiHang()
        {
            ViewBag.lh = new SelectList(db.tblnhomhang.ToList().OrderBy(n => n.id), "id", "ten");
            return View();
        }
        [HttpPost]  
        public ActionResult ThemMoi_LoaiHang(tblloaihang lh)
        {
            if (ModelState.IsValid)
            {
                var existingKho = db.tblloaihang.FirstOrDefault(k => k.ma == lh.ma);
                var existingKho2 = db.tblloaihang.FirstOrDefault(k => k.ten == lh.ten);
                ViewBag.lh = new SelectList(db.tblnhomhang.ToList().OrderBy(n => n.id), "id", "ten");
                if (existingKho != null)
                {
                    TempData["ErrorMessage"] = "Mã loại hàng đã tồn tại!";
                    return View(lh);
                }
                else if (existingKho2 != null)
                {
                    TempData["ErrorMessage"] = "Tên loại hàng này đã tồn tại!";
                    return View(lh);
                }
                    lh.userid = Convert.ToInt32(Session["idUser"]);
                    if (db.tblloaihang.Count() == 0)
                    {
                        lh.id = 0;
                    }
                    else
                    {
                        lh.id = db.tblloaihang.Max(n => n.id) + 1;
                    }
                    lh.hide = 0;
                    lh.idcongty = Convert.ToInt32(Session["Congty"]);
                    lh.ngayud = DateTime.Now.Date;
                    db.tblloaihang.Add(lh);
                    db.SaveChanges();
                    return RedirectToAction("LoaiHang");
            }
            return View(lh);
        }
        public ActionResult Xoa_LoaiHang(string ma)
        {
            tblloaihang lh = db.tblloaihang.FirstOrDefault(m => m.ma == ma);
            if (lh == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã loại hàng!";
                return RedirectToAction("LoaiHang", "TienIch");
            }
            
            try
            {
                db.tblloaihang.Remove(lh);
                db.SaveChanges();
                return RedirectToAction("LoaiHang", "TienIch");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dữ liệu đang được sử dụng và không thể xóa!";
            }
            return RedirectToAction("LoaiHang", "TienIch");
        }
        [HttpGet]
        public ActionResult Sua_LoaiHang(string ma)
        {
            tblloaihang kho = db.tblloaihang.FirstOrDefault(m => m.ma == ma);
            ViewBag.lh = new SelectList(db.tblnhomhang.ToList().OrderBy(n => n.id), "id", "ten");
            if (kho == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã loại hàng!";
                return RedirectToAction("LoaiHang", "TienIch");
            }

            return View(kho);
        }
        [HttpPost]
        public ActionResult Sua_LoaiHang(string ma, string ten)
        {
            tblloaihang lh = db.tblloaihang.FirstOrDefault(n => n.ma == ma);

            if (ModelState.IsValid)
            {
                UpdateModel(lh);
                db.SaveChanges();
            }
            return RedirectToAction("LoaiHang", "TienIch");
        }

         [HttpGet]
        public ActionResult NhomHang(int? size, int? page, string searchstring)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "25", Value = "25" }
            };
            foreach (var item in items)
            {
                if (item.Value == size.ToString())
                {
                    item.Selected = true;
                }
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            page = page ?? 1;
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.PageNumber = pageNumber;
            var links = db.tblnhomhang.OrderBy(x => x.ma).AsQueryable(); // Đưa truy vấn vào biến links
            if (!string.IsNullOrEmpty(searchstring)) // Kiểm tra xem searchstring có giá trị không
            {
                links = links.Where(s => s.ma.Contains(searchstring)); // Sử dụng Contains để kiểm tra chuỗi tồn tại trong ma
            }
            var pagedLinks = links.ToPagedList(pageNumber, pageSize); // Áp dụng phân trang cho kết quả
            return View(pagedLinks);
        }
        [HttpGet]
        public ActionResult ThemMoi_NhomHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi_NhomHang(tblnhomhang nh)
        {
            if (ModelState.IsValid)
            {
            var existingNhomHang = db.tblnhomhang.FirstOrDefault(k => k.ma == nh.ma);
            var existingNhomHang1 = db.tblnhomhang.FirstOrDefault(k => k.ten == nh.ten);
            if (existingNhomHang != null)
            {
                TempData["ErrorMessage"] = "Mã nhóm hàng đã tồn tại!";
                return View(nh);
            }
            else if (existingNhomHang1 != null)
            {
                TempData["ErrorMessage"] = "Tên nhóm hàng đã tồn tại!";
                return View(nh);
            }
                nh.userid = Convert.ToInt32(Session["idUser"]);
                if (db.tblnhomhang.Count() == 0)
                {
                    nh.id = 0;
                }
                else
                {
                    nh.id = db.tblnhomhang.Max(n => n.id) + 1;
                }
                nh.hide = 0;
                nh.idcongty = Convert.ToInt32(Session["Congty"]);
                nh.ngayud = DateTime.Now.Date;
                db.tblnhomhang.Add(nh);
                db.SaveChanges();
                return RedirectToAction("NhomHang");
            }
            return View(nh);
        }
        public ActionResult Xoa_NhomHang(string ma)
        {
            tblnhomhang lh = db.tblnhomhang.FirstOrDefault(m => m.ma == ma);
            if (lh == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã nhóm hàng!";
                return RedirectToAction("NhomHang", "TienIch");
            }

            try
            {
                db.tblnhomhang.Remove(lh);
                db.SaveChanges();
                return RedirectToAction("NhomHang", "TienIch");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dữ liệu đang được sử dụng và không thể xóa!";
            }
            return RedirectToAction("NhomHang", "TienIch");
        }
        [HttpGet]
        public ActionResult Sua_NhomHang(string ma)
        {
            tblnhomhang kho = db.tblnhomhang.FirstOrDefault(m => m.ma == ma);
            if (kho == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã nhóm hàng!";
                return RedirectToAction("NhomHang", "TienIch");
            }

            return View(kho);
        }
        [HttpPost]
        public ActionResult Sua_NhomHang(string ma, string ten)
        {
            tblnhomhang lh = db.tblnhomhang.FirstOrDefault(n => n.ma == ma);
            var existingNhomHang1 = db.tblnhomhang.FirstOrDefault(k => k.ten == ten);
            if (existingNhomHang1 != null)
            {
                TempData["ErrorMessage"] = "Tên nhóm hàng đã tồn tại!";
                return View();
            }
            if (ModelState.IsValid)
            {
                UpdateModel(lh);
                db.SaveChanges();
            }
            return RedirectToAction("NhomHang", "TienIch");
        }

           [HttpGet]
        public ActionResult Kho(int? size, int? page, string searchstring)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "25", Value = "25" }
            };
            foreach (var item in items)
            {
                if (item.Value == size.ToString())
                {
                    item.Selected = true;
                }
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            page = page ?? 1;
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.PageNumber = pageNumber;
            var links = db.tblkho.OrderBy(x => x.ma).AsQueryable(); // Đưa truy vấn vào biến links
            if (!string.IsNullOrEmpty(searchstring)) // Kiểm tra xem searchstring có giá trị không
            {
                links = links.Where(s => s.ma.Contains(searchstring)); // Sử dụng Contains để kiểm tra chuỗi tồn tại trong ma
            }
            var pagedLinks = links.ToPagedList(pageNumber, pageSize); // Áp dụng phân trang cho kết quả
            return View(pagedLinks);
        }
        [HttpGet]
        public ActionResult ThemMoi_Kho()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi_Kho(tblkho nh)
        {
            if (ModelState.IsValid)
            {
                var existingKho = db.tblkho.FirstOrDefault(k => k.ma == nh.ma);
                if (existingKho != null)
                {
                    TempData["ErrorMessage"] = "Mã kho đã tồn tại!";
                    return RedirectToAction("ThemMoi_Kho", "TienIch");
                }
                    nh.userid = Convert.ToInt32(Session["idUser"]);
                    if (db.tblkho.Count() == 0)
                    {
                        nh.id = 0;
                    }
                    else
                    {
                        nh.id = db.tblkho.Max(n => n.id) + 1;
                    }
                    nh.hide = 0;
                    nh.idcongty = Convert.ToInt32(Session["Congty"]);
                    nh.ngayud = DateTime.Now.Date;
                    db.tblkho.Add(nh);
                    db.SaveChanges();
                    return RedirectToAction("Kho");
            }
            return RedirectToAction("ThemMoi_Kho", "TienIch");
        }
        public ActionResult Xoa_Kho(string ma)
        {
            tblkho lh = db.tblkho.FirstOrDefault(m => m.ma == ma);
            if (lh == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã kho!";
                return RedirectToAction("Kho", "TienIch");
            }
            
            try
            {
                db.tblkho.Remove(lh);
                db.SaveChanges();
                return RedirectToAction("Kho", "TienIch");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dữ liệu đang được sử dụng và không thể xóa!";
            }
            return RedirectToAction("Kho", "TienIch");
        }
        [HttpGet]
        public ActionResult Sua_Kho(string ma)
        {
            tblkho kho = db.tblkho.FirstOrDefault(m => m.ma == ma);
            if (kho == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã kho!";
                return RedirectToAction("Kho", "TienIch");
            }
            return View(kho);
        }
        [HttpPost]
        public ActionResult Sua_Kho(string ma, string ten)
        {
            tblkho lh = db.tblkho.FirstOrDefault(n => n.ma == ma);
            //var existingKho = db.tblkho.FirstOrDefault(k => k.ten == ten);
            //if (existingKho != null)
            //{
            //    TempData["ErrorMessage"] = "Tên kho đã tồn tại!";
            //    return View();
            //}
            if (ModelState.IsValid)
            {
                UpdateModel(lh);
                db.SaveChanges();
            }
            return RedirectToAction("Kho", "TienIch");
        }

        public ActionResult CapNhatTrangThaiKho(int id, int selectedValue)
        {
            var khoToUpdate = db.tblkho.FirstOrDefault(k => k.id == id);
            if (selectedValue == 1)
            {
                selectedValue = 0;
            }
            else
            {
                selectedValue = 1;
            }
            if (khoToUpdate != null)
            {
               
                khoToUpdate.hide = selectedValue;
                db.SaveChanges();
                return RedirectToAction("Kho");
            }
            else
            {
                ViewBag.ErrorMessage = "Không tìm thấy kho!";
                return View("Error");
            }
        }
        public ActionResult CapNhatTrangThaiNhomHang(int id, int selectedValue)
        {
            var khoToUpdate = db.tblnhomhang.FirstOrDefault(k => k.id == id);
            if (selectedValue == 1)
            {
                selectedValue = 0;
            }
            else
            {
                selectedValue = 1;
            }
            if (khoToUpdate != null)
            {

                khoToUpdate.hide = selectedValue;
                db.SaveChanges();
                return RedirectToAction("NhomHang");
            }
            else
            {
                ViewBag.ErrorMessage = "Không tìm thấy nhóm hàng!";
                return View("Error");
            }
        }
        public ActionResult CapNhatTrangThaiLoaiHang(int id, int selectedValue)
        {
            var khoToUpdate = db.tblloaihang.FirstOrDefault(k => k.id == id);
            if (selectedValue == 1)
            {
                selectedValue = 0;
            }
            else
            {
                selectedValue = 1;
            }
            if (khoToUpdate != null)
            {

                khoToUpdate.hide = selectedValue;
                db.SaveChanges();
                return RedirectToAction("LoaiHang");
            }
            else
            {
                ViewBag.ErrorMessage = "Không tìm thấy loại hàng!";
                return View("Error");
            }
        }
        public ActionResult CapNhatTrangThaiDonViTinh(int id, int selectedValue)
        {
            var khoToUpdate = db.tbldvt.FirstOrDefault(k => k.id == id);
            if (selectedValue == 1)
            {
                selectedValue = 0;
            }
            else
            {
                selectedValue = 1;
            }
            if (khoToUpdate != null)
            {

                khoToUpdate.hide = selectedValue;
                db.SaveChanges();
                return RedirectToAction("DonViTinh");
            }
            else
            {
                ViewBag.ErrorMessage = "Không tìm thấy đơn vị tính!";
                return View("Error");
            }
        }
	}

}

