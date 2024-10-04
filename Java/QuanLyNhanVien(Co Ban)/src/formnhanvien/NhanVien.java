/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package formnhanvien;

/**
 *
 * @author ADMIN
 */
public class NhanVien {

    public String getTenNV() {
        return tenNV;
    }

    public void setTenNV(String tenNV) {
        this.tenNV = tenNV;
    }

    public int getTuoiNV() {
        return tuoiNV;
    }

    public void setTuoiNV(int tuoiNV) {
        this.tuoiNV = tuoiNV;
    }

    public String getChucvuNV() {
        return chucvuNV;
    }

    public void setChucvuNV(String chucvuNV) {
        this.chucvuNV = chucvuNV;
    }
    public String tenNV;
    public int tuoiNV;
    public String chucvuNV;
    
    public NhanVien(){}
    public NhanVien(String tenNV, int tuoiNV, String chucvuNV) {
        this.tenNV = tenNV;
        this.tuoiNV = tuoiNV;
        this.chucvuNV = chucvuNV;
    }
}
