Drop database QuanLyKho_HongLam

CREATE DATABASE [QuanLyKho_HongLam]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyKho_HongLam', FILENAME =N'C:\NQC\HK1(Nam3)(2023-2024)\job\QuanLyKho_HongLam.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB) --N'F:\data_sql\QuanLyKho_HongLam.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyKho_HongLam_log', FILENAME =N'C:\NQC\HK1(Nam3)(2023-2024)\job\QuanLyKho_HongLam_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB) --N'F:\data_sql\QuanLyKho_HongLam_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
GO
CREATE TABLE [dbo].[tblcongty](
	[id] [int] not NULL PRIMARY KEY,
	[ma] [nchar](100) not NULL,
	[ten] [nvarchar](500) not NULL,
	[diachi] [nvarchar](500),
	[msthue] [nvarchar](100)
) 
go
CREATE TABLE [dbo].[tbldangnhap](
	[id] [int] not NULL PRIMARY KEY,
	[ten] [nvarchar](500) not NULL,
	[email] [varchar](100),
	[password] [varchar](100) not NULL,
	[ngayud] [date] DEFAULT GETDATE(),
	[idCongTy] [int] not null,
	[hide] [int] not null,
	FOREIGN KEY (idCongTy) REFERENCES tblcongty(id)
)
GO
CREATE TABLE [dbo].[tblkho](
	[id] [int] not NULL PRIMARY KEY,
	[ma] [nchar](100) not NULL,
	[ten] [nvarchar](500) not NULL,
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id)
)
GO
CREATE TABLE [dbo].[tblnhomhang](
	[id] [int] not NULL PRIMARY KEY,
	[ma] [nchar](100) not NULL,
	[ten] [nvarchar](500) not NULL,
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id)
)
GO
CREATE TABLE [dbo].[tblloaihang](
	[id] [int] not NULL PRIMARY KEY,
	[nhomhang] [int] not NULL,
	[ma] [nchar](100) not NULL,
	[ten] [nvarchar](500) not NULL,
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	FOREIGN KEY (nhomhang) REFERENCES tblnhomhang(id),
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id)
)
GO
CREATE TABLE [dbo].[tbldvt](
	[id] [int] not NULL PRIMARY KEY,
	[ma] [nchar](100) not NULL,
	[ten] [nvarchar](500) not NULL,
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id)
)
GO
CREATE TABLE [dbo].[tblhanghoa](
	[id] [int] not NULL PRIMARY KEY,
	[ma] [nchar](100) not NULL,
	[ten] [nvarchar](500) not NULL,
	[mota] [nvarchar](500),
	[dvt] [int] not null,
	[nhomhang] [int] not NULL,
	[loaihang] [int] not NULL,
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	FOREIGN KEY (dvt) REFERENCES tbldvt(id),
	FOREIGN KEY (nhomhang) REFERENCES tblnhomhang(id),
	FOREIGN KEY (loaihang) REFERENCES tblloaihang(id),
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id)
)
go
CREATE TABLE [dbo].[tblnhacc](
	[id] [int] not NULL PRIMARY KEY,
	[ma] [nchar](100) not NULL,
	[ten] [nvarchar](500) not NULL,
	[loai] [int],
	[diachi] [nvarchar](500),
	[sdt] [nvarchar](10),
	[fax] [nvarchar](10),
	[email] [nvarchar](150),
	[mota] [nvarchar](500),
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id)
	)
go
CREATE TABLE [dbo].[tbltaotem](
	[ma] [int] not NULL,
	[lot] [nvarchar](50) not NULL,
	[nsx] [datetime] not NULL,
	[hsd] [datetime] not NULL,
	[loaihang] [int] not NULL,
	[nhacc] [int] not null,
	[sltem] [int] DEFAULT 0,
	[Image] [image],
	[ImageName] [varchar](150),
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	PRIMARY KEY(ma,lot,nsx,hsd,loaihang,nhacc),
	FOREIGN KEY (ma) REFERENCES tblhanghoa(id),
	FOREIGN KEY (loaihang) REFERENCES tblloaihang(id),
	FOREIGN KEY (nhacc) REFERENCES tblnhacc(id),
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id)
)
go
CREATE TABLE [dbo].[tblkiemkekho](
	[id] [decimal](20) not NULL PRIMARY KEY,
	[mmyyyy] [varchar](50) not NULL,
	[noidung] [nvarchar](500) not NULL,
	[nguoikiemke] [nvarchar](500) not NULL,
	[idkho] [int] not NULL,
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id),
	FOREIGN KEY (idkho) REFERENCES tblkho(id),
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id)
)
go
--Bảng thông tin hàng hóa--
CREATE TABLE [dbo].[tblphieunhapkho](
	[id] [decimal](20) not null, --id lưu phiếu => key của bảng--
	[ma] [int] not NULL, --mã phiếu nhập kho--
	[nguoinhapphieu] [nvarchar](500) not NULL, --Người tạo nên phiếu nhập kho này--
	--Thông tin phiếu--
	[nguoigiaohang] [nvarchar](500) not NULL, --Tên người giao những hàng hóa này
	[diachi] [nvarchar](500) not NULL, --Địa chỉ của người giao
	[masothue] [nvarchar](100) not NULL, --Mã số thuế
	[diengiai] [nvarchar](500) not NULL, --Diễn giải thông tin cụ thể
	[idkho] [int] not NULL, --id kho để biết nhập vào kho nào (người tạo chọn kho)
	[userid] [int] NULL, --ID tài khoản đăng nhập hiện tại--
	[hide] [int] NULL default 0, --Cho phép phiếu này còn sử dụng hay không--
	[idcongty] [int] not NULL, --id công ty của tài khoản đang sử dụng--
	CONSTRAINT PK_tblphieunhapkho PRIMARY KEY (id),
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id),
	FOREIGN KEY (idkho) REFERENCES tblkho(id),
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id)
)
go
--Bảng thông tin hàng hóa chi tiết--
CREATE TABLE [dbo].[tblphieunhapkhoct](
	[id] [decimal](20) not null, --id lưu phiếu => key của bảng--
	[mahanghoa] [int] not NULL, --Mã hàng hóa--
	[stt] [int] not null,
	[solo] [int] not null, --Số lô--
	[hsd] [datetime] not null,--Hạn sử dụng của hàng hóa này--
	[dvt] [nchar](100) not NULL, --ĐƠn vị tính của hàng hóa này--
	[soluong] [decimal] not null,--số lượng hàng hóa
	[dongia] [decimal] not null, -- đơn giá--
	[thanhtien] [decimal] not null, -- thành tiền (số lượng * đơn giá)
	[ngayud] [datetime] DEFAULT GETDATE(), -- Ngày tạo nên phiếu nhập kho này--
	[idcongty] [int] not NULL, --id công ty của tài khoản đang sử dụng--
	FOREIGN KEY (mahanghoa) REFERENCES tblhanghoa(id),
	FOREIGN KEY (id) REFERENCES tblphieunhapkho(id),
	CONSTRAINT PK_tblphieunhapkhoct PRIMARY KEY (id,mahanghoa,stt,solo,hsd),
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id),
	)
go
CREATE TABLE [dbo].[tbltonkhoct](
	[id] [decimal](20) not null, --id chuyển tồn kho => key của bảng--
	[idkho] [int] not NULL,
	FOREIGN KEY (idkho) REFERENCES tblkho(id),
	[idn] [decimal](20),
	[idx] [decimal](20),
	[mahanghoa] [int] not NULL, --Mã hàng hóa--
	FOREIGN KEY (mahanghoa) REFERENCES tblhanghoa(id),
	[solo] [int] not null, --Số lô--
	[hsd] [datetime] not null,--Hạn sử dụng của hàng hóa này--
	[tondau][decimal] DEFAULT 0,--số lượng tồn đầu kỳ hàng hóa
	[sln] [decimal] DEFAULT 0,--số lượng nhập hàng hóa
	[slx] [decimal] DEFAULT 0,--số xuất nhập hàng hóa
	[ngayud] [datetime] DEFAULT GETDATE(),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id),
	CONSTRAINT PK_tbltonkhoct PRIMARY KEY (id,idkho,mahanghoa,solo,hsd),
)
go
CREATE TABLE [dbo].[tblkiemkeCTkho](
	[id] [decimal](20) not NULL,
	[mahang] [int] not NULL,
	[solo] [nvarchar](100) not NULL,
	[HSD] [datetime] not NULL,
	[sl] [decimal] not NULL,
	[nsx] [datetime] not null,
	[loaihang] [int] not NULL,
	[ngayud] [date] not NULL ,
	[sttton] [int] not NULL,
	CONSTRAINT PK_tblkiemkeCTkho PRIMARY KEY (id, mahang, solo, HSD,sl,nsx, loaihang, ngayud),
	FOREIGN KEY (mahang) REFERENCES tblhanghoa(id),
	FOREIGN KEY (loaihang) REFERENCES tblloaihang(id),
	FOREIGN KEY (id) REFERENCES tblkiemkekho(id)
)
GO
CREATE TABLE [dbo].[tbltonkhoth](
	[idkho] [int] not NULL,
	FOREIGN KEY (idkho) REFERENCES tblkho(id),
	[mahanghoa] [int] not NULL, --Mã hàng hóa--
	FOREIGN KEY (mahanghoa) REFERENCES tblhanghoa(id),
	[tondau][decimal] DEFAULT 0,--số lượng tồn đầu kỳ hàng hóa
	[sln] [decimal] DEFAULT 0,--số lượng nhập hàng hóa
	[slx] [decimal] DEFAULT 0,--số xuất nhập hàng hóa
	[ngayud] [datetime] DEFAULT GETDATE(),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id),
	CONSTRAINT PK_tbltonkhoth PRIMARY KEY (idkho,mahanghoa),
)
go
CREATE TABLE [dbo].[tbllydoxuat](
	[id] [int] not NULL PRIMARY KEY,
	[ma] [nchar](100) not NULL,
	[ten] [nvarchar](500) not NULL,
	[ngayud] [date] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	[idcongty] [int] not NULL,
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id)
)
GO
CREATE TABLE [dbo].[tblphieuxuatkho](
	[id] [decimal](20) not null, --id lưu phiếu => key của bảng--
	[ma] [int] not NULL,--mã phiếu nhập kho--
	[nguoixuatphieu] [nvarchar](500) not NULL,--Người tạo nên phiếu xuất kho này--( người này chọn những hàng hóa để xuát đi)
	--Thông tin phiếu--
	[nguoinhanhang] [nvarchar](500) not NULL,--Tên người nhận những hàng hóa này
	[diachi] [nvarchar](500) not NULL,--Địa chỉ của người nhận
	[idlydoxuat] [int] not NULL,--Lý do xuất những hàng hóa nãy
	--Bảng thông tin hàng hóa--
	[idkho] [int] not NULL,
	[ngayud] [datetime] DEFAULT GETDATE(),
	[userid] [int] NULL,
	[hide] [int] NULL default 0,
	[idcongty] [int] not NULL,
	CONSTRAINT PK_tblphieuxuatkho PRIMARY KEY (id),
	FOREIGN KEY (idcongty) REFERENCES tblcongty(id),
	FOREIGN KEY (idkho) REFERENCES tblkho(id),
	FOREIGN KEY (userid) REFERENCES tbldangnhap(id),
	FOREIGN KEY (idlydoxuat) REFERENCES tbllydoxuat(id),
)
go
CREATE TABLE [dbo].[tblphieuxuatkhoct](
	[id] [decimal](20) not null, --id lưu phiếu => key của bảng--
	[mahanghoa] [int] not NULL, --Mã hàng hóa--
	[tenhanghoa] [nvarchar](500) not NULL,
	[stt] [int] not null,
	[solo] [int] not null,
	[hsd] [datetime] not null,
	[dvt] [nchar](100) not NULL,
	[soluong] [int] not null,
	[dongia] [float] not null,
	[thanhtien] [float] not null,
	CONSTRAINT PK_tblphieuxuatkhoct PRIMARY KEY (id,mahanghoa,stt,solo,hsd),
	FOREIGN KEY (mahanghoa) REFERENCES tblhanghoa(id)
)
go
CREATE TABLE [dbo].[bangtamnhapkho]
(
	[mahanghoa] [int] not NULL,
	[solo] [int] not null,
	[nsx] [datetime2],
	[hsd] [datetime2],
	[soluong] [int],
	CONSTRAINT bangtam PRIMARY KEY (mahanghoa,solo)
)
go
select * from tblcongty
select * from tbldangnhap
select * from tblkiemkekho
select * from tblkiemkeCTkho
select * from tblphieunhapkhoct
select * from tblphieunhapkho
select * from tbltonkhoct
select * from tbltonkhoth
select * from tblphieuxuatkho
select * from tblphieuxuatkhoct
select * from tblhanghoa
select * from tbldvt
select * from tblloaihang
select * from tblkho
select * from tblnhacc
select * from tblnhomhang
select*from tbllydoxuat

SELECT * FROM tbltaotem
select * from tbltaotem where ma =1 and lot = 909 and nsx IS NULL and hsd IS NULL
select*from bangtamnhapkho

DELETE FROM bangtamnhapkho

go
------------------------
ALTER TABLE dbo.tbltonkhoct
ADD idnguoikk decimal(20);

ALTER TABLE tbllydoxuat
ALTER COLUMN ma int;

ALTER TABLE [dbo].[tblphieunhapkhoct] DROP CONSTRAINT PK_tblphieunhapkhoct;

ALTER TABLE [dbo].[tblphieunhapkhoct]
DROP COLUMN [solo];

ALTER TABLE [dbo].[tblphieunhapkhoct]
ADD [lot] [nvarchar](50) NULL;

UPDATE [dbo].[tblphieunhapkhoct]
SET [lot] = 0;

ALTER TABLE [dbo].[tblphieunhapkhoct]
ALTER COLUMN [lot] [nvarchar](50) NOT NULL;

-- Tạo lại ràng buộc khóa chính mới không bao gồm cột [hsd]
ALTER TABLE [dbo].[tblphieunhapkhoct]
ADD CONSTRAINT PK_tblphieunhapkhoct PRIMARY KEY (id, mahanghoa, stt, lot);

ALTER TABLE [dbo].[tblphieunhapkhoct]
ADD [nsx] [datetime] NULL;

ALTER TABLE [dbo].[tblphieunhapkhoct]
ALTER COLUMN [hsd] [datetime] NULL;

ALTER TABLE [dbo].[tblphieunhapkhoct]
ALTER COLUMN [dongia] [decimal] NULL;

ALTER TABLE [dbo].[tblphieunhapkhoct]
ALTER COLUMN [thanhtien] [decimal] NULL;

ALTER TABLE dbo.tblphieunhapkhoct
DROP CONSTRAINT [FK__tblphieun__mahan__797309D9];

ALTER TABLE dbo.tblphieunhapkhoct
ALTER COLUMN hsd datetime2 NULL;

ALTER TABLE dbo.tblphieunhapkhoct
ALTER COLUMN nsx datetime2 NULL;

-- Bảng tbltonkhoth --
ALTER TABLE dbo.tbltonkhoth
DROP CONSTRAINT FK__tbltonkho__mahan__0C85DE4D;
-- Bảng tbltonkhoct --
ALTER TABLE dbo.tbltonkhoct
DROP CONSTRAINT FK__tbltonkho__mahan__7F2BE32F;

ALTER TABLE dbo.tbltaotem
DROP CONSTRAINT FK__tbltaotem__ma__656C112C;


ALTER TABLE dbo.tbltonkhoct
DROP CONSTRAINT PK_tbltonkhoct;
ALTER TABLE dbo.tbltonkhoct
ALTER COLUMN hsd datetime2 NULL;
ALTER TABLE dbo.tbltonkhoct
ADD CONSTRAINT PK_tbltonkhoct PRIMARY KEY (id, idkho, mahanghoa, solo);


--18/3/2024--
ALTER TABLE dbo.tbltaotem
DROP CONSTRAINT [PK__tbltaote__05A1D562D92618FC];
ALTER TABLE dbo.tbltaotem
ALTER COLUMN nsx datetime2 NULL;
ALTER TABLE dbo.tbltaotem
ALTER COLUMN hsd datetime2 NULL;

ALTER TABLE dbo.tbltaotem
ALTER COLUMN ImageName VARCHAR(150) NOT NULL;
ALTER TABLE dbo.tbltaotem
ADD CONSTRAINT PK_tbltaotem PRIMARY KEY (ma, lot, loaihang, nhacc, ImageName);

--19-03-2024
-- Xóa khóa chính hiện tại
ALTER TABLE [dbo].[tblphieuxuatkhoct] DROP CONSTRAINT PK_tblphieuxuatkhoct;
-- Thêm một khóa chính mới chỉ bao gồm các cột không bao gồm hsd
ALTER TABLE [dbo].[tblphieuxuatkhoct] ADD CONSTRAINT PK_tblphieuxuatkhoct PRIMARY KEY ([id], [mahanghoa], [stt], [solo]);
-- Thay đổi kiểu dữ liệu của cột hsd thành datetime2 và cho phép null
ALTER TABLE [dbo].[tblphieuxuatkhoct] ALTER COLUMN [hsd] datetime2 NULL;

--24-03-2024
ALTER TABLE dbo.tblphieunhapkho
ADD [ngayud] [datetime] DEFAULT GETDATE();

--27-03-2024
ALTER TABLE dbo.tblphieunhapkho
ALTER COLUMN ngayud datetime NOT NULL
ALTER TABLE dbo.tblphieunhapkhoct
ALTER COLUMN dvt nchar(100) NULL;

--01-04-2024
ALTER TABLE [dbo].[bangtamnhapkho]
ADD [tenhanghoa] NVARCHAR(300);

--5-4-2024
ALTER TABLE [dbo].[bangtamnhapkho]
ADD [maphieu] DECIMAL(20) not null;

ALTER TABLE [dbo].[bangtamnhapkho] DROP CONSTRAINT bangtam;

ALTER TABLE [dbo].[bangtamnhapkho]
ADD CONSTRAINT bangtam PRIMARY KEY (mahanghoa, solo, maphieu);

--9-4-2024
ALTER TABLE dbo.tblphieunhapkhoct
ADD maphieu decimal(20);

--30-03-2024
ALTER TABLE [dbo].[bangtamnhapkho]
ADD [stt] int NULL;
DECLARE @Counter int = 1;
UPDATE [dbo].[bangtamnhapkho]
SET [stt] = @Counter,
    @Counter = @Counter + 1;
ALTER TABLE [dbo].[bangtamnhapkho]
ALTER COLUMN [stt] int NOT NULL;

ALTER TABLE [dbo].[bangtamnhapkho] DROP CONSTRAINT bangtam;

ALTER TABLE [dbo].[bangtamnhapkho]
ADD CONSTRAINT bangtam PRIMARY KEY (mahanghoa, solo, maphieu, stt);