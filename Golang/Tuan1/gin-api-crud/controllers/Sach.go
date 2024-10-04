package controllers

import (
	"errors"
	"net/http"

	"gin-api-crud/database"

	"github.com/gin-gonic/gin"
	"gorm.io/gorm"
)

func CreateBook(c *gin.Context) {
	var book database.Book
	err := c.ShouldBindJSON(&book) // Thay đổi thành ShouldBindJSON
	if err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	res := database.DB.Create(&book) // Truyền địa chỉ của book
	if res.Error != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "error creating a book"})
		return
	}
	c.JSON(http.StatusCreated, gin.H{"book": book}) // Thay đổi mã trạng thái
}

func ReadBook(c *gin.Context) {
	var book database.Book
	id := c.Param("id")
	res := database.DB.First(&book, id) // Sử dụng First thay vì Find để tìm theo ID
	if errors.Is(res.Error, gorm.ErrRecordNotFound) {
		c.JSON(http.StatusNotFound, gin.H{"message": "book not found"})
		return
	} else if res.Error != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "error retrieving the book"})
		return
	}

	c.JSON(http.StatusOK, gin.H{"book": book})
}

func ReadBooks(c *gin.Context) {
	var books []database.Book
	res := database.DB.Find(&books)
	if res.Error != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "error retrieving books"})
		return
	}
	c.JSON(http.StatusOK, gin.H{"books": books})
}

func UpdateBook(c *gin.Context) {
	var book database.Book
	id := c.Param("id")

	err := c.ShouldBindJSON(&book) // Thay đổi thành ShouldBindJSON
	if err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	res := database.DB.Model(&book).Where("id = ?", id).Updates(book)
	if res.Error != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "error updating the book"})
		return
	}
	if res.RowsAffected == 0 {
		c.JSON(http.StatusNotFound, gin.H{"error": "book not found"})
		return
	}
	c.JSON(http.StatusOK, gin.H{"book": book})
}

func DeleteBook(c *gin.Context) {
	var book database.Book
	id := c.Param("id")
	res := database.DB.Delete(&book, id) // Xóa theo ID trực tiếp
	if res.Error != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "error deleting the book"})
		return
	}
	if res.RowsAffected == 0 {
		c.JSON(http.StatusNotFound, gin.H{"message": "book not found"})
		return
	}
	c.JSON(http.StatusOK, gin.H{"message": "book deleted successfully"})
}
