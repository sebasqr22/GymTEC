using System;
using System.Text;
using System.Data.SqlClient;

namespace DataBaseManagement{
  public class DatabaseHandler {
      public string pc_eduardo = "DESKTOP-E7FF35K";
      private string pc_jimena = "MENA-LAPTOPHP";
      string conexionText = "";
      public SqlConnection conectarDB = new SqlConnection();

      public void ConectarServer(string pc_name){
          conexionText = "Data Source=" + pc_name + "\\SQLEXPRESS;Initial Catalog=GymTEC-DB; Integrated Security=True;";
          conectarDB.ConnectionString = conexionText;
      }

      public void AbrirConexion() {
        try{
          conectarDB.Open();
        }catch(Exception e){
          Console.WriteLine(e);
        }
      }

      public void CerrarConexion() {
          conectarDB.Close();
      }

      public void ExecuteNonQueryH(string sql) {
          using (SqlCommand command = new SqlCommand(sql, conectarDB)) {
              command.ExecuteNonQuery();
          }
      }

      public SqlDataReader ExecuteReaderH(string sql) {
          SqlCommand command = new SqlCommand(sql, conectarDB);
          return command.ExecuteReader();
      }
  }
}
