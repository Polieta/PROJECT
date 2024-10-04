from django.db import models

class DanhMuc(models.Model):
    ma_danh_muc = models.AutoField(primary_key=True)
    ten_danh_muc = models.CharField(max_length=255)
    mo_ta = models.TextField(blank=True, null=True)

    class Meta:
        db_table = 'danhmuc'
    def __str__(self):
        return self.ten_danh_muc 
        
class SanPham(models.Model):
    ma_san_pham = models.AutoField(primary_key=True)
    ten_san_pham = models.CharField(max_length=255)
    mo_ta = models.TextField(blank=True, null=True)
    gia = models.DecimalField(max_digits=10, decimal_places=2)
    so_luong = models.IntegerField()
    ma_danh_muc = models.ForeignKey(DanhMuc, on_delete=models.CASCADE)
    hinh_anh = models.ImageField(upload_to='banner/', blank=True, null=True)

    class Meta:
        db_table = 'sanpham'

class KhachHang(models.Model):
    ma_khach_hang = models.AutoField(primary_key=True)
    ho_ten = models.CharField(max_length=255)
    email = models.EmailField(max_length=255, unique=True)
    so_dien_thoai = models.CharField(max_length=20, unique=True, blank=True, null=True)
    dia_chi = models.CharField(max_length=255, blank=True, null=True)
    gioi_tinh = models.CharField(max_length=100, blank=True, null=True)

    class Meta:
        db_table = 'khachhang'

class DonHang(models.Model):
    ma_don_hang = models.AutoField(primary_key=True)
    ma_khach_hang = models.ForeignKey(KhachHang, on_delete=models.CASCADE)
    ngay_dat_hang = models.DateTimeField()
    tong_tien = models.DecimalField(max_digits=10, decimal_places=2)

    class Meta:
        db_table = 'donhang'

class ChiTietDonHang(models.Model):
    ma_chi_tiet_don_hang = models.AutoField(primary_key=True)
    ma_don_hang = models.ForeignKey(DonHang, on_delete=models.CASCADE)
    ma_san_pham = models.ForeignKey(SanPham, on_delete=models.CASCADE)
    so_luong = models.IntegerField()
    gia_don_vi = models.DecimalField(max_digits=10, decimal_places=2)

    class Meta:
        db_table = 'chitietdonhang'

class TaiKhoan(models.Model):
    ma_khach_hang = models.AutoField(primary_key=True)
    ten_tai_khoan = models.CharField(max_length=255, unique=True)
    mat_khau = models.CharField(max_length=255)
    chuc_vu = models.IntegerField()

    class Meta:
        db_table = 'taikhoan'
