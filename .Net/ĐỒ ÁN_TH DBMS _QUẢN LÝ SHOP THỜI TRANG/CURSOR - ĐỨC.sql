


-- ======================================== CURSOR - ĐỨC TRÌNH BÀY =====================================
-- ======================================== CURSOR - ĐỨC TRÌNH BÀY =====================================
-- ======================================== CURSOR - ĐỨC TRÌNH BÀY =====================================


GO
USE QL_SHOPTHOITRANG;

---1. Lấy danh sách tất cả các nhân viên và thông tin về bộ phận của họ.
DECLARE @MaNV CHAR(10), @HoTenNV NVARCHAR(50), @MaBP CHAR(10)

DECLARE EmployeeCursor CURSOR FOR
SELECT MaNV, HoTenNV, MaBP
FROM NHANVIEN;

OPEN EmployeeCursor;
FETCH NEXT FROM EmployeeCursor INTO @MaNV, @HoTenNV, @MaBP;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Thực hiện công việc với thông tin nhân viên
    PRINT 'Employee ID: ' + @MaNV + ', Name: ' + @HoTenNV + ', Department: ' + @MaBP;

    FETCH NEXT FROM EmployeeCursor INTO @MaNV, @HoTenNV, @MaBP;
END

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;

---2. Lấy danh sách các sản phẩm và loại sản phẩm tương ứng.
DECLARE @MaSP CHAR(10), @TenSP NVARCHAR(50), @TenLoai NVARCHAR(30)

DECLARE ProductCursor CURSOR FOR
SELECT MaSP, TenSP, TenLoai
FROM SANPHAM sp
JOIN LOAI l ON sp.MaLoai = l.MaLoai;

OPEN ProductCursor;
FETCH NEXT FROM ProductCursor INTO @MaSP, @TenSP, @TenLoai;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Thực hiện công việc với thông tin sản phẩm
    PRINT 'Product ID: ' + @MaSP + ', Name: ' + @TenSP + ', Category: ' + @TenLoai;

    FETCH NEXT FROM ProductCursor INTO @MaSP, @TenSP, @TenLoai;
END

CLOSE ProductCursor;
DEALLOCATE ProductCursor;

---3. Lấy danh sách các hóa đơn và tổng số lượng sản phẩm trong mỗi hóa đơn.
DECLARE @MaHD INT, @SoLuongSanPham INT;

DECLARE InvoiceCursor CURSOR FOR
SELECT MaHD, COUNT(MaSP) AS SoLuongSanPham
FROM CHITIETHOADON
GROUP BY MaHD;

OPEN InvoiceCursor;
FETCH NEXT FROM InvoiceCursor INTO @MaHD, @SoLuongSanPham;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Thực hiện công việc với thông tin hóa đơn
    PRINT 'Invoice ID: ' + CONVERT(VARCHAR, @MaHD) + ', Total Products: ' + CONVERT(VARCHAR, @SoLuongSanPham);

    FETCH NEXT FROM InvoiceCursor INTO @MaHD, @SoLuongSanPham;
END

CLOSE InvoiceCursor;
DEALLOCATE InvoiceCursor;

---4. Lấy thông tin chi tiết các hóa đơn và giảm giá tương ứng.
DECLARE @MaHD INT, @MaSP VARCHAR(10), @SoLuong INT;

DECLARE InvoiceDetailCursor CURSOR FOR
SELECT MaHD, MaSP, SoLuong
FROM CHITIETHOADON;

OPEN InvoiceDetailCursor;
FETCH NEXT FROM InvoiceDetailCursor INTO @MaHD, @MaSP, @SoLuong;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Thực hiện công việc với chi tiết hóa đơn
    PRINT 'Invoice ID: ' + CONVERT(VARCHAR, @MaHD) + ', Product ID: ' + CONVERT(VARCHAR, @MaSP) + ', Quantity: ' + CONVERT(VARCHAR, @SoLuong);

    FETCH NEXT FROM InvoiceDetailCursor INTO @MaHD, @MaSP, @SoLuong;
END

CLOSE InvoiceDetailCursor;
DEALLOCATE InvoiceDetailCursor;


---6. Lấy danh sách khách hàng và tổng tiền mua hàng của mỗi khách hàng.
DECLARE @MaKH INT, @TenKH NVARCHAR(255), @TongTienMuaHang DECIMAL(18, 2);
DECLARE CustomerCursor CURSOR FOR
SELECT MaKH, TenKH, TongTienMuaHang
FROM KHACHHANG;

OPEN CustomerCursor;
FETCH NEXT FROM CustomerCursor INTO @MaKH, @TenKH, @TongTienMuaHang;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Thực hiện công việc với thông tin khách hàng
    PRINT 'Customer ID: ' + CONVERT(VARCHAR, @MaKH) + ', Customer Name: ' + @TenKH + ', Total Purchase Amount: ' + CONVERT(VARCHAR, @TongTienMuaHang);

    FETCH NEXT FROM CustomerCursor INTO @MaKH, @TenKH, @TongTienMuaHang;
END

CLOSE CustomerCursor;
DEALLOCATE CustomerCursor;

