package main

import (
	"fmt"
	"os"

	"gin-api-crud/controllers"
	"gin-api-crud/database"

	"github.com/gin-gonic/gin"
)

func main() {
	fmt.Println("Starting application ...")
	if err := database.DatabaseConnection(); err != nil {
		fmt.Printf("Database connection failed: %v\n", err)
		return
	}

	r := gin.Default()

	// Định tuyến cho API liên quan đến sách (books)
	bookRoutes := r.Group("/books")
	{
		bookRoutes.GET("/:id", controllers.ReadBook)
		bookRoutes.GET("", controllers.ReadBooks)
		bookRoutes.POST("", controllers.CreateBook)
		bookRoutes.PUT("/:id", controllers.UpdateBook)
		bookRoutes.DELETE("/:id", controllers.DeleteBook)
	}

	// Định tuyến cho API liên quan đến khách hàng (khachhang)
	khachHangRoutes := r.Group("/khachhang")
	{
		khachHangRoutes.GET("", controllers.ReadKhachHangs)
		khachHangRoutes.POST("", controllers.CreateKhachHang)
		khachHangRoutes.GET("/:id", controllers.ReadKhachHang)
		khachHangRoutes.DELETE("/:id", controllers.DeleteKhachHang)
	}

	// Định tuyến cho API liên quan đến hóa đơn (hoadon)
	hoaDonRoutes := r.Group("/hoadon")
	{
		hoaDonRoutes.GET("", controllers.ReadHoaDons)
		hoaDonRoutes.POST("", controllers.CreateHoaDon)
		hoaDonRoutes.GET("/:id", controllers.ReadHoaDon)
		hoaDonRoutes.DELETE("/:id", controllers.DeleteHoaDon)
	}

	port := os.Getenv("PORT")
	if port == "" {
		port = "5000"
	}
	r.Run(":" + port)
}
