
-- ============================== THÔNG TIN NHÓM - HỌC PHẦN ==============================
-- ============================== THÔNG TIN NHÓM - HỌC PHẦN ==============================

-- ĐỒ ÁN MÔN THỰC HÀNH DBMS.
-- Giảng viên: Thầy Nguyễn Thế Hữu.


-- NHÓM THỰC HIỆN: 
-- CÁC THÀNH VIÊN: Gồm 5 thành viên.
--	1. Nguyễn Văn Phú - 2001216041.
--	2. Phạm Nguyên Vũ - 2001216324.
--	3. Nguyễn Vũ Hiệp - 2001210163.
--	4. Huỳnh Hoàng Đức - 2001210945.
--	5. Nguyễn Quốc Cường - 2001215648.

-- =======================================================================================


-- Xóa DATABASE QL_SHOPTHOITRANG.
USE master;
DROP DATABASE QL_SHOPTHOITRANG;



-- =================================== TẠO DATABASE QL_SHOPTHOITRANG ===================================
-- =================================== TẠO DATABASE QL_SHOPTHOITRANG ===================================
-- =================================== TẠO DATABASE QL_SHOPTHOITRANG ===================================



CREATE DATABASE QL_SHOPTHOITRANG
ON PRIMARY
       (
           NAME = QL_SHOPTHOITRANG_PRIMARY,
           FILENAME = 'C:\NQC\HK1(Nam3)(2023-2024)\Thực hành hệ quản trị cơ sở dữ liệu\QL_SHOPTHOITRANG_PRIMARY.mdf',
           SIZE = 10MB,
           MAXSIZE = 20MB,
           FILEGROWTH = 10%
       ),
       (
           NAME = QL_SHOPTHOITRANG_SECOND,
           FILENAME = 'C:\NQC\HK1(Nam3)(2023-2024)\Thực hành hệ quản trị cơ sở dữ liệu\QL_SHOPTHOITRANG_SECOND.ndf',
           SIZE = 8MB,
           MAXSIZE = 20MB,
           FILEGROWTH = 10%
       )
LOG ON
    (
        NAME = QL_SHOPTHOITRANG_LOG,
        FILENAME = 'C:\NQC\HK1(Nam3)(2023-2024)\Thực hành hệ quản trị cơ sở dữ liệu\QL_SHOPTHOITRANG_LOG.ldf',
        SIZE = 8MB,
        MAXSIZE = 20MB,
        FILEGROWTH = 10%
    );
GO
USE QL_SHOPTHOITRANG;
GO


-- ======================================== TẠO BẢNG ========================================
-- ======================================== TẠO BẢNG ========================================
-- ======================================== TẠO BẢNG ========================================

CREATE TABLE Users
(
    Username VARCHAR(50) PRIMARY KEY NOT NULL,
    Password NVARCHAR(MAX) NOT NULL,
    Role VARCHAR(20) NOT NULL
);


CREATE TABLE NHANVIEN
(
    MaNV CHAR(10) PRIMARY KEY NOT NULL,
    MaBP CHAR(10) NOT NULL,
    Username VARCHAR(50)
        UNIQUE,
    HinhAnhNV VARBINARY(MAX),
    HoTenNV NVARCHAR(50) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(5),
    DiaChi NVARCHAR(60),
    Email CHAR(60) NOT NULL,
    TrangThai NVARCHAR(30),
    MaCa CHAR(10),
);

CREATE TABLE BOPHAN
(
    MaBP CHAR(10) PRIMARY KEY NOT NULL,
    TenBP NVARCHAR(40),
);

CREATE TABLE CALAMVIEC
(
    MaCa CHAR(10) PRIMARY KEY NOT NULL,
    TenCa NVARCHAR(20),
    TGBatDau TIME,
    TGKetThuc TIME,
);

CREATE TABLE SANPHAM
(
    MaSP CHAR(10) PRIMARY KEY NOT NULL,
    TenSP NVARCHAR(50),
    MaLoai CHAR(10),
    MaNhaCungCap CHAR(10),
    HinhAnhSP VARBINARY(MAX),
    GiaNhap FLOAT,
    Gia FLOAT,
    Sale FLOAT,
    MoTa NVARCHAR(60),
    SoLuongTon INT,
    MAKT CHAR(10),
    MauSac NVARCHAR(50),
);

CREATE TABLE KICHTHUOC
(
    MAKT CHAR(10) PRIMARY KEY NOT NULL,
    Size CHAR(3),
    CanNang FLOAT,
    ChieuCao FLOAT
);

CREATE TABLE LOAI
(
    MaLoai CHAR(10) NOT NULL,
    TenLoai NVARCHAR(30),
    CONSTRAINT PK_LOAI
        PRIMARY KEY (MaLoai)
);

CREATE TABLE NHACUNGCAP
(
    MaNhaCungCap CHAR(10) PRIMARY KEY NOT NULL,
    TenNhaCungCap NVARCHAR(50),
    NoiCap NVARCHAR(50), --Nơi nhập hàng về
);

CREATE TABLE HOADON
(
    MaHD INT PRIMARY KEY,
    NgayBan DATE,
    MaKH INT,
    MaNV CHAR(10),
    TongTien FLOAT,
    TongSoLuong INT
);

CREATE TABLE CHITIETHOADON
(
    MaHD INT NOT NULL,
    MaSP CHAR(10) NOT NULL,
    SoLuong INT,
    TongTien FLOAT,
    CONSTRAINT PK_CTHD
        PRIMARY KEY (
                        MaHD,
                        MaSP
                    )
);


CREATE TABLE KHACHHANG
(
    MaKH INT IDENTITY PRIMARY KEY,
    TenKH NVARCHAR(50),
    SoDienThoai BIGINT,
    DiaChi NVARCHAR(60),
    TongTienMuaHang INT,
);




-- ========================================= TẠO CÁC KHÓA NGOẠI và RBTV =========================================
-- ========================================= TẠO CÁC KHÓA NGOẠI và RBTV =========================================
-- ========================================= TẠO CÁC KHÓA NGOẠI và RBTV =========================================



ALTER TABLE NHANVIEN
ADD CONSTRAINT FK_NHANVIEN_BOPHAN
    FOREIGN KEY (MaBP)
    REFERENCES BOPHAN (MaBP);

ALTER TABLE NHANVIEN
ADD CONSTRAINT FK_NHANVIEN_CA
    FOREIGN KEY (MaCa)
    REFERENCES CALAMVIEC (MaCa);

ALTER TABLE NHANVIEN
ADD CONSTRAINT FK_NV_Users
    FOREIGN KEY (Username)
    REFERENCES Users (Username);

ALTER TABLE NHANVIEN
ADD CONSTRAINT CK_TrangThai CHECK (TrangThai IN ( N'Đang làm', N'Tạm nghỉ', N'Nghỉ việc' ));

ALTER TABLE NHANVIEN
ADD CONSTRAINT CK_GioiTinh CHECK (GioiTinh IN ( N'Nam', N'Nữ' ));

ALTER TABLE SANPHAM
ADD CONSTRAINT FK_SP_LOAI
    FOREIGN KEY (MaLoai)
    REFERENCES LOAI (MaLoai);

ALTER TABLE LOAI
ADD CONSTRAINT CK_TenLoai CHECK (TenLoai IN ( N'Quần', N'Áo', N'Áo Khoác', N'Váy' ));

ALTER TABLE SANPHAM
ADD CONSTRAINT FK_SP_NHACC
    FOREIGN KEY (MaNhaCungCap)
    REFERENCES NHACUNGCAP (MaNhaCungCap);

ALTER TABLE SANPHAM
ADD CONSTRAINT FK_SP_SIZE
    FOREIGN KEY (MAKT)
    REFERENCES KICHTHUOC (MAKT);

ALTER TABLE HOADON
ADD CONSTRAINT FK_HD_NV
    FOREIGN KEY (MaNV)
    REFERENCES NHANVIEN (MaNV);

ALTER TABLE HOADON
ADD CONSTRAINT FK_HD_KH
    FOREIGN KEY (MaKH)
    REFERENCES KHACHHANG (MaKH);

ALTER TABLE CHITIETHOADON
ADD CONSTRAINT FK_CTHD_HD
    FOREIGN KEY (MaHD)
    REFERENCES HOADON (MaHD);

ALTER TABLE CHITIETHOADON
ADD CONSTRAINT FK_CTHD_SP
    FOREIGN KEY (MaSP)
    REFERENCES SANPHAM (MaSP);





-- ============================= NHẬP DỮ LIỆU VÀO CSDL ===============================
-- ============================= NHẬP DỮ LIỆU VÀO CSDL ===============================
-- ============================= NHẬP DỮ LIỆU VÀO CSDL ===============================




INSERT INTO CALAMVIEC
(
    MaCa,
    TenCa,
    TGBatDau,
    TGKetThuc
)
VALUES
('CA01', N'Ca sáng', '08:00:00', '12:00:00'),
('CA02', N'Ca chiều', '12:00:00', '17:00:00'),
('CA03', N'Ca tối', '17:00:00', '22:00:00'),
('CA04', N'Ca cả ngày', '08:00:00', '17:00:00');




INSERT INTO BOPHAN
(
    MaBP,
    TenBP
)
VALUES
('BPQL', N'Bộ phận quản lý'),
('BPKT', N'Bộ phận kế toán'),
('BPCSKH', N'Bộ Phận CSKH'),
('BPBH', N'Bộ Phận bán hàng');




INSERT INTO KICHTHUOC
(
    MAKT,
    Size,
    CanNang,
    ChieuCao
)
VALUES
('KT1', 'S', 45, 150),
('KT2', 'M', 50, 160),
('KT3', 'L', 55, 165),
('KT4', 'XL', 70, 170),
('KT5', 'XLL', 75, 170);


INSERT INTO LOAI
(
    MaLoai,
    TenLoai
)
VALUES
('LH001', N'Áo'),
('LH002', N'Quần'),
('LH003', N'Váy'),
('LH004', N'Áo Khoác');


INSERT INTO NHACUNGCAP
(
    MaNhaCungCap,
    TenNhaCungCap,
    NoiCap
)
VALUES
('NCC001', N'Công ty ELISE', N'TP Hồ Chí Minh'),
('NCC002', N'Công ty YODY', N'Hà Nội'),
('NCC003', N'Công ty MAISON', N'TP Hồ Chí Minh'),
('NCC004', N'Công ty SIXDO', N'TP Hồ Chí Minh');



INSERT INTO KHACHHANG
(
    TenKH,
    SoDienThoai,
    DiaChi,
    TongTienMuaHang
)
VALUES
(N'Nguyễn Văn A', 123456789, N'Hà Nội', NULL),
(N'Trần Thị B', 987654321, N'Hồ Chí Minh', NULL),
(N'Phạm Văn C', 555123789, N'Hải Phòng', NULL),
(N'Lê Thị D', 112233445, N'Đà Nẵng', NULL),
(N'Nguyễn Văn E', 999888777, N'Hà Tĩnh', NULL),
(N'Trần Thị F', 666555444, N'Hà Nam', NULL),
(N'Phạm Văn G', 777777777, N'Hải Dương', NULL),
(N'Lê Thị H', 888888888, N'Bắc Giang', NULL),
(N'Nguyễn Văn I', 333333333, N'Bắc Ninh', NULL),
(N'Trần Thị K', 222222222, N'Hưng Yên', NULL),
(N'Phạm Văn L', 111111111, N'Thái Bình', NULL),
(N'Lê Thị M', 444444444, N'Thanh Hóa', NULL),
(N'Nguyễn Văn N', 555555555, N'Nghệ An', NULL),
(N'Trần Thị O', 666666666, N'Hà Tĩnh', NULL),
(N'Phạm Văn P', 777777888, N'Hải Phòng', NULL),
(N'Lê Thị Q', 888888999, N'Quảng Ninh', NULL),
(N'Nguyễn Văn R', 999999111, N'Bắc Kạn', NULL),
(N'Trần Thị S', 123456789, N'Cao Bằng', NULL),
(N'Phạm Văn T', 987654321, N'Lạng Sơn', NULL),
(N'Lê Thị U', 123456789, N'Yên Bái', NULL);


INSERT INTO Users
(
    Username,
    Password,
    Role
)
VALUES
('user001', 'password001', 'Admin'),
('user002', 'password002', 'User'),
('user003', 'password003', 'User'),
('user004', 'password004', 'User'),
('user005', 'password005', 'Admin'),
('user006', 'password006', 'User'),
('user007', 'password007', 'User'),
('user008', 'password008', 'Admin'),
('user009', 'password009', 'User'),
('user010', 'password010', 'User'),
('user011', 'password001', 'Admin'),
('user012', 'password002', 'User'),
('user013', 'password003', 'User'),
('user014', 'password004', 'User'),
('user015', 'password005', 'Admin'),
('user016', 'password006', 'User'),
('user017', 'password007', 'User'),
('user018', 'password008', 'Admin'),
('user019', 'password009', 'User'),
('user020', 'password010', 'User')



INSERT INTO SANPHAM
(
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
)
VALUES
('SP001', N'Sản phẩm 1', 'LH001', 'NCC001', NULL, 80000, 100000, 0, N'Mô tả sản phẩm 1', 50, 'KT1', N'Màu trắng'),
('SP002', N'Sản phẩm 2', 'LH002', 'NCC002', NULL, 120000, 150000, 0, N'Mô tả sản phẩm 2', 30, 'KT2', N'Màu đen'),
('SP003', N'Sản phẩm 3', 'LH003', 'NCC003', NULL, 196000, 200000, 0, N'Mô tả sản phẩm 3', 20, 'KT3', N'Màu xanh'),
('SP004', N'Sản phẩm 4', 'LH001', 'NCC004', NULL, 113000, 120000, 0, N'Mô tả sản phẩm 4', 40, 'KT4', N'Màu vàng'),
('SP005', N'Sản phẩm 5', 'LH002', 'NCC001', NULL, 150000, 180000, 0, N'Mô tả sản phẩm 5', 25, 'KT5', N'Màu hồng'),
('SP006', N'Sản phẩm 6', 'LH003', 'NCC002', NULL, 203000, 210000, 0, N'Mô tả sản phẩm 6', 35, 'KT1', N'Màu xám'),
('SP007', N'Sản phẩm 7', 'LH001', 'NCC003', NULL, 120000, 130000, 0, N'Mô tả sản phẩm 7', 45, 'KT2', N'Màu cam'),
('SP008', N'Sản phẩm 8', 'LH002', 'NCC004', NULL, 160000, 160000, 0, N'Mô tả sản phẩm 8', 28, 'KT3', N'Màu nâu'),
('SP009', N'Sản phẩm 9', 'LH003', 'NCC001', NULL, 185000, 190000, 0, N'Mô tả sản phẩm 9', 22, 'KT4', N'Màu xanh lá'),
('SP010', N'Sản phẩm 10', 'LH001', 'NCC002', NULL, 100000, 110000, 0, N'Mô tả sản phẩm 10', 33, 'KT5', N'Màu tím'),
('SP011', N'Sản phẩm 11', 'LH002', 'NCC003', NULL, 160000, 170000, 0, N'Mô tả sản phẩm 11', 60, 'KT1', N'Màu đỏ'),
('SP012', N'Sản phẩm 12', 'LH002', 'NCC003', NULL, 155000, 170000, 0, N'Mô tả sản phẩm 12', 60, 'KT1', N'Màu đỏ'),
('SP013', N'Sản phẩm 13', 'LH001', 'NCC001', NULL, 120000, 125000, 0, N'Mô tả sản phẩm 13', 37, 'KT3', N'Màu hồng'),
('SP014', N'Sản phẩm 14', 'LH002', 'NCC002', NULL, 150000, 155000, 0, N'Mô tả sản phẩm 14', 42, 'KT4', N'Màu xám'),
('SP015', N'Sản phẩm 15', 'LH003', 'NCC003', NULL, 200000, 205000, 0, N'Mô tả sản phẩm 15', 29, 'KT5', N'Màu cam'),
('SP016', N'Sản phẩm 16', 'LH001', 'NCC004', NULL, 130000, 135000, 0, N'Mô tả sản phẩm 16', 48, 'KT1', N'Màu nâu'),
('SP017', N'Sản phẩm 17', 'LH002', 'NCC001', NULL, 180000, 185000, 0, N'Mô tả sản phẩm 17', 26, 'KT2', N'Màu xanh lá'),
('SP018', N'Sản phẩm 18', 'LH003', 'NCC002', NULL, 200000, 215000, 0, N'Mô tả sản phẩm 18', 31, 'KT3', N'Màu tím'),
('SP019', N'Sản phẩm 19', 'LH001', 'NCC003', NULL, 130000, 140000, 0, N'Mô tả sản phẩm 19', 55, 'KT4', N'Màu đỏ'),
('SP020', N'Sản phẩm 20', 'LH002', 'NCC004', NULL, 160000, 165000, 0, N'Mô tả sản phẩm 20', 38, 'KT5', N'Màu vàng');



INSERT INTO NHANVIEN
(
    MaNV,
    MaBP,
    Username,
    HinhAnhNV,
    HoTenNV,
    NgaySinh,
    GioiTinh,
    DiaChi,
    Email,
    TrangThai,
    MaCa
)
VALUES
('NV001', 'BPQL', 'user001', NULL, N'Hồng Gấm', '1990-05-15', N'Nữ', N'Hà Nội', 'nv001@email.com', N'Đang làm',
 'CA01'),
('NV002', 'BPKT', 'user002', NULL, N'Tuấn Anh', '1985-08-20', N'Nam', N'Hồ Chí Minh', 'nv002@email.com',
 N'Đang làm', 'CA02'),
('NV003', 'BPCSKH', 'user003', NULL, N'Ngọc Trinh', '1992-01-10', N'Nữ', N'Hà Nội', 'nv003@email.com', N'Đang làm',
 'CA03'),
('NV004', 'BPBH', 'user004', NULL, N'Trường Giang', '1995-04-25', N'Nam', N'Đà Nẵng', 'nv004@email.com',
 N'Đang làm', 'CA04'),
('NV005', 'BPQL', 'user005', NULL, N'Thị Nga', '1988-11-30', N'Nữ', N'Hải Phòng', 'nv005@email.com', N'Đang làm',
 'CA01'),
('NV006', 'BPKT', 'user006', NULL, N'Đức Anh', '1993-07-08', N'Nam', N'Hà Nội', 'nv006@email.com', N'Đang làm',
 'CA02'),
('NV007', 'BPCSKH', 'user007', NULL, N'Thị Lan', '1996-02-18', N'Nữ', N'Thái Bình', 'nv007@email.com', N'Đang làm',
 'CA03'),
('NV008', 'BPBH', 'user008', NULL, N'Tuấn Dũng', '1991-09-12', N'Nam', N'Hồ Chí Minh', 'nv008@email.com',
 N'Đang làm', 'CA04'),
('NV009', 'BPQL', 'user009', NULL, N'Phương Thảo', '1987-06-05', N'Nữ', N'Hà Nội', 'nv009@email.com', N'Đang làm',
 'CA01'),
('NV010', 'BPKT', 'user010', NULL, N'Tuấn Anh', '1990-03-28', N'Nam', N'Hải Phòng', 'nv010@email.com', N'Đang làm',
 'CA02'),
('NV011', 'BPQL', 'user011', NULL, N'Thị Mai', '1994-04-14', N'Nữ', N'Hải Phòng', 'nv011@email.com', N'Đang làm',
 'CA01'),
('NV012', 'BPKT', 'user012', NULL, N'Quang Huy', '1993-09-20', N'Nam', N'Hồ Chí Minh', 'nv012@email.com',
 N'Đang làm', 'CA02'),
('NV013', 'BPCSKH', 'user013', NULL, N'Thị Hà', '1995-02-15', N'Nữ', N'Hà Nội', 'nv013@email.com', N'Đang làm',
 'CA03'),
('NV014', 'BPBH', 'user014', NULL, N'Tuấn Hùng', '1990-07-25', N'Nam', N'Đà Nẵng', 'nv014@email.com', N'Đang làm',
 'CA04'),
('NV015', 'BPQL', 'user015', NULL, N'Thị Hương', '1988-10-30', N'Nữ', N'Thái Bình', 'nv015@email.com', N'Đang làm',
 'CA01'),
('NV016', 'BPKT', 'user016', NULL, N'Văn Anh', '1992-07-08', N'Nam', N'Hà Nội', 'nv016@email.com', N'Đang làm',
 'CA02'),
('NV017', 'BPCSKH', 'user017', NULL, N'Thị Lan', '1996-03-18', N'Nữ', N'Thái Nguyên', 'nv017@email.com',
 N'Đang làm', 'CA03'),
('NV018', 'BPBH', 'user018', NULL, N'Tuấn Dũng', '1991-11-12', N'Nam', N'Hồ Chí Minh', 'nv018@email.com',
 N'Đang làm', 'CA04'),
('NV019', 'BPQL', 'user019', NULL, N'Thành Đạt', '1987-06-05', N'Nam', N'Hà Nội', 'nv019@email.com', N'Đang làm',
 'CA01'),
('NV020', 'BPKT', 'user020', NULL, N'Thị Thu', '1990-03-28', N'Nữ', N'Hải Phòng', 'nv020@email.com', N'Đang làm',
 'CA02');



INSERT INTO HOADON
(
    MaHD,
    NgayBan,
    MaKH,
    MaNV
)
VALUES
(1, '2023-01-01', 1, 'NV001'),
(2, '2023-01-02', 2, 'NV002'),
(3, '2023-01-03', 3, 'NV003'),
(4, '2023-01-04', 4, 'NV004'),
(5, '2023-01-05', 5, 'NV005'),
(6, '2023-01-06', 6, 'NV006'),
(7, '2023-01-07', 7, 'NV007'),
(8, '2023-01-08', 8, 'NV008'),
(9, '2023-01-09', 9, 'NV009'),
(10, '2023-01-10', 10, 'NV010'),
(11, '2023-01-11', 11, 'NV011'),
(12, '2023-01-12', 12, 'NV012'),
(13, '2023-01-13', 13, 'NV013'),
(14, '2023-01-14', 14, 'NV014'),
(15, '2023-01-15', 15, 'NV015'),
(16, '2023-01-16', 16, 'NV016'),
(17, '2023-01-17', 17, 'NV017'),
(18, '2023-01-18', 18, 'NV018'),
(19, '2023-01-19', 19, 'NV019'),
(20, '2023-01-20', 20, 'NV020');



INSERT INTO CHITIETHOADON
(
    MaHD,
    MaSP,
    SoLuong,
    TongTien
)
VALUES
(1, 'SP001', 2, 300000),
(2, 'SP002', 1, 220000),
(3, 'SP003', 3, 900000),
(4, 'SP001', 4, 600000),
(5, 'SP002', 2, 440000),
(6, 'SP003', 1, 300000),
(7, 'SP001', 3, 450000),
(8, 'SP002', 2, 440000),
(9, 'SP003', 1, 300000),
(10, 'SP001', 2, 300000),
(11, 'SP002', 3, 660000),
(12, 'SP003', 2, 600000),
(13, 'SP001', 1, 150000),
(14, 'SP002', 4, 880000),
(15, 'SP003', 3, 900000),
(16, 'SP001', 2, 300000),
(17, 'SP002', 1, 110000),
(18, 'SP003', 2, 600000),
(19, 'SP001', 3, 450000),
(20, 'SP002', 2, 220000);






-- Truy vấn cập nhật tổng tiền trong bảng chi tiết hóa đơn: theo công thức sau: (So luong * Gia ban) * (1 - Sale).
GO
UPDATE CHITIETHOADON
SET TongTien = (SoLuong * Gia * (1 - Sale))
FROM CHITIETHOADON cthd
INNER JOIN SANPHAM sp ON cthd.MaSP = sp.MaSP;



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


-- Cập nhật lại số lượng tồn sản phẩm trong bảng sản phẩm.
UPDATE SANPHAM
SET SoLuongTon = SoLuongTon - ISNULL((
    SELECT SUM(SoLuong)
    FROM CHITIETHOADON
    WHERE CHITIETHOADON.MaSP = SANPHAM.MaSP
), 0);


-- ========================================= XEM DỮ LIỆU ĐÃ NHẬP =========================================
-- ========================================= XEM DỮ LIỆU ĐÃ NHẬP =========================================
-- ========================================= XEM DỮ LIỆU ĐÃ NHẬP =========================================

SELECT * FROM NHANVIEN;

SELECT * FROM BOPHAN;

SELECT * FROM CALAMVIEC;

SELECT * FROM Users;

SELECT * FROM SANPHAM;

SELECT * FROM LOAI;

SELECT * FROM NHACUNGCAP;

SELECT * FROM KICHTHUOC;

SELECT * FROM KHACHHANG;

SELECT * FROM HOADON;

SELECT * FROM CHITIETHOADON;

