package controllers

import (
	"gin-api-crud/database"
	"net/http"

	"github.com/gin-gonic/gin"
)

// Tạo Khách Hàng mới
func CreateKhachHang(c *gin.Context) {
	var khachHang database.KhachHang
	if err := c.ShouldBindJSON(&khachHang); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	database.DB.Create(&khachHang)
	c.JSON(http.StatusCreated, khachHang)
}

// Lấy danh sách Khách Hàng
func ReadKhachHangs(c *gin.Context) {
	var khachHangs []database.KhachHang
	database.DB.Find(&khachHangs)
	c.JSON(http.StatusOK, khachHangs)
}

// Lấy thông tin Khách Hàng theo ID
func ReadKhachHang(c *gin.Context) {
	var khachHang database.KhachHang
	id := c.Param("id")
	if err := database.DB.First(&khachHang, id).Error; err != nil {
		c.JSON(http.StatusNotFound, gin.H{"message": "khách hàng không tìm thấy"})
		return
	}
	c.JSON(http.StatusOK, khachHang)
}

// Xóa Khách Hàng theo ID
func DeleteKhachHang(c *gin.Context) {
	var khachHang database.KhachHang
	id := c.Param("id")

	// Kiểm tra xem khách hàng có tồn tại không
	if err := database.DB.First(&khachHang, id).Error; err != nil {
		c.JSON(http.StatusNotFound, gin.H{"message": "khách hàng không tìm thấy"})
		return
	}

	// Xóa khách hàng
	database.DB.Delete(&khachHang)
	c.JSON(http.StatusOK, gin.H{"message": "khách hàng đã được xóa"})
}
