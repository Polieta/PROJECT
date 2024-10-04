




-- =================================== PROC - VŨ TRÌNH BÀY ========================================
-- =================================== PROC - VŨ TRÌNH BÀY ========================================
-- =================================== PROC - VŨ TRÌNH BÀY ========================================





USE QL_SHOPTHOITRANG;
GO

-- Câu 1: Viết thủ tục cộng vào Gía khi truyền vào MASP, TenSP, TenNhaCungCap, GIA
GO
CREATE PROC C1_Proc
    @MASP CHAR(10),
    @TenSP NVARCHAR(50),
    @TENNCC NVARCHAR(50),
    @GIA INT
AS
BEGIN
    UPDATE SANPHAM
    SET Gia = Gia + @GIA
    FROM NHACUNGCAP,
         SANPHAM
    WHERE SANPHAM.TenSP = @TenSP
          AND SANPHAM.MaSP = @MASP
          AND NHACUNGCAP.TenNhaCungCap = @TENNCC
          AND SANPHAM.TenSP = @TenSP
          AND NHACUNGCAP.MaNhaCungCap = SANPHAM.MaNhaCungCap;
END;
EXEC C1_Proc 'SP002', N'Sản phẩm 1', 'Công ty ELISE', 1000;
GO
SELECT * FROM SANPHAM
GO
-- Câu 2: Viết thủ tục truyền vào mã khách hàng, tên khách hàng nếu khách hàng mua thì in ra Tên,giá sản phẩm, còn không thì hiện ra "Chưa mua"
CREATE PROC C2_Proc
    @MAKH INT,
    @TenKH NVARCHAR(50)
AS
BEGIN
    DECLARE @CHECK INT;
    SET @CHECK =
    (
        SELECT COUNT(MaKH)FROM HOADON WHERE HOADON.MaKH = @MAKH
    );
    IF (@CHECK > 0)
        SELECT SANPHAM.TenSP,
               SANPHAM.Gia
        FROM CHITIETHOADON,
             HOADON,
             KHACHHANG,
             SANPHAM
        WHERE KHACHHANG.MaKH = HOADON.MaKH
              AND HOADON.MaHD = CHITIETHOADON.MaHD
              AND CHITIETHOADON.MaSP = SANPHAM.MaSP
              AND KHACHHANG.MaKH = @MAKH
              AND KHACHHANG.TenKH = @TenKH;
    ELSE
        PRINT (N'Khách Hàng chưa mua');
END;
GO
EXEC C2_Proc 21, N'Nguyễn Văn A';
EXEC C2_Proc 1, N'Nguyễn Văn A';
GO
-- Câu 3: Viết thủ tục truyền vào ngày bán của nhân viên trả về hóa đơn trong ngày hôm đó
CREATE PROC C3_Proc
    @DATE DATE,
    @MaNV CHAR(10)
AS
BEGIN
    DECLARE @CHECK INT;
    SET @CHECK =
    (
        SELECT COUNT(MaNV)
        FROM HOADON
        WHERE HOADON.MaNV = @MaNV
              AND HOADON.NgayBan = @DATE
    );
    IF (@CHECK > 0)
        SELECT HOADON.MaHD,
               HOADON.NgayBan
        FROM HOADON,
             NHANVIEN
        WHERE HOADON.NgayBan = @DATE
              AND HOADON.MaNV = NHANVIEN.MaNV
              AND NHANVIEN.MaNV = @MaNV;
    ELSE
        PRINT (N'Nhân Viên chưa hoàn thành đơn hàng nào!');
END;
GO
EXEC C3_Proc '2023-01-01', 'NV001';
GO
-- Câu 4: Viết thủ tục truyền vào tên nhà cung cấp, trả về số lượng sản phẩm cung cấp
CREATE PROC C4_Proc @TENNCC NVARCHAR(50)
AS
BEGIN
    SELECT NHACUNGCAP.TenNhaCungCap,
           COUNT(MaSP) AS SOSANPHAM
    FROM NHACUNGCAP,
         SANPHAM
    WHERE NHACUNGCAP.TenNhaCungCap = @TENNCC
          AND NHACUNGCAP.MaNhaCungCap = SANPHAM.MaNhaCungCap
    GROUP BY NHACUNGCAP.TenNhaCungCap;
END;
GO
EXEC C4_Proc 'Công ty ELISE';
GO
-- Câu 5: Viết thủ tục truyền vào TenBP, MaBP in ra nhân viên trong bộ phận đó 
CREATE PROC C5_Proc
    @MaBP CHAR(10),
    @TenBP NVARCHAR(40)
AS
BEGIN
    SELECT *
    FROM NHANVIEN,
         BOPHAN
    WHERE NHANVIEN.MaBP = BOPHAN.MaBP
          AND NHANVIEN.MaBP = @MaBP;
END;
GO
EXEC C5_Proc 'BPBH', 'Bộ Phận bán hàng';
GO
-- Câu 6: Viết thủ tục Đếm số nhân viên làm việc khi truyền vào MaCa
CREATE PROC C6_Proc @MACA CHAR(10)
AS
BEGIN
    DECLARE @CHECK INT;
    SET @CHECK =
    (
        SELECT COUNT(MaCa)FROM NHANVIEN WHERE NHANVIEN.MaCa = @MACA
    );
    IF (@CHECK > 0)
        SELECT CALAMVIEC.MaCa,
               CALAMVIEC.TenCa,
               COUNT(NHANVIEN.MaNV) AS SLNhanVien
        FROM CALAMVIEC,
             NHANVIEN
        WHERE CALAMVIEC.MaCa = NHANVIEN.MaCa
              AND CALAMVIEC.MaCa = @MACA
        GROUP BY CALAMVIEC.MaCa,
                 CALAMVIEC.TenCa;
    ELSE
        PRINT (N'Không có nhân viên trong ca này!!');
END;
GO
EXEC C6_Proc 'CA01';
GO
EXEC C6_Proc 'CA06';
GO



-- Câu 7: Truyền vào mã nhân viên đếm số hóa đơn (KPI) mà nhân viên đó xuất, nếu >= 2 "Đạt" , >= 1 && < 2 "Ổn" còn lại là "Chưa đạt"
CREATE PROC C7_Proc @MaNV CHAR(10)
AS
BEGIN
    DECLARE @Count INT;
    SET @Count =
    (
        SELECT COUNT(MaHD)
        FROM NHANVIEN,
             HOADON
        WHERE NHANVIEN.MaNV = HOADON.MaNV
              AND NHANVIEN.MaNV = @MaNV
    );
    IF (@Count >= 2)
        PRINT (N'Nhân Viên đạt chỉ tiêu: Đạt');
    ELSE IF (@Count < 2 AND @Count >= 1)
        PRINT (N'Nhân Viên đạt chỉ tiêu: Ổn');
    ELSE
        PRINT (N'Nhân Viên đạt chỉ tiêu: Chưa đạt');
END;
GO
EXEC C7_Proc 'NV001';
GO




-- Câu 8: Truyền vào Mã Loại, in ra sản phẩm có giá tiền cao nhất và cập nhật Sale 10000
CREATE PROC C8_Proc
    @MaLoai CHAR(10),
    @GIA INT
AS
BEGIN
    DECLARE @MAX INT;
    SET @MAX =
    (
        SELECT MAX(Gia) AS MAXGIA FROM SANPHAM WHERE SANPHAM.MaLoai = @MaLoai
    );
    UPDATE SANPHAM
    SET Sale = Sale + @GIA
    FROM SANPHAM
    WHERE SANPHAM.MaLoai = @MaLoai
          AND SANPHAM.Gia = @MAX;
    SELECT *
    FROM SANPHAM
    WHERE SANPHAM.MaLoai = @MaLoai
          AND SANPHAM.Gia = @MAX;
END;
GO
EXEC C8_Proc 'LH001', 222222;
EXEC C8_Proc 'LH002', 111111;





-- =================================== TẠO LOGIN - USERS - VŨ  TRÌNH BÀY ========================================
-- =================================== TẠO LOGIN - USERS - VŨ  TRÌNH BÀY ========================================
-- =================================== TẠO LOGIN - USERS - VŨ  TRÌNH BÀY ========================================





-- Create User, Login
-- Tạo 10 Tài Khoản
GO
sp_addlogin 'lOGIN_1', 'abc';
GO
sp_addlogin 'lOGIN_2', 'abc';
GO
sp_addlogin 'lOGIN_3', 'abc';
GO
sp_addlogin 'lOGIN_4', 'abc';
GO
sp_addlogin 'lOGIN_5', 'abc';
GO
sp_addlogin 'lOGIN_6', 'xyz';
GO
sp_addlogin 'lOGIN_7', 'xyz';
GO
sp_addlogin 'lOGIN_8', 'xyz';
GO
sp_addlogin 'lOGIN_9', 'xyz';
GO
sp_addlogin 'lOGIN_10', 'xyz';
GO



-- Tạo 10 User
USE QL_SHOPTHOITRANG;
GO
sp_adduser 'lOGIN_1', 'NguyenA';
GO
sp_adduser 'lOGIN_2', 'NguyenB';
GO
sp_adduser 'lOGIN_3', 'NguyenC';
GO
sp_adduser 'lOGIN_4', 'NguyenD';
GO
sp_adduser 'lOGIN_5', 'NguyenE';
GO
sp_adduser 'lOGIN_6', 'NguyenF';
GO
sp_adduser 'lOGIN_7', 'NguyenG';
GO
sp_adduser 'lOGIN_8', 'NguyenH';
GO
sp_adduser 'lOGIN_9', 'NguyenI';
GO
sp_adduser 'lOGIN_10', 'NguyenJ';




-- Tạo 5 nhóm quyền 
-- Ban Giám Đốc
USE QL_SHOPTHOITRANG;
CREATE ROLE BANGIAMDOC;
GO
sp_addrolemember 'BANGIAMDOC', 'NguyenA';
GO
sp_addrolemember 'BANGIAMDOC', 'NguyenB';

-- PHONGKETOAN
CREATE ROLE PHONGKETOAN;
GO
sp_addrolemember 'PHONGKETOAN', 'NguyenC';
GO
sp_addrolemember 'PHONGKETOAN', 'NguyenD';
GO
-- PHONGKINHDOANH
CREATE ROLE PHONGKINHDOANH;
GO
sp_addrolemember 'PHONGKINHDOANH', 'NguyenE';
GO
sp_addrolemember 'PHONGKINHDOANH', 'NguyenF';

-- PHONGIT
CREATE ROLE PHONGIT;
GO
sp_addrolemember 'PHONGIT', 'NguyenG';
GO
sp_addrolemember 'PHONGIT', 'NguyenH';

-- PHONGMARKETING
CREATE ROLE PHONGMARKETING;
GO
sp_addrolemember 'PHONGMARKETING', 'NguyenI';
GO
sp_addrolemember 'PHONGMARKETING', 'NguyenJ';



-- Gán quyền
-- USER TRONG MARKETING ĐƯỢC QUYỀN XEM SẢN PHẨM
GRANT SELECT ON SANPHAM TO NguyenI;
GRANT SELECT ON SANPHAM TO NguyenJ;

-- USER TRONG KẾ KẾ TOÁN ĐƯỢC QUYỀN XEM SẢN PHẨM
GRANT SELECT ON SANPHAM TO NguyenC;
GRANT SELECT ON SANPHAM TO NguyenD;

-- Cấp quyền 
-- Ban Giám Đốc, Phòng IT được toàn quyền
GRANT CONTROL TO BANGIAMDOC;
GRANT CONTROL TO PHONGIT;

-- Phòng kế toán được quyền xem,sửa HoaDon, ChiTietHoaDon
GRANT SELECT, UPDATE ON CHITIETHOADON TO PHONGKETOAN;
GRANT SELECT, UPDATE ON HOADON TO PHONGKETOAN;

-- Phòng Kinh doanh xem, xóa, sửa,thêm NhaCungCap, DoanhThu
GRANT SELECT, INSERT, UPDATE, DELETE ON NHACUNGCAP TO PHONGKINHDOANH;

-- PHONGMARKETING xem SanPham, KhachHang
GRANT SELECT ON KHACHHANG TO PHONGMARKETING;

-- Thu hồi quyền
-- Thu hồi quyền SELECT từ người dùng phòng marketing
REVOKE SELECT ON SANPHAM FROM NguyenI;
REVOKE SELECT ON SANPHAM FROM NguyenJ;

-- Thu hồi toàn bộ quyền nhóm ban giám đốc
REVOKE ALL FROM BANGIAMDOC;
