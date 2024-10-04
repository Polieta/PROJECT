from django.shortcuts import render, redirect, get_object_or_404
from django.http import HttpResponse,HttpResponseRedirect
from .models import *
from .forms import LoginForm,RegisterForm,SanPhamForm,DanhMucForm
from django.contrib.auth.decorators import login_required
from .forms import ThanhToanForm
from decimal import Decimal 
from django.core.paginator import Paginator
# Create your views here.

def index(request):
    return render(request,'pages/login.html')
    
def Login(request):
    form = LoginForm()
    if request.method == 'POST':
        form = LoginForm(request.POST)
        if form.is_valid():
            username = form.cleaned_data.get('username')
            password = form.cleaned_data.get('password')

            user = TaiKhoan.objects.filter(ten_tai_khoan=username,mat_khau=password).first()
            if user is not None and user.chuc_vu == 1:
                request.session['username'] = user.ten_tai_khoan
                request.session['role'] = user.chuc_vu
                return render(request,'pages/admin.html',{'user':user})
            elif user is not None and user.chuc_vu != 1:
                request.session['username'] = user.ten_tai_khoan
                request.session['role'] = user.chuc_vu
                return render(request,'pages/Home.html',{'user':user})
            else:
                form.add_error(None, "Username hoặc password không đúng.")
    return render(request,'pages/login.html',{'form':form})

def admin(request):
    return render(request,'pages/admin.html')

def Register(request):
    form = RegisterForm()
    if request.method == 'POST':
        form = RegisterForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request, 'Đăng ký thành công! Vui lòng đăng nhập để tiếp tục.')
            return render(request,'pages/login.html')
    return render(request,'pages/register.html',{'form':form})

def them_san_pham(request):
    if request.method == 'POST':
        form = SanPhamForm(request.POST, request.FILES)
        if form.is_valid():
            san_pham = form.save(commit=False)
            if 'hinh_anh' in request.FILES:
                hinh_anh = request.FILES['hinh_anh']
                relative_path = os.path.join('DO AN', 'DoAN/DaAnNNLTHD/static/banner/', hinh_anh.name)  # Đường dẫn tương đối
                san_pham.hinh_anh = relative_path
            san_pham.save()
            return redirect('danh_sach_san_pham')
    else:
        form = SanPhamForm()
    return render(request, 'pages/them_san_pham.html', {'form': form})

def xoa_san_pham(request, ma_san_pham):
    san_pham = get_object_or_404(SanPham, ma_san_pham=ma_san_pham)
    san_pham.delete()
    return redirect('danh_sach_san_pham')

def danh_sach_san_pham(request):
    if request.method == 'GET':
        san_pham_list = SanPham.objects.all()
        return render(request, 'pages/san_pham_list.html', {'san_pham_list': san_pham_list})

def them_danh_muc(request):
    if request.method == 'POST':
        form = DanhMucForm(request.POST)
        if form.is_valid():
            form.save()
            return redirect('admin')
    else:
        form = DanhMucForm()
    return render(request, 'pages/them_danh_muc.html', {'form': form})

def home(request):
    sanphams = SanPham.objects.all()
    danh_muc_list = DanhMuc.objects.all()  # Truy vấn danh sách danh mục
    paginator = Paginator(sanphams, 6)  # Số sản phẩm trên mỗi trang là 6
    page_number = request.GET.get('page')
    page_obj = paginator.get_page(page_number)
    return render(request, 'pages/Home.html', {'page_obj': page_obj, 'danh_muc_list': danh_muc_list})

def tai_khoan_khach_hang(request):
    khach_hang_tai_khoan_list = []
    for khach_hang in KhachHang.objects.all():
        try:
            tai_khoan = TaiKhoan.objects.get(ma_khach_hang=khach_hang.ma_khach_hang)
            khach_hang_tai_khoan_list.append({'khach_hang': khach_hang, 'tai_khoan': tai_khoan})
        except TaiKhoan.DoesNotExist:
            # Trong trường hợp không có tài khoản cho khách hàng này
            khach_hang_tai_khoan_list.append({'khach_hang': khach_hang, 'tai_khoan': None})
    
    context = {
        'khach_hang_tai_khoan_list': khach_hang_tai_khoan_list,
    }
    return render(request, 'pages/tai_khoan_khach_hang.html', context)

def xoa_tai_khoan(request, tai_khoan_id):
    tai_khoan = get_object_or_404(TaiKhoan, ma_khach_hang=tai_khoan_id)
    tai_khoan.delete()
    messages.success(request, 'Xóa tài khoản thành công.')
    return redirect('tai_khoan_khach_hang')

def quan_ly_hoa_don(request):
    don_hang_list = DonHang.objects.all()
    return render(request, 'pages/quan_ly_hoa_don.html', {'don_hang_list': don_hang_list})

def chi_tiet_hoa_don(request, don_hang_id):
    don_hang = get_object_or_404(DonHang, ma_don_hang = don_hang_id)
    chi_tiet_don_hang_list = ChiTietDonHang.objects.filter(ma_don_hang=don_hang.ma_don_hang)
    return render(request, 'pages/chi_tiet_hoa_don.html', {'don_hang': don_hang, 'chi_tiet_don_hang_list': chi_tiet_don_hang_list})

def list_danh_muc(request):
    danh_muc_list = DanhMuc.objects.all()
    return render(request, 'pages/dashboard.html', {'danh_muc_list': danh_muc_list})

def list_san_pham(request):
    danh_muc_id = request.GET.get('danh_muc_id')
    danh_muc = get_object_or_404(DanhMuc, ma_danh_muc=danh_muc_id)
    san_pham_list = SanPham.objects.filter(ma_danh_muc=danh_muc.ma_danh_muc)
    return render(request, 'pages/list_san_pham.html', {'san_pham_list': san_pham_list})

def XoaKhoiGioHang(request, msp):
    cart = request.session.get('cart', {})
    
    if str(msp) in cart:
        del cart[str(msp)]
    
    request.session['cart'] = cart
    
    return redirect('KtraGioHang')

def CapNhatGioHang(request, msp):
    if request.method == 'POST':
        quantity = int(request.POST.get('quantity'))
        cart = request.session.get('cart', {})
        
        if str(msp) in cart and quantity > 0:
            cart[str(msp)]['quantity'] = quantity
        
        request.session['cart'] = cart
        
    return redirect('KtraGioHang')
def ThemGioHang(request, msp):
    cart = request.session.get('cart', {})
    
    if str(msp) not in cart:
        san_pham = get_object_or_404(SanPham, ma_san_pham=msp)
        cart[str(msp)] = {
            'name': san_pham.ten_san_pham,
            'price': str(san_pham.gia),
            'quantity': 1
        }
    else:
        cart[str(msp)]['quantity'] += 1
    
    request.session['cart'] = cart
    
    return redirect('KtraGioHang')
def XemGioHang(request):
    cart = request.session.get('cart', {})
    tong_tien = Decimal(0)
    for item in cart.values():
        tong_tien += Decimal(item['price']) * item['quantity']
    
    if request.method == 'POST':
        form = ThanhToanForm(request.POST)
        if form.is_valid():
            return HttpResponseRedirect('/')
    else:
        form = ThanhToanForm()
    
    context = {'gio': cart, 'form': form, 'tong_tien': tong_tien}
    return render(request, 'pages/KtraGioHang.html', context)
def thanh_toan(request):
    if request.method == 'POST':
        form = ThanhToanForm(request.POST)
        if form.is_valid():
            return render(request, 'pages/thanh_toan_thanh_cong.html')
    else:
        form = ThanhToanForm()
    return render(request, 'pages/thanh_toan.html', {'form': form})
def tim_kiem_san_pham(request):
    query = request.GET.get('q')
    gia_min = request.GET.get('gia_min')
    gia_max = request.GET.get('gia_max')
    sanphams = SanPham.objects.all()

    if query:
        sanphams = sanphams.filter(ten_san_pham__icontains=query)
    if gia_min:
        sanphams = sanphams.filter(gia__gte=gia_min)
    if gia_max:
        sanphams = sanphams.filter(gia__lte=gia_max)
    
    paginator = Paginator(sanphams, 6)
    page_number = request.GET.get('page')
    page_obj = paginator.get_page(page_number)

    return render(request, 'pages/tim_kiem_san_pham.html', {'page_obj': page_obj, 'query': query, 'gia_min': gia_min, 'gia_max': gia_max})
    
def danh_muc_detail(request, ma_danh_muc):
    sanphams = SanPham.objects.filter(ma_danh_muc=ma_danh_muc)
    paginator = Paginator(sanphams, 6)
    page_number = request.GET.get('page')
    page_obj = paginator.get_page(page_number)
    page_obj = {
        'page_obj': page_obj
    }
    return render(request, 'pages/home.html', page_obj)
def ds_Danh_Muc(request):
    danh_muc_list = DanhMuc.objects.all()
    return render(request, 'pages/home.html', {'danh_muc_list': danh_muc_list})