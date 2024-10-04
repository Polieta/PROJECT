from django.urls import path
from django.contrib import admin
from . import views
from django.conf.urls.static import static
from django.conf import settings

urlpatterns = [
    path('', views.Login, name='index'),
    path('register/', views.Register, name='register'),
    path('login', views.Login, name='login'),
    path('admin', views.admin, name='admin'),  # Đảm bảo rằng đường dẫn này đúng
    path('TuiSach/them_san_pham', views.them_san_pham, name='them_san_pham'),
    path('TuiSach/xoa_san_pham/<int:ma_san_pham>/', views.xoa_san_pham, name='xoa_san_pham'),
    path('TuiSach/danh_sach_san_pham', views.danh_sach_san_pham, name='danh_sach_san_pham'),
    path('TuiSach/them_danh_muc', views.them_danh_muc, name='them_danh_muc'),
     path('TuiSach/tai_khoan_khach_hang', views.tai_khoan_khach_hang, name='tai_khoan_khach_hang'),
    path('TuiSach/xoa-tai-khoan/<int:tai_khoan_id>/', views.xoa_tai_khoan, name='xoa_tai_khoan'),
    path('TuiSach/quan_ly_hoa_don', views.quan_ly_hoa_don, name='quan_ly_hoa_don'),
    path('TuiSach/chi_tiet_hoa_don/<int:don_hang_id>', views.chi_tiet_hoa_don, name='chi_tiet_hoa_don'),
    path('TuiSach/list_danh_muc', views.list_danh_muc, name='list_danh_muc'),
    path('TuiSach/list_san_pham', views.list_san_pham, name='list_san_pham'),
    path('home', views.home, name='home'),
    path('XemGioHang', views.XemGioHang, name='KtraGioHang'),
    path('ThemGioHang/<int:msp>/', views.ThemGioHang, name='them_vao_gio_hang'),
    path('CapNhatGioHang/<int:msp>/', views.CapNhatGioHang, name='cap_nhat_gio_hang'),
    path('XoaKhoiGioHang/<int:msp>/', views.XoaKhoiGioHang, name='xoa_khoi_gio_hang'),
    path('thanh_toan/', views.thanh_toan, name='thanh_toan'),
    path('tim_kiem/', views.tim_kiem_san_pham, name='tim_kiem_san_pham'),
    path('danh_muc_detail/<int:ma_danh_muc>', views.danh_muc_detail, name='danh_muc_detail'),
]
urlpatterns += static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)