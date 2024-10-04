/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package formnhanvien;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author ADMIN
 */
public class NhanVienDAO {
    public List<NhanVien> readAllNhanVien(){
        List<NhanVien> listSP = new ArrayList<>();
        try{
            String sql = "select * from NhanVien";
            Connection con = ketnoiCSDL.openConnection();
            Statement stm = con.createStatement();
            ResultSet rs = stm.executeQuery(sql);
            listSP.clear();
            while(rs.next()){
                NhanVien nv = new NhanVien();
                nv.setTenNV(rs.getString(1));
                nv.setTuoiNV(rs.getInt(2));
                nv.setChucvuNV(rs.getString(3));
                listSP.add(nv);
            }
            con.close();
        }catch(Exception e){
            e.printStackTrace();
        }
        return listSP;
    }
    
    public String kiemTraDangNhap(String tenDN, String matKhau){
        try{
            String sql = "SELECT chucvu FROM NhanVien WHERE tenDN = '" + tenDN + "' AND matKhau = '" + matKhau + "'";
            Connection con = ketnoiCSDL.openConnection();
            Statement stm = con.createStatement();
            ResultSet rs = stm.executeQuery(sql);
            if(rs.next()){
                String chucvu = rs.getString("chucvu");
                con.close();
                return chucvu; // Trả về chức vụ của người dùng
            }
            con.close();
        }catch(Exception e){
            e.printStackTrace();
        }
        return null; // Trả về null nếu không có bản ghi phù hợp hoặc xảy ra lỗi
    }

    public int layIDNhanVien(String tenDN, String matKhau){
        try{
            String sql = "SELECT idNV FROM NhanVien WHERE tenDN = '" + tenDN + "' AND matKhau = '" + matKhau + "'";
            Connection con = ketnoiCSDL.openConnection();
            Statement stm = con.createStatement();
            ResultSet rs = stm.executeQuery(sql);
            if(rs.next()){
                int id = rs.getInt("idNV");
                con.close();
                return id; // Trả về ID của nhân viên
            }
            con.close();
        }catch(Exception e){
            e.printStackTrace();
        }
        return -1; // Trả về -1 nếu không có bản ghi phù hợp hoặc xảy ra lỗi
    }
}
