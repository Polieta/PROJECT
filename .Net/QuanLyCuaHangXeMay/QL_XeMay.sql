create database QL_XeMay
use QL_XeMay
drop database QL_XeMay
CREATE TABLE Customers (
    IDKhachHang NVARCHAR(10)NOT NULL PRIMARY KEY,
    HoVaTen NVARCHAR(30),
    DiaChi NVARCHAR(40),
    SoDienThoai NVARCHAR(15),
);

CREATE TABLE Employees(
    IDNhanVien NVARCHAR(10)NOT NULL PRIMARY KEY,
    HoVaTen NVARCHAR(30),
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(40),
    SoDienThoai NVARCHAR(15),
    NgaySinh DATE
);

CREATE TABLE Motorbikes (
    IDXeMay NVARCHAR(10)NOT NULL PRIMARY KEY,
    TenXe NVARCHAR(30),
    HangSanXuat NVARCHAR(10),
    SoLuong INT,
    DonGiaBan FLOAT
);

CREATE TABLE Orders (
    IDDonHang NVARCHAR(10)NOT NULL PRIMARY KEY,
    IDKhachHang NVARCHAR(10)NOT NULL,
    NgayDatHang DATE DEFAULT GETDATE(),
    TongTien FLOAT,
    FOREIGN KEY (IDKhachHang) REFERENCES Customers(IDKhachHang)
);

CREATE TABLE OrderDetails (
    IDDonHang NVARCHAR(10)NOT NULL,
    IDXeMay NVARCHAR(10)NOT NULL,
    SoLuong INT,
    DonGia FLOAT,
    ThanhTien FLOAT,
    PRIMARY KEY (IDDonHang, IDXeMay),
    FOREIGN KEY (IDDonHang) REFERENCES Orders(IDDonHang),
    FOREIGN KEY (IDXeMay) REFERENCES Motorbikes(IDXeMay)
);
Create table TAIKHOAN
(
	IDTaiKhoan varchar(20),
	TaiKhoan varchar(20),
	MatKhau varchar(20),
	IDNhanVien NVARCHAR(10),
	FOREIGN KEY (IDNhanVien) REFERENCES Employees(IDNhanVien)
)

-- Nhập dữ liệu vào bảng Customers
INSERT INTO Customers VALUES 
('KH01', N'Nguyễn Văn An', N'123 Đường 1, Quận 1', '0123456789'),
('KH02', N'Trần Thị Bình', N'456 Đường 2, Quận 2', '0987654321'),
('KH03', N'Vương Anh Vũ', N'74/7 Độc Lập, Quận Tân Phú', '0283919011'),
('KH04', N'Trần Quốc Anh', N'113 Lê Lợi, Quận 3', '0348762122'),
('KH05', N'Nguyễn Ngọc Ánh Dương', N'365 Phạm Văn Đồng, Quận Bình Thạnh', '0863521890'),
('KH06', N'Phạm Thị Kim Phương', N'Đường số 12, Quận 11', '0985342178'),
('KH07', N'Lê Văn Khải', N'789 Đường 10, Quận 10', '0123987654');

-- Nhập dữ liệu vào bảng Employees
INSERT INTO Employees VALUES 
('NV01', N'Phạm Thị Lựu', N'Nữ', N'321 Đường 1, Quận 1', '0147258369', '2000-01-01'),
('NV02', N'Nguyễn Văn Mạnh', N'Nam', N'654 Đường 2, Quận 2', '0963854721', '1999-02-02'),
('NV03', N'Nguyễn Anh Quốc', N'Nam', N'212 Đường 12, Quận 11', '0876425632', '2001-06-14'),
('NV04', N'Cao Trần Uyên Nhi', N'Nữ', N'600 Đường 10, Quận 7', '0347823103', '2000-08-28'),
('NV05', N'Trần Thị Nữ', N'Nữ', N'987 Đường 10, Quận 10', '0147632598', '2001-10-10');

-- Nhập dữ liệu vào bảng Motorbikes
INSERT INTO Motorbikes VALUES 
('XM01', N'Xe Wave Alpha 110cc', 'Honda',40,20000000),
('XM02', N'Xe Winner X', 'Honda',30, 25000000),
('XM03', N'Xe Vision', 'Honda',22, 28000000),
('XM04', N'Xe SH Mode 125cc', 'Honda',34, 30000000),
('XM05', N'Xe số  Sirius', 'Yamaha',34, 21000000),
('XM06', N'Xe máy điện NEO’s', 'Yamaha', 12,13000000),
('XM07', N'Xe số Exciter', 'Yamaha',23 ,26000000),
('XM08', N'Xe ô tô Suzuki XL7 EURO 5', 'Suzuki',15, 52000000),
('XM09', N'Xe máy Raider R150', 'Suzuki',20, 30000000),
('XM10', N'Xe TM Super Carry Pro EURO 5', 'Suzuki', 20,30000000),
('XM11', N'Xe ô tô Ciaz EURO 5', 'Suzuki',22, 500000000);

-- Nhập dữ liệu vào bảng Orders
INSERT INTO Orders VALUES 
('DH01','KH01',DEFAULT,20000000),
('DH02','KH02','2023-02-02',25000000),
('DH03','KH03','2023-04-22',21000000),
('DH04','KH04','2023-05-12',50000000),
('DH05','KH05','2023-06-19',52000000),
('DH06','KH06','2023-02-21',13000000),
('DH07','KH07','2023-11-10',30000000);

-- Nhập dữ liệu vào bảng OrderDetails
INSERT INTO OrderDetails VALUES 
('DH01','XM01',1,20000000,20000000),
('DH01','XM02',2,25000000,50000000),
('DH02','XM02',1,25000000,NULL),
('DH03','XM05',1,21000000,NULL),
('DH04','XM11',1,50000000,NULL),
('DH05','XM08',1,52000000,NULL),
('DH06','XM06',1,13000000,NULL),
('DH07','XM10',1,30000000,NULL);

INSERT INTO TAIKHOAN VALUES 
('TK1','hiep','123','NV01')


select * from Customers
select * from Employees
select * from Motorbikes
select * from Orders
select * from OrderDetails
select * from TAIKHOAN
update Motorbikes set SoLuong = SoLuong -2 where TenXE = 'Xe Wave Alpha 110cc'