-- ======================================== FUNCTION - CƯỜNG TRÌNH BÀY ========================================
-- ======================================== FUNCTION - CƯỜNG TRÌNH BÀY ========================================

GO
USE QL_SHOPTHOITRANG;
---1. Tìm sản phẩm theo tên.
GO
CREATE FUNCTION dbo.SearchProductByName
(
    @ProductName NVARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM SANPHAM
    WHERE TenSP LIKE '%' + @ProductName + '%'
);
GO
SELECT * 
FROM dbo.SearchProductByName(N'Sản phẩm 1');

---2. Hàm để tính tổng số lượng sản phẩm theo loại:
GO
CREATE FUNCTION dbo.TinhTongSoLuongTheoLoai
(
    @MaLoai CHAR(10)
)
RETURNS INT
AS
BEGIN
    DECLARE @TongSoLuong INT;
    SELECT @TongSoLuong = SUM(SoLuongTon)
    FROM SANPHAM
    WHERE MaLoai = @MaLoai;

    RETURN @TongSoLuong;
END;
GO
PRINT dbo.TinhTongSoLuongTheoLoai('LH003');

---3. Hàm để lấy tên bộ phận của một nhân viên:
GO
CREATE FUNCTION dbo.LayTenBoPhan
(
    @MaNV CHAR(10)
)
RETURNS NVARCHAR(50)
AS
BEGIN
    DECLARE @TenBoPhan NVARCHAR(50);
    SELECT @TenBoPhan = TenBP
    FROM BOPHAN bp
        INNER JOIN NHANVIEN nv
            ON bp.MaBP = nv.MaBP
    WHERE nv.MaNV = @MaNV;
    RETURN @TenBoPhan;
END;
GO
PRINT dbo.LayTenBoPhan('NV001');

---4. Hàm để tính tổng số lượng sản phẩm đã bán trong một hóa đơn(mã hóa đơn):
GO
CREATE FUNCTION dbo.TinhTongSoLuongBan
(
    @MaHD INT
)
RETURNS INT
AS
BEGIN
    DECLARE @TongSoLuongBan INT;
    SELECT @TongSoLuongBan = SUM(SoLuong)
    FROM CHITIETHOADON
    WHERE MaHD = @MaHD;
    RETURN @TongSoLuongBan;
END;
GO
PRINT dbo.TinhTongSoLuongBan(7);

---5. Kiểm tra nhân viên có phải là quản lý hay không
GO
CREATE FUNCTION dbo.KiemTraQuanLy
(
    @MaNV CHAR(10)
)
RETURNS VARCHAR(5)
AS
BEGIN
    DECLARE @IsQuanLy VARCHAR(5);
    SELECT @IsQuanLy = CASE
                           WHEN Role = 'Admin' THEN
                               'Yes'
                           ELSE
                               'No'
                       END
    FROM Users
    WHERE Username =
    (
        SELECT Username FROM NHANVIEN WHERE MaNV = @MaNV
    );
    RETURN @IsQuanLy;
END;
GO
PRINT dbo.KiemTraQuanLy('NV001');

---6. Hàm lấy thông tin chi tiết hóa đơn và sản phẩm theo mã hóa đơn
GO
CREATE FUNCTION laychitiethoadon
(
    @MaHD INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT HD.MaHD,
           HD.NgayBan,
           HD.MaKH,
           HD.MaNV,
           CTHD.MaSP,
           SP.TenSP,
           CTHD.SoLuong,
           CTHD.TongTien
    FROM HOADON HD
        INNER JOIN CHITIETHOADON CTHD
            ON HD.MaHD = CTHD.MaHD
        INNER JOIN SANPHAM SP
            ON CTHD.MaSP = SP.MaSP
    WHERE HD.MaHD = @MaHD
);
GO
SELECT *
FROM laychitiethoadon(2);

---7. Hàm lấy thông tin những sản phẩm có số lượng tồn ít hơn hoặc bằng một ngưỡng cho trước
GO
CREATE FUNCTION laysanphamtonkhothap
(
    @NguongSoLuong INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM SANPHAM
    WHERE SoLuongTon <= @NguongSoLuong
);
GO
SELECT *
FROM laysanphamtonkhothap(25);

---8. Trả về bảng dựa trên nhiều điều kiện.
GO
CREATE FUNCTION LayDanhSachSanPhamTheoDieuKien
(
    @MaLoai CHAR(10),
    @GiaTien FLOAT,
    @SoLuong INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM SANPHAM
    WHERE MaLoai = @MaLoai
          AND Gia <= @GiaTien
          AND SoLuongTon <= @SoLuong
);
GO
SELECT *
FROM LayDanhSachSanPhamTheoDieuKien('LH001', 150000, 50);

---9. Trả về bảng thống kê doanh thu theo tháng.
GO
CREATE FUNCTION thongkedoanhthutheothang
(
    @Nam INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT MONTH(H.NgayBan) AS Thang,
           SUM(CTHD.TongTien) AS TongDoanhThu
    FROM HOADON H
        INNER JOIN CHITIETHOADON CTHD
            ON H.MaHD = CTHD.MaHD
    WHERE YEAR(H.NgayBan) = @Nam
    GROUP BY MONTH(H.NgayBan)
);
GO
SELECT *
FROM thongkedoanhthutheothang(2023);


