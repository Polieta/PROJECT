package controllers

import (
	"gin-api-crud/database"
	"net/http"

	"github.com/gin-gonic/gin"
)

// Tạo hóa đơn mới
func CreateHoaDon(c *gin.Context) {
	var hoaDon database.HoaDon
	if err := c.ShouldBindJSON(&hoaDon); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	if err := database.DB.Create(&hoaDon).Error; err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "error creating hoa don"})
		return
	}
	c.JSON(http.StatusCreated, gin.H{"hoa_don": hoaDon})
}

// Lấy danh sách hóa đơn
func ReadHoaDons(c *gin.Context) {
	var hoaDons []database.HoaDon
	if err := database.DB.Find(&hoaDons).Error; err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "error retrieving hoa dons"})
		return
	}
	c.JSON(http.StatusOK, gin.H{"hoa_dons": hoaDons})
}

// Lấy hóa đơn theo ID
func ReadHoaDon(c *gin.Context) {
	var hoaDon database.HoaDon
	id := c.Param("id")
	if err := database.DB.First(&hoaDon, id).Error; err != nil {
		c.JSON(http.StatusNotFound, gin.H{"message": "hoa don not found"})
		return
	}
	c.JSON(http.StatusOK, gin.H{"hoa_don": hoaDon})
}

// Xóa hóa đơn
func DeleteHoaDon(c *gin.Context) {
	var hoaDon database.HoaDon
	id := c.Param("id")
	if err := database.DB.Delete(&hoaDon, id).Error; err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "error deleting hoa don"})
		return
	}
	if database.DB.RowsAffected == 0 {
		c.JSON(http.StatusNotFound, gin.H{"message": "hoa don not found"})
		return
	}
	c.JSON(http.StatusOK, gin.H{"message": "hoa don deleted successfully"})
}
