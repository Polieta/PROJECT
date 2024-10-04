from django import forms
from .models import KhachHang, TaiKhoan
from .models import TaiKhoan
from .models import SanPham
from .models import DanhMuc

class LoginForm(forms.Form):
    username = forms.CharField(label='Tài khoản', max_length=100,widget=forms.TextInput(attrs={'style': 'width: 300px;'}))
    password = forms.CharField(label='Mật khẩu', widget=forms.PasswordInput(attrs={'style': 'width: 300px;'}))

class RegisterForm(forms.ModelForm):
    ten_tai_khoan = forms.CharField(label='Tên tài khoản', max_length=255)
    password = forms.CharField(label='Mật khẩu', widget=forms.PasswordInput)
    confirm_password = forms.CharField(label='Xác nhận mật khẩu', widget=forms.PasswordInput)

    class Meta:
        model = KhachHang
        fields = ['ho_ten', 'email', 'so_dien_thoai', 'dia_chi', 'gioi_tinh']

    def clean_confirm_password(self):
        password = self.cleaned_data.get('password')
        confirm_password = self.cleaned_data.get('confirm_password')
        if password != confirm_password:
            raise forms.ValidationError("Mật khẩu không khớp.")
        return confirm_password
    def save(self, commit=True):
        khach_hang = super(RegisterForm, self).save(commit=False)
        ten_tai_khoan = self.cleaned_data['ten_tai_khoan']
        password = self.cleaned_data['password']
        if commit:
            khach_hang.save()
            tai_khoan = TaiKhoan(ten_tai_khoan=ten_tai_khoan,mat_khau=password,chuc_vu = 2)
            tai_khoan.save()
        return khach_hang


class SanPhamForm(forms.ModelForm):
    ma_danh_muc = forms.ModelChoiceField(
        queryset=DanhMuc.objects.all(),
        label="Danh mục",
        widget=forms.Select(attrs={'style': 'width: 300px;', 'class': 'form-control'})
    )
    hinh_anh = forms.FileField(label='Hình ảnh', required=False)

    class Meta:
        model = SanPham
        fields = ['ten_san_pham', 'mo_ta', 'gia', 'so_luong', 'ma_danh_muc', 'hinh_anh']
        labels = {
            'ten_san_pham': 'Tên sản phẩm',
            'mo_ta': 'Mô tả',
            'gia': 'Giá',
            'so_luong': 'Số lượng',
            'ma_danh_muc': 'Danh mục',
        }
        widgets = {
            'ten_san_pham': forms.TextInput(attrs={'style': 'width: 300px;', 'class': 'form-control'}),
            'mo_ta': forms.Textarea(attrs={'style': 'width: 300px; height: 75px;', 'class': 'form-control'}),
            'gia': forms.NumberInput(attrs={'style': 'width: 300px;', 'class': 'form-control'}),
            'so_luong': forms.NumberInput(attrs={'style': 'width: 300px;', 'class': 'form-control'}),
        }


class DanhMucForm(forms.ModelForm):
    class Meta:
        model = DanhMuc
        fields = ['ten_danh_muc', 'mo_ta']
        labels = {
            'ten_danh_muc': 'Tên danh mục',
            'mo_ta': 'Mô tả',
        }
        widgets = {
            'ten_danh_muc': forms.TextInput(attrs={'style': 'width: 300px;', 'class': 'form-control'}),
            'mo_ta': forms.Textarea(attrs={'style': 'width: 300px; height: 75px;', 'class': 'form-control'}),
        }

    def save(self, commit=True):
        instance = super(DanhMucForm, self).save(commit=False)
        if commit:
            instance.save()
        return instance

class ThanhToanForm(forms.Form):
    ho_ten = forms.CharField(
        label='Họ tên', 
        max_length=100, 
        widget=forms.TextInput(attrs={
            'style': 'width: 300px; margin: 0 auto;',  # Canh giữa theo chiều ngang
            'class': 'form-control'
        })
    )
    dia_chi = forms.CharField(
        label='Địa chỉ', 
        max_length=255, 
        widget=forms.TextInput(attrs={
            'style': 'width: 300px; margin: 0 auto;',  # Canh giữa theo chiều ngang
            'class': 'form-control'
        })
    )
    so_dien_thoai = forms.CharField(
        label='Số điện thoại', 
        max_length=15, 
        widget=forms.TextInput(attrs={
            'style': 'width: 300px; margin: 0 auto;',  # Canh giữa theo chiều ngang
            'class': 'form-control'
        })
    )
    email = forms.EmailField(
        label='Email', 
        widget=forms.EmailInput(attrs={
            'style': 'width: 300px; margin: 0 auto;',  # Canh giữa theo chiều ngang
            'class': 'form-control'
        })
    )