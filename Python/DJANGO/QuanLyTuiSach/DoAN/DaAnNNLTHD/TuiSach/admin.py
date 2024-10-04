from django.contrib import admin

# Register your models here.
from .models import *

admin.site.register(DanhMuc)
admin.site.register(SanPham)
admin.site.register(KhachHang)
admin.site.register(DonHang)
admin.site.register(ChiTietDonHang)
admin.site.register(TaiKhoan)