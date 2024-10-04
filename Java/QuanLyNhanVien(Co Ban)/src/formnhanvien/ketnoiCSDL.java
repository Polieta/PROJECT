/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package formnhanvien;

import java.sql.Connection;
import java.sql.DriverManager;

/**
 *
 * @author ADMIN
 */
public class ketnoiCSDL {
     public static Connection openConnection() throws Exception{
        Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
        System.out.println("Loaded...");
        
        String url = "jdbc:sqlserver://localhost:1433;databaseName=DuAnJava";
//        String user = "sa";
//        String password = "123";
        
        Connection con = DriverManager.getConnection(url);
        System.out.println("Connected...");
        return con;
        
    }
}
