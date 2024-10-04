

-- ==================================== TRUY VẤN CSDL - HIỆP TRÌNH BÀY =======================================
-- ==================================== TRUY VẤN CSDL - HIỆP TRÌNH BÀY =======================================
-- ==================================== TRUY VẤN CSDL - HIỆP TRÌNH BÀY =======================================




USE QL_SHOPTHOITRANG
GO


--1.Xuat thong tin khach hang co dia chi tai 'Ha Noi'
SELECT *
FROM KHACHHANG
WHERE DiaChi = N'Hà Nội';


--2.Xuat ten khach hang,so dien thoai,tong tien mua hang thong tin khach hang mua trong ngay 04/01/2023
SELECT KHACHHANG.TenKH,
       SoDienThoai,
       TongTienMuaHang
FROM HOADON,
     KHACHHANG
WHERE KHACHHANG.MaKH = HOADON.MaKH
      AND NgayBan = '2023-01-06';


--3 Xuat thong tin khach hang co tong tien mua hang lon hon 2000000
SELECT *
FROM KHACHHANG
WHERE TongTienMuaHang > 200000;


--4. Xuat ho ten nhan vien gioi tinh nu lam ca sang
SELECT HoTenNV
FROM NHANVIEN,
     CALAMVIEC
WHERE NHANVIEN.MaCa = CALAMVIEC.MaCa
      AND TenCa = N'Ca sáng';


--5. Xuat ho ten,gioi tinh nhan vien bo phan ban hang lam ca chieu
SELECT HoTenNV,
       GioiTinh
FROM NHANVIEN,
     BOPHAN,
     CALAMVIEC
WHERE NHANVIEN.MaBP = BOPHAN.MaBP
      AND NHANVIEN.MaCa = CALAMVIEC.MaCa
      AND TenCa = N'Ca chiều'
      AND BOPHAN.TenBP = N'Bộ phận kế toán';


--6. Xuat thong tin ho ten, gioi tinh, dia chi nhan vien thuoc bo phan ke toan co quyen admin
SELECT *
FROM NHANVIEN,
     Users,
     BOPHAN
WHERE NHANVIEN.Username = Users.Username
      AND BOPHAN.MaBP = NHANVIEN.MaBP
      AND Role = 'admin'
      AND TenBP = N'Bộ Phận bán hàng';


--7. Xuat tong so luong ma san pham 1 da ban 
SELECT SUM(CHITIETHOADON.SoLuong) AS 'Tong SL'
FROM SANPHAM,
     CHITIETHOADON
WHERE SANPHAM.MaSP = CHITIETHOADON.MaSP
      AND TenSP = N'Sản phẩm 1';


--8. Xuat ho ten, ten san pham nhan vien co bo phan ban hang,ngay 4/1/2023 ban duoc 
SELECT HoTenNV,
       TenSP
FROM NHANVIEN,
     BOPHAN,
     HOADON,
     CHITIETHOADON,
     SANPHAM
WHERE NHANVIEN.MaBP = BOPHAN.MaBP
      AND NHANVIEN.MaNV = HOADON.MaNV
      AND HOADON.MaHD = CHITIETHOADON.MaHD
      AND SANPHAM.MaSP = CHITIETHOADON.MaSP
      AND BOPHAN.TenBP = N'Bộ Phận bán hàng'
      AND NgayBan = '2023-01-04';


--9. in ten san pham va tien cua tung san pham da ban duoc
SELECT TenSP,
       SUM(CHITIETHOADON.TongTien) AS 'Tong tien ban duoc'
FROM SANPHAM,
     CHITIETHOADON,
     HOADON
WHERE CHITIETHOADON.MaSP = SANPHAM.MaSP
      AND HOADON.MaHD = CHITIETHOADON.MaHD
GROUP BY SANPHAM.TenSP;