package database

import (
	"fmt"

	"gorm.io/driver/postgres"
	"gorm.io/gorm"
)

var DB *gorm.DB
var err error

type Book struct {
	gorm.Model
	Title  string `json:"title"`
	Author string `json:"author"`
}

// Struct đại diện cho Khách Hàng
type KhachHang struct {
	gorm.Model
	Ten     string   `json:"ten"`                    // Tên khách hàng
	Email   string   `json:"email"`                  // Email khách hàng
	Sdt     string   `json:"sdt"`                    // Số điện thoại khách hàng
	HoaDons []HoaDon `gorm:"foreignKey:KhachHangID"` // Mối quan hệ 1-N với Hóa Đơn
}

// Struct đại diện cho Hóa Đơn
type HoaDon struct {
	gorm.Model
	KhachHangID uint          `json:"khach_hang_id"`       // Khóa ngoại liên kết đến Khách Hàng
	ChiTiet     HoaDonChiTiet `gorm:"foreignKey:HoaDonID"` // Quan hệ 1-1 với Hóa Đơn Chi Tiết
	TongTien    float64       `json:"tong_tien"`           // Tổng tiền hóa đơn
}

type HoaDonChiTiet struct {
	gorm.Model
	HoaDonID uint          `json:"hoadon_id"`                  // Khóa ngoại liên kết với hóa đơn
	Saches   []SachChiTiet `gorm:"foreignKey:HoaDonChiTietID"` // Mối quan hệ 1-N với Sách
}

// Struct đại diện cho Sách trong Chi Tiết Hóa Đơn
type SachChiTiet struct {
	gorm.Model
	HoaDonChiTietID uint    `json:"hoadon_chitiet_id"` // Khóa ngoại liên kết với Chi Tiết Hóa Đơn
	SachID          uint    `json:"sach_id"`           // Khóa ngoại liên kết với sách
	SoLuong         int     `json:"so_luong"`          // Số lượng sách
	Gia             float64 `json:"gia"`               // Giá của sách
}

func DatabaseConnection() error {
	host := "localhost"
	port := "5432"
	dbName := "TEST"
	dbUser := "postgres"
	password := "130303"
	dsn := fmt.Sprintf("host=%s port=%s user=%s dbname=%s password=%s sslmode=disable",
		host,
		port,
		dbUser,
		dbName,
		password,
	)

	var err error
	DB, err = gorm.Open(postgres.Open(dsn), &gorm.Config{})
	if err != nil {
		return fmt.Errorf("error connecting to the database: %w", err)
	}

	// Tự động di chuyển bảng (migration)
	if err = DB.AutoMigrate(&Book{}, &KhachHang{}, &HoaDon{}, &HoaDonChiTiet{}, &SachChiTiet{}); err != nil {
		return fmt.Errorf("error during auto-migration: %w", err)
	}

	fmt.Println("Database connection successful...")
	return nil // Trả về nil nếu không có lỗi
}
