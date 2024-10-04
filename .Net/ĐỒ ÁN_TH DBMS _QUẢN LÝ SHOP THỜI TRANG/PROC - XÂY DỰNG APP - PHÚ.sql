

-- =================== PROC DÀNH CHO WINFORM - CÀI ĐẶT CHƯƠNG TRÌNH  =======================

-- =========================================================================================



GO
USE QL_SHOPTHOITRANG
-- ===================================== PROC TRANG SẢN PHẨM  ==========================================

-- =====================================================================================================
-- Proc lấy dữ liệu KICHTHUOC.
GO
CREATE PROCEDURE GetKichThuocData
AS
BEGIN
    SELECT DISTINCT MaKT, Size
    FROM KICHTHUOC;
END;


-- Proc lấy dữ liệu LOAI.
GO
CREATE PROCEDURE GetLoaiData
AS
BEGIN
    SELECT TenLoai, MaLoai
    FROM LOAI;
END;


-- Proc lấy dữ liệu NHACUNGCAP.
GO
CREATE PROCEDURE GetNhaCungCapData
AS
BEGIN
    SELECT MaNhaCungCap, TenNhaCungCap
    FROM NHACUNGCAP;
END;



-- Proc lấy dữ liệu SANPHAM.
GO
CREATE PROCEDURE GetSanPhamData
AS
BEGIN
    SELECT 
        MaSP,
        TenSP,
        MaLoai,
        MaNhaCungCap,
        HinhAnhSP,
		GiaNhap,
        Gia,
        Sale,
        MoTa,
        SoLuongTon,
        MAKT,
        MauSac
    FROM SANPHAM;
END;


-- Lọc sản phẩm theo mã loại.
GO
CREATE PROCEDURE GetSanPhamByLoai
    @MaLoai NVARCHAR(50)
AS
BEGIN
    SELECT SANPHAM.*
    FROM SANPHAM
    WHERE MaLoai = @MaLoai;
END;


-- Lọc sản phẩm theo size.
GO
CREATE PROCEDURE GetSanPhamBySize
    @Size NVARCHAR(50)
AS
BEGIN
    SELECT SANPHAM.*
    FROM SANPHAM
    WHERE MAKT = @Size;
END;



-- Tìm kiếm sản phẩm theo tên.
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


-- Xóa sản phẩm.
GO
CREATE PROCEDURE DeleteProduct
    @MaSP CHAR(10)
AS
BEGIN
    DELETE FROM SANPHAM
    WHERE MaSP = @MaSP;
END;


-- Thêm sản phẩm.
GO
CREATE PROCEDURE InsertProduct
	@MaSP CHAR(10),
    @TenSP NVARCHAR(100),
    @MaLoai NVARCHAR(50),
    @MaNhaCungCap NVARCHAR(50),
    @HinhAnhSP VARBINARY(MAX) = NULL,
    @GiaNhap FLOAT,
    @Gia FLOAT,
    @Sale FLOAT,
    @MoTa NVARCHAR(MAX),
    @SoLuongTon INT,
    @MAKT NVARCHAR(50),
    @MauSac NVARCHAR(50)
AS
BEGIN
    INSERT INTO SANPHAM (MaSP, TenSP, MaLoai, MaNhaCungCap, HinhAnhSP, GiaNhap, Gia, Sale, MoTa, SoLuongTon, MAKT, MauSac)
    VALUES (@MaSP, @TenSP, @MaLoai, @MaNhaCungCap, @HinhAnhSP,@GiaNhap, @Gia, @Sale, @MoTa, @SoLuongTon, @MAKT, @MauSac);
END;


-- Cập nhật thông tin sản phẩm.
GO
CREATE PROCEDURE UpdateSanPham
    @MaSP CHAR(10),
    @TenSP NVARCHAR(50),
    @MaLoai CHAR(10),
    @MaNhaCungCap CHAR(10),
    @HinhAnhSP VARBINARY(MAX) = NULL,
	@GiaNhap FLOAT,
    @Gia FLOAT,
    @Sale FLOAT,
    @MoTa NVARCHAR(60),
    @SoLuongTon INT,
    @MAKT CHAR(10),
    @MauSac NVARCHAR(50)
AS
BEGIN
    UPDATE SANPHAM
    SET
        TenSP = @TenSP,
        MaLoai = @MaLoai,
        MaNhaCungCap = @MaNhaCungCap,
        HinhAnhSP = @HinhAnhSP,
		GiaNhap = @GiaNhap,
        Gia = @Gia,
        Sale = @Sale,
        MoTa = @MoTa,
        SoLuongTon = @SoLuongTon,
        MAKT = @MAKT,
        MauSac = @MauSac
    WHERE MaSP = @MaSP;
END;




-- ===================================== PROC TRANG KHÁCH HÀNG  ==========================================

-- =====================================================================================================
-- Lấy thông tin khách hàng.
GO
CREATE PROCEDURE GetKhachHangData
AS
BEGIN
    SELECT 
        MaKH,
        TenKH,
        SoDienThoai,
        DiaChi,
        TongTienMuaHang
    FROM KHACHHANG;
END;


-- Thêm khách hàng.
GO
CREATE PROCEDURE InsertKhachHang
    @TenKH NVARCHAR(50),
    @SoDienThoai BIGINT,
    @DiaChi NVARCHAR(60),
    @TongTienMuaHang INT
AS
BEGIN
    INSERT INTO KHACHHANG (TenKH, SoDienThoai, DiaChi, TongTienMuaHang)
    VALUES (@TenKH, @SoDienThoai, @DiaChi, @TongTienMuaHang);
END;

-- Cập nhật khách hàng.
GO
CREATE PROCEDURE UpdateKhachHang
    @MaKH INT,
    @TenKH NVARCHAR(50),
    @SoDienThoai BIGINT,
    @DiaChi NVARCHAR(60),
    @TongTienMuaHang INT
AS
BEGIN
    UPDATE KHACHHANG
    SET
        TenKH = @TenKH,
        SoDienThoai = @SoDienThoai,
        DiaChi = @DiaChi,
        TongTienMuaHang = @TongTienMuaHang
    WHERE MaKH = @MaKH;
END;

-- Xóa khách hàng.
GO
CREATE PROCEDURE DeleteKhachHang
    @MaKH INT
AS
BEGIN
    DELETE FROM KHACHHANG
    WHERE MaKH = @MaKH;
END;

-- Tìm kiếm khách hàng.
GO
CREATE PROCEDURE SearchKhachHang
    @TenKH NVARCHAR(50)
AS
BEGIN
    SELECT 
        MaKH,
        TenKH,
        SoDienThoai,
        DiaChi,
        TongTienMuaHang
    FROM KHACHHANG
    WHERE TenKH LIKE '%' + @TenKH + '%';
END;





-- ===================================== PROC TRANG LẬP HÓA ĐƠN  ==========================================

-- ========================================================================================================
-- Lấy thông tin số lượng tồn sản phẩm.
GO
CREATE PROCEDURE dbo.GetSanPhamSoLuongTon
    @MaSP CHAR(10)
AS
BEGIN
    SELECT SoLuongTon
    FROM SANPHAM
	WHERE MaSP = @MaSP
END;


-- Lấy thông tin sản phẩm.
GO
CREATE PROCEDURE dbo.GetSanPhamDataHD
AS
BEGIN
    SELECT MaSP, TenSP
    FROM SANPHAM;
END;


-- Lấy thong tin khách hàng.
GO
CREATE PROCEDURE dbo.GetKhachHangDataHD
AS
BEGIN
    SELECT MaKH, TenKH
    FROM dbo.KHACHHANG;
END;


-- Lấy thông tin nhân viên.
GO
CREATE PROCEDURE dbo.GetNhanVienDataHD
AS
BEGIN
    SELECT MaNV, HoTenNV
    FROM dbo.NHANVIEN;
END;


-- Lấy thông tin háo đơn.
GO
CREATE PROCEDURE dbo.GetHoaDonData
	@MaHD INT
AS
BEGIN
	SELECT MaHD, TongTien, TongSoLuong
    FROM dbo.HOADON
    WHERE MaHD = @MaHD
END;


-- Thêm hóa đơn.
GO
CREATE PROCEDURE dbo.InsertHoaDon
	@MaHD INT,
    @MaKH INT,
    @MaNV CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO HOADON (MaHD, MaKH, MaNV)
    VALUES (@MaHD, @MaKH, @MaNV);
END;


-- Thêm chi tiết hóa đơn.
GO
CREATE PROCEDURE dbo.InsertChiTietHoaDon
    @MaHD INT,
    @MaSP CHAR(10),
    @SoLuong INT
AS
BEGIN
    -- Thêm chi tiết hóa đơn
    INSERT INTO CHITIETHOADON (MaHD, MaSP, SoLuong)
    VALUES (@MaHD, @MaSP, @SoLuong);
END;



-- Lấy thông tin chi tiết hóa đơn.
GO
CREATE PROCEDURE dbo.GetChiTietHoaDonData
    @MaHD INT
AS
BEGIN
    SELECT ct.MaHD, ct.MaSP, sp.Gia, sp.Sale, ct.SoLuong, ct.TongTien
    FROM CHITIETHOADON ct, SANPHAM sp
    WHERE MaHD = @MaHD
	AND sp.MaSP = ct.MaSP
END;

-- Xóa chi tiết hóa đơn.
GO
CREATE PROCEDURE dbo.DeleteChiTietHoaDon
    @MaHD INT,
    @MaSP CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM CHITIETHOADON
    WHERE MaHD = @MaHD AND MaSP = @MaSP;
END;


-- Sửa chi tiết hóa đơn.
GO
CREATE PROCEDURE dbo.UpdateChiTietHoaDon
    @MaHD INT,
    @MaSP CHAR(10),
    @SoLuong INT
AS
BEGIN
    UPDATE CHITIETHOADON
    SET SoLuong = @SoLuong
    WHERE MaHD = @MaHD AND MaSP = @MaSP;
END;


-- Xóa hóa đơn.
GO
CREATE PROCEDURE DeleteHoaDon
    @MaHD INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DELETE FROM CHITIETHOADON WHERE MaHD = @MaHD;
        DELETE FROM HOADON WHERE MaHD = @MaHD;
        COMMIT TRANSACTION;
        PRINT 'Hóa đơn đã được xóa thành công.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Đã xảy ra lỗi trong quá trình xóa hóa đơn.';
    END CATCH;
END;


-- Sửa thông tin hóa đơn.
GO
CREATE PROCEDURE UpdateHoaDon
    @MaHD INT,
    @MaKH INT,
	@MaNV CHAR(10)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE HOADON
        SET 
            MaNV = @MaNV,
            MaKH = @MaKH
        WHERE MaHD = @MaHD;
        COMMIT TRANSACTION;
       PRINT 'Thông tin hóa đơn đã được cập nhật thành công.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Đã xảy ra lỗi trong quá trình cập nhật thông tin hóa đơn.';
    END CATCH;
END;


-- ===================================== PROC TRANG QUẢN LÝ HÓA ĐƠN  ==========================================

-- ============================================================================================================
-- Lọc hóa đơn theo ngày.
GO
CREATE PROCEDURE FilterHoaDonByNgayBan
    @NgayBan DATE
AS
BEGIN
    SELECT * FROM HOADON WHERE NgayBan = @NgayBan
END


-- Lọc theo khoảng thời gian.
GO
CREATE PROCEDURE FilterHoaDonByThoiGian
    @NgayBatDau DATE,
    @NgayKetThuc DATE
AS
BEGIN
    SELECT * FROM HOADON 
    WHERE NgayBan >= @NgayBatDau AND NgayBan <= @NgayKetThuc
END


-- Lấy thông tin khách hàng dựa trên mã hóa đơn.
GO
CREATE PROCEDURE GetHangHoaByMaHoaDon
    @MaHoaDon INT
AS
BEGIN
    SELECT dbo.KHACHHANG.MaKH, dbo.KHACHHANG.TenKH, dbo.KHACHHANG.SoDienThoai, dbo.KHACHHANG.TongTienMuaHang
    FROM HOADON hd
    INNER JOIN dbo.KHACHHANG ON hd.MaKH = KHACHHANG.MaKH
    WHERE HD.MaHD = @MaHoaDon
END


-- Lấy thông tin nhân viên dựa trên mã hóa đơn.
GO
CREATE PROCEDURE GetNhanVienHD
    @MaHoaDon INT
AS
BEGIN
    SELECT dbo.NHANVIEN.MaNV, dbo.NHANVIEN.HoTenNV, dbo.NHANVIEN.GioiTinh, dbo.NHANVIEN.DiaChi
    FROM HOADON hd
    INNER JOIN dbo.NHANVIEN ON hd.MaNV = NHANVIEN.MaNV
    WHERE HD.MaHD = @MaHoaDon
END




-- ===================================== PROC TRANG DOANH THU  ==========================================

-- ============================================================================================================
-- Báo cáo doanh thu trong khoảng thời gian.
GO
CREATE PROCEDURE GetDoanhThuByDate
    @NgayBatDau DATE,
    @NgayKetThuc DATE
AS
BEGIN
    CREATE TABLE #DoanhThu
    (
        MaSP CHAR(10),
        TenSP NVARCHAR(50),
        SoLuongBan INT,
        DoanhThu FLOAT,
        LoiNhuan FLOAT
    );

    INSERT INTO #DoanhThu (MaSP, TenSP, SoLuongBan, DoanhThu, LoiNhuan)
    SELECT
        SP.MaSP,
        SP.TenSP,
        SUM(CTHD.SoLuong) AS SoLuongBan,
        SUM(CTHD.TongTien) AS DoanhThu,
        SUM(SP.Gia * CTHD.SoLuong) - SUM(SP.GiaNhap * CTHD.SoLuong) AS LoiNhuan
    FROM
        CHITIETHOADON CTHD
        INNER JOIN HOADON HD ON CTHD.MaHD = HD.MaHD
        INNER JOIN SANPHAM SP ON CTHD.MaSP = SP.MaSP
        INNER JOIN NHACUNGCAP NCC ON SP.MaNhaCungCap = NCC.MaNhaCungCap
    WHERE
        HD.NgayBan BETWEEN @NgayBatDau AND @NgayKetThuc
    GROUP BY
        SP.MaSP, SP.TenSP;
    SELECT * FROM #DoanhThu;

    DROP TABLE #DoanhThu;
END;


-- Báo cáo doanh thu ngày hiện tại.
GO
CREATE PROCEDURE GetDoanhThuDate
    @NgayDT DATE
AS
BEGIN
    CREATE TABLE #DoanhThu
    (
        MaSP CHAR(10),
        TenSP NVARCHAR(50),
        SoLuongBan INT,
        DoanhThu FLOAT,
        LoiNhuan FLOAT
    );
    INSERT INTO #DoanhThu (MaSP, TenSP, SoLuongBan, DoanhThu, LoiNhuan)
    SELECT
        SP.MaSP,
        SP.TenSP,
        SUM(CTHD.SoLuong) AS SoLuongBan,
        SUM(CTHD.TongTien) AS DoanhThu,
        SUM(SP.Gia * CTHD.SoLuong) - SUM(SP.GiaNhap * CTHD.SoLuong) AS LoiNhuan
    FROM
        CHITIETHOADON CTHD
        INNER JOIN HOADON HD ON CTHD.MaHD = HD.MaHD
        INNER JOIN SANPHAM SP ON CTHD.MaSP = SP.MaSP
        INNER JOIN NHACUNGCAP NCC ON SP.MaNhaCungCap = NCC.MaNhaCungCap
    WHERE
        HD.NgayBan = @NgayDT
    GROUP BY
        SP.MaSP, SP.TenSP;

    SELECT * FROM #DoanhThu;

    DROP TABLE #DoanhThu;
END;

