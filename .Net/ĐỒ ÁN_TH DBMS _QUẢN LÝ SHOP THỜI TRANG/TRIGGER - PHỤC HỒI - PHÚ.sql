
GO
USE QL_SHOPTHOITRANG


-- ======================================= CÀI ĐẶT TRIGGER - TRÌNH BÀY PHÚ =======================================
-- ======================================= CÀI ĐẶT TRIGGER - TRÌNH BÀY PHÚ =======================================


-- Câu 1: Viết trigger kiểm tra thời gian bắt đầu đến thời gian kết thúc 
-- cho mỗi ca làm việc là từ 8h trở lên mỗi khi thêm ca làm việc vào bảng CALAMVIEC.
GO
CREATE TRIGGER TRIGGER_CHECK_CALAMVIEC
ON CALAMVIEC
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE DATEDIFF(HOUR, TGBatDau, TGKetThuc) < 8
    )
    BEGIN
        PRINT N'Thời gian ca làm việc phải từ 8 giờ trở lên !.'
        ROLLBACK;
    END
END
GO
-- TEST DỮ LIỆU INSERT, UPDATE CHO BẢNG CALAMVIEC:
-- TEST INSERT - Dữ liệu đúng:
INSERT INTO CALAMVIEC(MaCa, TenCa, TGBatDau, TGKetThuc) VALUES 
																('CATK567', N'Ca sáng', '06:00:00', '14:00:00'),
																('CATK890', N'Ca Full Time', '09:00:00', '22:00:00')
-- TEST INSERT - dữ liệu sai:
INSERT INTO CALAMVIEC(MaCa, TenCa, TGBatDau, TGKetThuc) VALUES 
																('CATKD768', N'Ca sáng', '06:00:00', '10:00:00'),
																('CATK8907', N'Ca sáng', '09:00:00', '14:00:00')
SELECT * FROM CALAMVIEC


-- Câu 2: Viết trigger thực hiện lấy ngày hiện tại cho cột NGAYBAN mỗi khi thêm dữ liệu vào bảng HOADON. 
GO
CREATE TRIGGER TRIGGER_UPDATE_NGAYBAN
ON HOADON
AFTER INSERT
AS
BEGIN
    UPDATE HOADON
    SET NGAYBAN = GETDATE()
    FROM dbo.HOADON, Inserted
	WHERE Inserted.MaHD = dbo.HOADON.MaHD
END;

-- TEST DỮ LIỆU INSERT CHO BẢNG HOADON DÙNG CHO TRIGGER TRIGGER_UPDATE_NGAYBAN:
-- TEST INSERT - Dữ liệu khi thêm vào:
INSERT INTO HOADON (MaHD, MaKH, MaNV) VALUES
										(100, 1, 'NV001'),
										(101, 2, 'NV002')
SELECT * FROM HOADON



-- Câu 3: Viết trigger kiểm tra Sale trong bảng SanPham phải từ 0 trở lên mỗi khi thêm hay sửa trong bảng SanPham.
GO
CREATE TRIGGER CHECK_SALE
ON SANPHAM
FOR INSERT, UPDATE
AS
BEGIN
		IF EXISTS(SELECT 1 FROM dbo.SANPHAM, Inserted WHERE Inserted.MaSP = dbo.SANPHAM.MaSP AND dbo.SANPHAM.Sale < 0)
		BEGIN
		PRINT N'Sale trong bảng sản phẩm phải từ 0 trở lên !'
			ROLLBACK TRAN
		END
END;
GO
-- THÊM DỮ LIỆU SẢN PHẨM - dữ liệu đúng:
INSERT INTO SANPHAM (MaSP, TenSP, MaLoai, MaNhaCungCap, HinhAnhSP, GiaNhap, Gia, Sale, MoTa, SoLuongTon, MAKT, MauSac)
VALUES
    ('SPTES01', N'Sản phẩm 1', 'LH001', 'NCC001', NULL, 50000, 100000, 0.1, N'Mô tả sản phẩm 1', 50, 'KT1', N'Màu trắng sám'),
    ('SPTES02', N'Sản phẩm 2', 'LH002', 'NCC002', NULL, 120000, 150000, 0.1, N'Mô tả sản phẩm 2', 30, 'KT2', N'Màu đen')
SELECT * FROM dbo.SANPHAM

-- THÊM DỮ LIỆU SẢN PHẨM - dữ liệu sai:
INSERT INTO SANPHAM (MaSP, TenSP, MaLoai, MaNhaCungCap, HinhAnhSP, GiaNhap, Gia,  Sale, MoTa, SoLuongTon, MAKT, MauSac)
VALUES
    ('SPTES06', N'Sản phẩm 1', 'LH001', 'NCC001', NULL, 60000, 100000, -0.6, N'Mô tả sản phẩm 1', 50, 'KT1', N'Màu trắng sám'),
    ('SPTES04', N'Sản phẩm 2', 'LH002', 'NCC002', NULL, 110000, 150000, -0.3, N'Mô tả sản phẩm 2', 30, 'KT2', N'Màu đen')
SELECT * FROM dbo.SANPHAM




-- Câu 4: Viết Trigger tự động thực hiện tính tổng tiền cho bảng CHITIETHOADON bằng công thức TongTien = (So luong * Gia ban) * (1 - Sale), Giá bán và Sale dựa vào bảng SANPHAM.

-- Truy vấn cập nhật tổng tiền trong bảng chi tiết hóa đơn: theo công thức sau: (So luong * Gia ban) * (1 - Sale).
GO
UPDATE CHITIETHOADON
SET TongTien = (SoLuong * Gia * (1 - Sale))
FROM CHITIETHOADON cthd
INNER JOIN SANPHAM sp ON cthd.MaSP = sp.MaSP;

--
GO
CREATE TRIGGER Trg_UpdateTongTien
ON CHITIETHOADON
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Cập nhật tổng tiền sau khi thêm, sửa hoặc xóa
    UPDATE cthd
    SET TongTien = (cthd.SoLuong * sp.Gia * (1 - sp.Sale))
    FROM CHITIETHOADON cthd
    INNER JOIN SANPHAM sp ON cthd.MaSP = sp.MaSP
    WHERE cthd.MaHD IN (SELECT MaHD FROM INSERTED) OR cthd.MaHD IN (SELECT MaHD FROM DELETED);
END;

-- THÊM DỮ LIỆU SẢN PHẨM:
INSERT INTO SANPHAM (MaSP, TenSP, MaLoai, MaNhaCungCap, HinhAnhSP, GiaNhap, Gia, Sale, MoTa, SoLuongTon, MAKT, MauSac)
VALUES
    ('SPTE001', N'Sản phẩm 1', 'LH001', 'NCC001', NULL, 90000, 100000, 0.1, N'Mô tả sản phẩm 1', 50, 'KT1', N'Màu trắng sám'),
    ('SPTE002', N'Sản phẩm 2', 'LH002', 'NCC002', NULL, 100000, 150000, 0.1, N'Mô tả sản phẩm 2', 30, 'KT2', N'Màu đen'),
    ('SPTE003', N'Sản phẩm 3', 'LH003', 'NCC003', NULL, 150000, 200000, 0.5, N'Mô tả sản phẩm 3', 20, 'KT3', N'Màu xanh'),
    ('SPTE004', N'Sản phẩm 4', 'LH001', 'NCC004', NULL, 100000, 120000, 0.3, N'Mô tả sản phẩm 4', 40, 'KT4', N'Màu vàng')
SELECT * FROM dbo.SANPHAM
-- TEST DỮ LIỆU INSERT CHO BẢNG CHITIETHOADON DÙNG CHO TRIGGER TRIGGER_UPDATE_TONGTIEN_CTHD
-- TEST INSERT - Dữ liệu khi thêm vào:
INSERT INTO CHITIETHOADON (MaHD, MaSP, SoLuong) VALUES
																	(1, 'SPTE001', 2),
																	(1, 'SPTE002', 1),
																	(1, 'SPTE003', 3),
																	(3, 'SPTE001', 4),
																	(3, 'SPTE004', 2)
SELECT * FROM CHITIETHOADON
-- Dữ liệu cập nhật.
UPDATE dbo.CHITIETHOADON SET SoLuong = 5 WHERE MaHD = 1 AND MaSP = 'SPTE001'
SELECT * FROM CHITIETHOADON




-- Câu 5: Viết trigger cập nhật tổng tiền và tổng số sản phẩm của hóa đơn mỗi khi thêm hay xóa, sửa dữ liệu trong bảng CHITIETHOADON.

-- Cập nhật Tổng tiền trong bảng hóa đơn dựa trên cột tổng tiền trong bảng chi tiết hóa đơn.
GO
UPDATE HOADON
SET TongTien = (
    SELECT ISNULL(SUM(TongTien), 0)
    FROM CHITIETHOADON
    WHERE CHITIETHOADON.MaHD = HOADON.MaHD
)
FROM HOADON;
SELECT * FROM HOADON


-- Cập nhật Tổng số lượng sản phẩm trong bảng hóa đơn dựa trên cột số lượng trong bảng chi tiết hóa đơn.
UPDATE HOADON
SET TongSoLuong = (
    SELECT ISNULL(SUM(SoLuong), 0)
    FROM CHITIETHOADON
    WHERE CHITIETHOADON.MaHD = HOADON.MaHD
)
FROM HOADON;
SELECT * FROM HOADON

--
GO
CREATE TRIGGER Trg_UpdateHoaDon
ON CHITIETHOADON
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Cập nhật tổng tiền và tổng số lượng sau khi thêm, sửa hoặc xóa
    IF EXISTS (SELECT 1 FROM INSERTED)
    BEGIN
        UPDATE HOADON
        SET 
            TongTien = ISNULL((SELECT SUM(TongTien) FROM CHITIETHOADON WHERE CHITIETHOADON.MaHD = HOADON.MaHD), 0),
            TongSoLuong = ISNULL((SELECT SUM(SoLuong) FROM CHITIETHOADON WHERE CHITIETHOADON.MaHD = HOADON.MaHD), 0)
        FROM HOADON
        INNER JOIN INSERTED i ON HOADON.MaHD = i.MaHD;
    END
    ELSE
    BEGIN
        UPDATE HOADON
        SET 
            TongTien = ISNULL((SELECT SUM(TongTien) FROM CHITIETHOADON WHERE CHITIETHOADON.MaHD = HOADON.MaHD), 0),
            TongSoLuong = ISNULL((SELECT SUM(SoLuong) FROM CHITIETHOADON WHERE CHITIETHOADON.MaHD = HOADON.MaHD), 0)
        FROM HOADON
        INNER JOIN DELETED d ON HOADON.MaHD = d.MaHD;
    END
END;


-- TEST DỮ LIỆU INSERT CHO BẢNG CHITIETHOADON:
-- TEST INSERT - Dữ liệu khi thêm vào:
INSERT INTO CHITIETHOADON (MaHD, MaSP, SoLuong) VALUES
														(4, 'SPTE001', 2),
														(4, 'SPTE002', 1),
														(4, 'SPTE003', 3),
														(5, 'SPTE001', 4),
														(5, 'SPTE004', 2)
SELECT * FROM CHITIETHOADON
SELECT * FROM HOADON
-- Dữ liệu cập nhật.
UPDATE dbo.CHITIETHOADON SET SoLuong = 10 WHERE MaHD = 5 AND MaSP = 'SPTE004'
SELECT * FROM CHITIETHOADON
SELECT * FROM HOADON




-- Câu 6: Viết trigger cập nhật tổng tiền mua hàng của mỗi khách hàng mỗi khi thêm xóa sửa hóa đơn thuộc khách hàng đó.

-- Cập nhật tổng tiền mua hàng của mỗi khách hàng có trong hóa đơn.
GO
UPDATE KHACHHANG
SET TongTienMuaHang = (
    SELECT ISNULL(SUM(TongTien), 0)
    FROM HOADON
    WHERE HOADON.MaKH = KHACHHANG.MaKH
)
FROM KHACHHANG;

SELECT * FROM KHACHHANG

--
GO
CREATE TRIGGER Trg_UpdateKhachHang
ON HOADON
AFTER UPDATE
AS
BEGIN
    -- Cập nhật tổng tiền mua hàng cho khách hàng cũ
    UPDATE KHACHHANG
    SET 
        TongTienMuaHang = ISNULL((SELECT SUM(hd.TongTien) 
                                  FROM HOADON hd
                                  WHERE hd.MaKH = KHACHHANG.MaKH), 0)
    FROM KHACHHANG
    INNER JOIN DELETED d ON KHACHHANG.MaKH = d.MaKH;

    -- Cập nhật tổng tiền mua hàng cho khách hàng mới (nếu có)
    UPDATE KHACHHANG
    SET 
        TongTienMuaHang = ISNULL((SELECT SUM(hd.TongTien) 
                                  FROM HOADON hd
                                  WHERE hd.MaKH = KHACHHANG.MaKH), 0)
    FROM KHACHHANG
    INNER JOIN INSERTED i ON KHACHHANG.MaKH = i.MaKH;

    -- Nếu là sự kiện DELETE, xóa các dòng không còn tồn tại trong bảng HOADON
    DELETE FROM KHACHHANG
    FROM KHACHHANG
    INNER JOIN DELETED d ON KHACHHANG.MaKH = d.MaKH
    WHERE NOT EXISTS (SELECT 1 FROM HOADON WHERE HOADON.MaKH = d.MaKH);
END;



DROP TRIGGER Trg_UpdateKhachHang

-- Thêm dữ liệu test.
INSERT INTO CHITIETHOADON (MaHD, MaSP, SoLuong) VALUES
														(6, 'SPTE001', 8)
-- Dữ liệu cập nhật.
SELECT * FROM KHACHHANG
SELECT * FROM CHITIETHOADON
SELECT * FROM HOADON


-- Thêm dữ liệu tiếp tục:
-- Thêm dữ liệu test.
INSERT INTO CHITIETHOADON (MaHD, MaSP, SoLuong) VALUES
														(6, 'SPTE002', 1)
-- Dữ liệu cập nhật.
SELECT * FROM KHACHHANG
SELECT * FROM CHITIETHOADON
SELECT * FROM HOADON

-- Xóa đi dữ liệu vừa thêm.
DELETE FROM CHITIETHOADON WHERE MAHD = 6 AND MaSP = 'SPTE002'



-- Câu 7: Viết trigger cập nhật số lượng tồn của sản phẩm mỗi khi thêm, xóa, sửa trong bảng chi tiết hóa đơn.

-- Cập nhật lại số lượng tồn sản phẩm trong bảng sản phẩm.
UPDATE SANPHAM
SET SoLuongTon = SoLuongTon - ISNULL((
    SELECT SUM(SoLuong)
    FROM CHITIETHOADON
    WHERE CHITIETHOADON.MaSP = SANPHAM.MaSP
), 0);

--
GO
CREATE TRIGGER UpdateSoLuongTonTrigger
ON CHITIETHOADON
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Cập nhật số lượng tồn khi có thay đổi trong bảng CHITIETHOADON
    UPDATE SANPHAM
    SET SoLuongTon = SoLuongTon
        + ISNULL((SELECT SUM(SoLuong) FROM deleted WHERE MaSP = SANPHAM.MaSP), 0)
        - ISNULL((SELECT SUM(SoLuong) FROM inserted WHERE MaSP = SANPHAM.MaSP), 0)
    FROM SANPHAM
    WHERE SANPHAM.MaSP IN (SELECT MaSP FROM deleted UNION SELECT MaSP FROM inserted);
END;



-- Thêm dữ liệu vào bảng chi tiết hóa đơn:
-- Thêm dữ liệu test.
INSERT INTO CHITIETHOADON (MaHD, MaSP, SoLuong) VALUES
														(7, 'SPTE001', 3)

-- CẬP NHẬT THAY ĐỔI SỐ LƯỢNG:													
UPDATE CHITIETHOADON SET SoLuong = 5  WHERE MaHD = 7 AND MaSP = 'SPTE001'
-- XÓA DỮ LIỆU ĐI:
DELETE  FROM CHITIETHOADON WHERE MAHD = 7 AND MaSP = 'SPTE001'

-- Dữ liệu cập nhật.
SELECT * FROM SANPHAM;
SELECT * FROM KHACHHANG
SELECT * FROM CHITIETHOADON
SELECT * FROM HOADON




-- ============================== SAO LƯU VÀ PHỤC HỒI CSDL =================================
-- ============================= SAO LƯU VÀ PHỤC HỒI CSDL ==================================



-- ============================== SAO LƯU CSDL [QL_SHOPTHOITRANG] =====================================
-- ============================== SAO LƯU CSDL [QL_SHOPTHOITRANG] =====================================


-- SAO LƯU CƠ SỞ DỮ LIỆU BẰNG FULL BACKUP.
GO
USE QL_SHOPTHOITRANG
ALTER DATABASE QL_SHOPTHOITRANG
SET RECOVERY FULL


-- 1. Đầu tiên tạo bản Full backup QUANLY_SHOPTHOITRANG_FULL.bak lần 1.
GO
BACKUP DATABASE [QL_SHOPTHOITRANG] TO  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_FULL.bak' 
GO


-- 2. Tiến hành thêm bảng DMKhach vào CSDL QL_SHOPTHOITRANG.
GO
USE QL_SHOPTHOITRANG
CREATE TABLE DMKhach
(
	MaKhach VARCHAR(20) NOT NULL PRIMARY KEY,
	TenKhach NVARCHAR(50),
	DiaChi NVARCHAR(50),
	SDT VARCHAR(20) UNIQUE
)
-- Sau đó Thêm dữ liệu cho bảng DMHang gồm: 3 bản ghi.
INSERT INTO DMKhach VALUES ('KH01', N'Nguyễn Thanh Tùng', N'Hồ Chí Minh', '9-9091-2233');
INSERT INTO DMKhach VALUES ('KH02', N'Lê Nhật Nam', N'Hồ Chí Minh', '9-1234-2134');
INSERT INTO DMKhach VALUES ('KH03', N'Nguyễn Thị Thanh', N'Cà Mau', '9-2222-3333');



-- 3. Tạo bản backup Differential lần 1.
GO
BACKUP DATABASE [QL_SHOPTHOITRANG] TO  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_DIFF.bak' WITH  DIFFERENTIAL
GO


-- 4. Thêm dữ liệu vào bảng DMKhach gồm: 4 bản ghi.
INSERT INTO DMKhach VALUES ('KH04', N'Lê Thị Lan', N'Bình Dương', '9-1111-1111');
INSERT INTO DMKhach VALUES ('KH05', N'Trần Minh Quang', N'Đồng Nai', '9-2222-5555');
INSERT INTO DMKhach VALUES ('KH06', N'Lê Văn Hải', N'Hồ Chí Minh', '9-1234-4321');
INSERT INTO DMKhach VALUES ('KH07', N'Dương Văn Hai', N'Đồng Nai', '9-1111-0000');



-- 5. Tạo bản backup Differential lần 2.
GO
BACKUP DATABASE [QL_SHOPTHOITRANG] TO  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_DIFF2.bak' WITH  DIFFERENTIAL
GO


-- 6. Thêm dữ liệu vào bảng DMKhach gồm: 2 bản ghi.
INSERT INTO DMKhach VALUES ('KH02DF4', N'Lê Nhật Nam', N'Hồ Chí Minh', '9-1215-2987');
INSERT INTO DMKhach VALUES ('KH03DF4', N'Nguyễn Thị Thanh', N'Cà Mau', '9-2892-3569');



-- 7. Tạo bản backup Transaction Log.
GO
BACKUP LOG [QL_SHOPTHOITRANG] TO  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_TRAN.trn'
GO


-- 8. Tạo bản backup Full lần 2 QUANLY_SHOPTHOITRANG_FULL2.bak
GO
BACKUP DATABASE [QL_SHOPTHOITRANG] TO  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_FULL2.bak'
GO




-- ============================== KHÔI PHỤC CSDL [QL_SHOPTHOITRANG] =====================================
-- ============================== KHÔI PHỤC CSDL [QL_SHOPTHOITRANG] =====================================

-- 1. Phục hồi CSDL [QL_SHOPTHOITRANG] FULL BACKUP LẦN 1.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_FULL.bak' WITH NORECOVERY
GO


-- 2. Phục hồi CSDL [QL_SHOPTHOITRANG] Differential lần 1.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_DIFF.bak' WITH RECOVERY
GO

-- =======================================================================================================
-- KIỂM TRA DỮ LIỆU.
-- =======================================================================================================

-- 1. Phục hồi CSDL [QL_SHOPTHOITRANG] FULL BACKUP LẦN 1.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_FULL.bak' WITH NORECOVERY
GO


-- 2. Phục hồi CSDL [QL_SHOPTHOITRANG] Differential lần 1.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_DIFF.bak' WITH NORECOVERY
GO


-- 3. Phục hồi CSDL [QL_SHOPTHOITRANG] Differential lần 2.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_DIFF2.bak' WITH RECOVERY
GO

-- =======================================================================================================
-- KIỂM TRA DỮ LIỆU.
-- =======================================================================================================


-- 1. Phục hồi CSDL [QL_SHOPTHOITRANG] FULL BACKUP LẦN 1.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_FULL.bak' WITH NORECOVERY
GO


-- 2. Phục hồi CSDL [QL_SHOPTHOITRANG] Differential lần 1.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_DIFF.bak' WITH NORECOVERY
GO


-- 3. Phục hồi CSDL [QL_SHOPTHOITRANG] Differential lần 2.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_DIFF2.bak' WITH NORECOVERY
GO


-- 4. Phục hồi CSDL [QL_SHOPTHOITRANG] Transaction Log.
GO
RESTORE LOG [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_TRAN.trn' WITH RECOVERY
GO



-- =======================================================================================================
-- KIỂM TRA DỮ LIỆU.
-- =======================================================================================================

-- 1. Phục hồi CSDL [QL_SHOPTHOITRANG] FULL BACKUP LẦN 2.
GO
RESTORE DATABASE [QL_SHOPTHOITRANG] FROM  DISK = N'D:\BACKUP_QUANLY_SHOPTHOITRANG_FULL2.bak' WITH RECOVERY
GO