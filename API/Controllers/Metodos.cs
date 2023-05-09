using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Data.SqlClient;
using System.Globalization;

using DataBaseManagement;
using funcionesAuxiliares;

namespace Metodos{

    [ApiController]
    [Route("usuarios")]
    public class MetodosAPI: ControllerBase{
      private DatabaseHandler DB_Handler = new DatabaseHandler();  
      AuxiliarFunctions aux = new AuxiliarFunctions();

      [HttpGet]
      [Route("admin/VerClientes")]
      public dynamic VerClientes() {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = "SELECT * FROM CLIENTE";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                 var clientes = new List<dynamic>();
                 while (reader.Read()) {
                  clientes.Add(new {
                    Cedula = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellido1 = reader.GetString(2),
                    Apellido2 = reader.GetString(3),
                    Dia_nacimiento = reader.GetString(4),
                    Mes_nacimiento = reader.GetString(5),
                    Ano_nacimiento = reader.GetString(6),
                    Peso = reader.GetDouble(7),
                    Direccion = reader.GetString(8),
                    Correo = reader.GetString(9),
                    Contrasena = reader.GetString(10)
                  });
                 }
                 DB_Handler.CerrarConexion();
                 return new JsonResult(clientes);
              } else {
                return new { message = "no hay clientes" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }
      //Función utilizada para ver los datos correspondientes a un cliente en especifico
      [HttpPost]
      [Route("admin/VerClienteEspecifico")]
      public dynamic VerClienteEspecifico(string cedula){
        try{
          // Verificar que el cliente exista
          dynamic existeCliente = aux.VerificarExistenciaCliente_aux(cedula);
          if (!existeCliente) {
            return new { message = "No existe este cliente" };
          }

          return aux.VerClienteEspecifico_aux(cedula);

        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para eliminar una sucursal de la db
      [HttpPost]
      [Route("admin/EliminarSucursal")]
      public dynamic EliminarSucursal(string codigo_suc){
        try{
          // Verificar que la sucursal exista
          dynamic existeSucursal = aux.VerificarExistenciaSucursal_aux(codigo_suc);
          if (!existeSucursal) {
            return new { message = "No existe esta sucursal" };
          }

          // Eliminar sucursal
          string queryDelete = "DELETE FROM SUCURSAL WHERE Codigo_sucursal = @codigo";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();

          using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@codigo", Int64.Parse(codigo_suc));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();


          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para los datos correspondientes a una sucursal
      [HttpGet]
      [Route("admin/VerSucursal")]
      public dynamic VerSucursales(){
        try{
          return aux.VerSucursales_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar una sucursal a una db
      [HttpPost]
      [Route("admin/AgregarSucursal")]
      public dynamic AgregarSucursal(string Codigo_sucursal, string Nombre, string Distrito, string Canton, string Provincia, string Fecha_apertura, string Hora_apertura, string Hora_cierre, string Max_capacidad, string Cedula_administrador){
        try{

          string queryInsert = "INSERT INTO SUCURSAL VALUES (@Codigo_sucursal, @Nombre, @Distrito, @Canton, @Provincia, @Fecha_apertura, @Hora_apertura, @Hora_cierre, @Max_capacidad, @Cedula_administrador)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();

          // Verificar que el administrador exista y la sucursal no
          dynamic existeEmpleado = aux.VerificarExistenciaEmpleado_aux(Cedula_administrador);
          if (!existeEmpleado) {
            return new { message = "No existe este empleado" };
          }
          dynamic existeSucursal = aux.VerificarExistenciaSucursal_aux(Codigo_sucursal);
          if (existeSucursal) {
            return new { message = "Ya existe esta sucursal" };
          }

          // Insertar sucursal
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Int64.Parse(Codigo_sucursal));
            comando.Parameters.AddWithValue("@Nombre", Nombre);
            comando.Parameters.AddWithValue("@Distrito", Distrito);
            comando.Parameters.AddWithValue("@Canton", Canton);
            comando.Parameters.AddWithValue("@Provincia", Provincia);
            comando.Parameters.AddWithValue("@Fecha_apertura", Fecha_apertura);
            comando.Parameters.AddWithValue("@Hora_apertura", Hora_apertura);
            comando.Parameters.AddWithValue("@Hora_cierre", Hora_cierre);
            comando.Parameters.AddWithValue("@Max_capacidad", Int64.Parse(Max_capacidad));
            comando.Parameters.AddWithValue("@Cedula_administrador", Int64.Parse(Cedula_administrador));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };

        }catch( Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para asociar servicios a una sucursal ya existente
      [HttpPost]
      [Route("admin/AsociarServiciosASucursal")]
      public dynamic AsociarServiciosASucursal(string Codigo_sucursal, string idServicio){
        try{
          string queryInsert = "INSERT INTO SERVICIOS_EN_SUCURSAL VALUES (@codigoSucursal, @idServicio)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();

          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@codigoSucursal", Int64.Parse(Codigo_sucursal));
            comando.Parameters.AddWithValue("@idServicio", Int64.Parse(idServicio));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
          
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerServiciosAsociados")]
      public dynamic VerServiciosAsociados(string Codigo_Sucursal) {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          var total = new List<dynamic>();
          string query1 = @"SELECT Identificador, Descripcion
                          FROM SERVICIO LEFT JOIN SERVICIOS_EN_SUCURSAL ON Identificador = Id_servicio
                          WHERE Codigo_sucursal = @Code";
          using (SqlCommand comando = new SqlCommand(query1, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Code", Int64.Parse(Codigo_Sucursal));
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var serviciosAsociados = new List<dynamic>();
                while (reader.Read()) {
                  serviciosAsociados.Add(new {
                    Identificador = reader.GetInt32(0),
                    Descripcion = reader.GetString(1)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(serviciosAsociados);
              }
              else {
                return new { message = "no hay servicios asociados"};
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerServiciosNoAsociados")]
      public dynamic VerServiciosNoAsociados(string codigo_sucursal) {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query2 = @"SELECT Identificador, Descripcion
                          FROM SERVICIO LEFT JOIN SERVICIOS_EN_SUCURSAL ON Identificador = Id_servicio
                          WHERE Codigo_sucursal IS NULL OR Codigo_sucursal <> @Code";
          using (SqlCommand comando = new SqlCommand(query2, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Code",Int64.Parse(codigo_sucursal));
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var serviciosNoAsociados = new List<dynamic>();
                while (reader.Read()) {
                  serviciosNoAsociados.Add(new {
                    Identificador = reader.GetInt32(0),
                    Descripcion = reader.GetString(1)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(serviciosNoAsociados);
              }
              else {
                return new { message = "vacio" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para asociar inventario a una sucursal ya existente
      [HttpPost]
      [Route("admin/AsociarInventario")]
      public dynamic AsociarInventario(string Codigo_sucursal, string num_serie, string costo) {
        try { 

          // Insertar en la tabla que relaciona o asocia inventario con sucursal
          string queryInventario = @"INSERT INTO INVENTARIO_EN_SUCURSAL
                                    VALUES (@Codigo_sucursal, @NumSerie, @Costo)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          using (SqlCommand comando = new SqlCommand(queryInventario, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Int64.Parse(Codigo_sucursal));
            comando.Parameters.AddWithValue("@NumSerie", Int64.Parse(num_serie));
            comando.Parameters.AddWithValue("@Costo", Math.Round(Convert.ToDouble(costo, CultureInfo.InvariantCulture), 2));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();

          return new { message = "ok"};
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para ver el inventario existente
      [HttpGet]
      [Route("admin/VerInventario")]
      public dynamic VerInventario(string codigo_suc){
        try{
          return aux.VerInventario_aux(codigo_suc);
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerInventarioNoAsociado")]
      public dynamic VerInventarioNoAsociado(string codigo_sucursal) {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query2 = @"SELECT Numero_serie, Marca, Tipo
                          FROM INVENTARIO LEFT JOIN INVENTARIO_EN_SUCURSAL ON Numero_serie = Num_serie_maquina
                          WHERE Codigo_sucursal IS NULL OR Codigo_sucursal <> @Code";
          using (SqlCommand comando = new SqlCommand(query2, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Code", Int64.Parse(codigo_sucursal));
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var inventarioNoAsociados = new List<dynamic>();
                while (reader.Read()) {
                  inventarioNoAsociados.Add(new {
                    Numero_serie = reader.GetInt32(0),
                    Marca = reader.GetString(1),
                    Tipo = reader.GetInt32(2)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(inventarioNoAsociados);
              }
              else {
                return new { message = "vacio" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }
      
      //Función utilizada para asociar productos a una tienda ya existente
      [HttpPost]
      [Route("admin/AsociarProductosATienda")]
      public dynamic AsociarProductosATienda(string Codigo_sucursal, string Codigo_producto){
        try{
          string queryInsert = "INSERT INTO VENTA_PRODUCTO VALUES (@codigoSucursal, @codigoProducto)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@codigoSucursal", Int64.Parse(Codigo_sucursal));
            comando.Parameters.AddWithValue("@codigoProducto", Int64.Parse(Codigo_producto));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
          
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para ver los productos exitentes en la db
      [HttpGet]
      [Route("admin/VerProductos")]
      public dynamic VerProductos(string codigo_gym){
        try{
          return aux.VerProductos_aux(codigo_gym);
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerProductosNoAsociados")]
      public dynamic VerProductosNoAsociados(string codigo_sucursal) {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Codigo_barras, Nombre, Descripcion, Costo
                          FROM PRODUCTO LEFT JOIN VENTA_PRODUCTO ON Codigo_barras = Codigo_producto
                          WHERE Codigo_sucursal IS NULL OR Codigo_sucursal <> @Code";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Code", Int64.Parse(codigo_sucursal));
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var productosNoAsociados = new List<dynamic>();
                while (reader.Read()) {
                  productosNoAsociados.Add(new {
                    Codigo_barras = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.GetString(2),
                    Costo = reader.GetDouble(3)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(productosNoAsociados);
              }
              else {
                return new { message = "Vacio" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Funcion utilizada para eliminar o desasociar un producto de una tienda
      [HttpPost]
      [Route("admin/EliminarProductoDeTienda")]
      public dynamic EliminarProductoDeTienda(string codigo_sucursal, string Codigo_producto) {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryDelete = "DELETE FROM VENTA_PRODUCTO WHERE Codigo_sucursal = @Suc AND Codigo_producto = @Pro";
          using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Suc", Int64.Parse(codigo_sucursal));
            comando.Parameters.AddWithValue("@Pro", Int64.Parse(Codigo_producto));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para asociar tratamientos a un spa ya existente
      [HttpPost]
      [Route("admin/AsociarTratamientoASPA")]
      public dynamic AsociarTratamientosASPA(string Codigo_sucursal, string Id_tratamiento){
        try{
          // ACTUALIZAR TRATAMIENTO_SPA
          string query = "INSERT INTO TRATAMIENTO_SPA VALUES (@Codigo_sucursal, @idTratamiento)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Int64.Parse(Codigo_sucursal));
            comando.Parameters.AddWithValue("@idTratamiento", Int64.Parse(Id_tratamiento));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };

        }catch  (Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerTratamientosAsociados")]
      public dynamic VerTratamientosAsociados(string codigo_sucursal) {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Id_tratamiento, Descripcion 
                          FROM TRATAMIENTO_SPA JOIN TRATAMIENTO ON Id_tratamiento = Identificador 
                          WHERE Codigo_sucursal = @Code";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Code", Int64.Parse(codigo_sucursal));
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var tratamientosAsociados = new List<dynamic>();
                while (reader.Read()) {
                  tratamientosAsociados.Add(new {
                    Identificador = reader.GetInt32(0),
                    Descripcion = reader.GetString(1)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(tratamientosAsociados);
              } else {
                return new { message = "vacio" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerTratamientosNoAsociados")]
      public dynamic VerTratamientosNoAsociados(string codigo_sucursal) {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Identificador, Descripcion
                          FROM TRATAMIENTO LEFT JOIN TRATAMIENTO_SPA ON Identificador = Id_tratamiento
                          WHERE Codigo_sucursal IS NULL OR Codigo_sucursal <> @Code";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Code", Int64.Parse(codigo_sucursal));
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var tratamientosNoAsociados = new List<dynamic>();
                while (reader.Read()) {
                  tratamientosNoAsociados.Add(new {
                    Identificador = reader.GetInt32(0),
                    Descripcion = reader.GetString(1)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(tratamientosNoAsociados);
              } else {
                return new { message = "vacio" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

       //Funcion utilizada para eliminar la relacion de una sucursal con un tratamiento
      [HttpPost]
      [Route("admin/EliminarTratamientoDeSPA")]
      public dynamic EliminarTratamientoDeSPA(string codigo_sucursal, int idTratamiento){
        try{
            // VERIFICACION DE DATOS
            if (string.IsNullOrEmpty(codigo_sucursal)) {
                return new { message = "error" };}

            // ELIMINAR TRATAMIENTO_SPA EN LA BASE DE DATOS
            dynamic existeTratamientoSPA = aux.VerificarExistenciaTratamientoSPA_aux(codigo_sucursal, idTratamiento);
            if (!existeTratamientoSPA) {
                return new { message = "Tratamiento no existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryDelete = "DELETE FROM TRATAMIENTO_SPA WHERE Codigo_sucursal = @Codigo AND Id_tratamiento = @Trata";
            using (SqlCommand comando2 = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
                comando2.Parameters.AddWithValue("@Codigo", Int64.Parse(codigo_sucursal));
                comando2.Parameters.AddWithValue("@Trata", idTratamiento);
                comando2.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
                return new { message = "ok" };
            }
                  
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }

      //Función utilizada para copiar un gym y todos sus parametros en un completamente nuevo
      [HttpPost]
      [Route("admin/CopiarGimnasio")]
      public dynamic CopiarGimnasio(string new_gym, string copied_gym) {
        try {
          dynamic existeGym1 = aux.VerificarExistenciaSucursal_aux(new_gym);
          dynamic existeGym2 = aux.VerificarExistenciaSucursal_aux(copied_gym);
          if (!existeGym1 || !existeGym2) {
            return new { message = "No existe esta sucursal" };
          }
          string queryCopiar = @"INSERT INTO TRATAMIENTO_SPA
                              SELECT @CodigoGym1, Id_tratamiento
                              FROM TRATAMIENTO_SPA 
                              WHERE Codigo_sucursal = @CodigoGym2

                              INSERT INTO VENTA_PRODUCTO
                              SELECT @CodigoGym1, Codigo_producto 
                              FROM VENTA_PRODUCTO 
                              WHERE Codigo_sucursal = @CodigoGym2

                              INSERT INTO CLASE (Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad)
                              SELECT Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad
                              FROM CLASE JOIN EMPLEADO ON Cedula_instructor = Cedula
                              WHERE Codigo_suc = @CodigoGym2";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          using (SqlCommand comando = new SqlCommand(queryCopiar, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@CodigoGym1", Int64.Parse(new_gym));
            comando.Parameters.AddWithValue("@CodigoGym2", Int64.Parse(copied_gym));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }

      }

      //Función utilizada para registrar clientes a una clase ya existente
      [HttpPost]
      [Route("cliente/RegistrarClienteEnClase")]
      public dynamic RegistrarClienteEnClase(string cedulaClient, string Num_clase){
        try{
          // VERIFICAR EXISTENCIA DE CLASE
          dynamic existeClase = aux.VerificarExistenciaClasePorNum_aux(Num_clase);
          if(!existeClase){
            return new { message = "No existe esta clase en la BD" };
          }
          // REGISTRAR CLIENTE EN CLASE
          string queryInsert = "INSERT INTO ASISTENCIA_CLASE VALUES (@Cedula_cliente, @Num_clase)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Num_clase", Num_clase);
            comando.Parameters.AddWithValue("@Cedula_cliente", cedulaClient);
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion(); 
          return new {message = "ok"};
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para buscar clases correspondientes a una sucursal ya existente
      [HttpGet]
      [Route("cliente/BuscarClasePorSucursal")]
      public dynamic BuscarClasePorSucursal(string Codigo_sucursal){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles
                           FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Codigo_suc = SUCURSAL.Codigo_sucursal
                           WHERE SUCURSAL.Codigo_sucursal = @Codigo_sucursal
                           GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Int64.Parse(Codigo_sucursal));
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) { 
                  var clasesBuscadas = new List<dynamic>();
                  while (reader.Read()) {
                      clasesBuscadas.Add(new {
                          Fecha = reader.GetDateTime(0),
                          Hora_inicio = reader.GetTimeSpan(1),
                          Hora_fin = reader.GetTimeSpan(2),
                          Instructor = reader.GetString(3),
                          Cupos = reader.GetInt32(4)
                      });
                  }
                  DB_Handler.CerrarConexion();
                  return new JsonResult(clasesBuscadas);
              }
            }
          }
          DB_Handler.CerrarConexion();
          return new { message = "No hay clases para esta sucursal" };

        }catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para buscar clases ya existente por servicio que muestran
      [HttpGet]
      [Route("cliente/BuscarClasePorServicio")]
      public dynamic BuscarClasePorServicio(string Id_servicio){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles
                           FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Codigo_suc = SUCURSAL.Codigo_sucursal
                           WHERE CLASE.Id_servicio = @Id_servicio
                           GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Id_servicio", Int64.Parse(Id_servicio));
            comando.ExecuteNonQuery();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows) { 
                var clasesBuscadas = new List<dynamic>();
                while (reader.Read()) {
                    clasesBuscadas.Add(new {
                        Fecha = reader.GetDateTime(0),
                        Hora_inicio = reader.GetTimeSpan(1),
                        Hora_fin = reader.GetTimeSpan(2),
                        Empleado = reader.GetString(3),
                        Cupos = reader.GetInt32(4)
                    });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(clasesBuscadas);
            }
          }
          DB_Handler.CerrarConexion();
          return new { message = "No hay clases de este tipo en ninguna sucursal" };

        }catch{
          return new { message = "error" };
        }
      }

      
      //Función utilizada para buscar clases por periodos de tiempo
      [HttpGet]
      [Route("cliente/BuscarClasePorPeriodos")]
      public dynamic BuscarClasePorPeriodos(string fechaInicio, string fecha_fin){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles
                           FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula
                           WHERE @fecha_inicio <= Fecha AND Fecha <= @fecha_fin
                           GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@fecha_inicio", DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            comando.Parameters.AddWithValue("@fecha_fin", DateTime.ParseExact(fecha_fin, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            comando.ExecuteNonQuery();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows) { 
                var clasesBuscadas = new List<dynamic>();
                while (reader.Read()) {
                    clasesBuscadas.Add(new {
                        Fecha = reader.GetDateTime(0),
                        Hora_inicio = reader.GetTimeSpan(1),
                        Hora_fin = reader.GetTimeSpan(2),
                        Empleado = reader.GetString(3),
                        Cupos = reader.GetInt32(4)
                    });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(clasesBuscadas);
            }
          }
          DB_Handler.CerrarConexion();
          return new { message = "No hay clases en este rango de fechas" };

        }catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para copiar todas las actividades en un rango de tiempo a otro rango de tiempo
      [HttpPost]
      [Route("admin/CopiarCalendarioActividades")]
      public dynamic CopiarCalendarioActividades(string fechaInicio, string fechaFin, string semanasMover){
        try{ 
          // VERIFICAR EXISTENCIA DE CLASE 
          // dynamic existeCalendario = aux.VerificarExistenciaClase_aux(Id_servicio, Cedula_instructor, Modalidad, fechaInicio, Hora_inicio);
          //if(!existeCalendario){
            //return new { message = "No existe este calendario en la BD" };
          //}
          // COPIAR CALENDARIO EN CLASE
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryInsert = @"INSERT INTO CLASE (Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor)
                                SELECT Id_servicio, DATEADD(WEEK, @CantidadSemanas, Fecha), Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor
                                FROM CLASE WHERE @Fecha_Inicio <= Fecha AND Fecha <= @Fecha_fin
                                GROUP BY Id_servicio, Num_clase, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor
                                ";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@CantidadSemanas", Int64.Parse(semanasMover));
            comando.Parameters.AddWithValue("@Fecha_Inicio", fechaInicio);
            comando.Parameters.AddWithValue("@Fecha_fin", fechaFin);
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new {message = "ok"};
          } catch (Exception e) {
            Console.WriteLine(e);
            return new { message = "Error al copiar calendario" };
          }
      }
      
      //Función utilizada para eliminar un inventario ya existente
      [HttpPost]
      [Route("admin/EliminarInventario")] 
      public dynamic EliminarInventario(string Numero_serie){
        // VERIFICAR QUE EXISTE EL INVENTARIO EN LA BASE DE DATOS
        dynamic existeInventario = aux.VerificarExistenciaInventario_aux(Numero_serie);
        if(!existeInventario){
          return new { message = "No existe en el inventario en la BD" };
        }
        // ELIMINAR UN ARTICULO DEL INVENTARIO EN LA BASE DE DATOS
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          // VERIFICAR SI EXISTE EN UNA SUCURSAL DE LA BD
          dynamic existeInventarioSucursal = aux.VerificarExistenciaInventarioEnSucursal_aux(Numero_serie);
          if(existeInventarioSucursal){
            string queryInsert2 = "DELETE FROM INVENTARIO_EN_SUCURSAL WHERE Num_serie_maquina = @Numero_serie";
            using (SqlCommand comando = new SqlCommand(queryInsert2, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Numero_serie", Int64.Parse(Numero_serie));
              comando.ExecuteNonQuery();
            }
          }
          string queryInsert = "DELETE FROM INVENTARIO WHERE Numero_serie = @Numero_serie";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Numero_serie", Int64.Parse(Numero_serie));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerTotalidadInventario")]
      public dynamic VerTotalidadInventario() {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryGet = "SELECT * FROM INVENTARIO";
          using (SqlCommand comando = new SqlCommand(queryGet, DB_Handler.conectarDB)) {
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var todoInventario = new List<dynamic>();
                while (reader.Read()) {
                  todoInventario.Add(new {
                    Num_serie = reader.GetInt32(0),
                    Marca = reader.GetString(1),
                    Tipo_equipo = reader.GetInt32(2)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(todoInventario);
              }
              else {
                return new { message = "no hay inventario" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar nuevo inventario a la db
      [HttpPost]
      [Route("admin/AgregarInventario")]
      public dynamic AgregarInventario(string numSerie, string marca, string idTipoEquipo){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          // VERIFICAR QUE NO EXISTA PREVIAMENTE EL INVENTARIO EN LA BASE DE DATOS
          dynamic existeInventario = aux.VerificarExistenciaInventario_aux(numSerie);
          if(existeInventario){
            return new { message = "Ya existe en el inventario en la BD" };
          }
          // VERIFICAR QUE EXISTE EL TIPO DE EQUIPO EN LA BASE DE DATOS
          dynamic existeTipoEquipo = aux.VerificarExistenciaTipoEquipo_aux(idTipoEquipo);
          if(!existeTipoEquipo){
            return new { message = "No existe el tipo de equipo en la BD" };
          }
          
          // INSERTAR INVENTARIO EN LA BASE DE DATOS
          string queryInsert = "INSERT INTO INVENTARIO VALUES (@Num_serie, @Marca, @Id_Tipo)";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Num_serie", Int64.Parse(numSerie));
            comando.Parameters.AddWithValue("@Marca", marca);
            comando.Parameters.AddWithValue("@Id_Tipo", Int64.Parse(idTipoEquipo));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para ver las clases existentes
      [HttpGet]
      [Route("admin/VerClases")]
      public dynamic VerClases(){
        try{
          return aux.VerClases_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerClasesConCupo")]
      public dynamic VerClasesConCupo() {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT CLASE.Num_clase, CLASE.Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Cedula_instructor, Codigo_suc, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos
                          FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula
                          GROUP BY CLASE.Num_clase, CLASE.Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Cedula_instructor, Codigo_suc, Capacidad
                          HAVING Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) > 0";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var clases = new List<dynamic>();
                while (reader.Read()) {
                  clases.Add(new {
                    Num_clase = reader.GetInt32(0),
                    Id_servicio = reader.GetInt32(1),
                    Fecha = reader.GetDateTime(2),
                    Hora_inicio = reader.GetTimeSpan(3),
                    Hora_fin = reader.GetTimeSpan(4),
                    Modalidad = reader.GetString(5),
                    Cedula_instructor = reader.GetInt32(6),
                    Codigo_sucursal = reader.GetInt32(7),
                    Cupos = reader.GetInt32(8)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(clases);
              } else {
                return new { message = "vacio" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para eliminar una clase ya existente
      [HttpPost]
      [Route("admin/EliminarClase")] 
      public dynamic EliminarClase(string Num_clase) {//, string Id_servicio, string cedulaInstructor, string modalidad, string fecha, string horaInicio){
        // VERIFICAR QUE NO EXISTE LA CLASE EN LA BASE DE DATOS
        //dynamic existeClase = aux.VerificarExistenciaClase_aux(Id_servicio, cedulaInstructor, modalidad, fecha, horaInicio);
        //if(!existeClase){
          //return new { message = "No existe esta clase en la BD" };
        //}
        // ELIMINAR CLASE EN LA BASE DE DATOS
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryInsert = "DELETE FROM CLASE WHERE Num_clase = @Num";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Num", Int64.Parse(Num_clase));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para eliminar un servicio ya existente
      [HttpPost]
      [Route("admin/EliminarServicio")]
      public dynamic EliminarServicio(string Id_servicio){
        // VERIFICAR QUE NO EXISTE EL SERVICIO EN LA BASE DE DATOS
        dynamic existeServicio = aux.VerificarExistenciaServicio_aux(Id_servicio);
        if(!existeServicio){
          return new { message = "No existe este servicio en la BD" };
        }
        // ELIMINAR SERVICIO EN LA BASE DE DATOS
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryInsert = "DELETE FROM SERVICIO WHERE Identificador = @Id_servicio";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Id_servicio", Int64.Parse(Id_servicio));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para ver los servicios existentes en la db
      [HttpGet]
      [Route("admin/VerServicios")]
      public dynamic VerServicios(){
        try{
          return aux.VerServicios_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar un nuevo servicio a la db
      [HttpPost]
      [Route("admin/AgregarServicio")]
      public dynamic AgregarServicio(string descripcion) {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryInsert = "INSERT INTO SERVICIO (Descripcion) VALUES (@descripcion)";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@descripcion", descripcion);
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error"};
        }
      }

      //Función utilizada para crear una nueva clase
      [HttpPost]
      [Route("admin/CrearClase")]
      public dynamic CrearClase(string idServicio, string cedulaInstructor, string modalidad, string capacidad, string fecha, string horaInicio, string horaFinal){
        try{
            if(string.IsNullOrEmpty(idServicio) || string.IsNullOrEmpty(modalidad) || string.IsNullOrEmpty(fecha) || string.IsNullOrEmpty(horaInicio) || string.IsNullOrEmpty(horaFinal) || string.IsNullOrEmpty(capacidad) || string.IsNullOrEmpty(cedulaInstructor)){
              return new { message = "error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO CLASE VALUES (@Id_servicio, @Fecha, @Hora_inicio, @Hora_fin, @Modalidad, @Capacidad, @Cedula_instructor)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Id_servicio", Int64.Parse(idServicio));
              comando.Parameters.AddWithValue("@Fecha", DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
              comando.Parameters.AddWithValue("@Hora_inicio", TimeSpan.ParseExact(horaInicio, @"hh\:mm\:ss", System.Globalization.CultureInfo.InvariantCulture));
              comando.Parameters.AddWithValue("@Hora_fin", TimeSpan.ParseExact(horaFinal, @"hh\:mm\:ss", System.Globalization.CultureInfo.InvariantCulture));
              comando.Parameters.AddWithValue("@Modalidad", modalidad);
              comando.Parameters.AddWithValue("@Capacidad", Int64.Parse(capacidad));
              comando.Parameters.AddWithValue("@Cedula_instructor", Int64.Parse(cedulaInstructor));
              comando.ExecuteNonQuery();
            }
            DB_Handler.CerrarConexion();
            return new { message = "ok" };
          }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
          }
      }

      [HttpGet]
      [Route("admin/VerTotalidadProductos")]
      public dynamic VerTotalidadProductos() {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = "SELECT * FROM PRODUCTO";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var totalProductos = new List<dynamic>();
                while (reader.Read()) {
                  totalProductos.Add(new {
                    Codigo_barras = reader.GetInt32(0), 
                    Nombre = reader.GetString(1),
                    Descripcion = reader.GetString(2),
                    Costo = reader.GetDouble(3)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(totalProductos);
              } else {
                return new { message = "no hay productos" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar un nuevo producto a la db
      [HttpPost]
      [Route("admin/AgregarProducto")]
      public dynamic AgregarProducto(string codigoBarras, string nombreProducto, string Descripcion, string costo){
        // VERIFICAR QUE NO EXISTE EL PRODUCTO EN LA BASE DE DATOS
        dynamic existeProducto = aux.VerificarExistenciaProducto_aux(codigoBarras);
        if(existeProducto){
          return new { message = "Ya existe este producto en la BD" };
        }
        // INSERTAR PRODUCTO EN LA BASE DE DATOS
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryInsert = "INSERT INTO PRODUCTO VALUES (@Codigo_barras, @Nombre, @Descripcion, @Costo)";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_barras", Int64.Parse(codigoBarras));
            comando.Parameters.AddWithValue("@Nombre", nombreProducto);
            comando.Parameters.AddWithValue("@Descripcion", Descripcion);
            comando.Parameters.AddWithValue("@Costo", Math.Round(Convert.ToDouble(costo, CultureInfo.InvariantCulture), 2));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para eliminar un producto ya existente
      [HttpPost]
      [Route("admin/EliminarProducto")]
      public dynamic EliminarProducto(string codigoBarras){
        // VERIFICAR QUE NO EXISTE EL PRODUCTO EN LA BASE DE DATOS
        dynamic existeProducto = aux.VerificarExistenciaProducto_aux(codigoBarras);
        if(!existeProducto){
          return new { message = "No existe este producto en la BD" };
        }
        // ELIMINAR PRODUCTO EN LA BASE DE DATOS
        try{

          // ELIMINAR DE VENTA_PRODUCTO

          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryDelete = "DELETE FROM VENTA_PRODUCTO WHERE Codigo_producto = @Codigo_barras";
          using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_barras", Int64.Parse(codigoBarras));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();

          // ELIMINAR DE PRODUCTO
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryDelete2 = "DELETE FROM PRODUCTO WHERE Codigo_barras = @Codigo_barras";
          using (SqlCommand comando = new SqlCommand(queryDelete2, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_barras", Int64.Parse(codigoBarras));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();


          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para activar la tienda correspondiente a una sucursal
      [HttpPost]
      [Route("admin/ActivarTienda")]
      public dynamic ActivarTienda(string codigo_sucursal){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = "UPDATE TIENDA SET Estado = 1 WHERE Codigo_sucursal = @Codigo";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo", Int64.Parse(codigo_sucursal));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };

        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }

      }

      //Función utilizada para activar el spa correspondiente a una sucursal
      [HttpPost]
      [Route("admin/ActivarSPA")]
      public dynamic ActivarSPA(string codigo_sucursal){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = "UPDATE SPA SET Estado = 1 WHERE Codigo_sucursal = @Codigo";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo", Int64.Parse(codigo_sucursal));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };

        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerTiposEquipo")]
      public dynamic VerTiposEquipo(){
        try{
          return aux.VerTiposEquipo_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar un nuevo tipo de equipo a la db
      [HttpPost]
      [Route("admin/AgregarTipoEquipo")]
      public dynamic AgregarTipoEquipo(string descripcion){
        try{
            if(string.IsNullOrEmpty(descripcion)){
              return new { message = "error" };}

            // INSERTAR TIPO DE EQUIPO EN LA BASE DE DATOS
            dynamic existeTipoEquipo = aux.verExistenciaTipoEquipoPorDesc(descripcion);
            if(existeTipoEquipo){
              return new { message = "Ya existe este tipo de equipo en la BD" };
            }
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO TIPO_EQUIPO VALUES (@Descripcion)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Descripcion", descripcion);
              comando.ExecuteNonQuery();
            }
            DB_Handler.CerrarConexion();
            return new { message = "ok" };
          }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
          }
      }

      //Función utilizada para eliminar un tipo de equipo ya existente
      [HttpPost]
      [Route("admin/EliminarTipoEquipo")]
      public dynamic EliminarTipoEquipo(string idTipoEquipo){
        try{
            if(string.IsNullOrEmpty(idTipoEquipo)){
              return new { message = "error" };}

            // ELIMINAR TIPO DE EQUIPO EN LA BASE DE DATOS
            dynamic existeTipoEquipo = aux.VerificarExistenciaTipoEquipo_aux(idTipoEquipo);
            if(!existeTipoEquipo){
              return new { message = "No existe este tipo de equipo en la BD" };
            }
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryDelete = "DELETE FROM TIPO_EQUIPO WHERE Identificador = @Identificador";
            using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Identificador", Int64.Parse(idTipoEquipo));
              comando.ExecuteNonQuery();
            }
            DB_Handler.CerrarConexion();
            return new { message = "ok" };
          }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
          }
      }
      
      //Función utilizada para ver los empleados existentes en la db
      [HttpGet]
      [Route("admin/VerEmpleados")]
      public dynamic VerEmpleados(){
        try{
          return aux.VerEmpleados_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerAdministradores")]
      public dynamic VerAdministradores() {
        try {
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = "SELECT * FROM EMPLEADO WHERE Id_puesto = 1";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.ExecuteNonQuery();
            using (SqlDataReader reader = comando.ExecuteReader()) {
              if (reader.HasRows) {
                var admins = new List<dynamic>();
                while (reader.Read()) {
                  admins.Add(new {
                    Cedula = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellido1 = reader.GetString(2),
                    Apellido2 = reader.GetString(3),
                    Distrito = reader.GetString(4),
                    Canton = reader.GetString(5),
                    Provincia = reader.GetString(6),
                    Correo = reader.GetString(7),
                    Contrasena = reader.GetString(8),
                    Salario = reader.GetDouble(9),
                    Id_puesto = reader.GetInt32(10),
                    Id_planilla = reader.GetInt32(11),
                    Codigo_suc = reader.GetInt32(12)
                  });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(admins);
              } else {
                return new { message = "vacio" };
              }
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar un nuevo empleado a la db
      [HttpPost]
      [Route("admin/AgregarEmpleado")]
      public dynamic AgregarEmpleado(string cedula, string nombre, string apellido1, string apellido2, string distrito, string canton, string provincia, string correo, string contrasena, string salario, string id_puesto, string id_planilla, string codigo_suc){
        try{
          if(string.IsNullOrEmpty(cedula) || cedula.Length != 9 ||string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2) || string.IsNullOrEmpty(distrito) || string.IsNullOrEmpty(canton) || string.IsNullOrEmpty(provincia) || string.IsNullOrEmpty(correo) || !correo.Contains('@') || !correo.Contains('.') || string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(salario) || string.IsNullOrEmpty(id_puesto) || string.IsNullOrEmpty(id_planilla) || string.IsNullOrEmpty(codigo_suc)){
            return new { message = "error" };}

          // INSERTAR EMPLEADO EN LA BASE DE DATOS
          dynamic existeEmpleado = aux.VerificarExistenciaEmpleado_aux(cedula);
          if(existeEmpleado){
            return new { message = "Ya existe este empleado en la BD" };
          }
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryInsert = "INSERT INTO EMPLEADO VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @Distrito, @Canton, @Provincia, @Correo, @Contrasena, @Salario, @Id_Puesto, @Id_Planilla, @Codigo_suc)";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Apellido1", apellido1);
            comando.Parameters.AddWithValue("@Apellido2", apellido2);
            comando.Parameters.AddWithValue("@Distrito", distrito);
            comando.Parameters.AddWithValue("@Canton", canton);
            comando.Parameters.AddWithValue("@Provincia", provincia);
            comando.Parameters.AddWithValue("@Correo", correo);
            comando.Parameters.AddWithValue("@Contrasena", contrasena);
            comando.Parameters.AddWithValue("@Salario", Int64.Parse(salario));
            comando.Parameters.AddWithValue("@Id_Puesto", Int64.Parse(id_puesto));
            comando.Parameters.AddWithValue("@Id_Planilla", Int64.Parse(id_planilla));
            comando.Parameters.AddWithValue("@Codigo_suc", Int64.Parse(codigo_suc));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para eliminar un empleado ya existente
      [HttpPost]
      [Route("admin/EliminarEmpleado")]
      public dynamic EliminarEmpleado(string cedula){
        try{
          if(string.IsNullOrEmpty(cedula) || cedula.Length != 9){
            return new { message = "error" };}

          // ELIMINAR EMPLEADO EN LA BASE DE DATOS
          dynamic existeEmpleado = aux.VerificarExistenciaEmpleado_aux(cedula);
          if(!existeEmpleado){
            return new { message = "No existe este empleado en la BD" };
          }
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryDelete = "DELETE FROM EMPLEADO WHERE Cedula = @Cedula";
          using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }
      
      //Función utilizada para ver las planillas ya existentes en la db
      [HttpGet]
      [Route("admin/VerPlanillas")]
      public dynamic VerPlanillas(){
        try{
          return aux.VerPlanillas_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para generar todas las planillas correspondientes de los empleados
      [HttpGet]
      [Route("admin/GenerarPlanillasTodos")]
      public dynamic GenerarPlanillasTodos(){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Codigo_suc AS Sucursal, Cedula, Nombre, Apellido1 AS Primer_apellido, Apellido2 AS Segundo_apellido, 
                          CASE
                            WHEN Id_planilla = 2 THEN CAST(8 AS VARCHAR)
                            ELSE 'N/A'
                          END AS Horas_laboradas,
                          CASE
                            WHEN Id_planilla = 3 THEN CAST(COUNT(Cedula_instructor) AS VARCHAR)
                            ELSE 'N/A'
                          END AS Clases_impartidas,
                          CASE
                            WHEN Id_planilla = 1 THEN Salario
                            WHEN Id_planilla = 2 THEN Salario * 8
                          WHEN Id_planilla = 3 THEN Salario * COUNT(Cedula_instructor)
                        END AS Monto_total
                        FROM EMPLEADO LEFT JOIN CLASE ON Cedula = Cedula_instructor 
                        GROUP BY Codigo_suc, Cedula, Nombre, Apellido1, Apellido2, Id_planilla, Salario;
                        ";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.ExecuteNonQuery();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows) { 
                var planillaDeTodosLosEmpleados = new List<dynamic>();
                while (reader.Read()) {
                    planillaDeTodosLosEmpleados.Add(new {
                        Sucursal = reader.GetInt32(0),
                        Cedula_empleado = reader.GetInt32(1),
                        Nombre = reader.GetString(2),
                        Primer_apellido = reader.GetString(3),
                        Segundo_apellido = reader.GetString(4),
                        Horas_laboradas = reader.GetString(5),
                        Clases_impartidas = reader.GetString(6),
                        Monto_total = reader.GetDouble(7)
                    });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(planillaDeTodosLosEmpleados);
            }
            else {
                DB_Handler.CerrarConexion();
                return new { message = "No hay tipos de planillas" };
            } 
          }
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar una nueva planilla a la db
      [HttpPost]
      [Route("admin/AgregarPlanilla")]
        public dynamic AgregarPlanilla(string descripcionPlanilla){
          try{

              // VERIFICACION DE DATOS
              if(string.IsNullOrEmpty(descripcionPlanilla)){
                  return new { message = "error" };}

              // INSERTAR PLANILLA EN LA BASE DE DATOS
              dynamic existePlanilla = aux.verExistenciaPlanillaPorDesc(descripcionPlanilla);
              if(existePlanilla){
                  return new { message = "Ya existe esta planilla en la BD" };
              }

              DB_Handler.ConectarServer();
              DB_Handler.AbrirConexion();
              string queryInsert = "INSERT INTO PLANILLA (Descripcion) VALUES (@Descripcion)";
              using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                  comando.Parameters.AddWithValue("@Descripcion", descripcionPlanilla);
                  comando.ExecuteNonQuery();
              }
              DB_Handler.CerrarConexion();
              return new { message = "ok" };

          }catch(Exception e){
              Console.WriteLine(e);
              return new { message = "error" };
          }
        }

      //Función utilizada para eliminar una planilla ya existente
      [HttpPost]
      [Route("admin/EliminarPlanilla")]
      public dynamic EliminarPlanilla(string idPlanilla){
        try{
            // VERIFICACION DE DATOS
            if(string.IsNullOrEmpty(idPlanilla)){
                return new { message = "error" };}

            // ELIMINAR PLANILLA EN LA BASE DE DATOS
            dynamic existePlanilla = aux.VerificarExistenciaPlanilla_aux(idPlanilla);
            if(!existePlanilla){
                return new { message = "No existe esta planilla en la BD" };
            }

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryDelete = "DELETE FROM PLANILLA WHERE Identificador = @idPlanilla";
            using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@idPlanilla", Int64.Parse(idPlanilla));
                comando.ExecuteNonQuery();
            }
            DB_Handler.CerrarConexion();
            return new { message = "ok" };

        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }

      //Función utilizada para ver todos los puestos ya existentes en la db
      [HttpGet]
      [Route("admin/VerPuestos")]
      public dynamic VerPuestos(){
        try{
          return aux.VerPuestos_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar un nuevo puesto a la db
      [HttpPost]
      [Route("admin/AgregarPuesto")]
      public dynamic AgregarPuesto(string descripcionPuesto){
        try{
            if(string.IsNullOrEmpty(descripcionPuesto)){
                return new { message = "error" };}

            dynamic existePuesto = aux.VerificarExistenciaPuesto_aux(descripcionPuesto);
            if (existePuesto) {
                return new { message = "Puesto ya existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO PUESTO (Descripcion) VALUES (@Descripcion)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Descripcion", descripcionPuesto);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }
            return new { message = "ok" };
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }

      //Función utilizada para eliminar un puesto ya existente
      [HttpPost]
      [Route("admin/EliminarPuesto")]
      public dynamic EliminarPuesto(string Id_puesto){
        try{
            if(string.IsNullOrEmpty(Id_puesto)){
                return new { message = "error" };}

            // ELIMINAR PUESTO EN LA BASE DE DATOS
            bool existePuesto = aux.VerificarExistenciaPuesto_aux(Id_puesto);
            if (!existePuesto) {
                return new { message = "Puesto no existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryDelete = "DELETE FROM PUESTO WHERE Identificador = @Id_puesto";
            using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Id_puesto", Id_puesto);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }
            return new { message = "ok" };
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }      

      //Función utilizada para ver los tratamientos ya existentes ya en la db
      [HttpGet]
      [Route ("admin/VerTratamientos")]
      public dynamic VerTratamientos(){
        try{
          return aux.VerTratamientos_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para agregar un nuevo tratamiento a la db
      [HttpPost]
      [Route("admin/AgregarTratamiento")]
      public dynamic AgregarTratamiento(string descripcionTratamiento){
        try{
          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(descripcionTratamiento)) {
              return new { message = "error" };}

          // INSERTAR TRATAMIENTO EN LA BASE DE DATOS
          dynamic existeTratamiento = aux.VerExistenciaTratamientoPorDesc_aux(descripcionTratamiento);
            if (existeTratamiento) {
                return new { message = "Tratamiento ya existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO TRATAMIENTO (Descripcion) VALUES (@Nombre)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Nombre", descripcionTratamiento);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }
            return new { message = "ok" };
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }

      //Función utilizada para eliminar un tratamiento ya existente
      [HttpPost]
      [Route("admin/EliminarTratamiento")]
      public dynamic EliminarTratamiento(string idTratamiento){
        try{
          // VERIFICAR QUE EXISTE EL TRATAMIENTO
          if (string.IsNullOrEmpty(idTratamiento)) {
              return new { message = "error" };}
          bool existeTratamiento = aux.VerificarExistenciaTratamiento_aux(idTratamiento);
          if (!existeTratamiento) {
              return new { message = "Tratamiento no existe en la BD. Error" };}
          // ELIMINAR TRATAMIENTO EN LA BASE DE DATOS
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryDelete = "DELETE FROM TRATAMIENTO WHERE Identificador = @Id_tratamiento";
          using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Id_tratamiento", Int64.Parse(idTratamiento));
              comando.ExecuteNonQuery();
              DB_Handler.CerrarConexion();
          }
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      //Función utilizada para realizar el registro de un cliente a la db
      [HttpPost]
      [Route("login/SignUpCliente")]
      public dynamic SignUpCliente(string cedula, string nombre, string apellido1, string apellido2, string fechaNacimiento, string peso, string direccion, string correoElectronico, string contrasena) {
          try {              
              // VERIFICACION DE DATOS
              if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre)|| string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2) || string.IsNullOrEmpty(fechaNacimiento)|| string.IsNullOrEmpty(peso) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correoElectronico)|| string.IsNullOrEmpty(contrasena)) {
                  return new { message = "error1" };}
              if (cedula.Length < 9 || cedula[0] == '0' || !correoElectronico.Contains("@") || !correoElectronico.Contains(".")) {
                  return new { message = cedula.Length };}

              // SEPARAR FECHA DE NACIMIENTO
              string[] fechaNSeparada = fechaNacimiento.Split("-");
              string anio = fechaNSeparada[0];
              string mes = fechaNSeparada[1];
              string dia = fechaNSeparada[2];

              // INSERTAR DATOS EN LA BASE DE DATOS
              DB_Handler.ConectarServer();
              DB_Handler.AbrirConexion();
              string queryInsert = "INSERT INTO CLIENTE VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @Dia_nacimiento, @Mes_nacimiento, @Ano_nacimiento, @Peso, @Direccion, @Correo, @Contrasena)";
              using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                  comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
                  comando.Parameters.AddWithValue("@Nombre", nombre);
                  comando.Parameters.AddWithValue("@Apellido1", apellido1);
                  comando.Parameters.AddWithValue("@Apellido2", apellido2);
                  comando.Parameters.AddWithValue("@Dia_nacimiento", dia);
                  comando.Parameters.AddWithValue("@Mes_nacimiento", mes);
                  comando.Parameters.AddWithValue("@Ano_nacimiento", anio);
                  comando.Parameters.AddWithValue("@Peso", Math.Round(Convert.ToDouble(peso, CultureInfo.InvariantCulture), 2));
                  comando.Parameters.AddWithValue("@Direccion", direccion);
                  comando.Parameters.AddWithValue("@Correo", correoElectronico);
                  comando.Parameters.AddWithValue("@Contrasena", aux.EncriptarContrasenaMD5_aux(contrasena));
                  comando.ExecuteNonQuery();
              }
              DB_Handler.CerrarConexion();

              return new { message = "ok" };

          } catch (Exception e) {
              Console.WriteLine(e);
              return new { message = "error" };
          }
      }

      //Función utilizada para realizar el login de un cliente
      [HttpPost]
      [Route("login/Login")]
      public dynamic Login(string cedula, string contrasena){
        try{
          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(contrasena)) {
              return new { message = "error: valores nulos" };}

          // VERIFICAR QUE NO SEA UN EMPLEADO
          dynamic esEmpleado = aux.VerificarExistenciaEmpleado_aux(cedula);
          if (esEmpleado) {
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string querySelect = "SELECT * FROM EMPLEADO WHERE Cedula = @Cedula AND Contrasena = @Contrasena";
            using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
                comando.Parameters.AddWithValue("@Contrasena", aux.EncriptarContrasenaMD5_aux(contrasena));
                comando.ExecuteNonQuery();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows) {
                    var empleado = new List<dynamic>();
                    while (reader.Read()) {
                      empleado.Add(new {
                        Cedula = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido1 = reader.GetString(2),
                        Apellido2 = reader.GetString(3),
                        Distrito = reader.GetString(4),
                        Canton = reader.GetString(5),
                        Provincia = reader.GetString(6),
                        Correo = reader.GetString(7),
                        Contrasena = reader.GetString(8),
                        Salario = reader.GetDouble(9),
                        Id_puesto = reader.GetInt32(10),
                        Id_planilla = reader.GetInt32(11),
                        Codigo_suc = reader.GetInt32(12),
                        Tipo = "admin"
                      });
                    }
                    DB_Handler.CerrarConexion();
                    return new JsonResult(empleado);
                } else {
                    DB_Handler.CerrarConexion();
                    return new { message = "no existe ese usuario" };
                }
            }
          }
          else{
            // VERIFICAR QUE EL CLIENTE EXISTA EN LA BASE DE DATOS
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string querySelect2 = "SELECT * FROM CLIENTE WHERE Cedula = @Cedula AND Contrasena = @Contrasena";
            using (SqlCommand comando = new SqlCommand(querySelect2, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
                comando.Parameters.AddWithValue("@Contrasena", aux.EncriptarContrasenaMD5_aux(contrasena));
                comando.ExecuteNonQuery();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows) {
                    var cliente = new List<dynamic>();
                    while (reader.Read()) {
                      cliente.Add(new {
                        Cedula = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido1 = reader.GetString(2),
                        Apellido2 = reader.GetString(3),
                        Dia_nacimiento = reader.GetString(4),
                        Mes_nacimiento = reader.GetString(5),
                        Ano_nacimiento = reader.GetString(6),
                        Peso = reader.GetDouble(7),
                        Direccion = reader.GetString(8),
                        Correo = reader.GetString(9),
                        Contrasena = reader.GetString(10),
                        Tipo = "cliente"
                      });
                    }
                    DB_Handler.CerrarConexion();
                    return new JsonResult(cliente);
                } else {
                    DB_Handler.CerrarConexion();
                    return new { message = "no existe ese usuario" };
                }
            }
          }

        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }

          
      }

    }
  }