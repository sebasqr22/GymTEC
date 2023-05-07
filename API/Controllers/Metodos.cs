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

      [HttpPost]
      [Route("admin/VerSucursal")]
      public dynamic VerSucursales(){
        try{
          return aux.VerSucursales_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/AgregarSucursal")]
      public dynamic AgregarSucursal(string Codigo_sucursal, string Nombre, string Distrito, string Canton, string Provincia, string Fecha_apertura, string Hora_apertura, string Hora_cierre, string Max_capacidad, string Cedula_administrador){
        try{

          string queryInsert = "INSERT INTO SUCURSAL VALUES (@Nombre, @Distrito, @Canton, @Provincia, @Fecha_apertura, @Hora_apertura, @Hora_cierre, @Max_capacidad, @Cedula_administrador)";
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
            comando.Parameters.AddWithValue("@Fecha_apertura", DateTime.ParseExact(Hora_apertura, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            comando.Parameters.AddWithValue("@Hora_apertura", DateTime.ParseExact(Hora_apertura, "HH:mm:ss", CultureInfo.InvariantCulture));
            comando.Parameters.AddWithValue("@Hora_cierre", DateTime.ParseExact(Hora_cierre, "HH:mm:ss", CultureInfo.InvariantCulture));
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

      [HttpPost]
      [Route("admin/AsociarServiciosASucursal")]
      public dynamic AsociarServiciosASucursal(string Codigo_sucursal, string idServicio){
        try{
          string queryInsert = "INSERT INTO SERVICIOS_EN_SUCURSAL VALUES (@codigoSucursal, @idServicio)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();

          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Int64.Parse(Codigo_sucursal));
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
            comando.Parameters.AddWithValue("@NumSerie", num_serie);
            comando.Parameters.AddWithValue("@Costo", Math.Round(Convert.ToDouble(costo, CultureInfo.InvariantCulture), 2));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();

          // Actualizar sucursal en inventario
          string queryUpdate = "UPDATE INVENTARIO SET Codigo_sucursal = @Codigo_sucursal WHERE Num_serie = @Num_serie";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          using (SqlCommand comando = new SqlCommand(queryUpdate, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Int64.Parse(Codigo_sucursal));
            comando.Parameters.AddWithValue("@Num_serie", num_serie);
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();

          return new { message = "ok"};
        } catch (Exception e) {
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }
      
      [HttpPost]
      [Route("admin/AsociarProductosATienda")]
      public dynamic AsociarProductosATienda(string Codigo_sucursal, string Codigo_producto){
        try{
          string queryInsert = "INSERT INTO VENTA_PRODUCTO VALUES (@codigoSucursal, @codigoProducto)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Int64.Parse(Codigo_sucursal));
            comando.Parameters.AddWithValue("@Codigo_producto", Int64.Parse(Codigo_producto));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
          
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

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

      [HttpPost]
      [Route("cliente/RegistrarClienteEnClase")]
      public dynamic RegistrarClienteEnClase(string cedulaClient, string Num_clase, string Id_servicio, string Fecha, string Hora_inicio,string Modalidad, string Cedula_instructor){
        try{
          // VERIFICAR EXISTENCIA DE CLASE
          dynamic existeClase = aux.VerificarExistenciaClase_aux(Id_servicio, Cedula_instructor, Modalidad, Fecha, Hora_inicio);
          if(!existeClase){
            return new { message = "No existe esta clase en la BD" };
          }
          // REGISTRAR CLIENTE EN CLASE
          string queryInsert = "INSERT INTO ASISTENCIA_CLASE VALUES (@Num_clase, @Id_servicio, @Cedula_cliente)";
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Num_clase", Num_clase);
            comando.Parameters.AddWithValue("@Id_servicio", Id_servicio);
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

      [HttpGet]
      [Route("cliente/BuscarClase")]
      public dynamic BuscarClase(string Codigo_sucursal,string Id_servicio, string fechaInicio, string fecha_fin){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles
                           FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre
                           WHERE SUCURSAL.Codigo_sucursal = @Codigo_sucursal AND CLASE.Id_servicio = Id_servicio AND @fecha_inicio <= Fecha AND Fecha <= fecha_fin
                           GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Codigo_sucursal);
            comando.Parameters.AddWithValue("@Id_servicio", Id_servicio);
            comando.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
            comando.Parameters.AddWithValue("@fecha_fin", fecha_fin);
            comando.ExecuteNonQuery();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows) { 
                var clasesBuscadas = new List<dynamic>();
                while (reader.Read()) {
                    clasesBuscadas.Add(new {
                        Fecha = reader.GetString(0),
                        Hora_inicio = reader.GetString(1),
                        Hora_fin = reader.GetString(2),
                        EmpleadoNombre = reader.GetString(3),
                        EmpleadoApellido1 = reader.GetString(4),
                        EmpleadoApellido2 = reader.GetString(5),
                        Capacidad = reader.GetInt32(6)
                    });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(clasesBuscadas);
            }
          }
          DB_Handler.CerrarConexion();
          return new { message = "No hay clases en este rango de fechas" };

        }catch{
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("cliente/BuscarClasePorSucursal")]
      public dynamic BuscarClasePorSucursal(string Codigo_sucursal){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles
                           FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre
                           WHERE SUCURSAL.Codigo_sucursal = @Codigo_sucursal
                           GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_sucursal", Codigo_sucursal);
            comando.ExecuteNonQuery();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows) { 
                var clasesBuscadas = new List<dynamic>();
                while (reader.Read()) {
                    clasesBuscadas.Add(new {
                        Fecha = reader.GetString(0),
                        Hora_inicio = reader.GetString(1),
                        Hora_fin = reader.GetString(2),
                        EmpleadoNombre = reader.GetString(3),
                        EmpleadoApellido1 = reader.GetString(4),
                        EmpleadoApellido2 = reader.GetString(5),
                        Capacidad = reader.GetInt32(6)
                    });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(clasesBuscadas);
            }
          }
          DB_Handler.CerrarConexion();
          return new { message = "No hay clases para esta sucursal" };

        }catch{
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("cliente/BuscarClasePorServicio")]
      public dynamic BuscarClasePorServicio(string Id_servicio){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles
                           FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre
                           WHERE CLASE.Id_servicio = Id_servicio
                           GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Id_servicio", Id_servicio);
            comando.ExecuteNonQuery();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows) { 
                var clasesBuscadas = new List<dynamic>();
                while (reader.Read()) {
                    clasesBuscadas.Add(new {
                        Fecha = reader.GetString(0),
                        Hora_inicio = reader.GetString(1),
                        Hora_fin = reader.GetString(2),
                        EmpleadoNombre = reader.GetString(3),
                        EmpleadoApellido1 = reader.GetString(4),
                        EmpleadoApellido2 = reader.GetString(5),
                        Capacidad = reader.GetInt32(6)
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

      [HttpGet]
      [Route("cliente/BuscarClasePorPeriodos")]
      public dynamic BuscarClasePorPeriodos(string fechaInicio, string fecha_fin){
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string query = @"SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles
                           FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre
                           WHERE @fecha_inicio <= Fecha AND Fecha <= fecha_fin
                           GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad";
          using (SqlCommand comando = new SqlCommand(query, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
            comando.Parameters.AddWithValue("@fecha_fin", fecha_fin);
            comando.ExecuteNonQuery();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows) { 
                var clasesBuscadas = new List<dynamic>();
                while (reader.Read()) {
                    clasesBuscadas.Add(new {
                        Fecha = reader.GetString(0),
                        Hora_inicio = reader.GetString(1),
                        Hora_fin = reader.GetString(2),
                        EmpleadoNombre = reader.GetString(3),
                        EmpleadoApellido1 = reader.GetString(4),
                        EmpleadoApellido2 = reader.GetString(5),
                        Capacidad = reader.GetInt32(6)
                    });
                }
                DB_Handler.CerrarConexion();
                return new JsonResult(clasesBuscadas);
            }
          }
          DB_Handler.CerrarConexion();
          return new { message = "No hay clases en este rango de fechas" };

        }catch{
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/CopiarCalendarioActividades")]
      public dynamic CopiarCalendarioActividades(string Id_servicio, string fechaInicio, string fechaFin, string Hora_inicio, string Hora_fin, string Modalidad, string Capacidad, string Cedula_instructor){
        try{ 
          // VERIFICAR EXISTENCIA DE CLASE 
          dynamic existeCalendario = aux.VerificarExistenciaClase_aux(Id_servicio, Cedula_instructor, Modalidad, fechaInicio, Hora_inicio);
          if(!existeCalendario){
            return new { message = "No existe este calendario en la BD" };
          }
          // COPIAR CALENDARIO EN CLASE
          try{
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = @"INSERT INTO CLASE (Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor)
                                   SELECT Id_servicio, DATEADD(WEEK, 1, Fecha), Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor
                                   FROM CLASE WHERE @Fecha_Inicio < Fecha AND Fecha < @Fecha_fin
                                   GROUP BY Id_servicio, Num_clase, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor
                                   ";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Fecha_Inicio", fechaInicio);
              comando.Parameters.AddWithValue("@Fecha_fin", fechaFin);
              comando.ExecuteNonQuery();
            }

            DB_Handler.CerrarConexion();
            return new {message = "ok"};
          }catch{
            return new { message = "error1" };
          }
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error2" };
        }
      }
      
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
              comando.Parameters.AddWithValue("@Num_serie_maquina", Int64.Parse(Numero_serie));
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
      [Route("admin/VerInventario")]
      public dynamic VerInventario(){
        try{
          return aux.VerInventario_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/AgregarInventario")]
      public dynamic AgregarInventario(string numSerie, string marca, string idTipoEquipo){
        try{

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

      [HttpPost]
      [Route("admin/EliminarClase")] 
      public dynamic EliminarClase(string Id_servicio, string cedulaInstructor, string modalidad, string fecha, string horaInicio){
        // VERIFICAR QUE NO EXISTE LA CLASE EN LA BASE DE DATOS
        dynamic existeClase = aux.VerificarExistenciaClase_aux(Id_servicio, cedulaInstructor, modalidad, fecha, horaInicio);
        if(!existeClase){
          return new { message = "No existe esta clase en la BD" };
        }
        // ELIMINAR CLASE EN LA BASE DE DATOS
        try{
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryInsert = "DELETE FROM SERVICIO WHERE Id_servicio = @Id_servicio AND Cedula_instructor = @Cedula_instructor AND Modalidad = @Modalidad AND Fecha = @Fecha AND Hora_inicio = @Hora_inicio";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Id_servicio", Int64.Parse(Id_servicio));
            comando.Parameters.AddWithValue("@Fecha", DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
            comando.Parameters.AddWithValue("@Hora_inicio", TimeSpan.ParseExact(horaInicio, @"hh\:mm\:ss", System.Globalization.CultureInfo.InvariantCulture));
            comando.Parameters.AddWithValue("@Modalidad", modalidad);
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
          string queryInsert = "DELETE FROM SERVICIO WHERE Id_servicio = @Id_servicio";
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

      
      [HttpPost]
      [Route("admin/CrearClase")]
      public dynamic CrearClase(string servicioClase, string cedulaInstructor, string modalidad, string capacidad, string fecha, string horaInicio, string horaFinal){
        try{
            if(string.IsNullOrEmpty(servicioClase) || string.IsNullOrEmpty(modalidad) || string.IsNullOrEmpty(fecha) || string.IsNullOrEmpty(horaInicio) || string.IsNullOrEmpty(horaFinal) || string.IsNullOrEmpty(capacidad) || string.IsNullOrEmpty(cedulaInstructor)){
              return new { message = "error" };}

            // INSERTAR CLASE EN LA BASE DE DATOS
            dynamic existeClase = aux.VerificarExistenciaClase_aux(servicioClase, cedulaInstructor, modalidad, fecha, horaInicio);
            if(existeClase){
              return new { message = "Ya existe esta clase en la BD" };
            }
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO CLASE VALUES (@Id_servicio, @Fecha, @Hora_inicio, @Hora_fin, @Modalidad, @Capacidad,@Cedula_instructor)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Id_servicio", servicioClase);
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
      [Route("admin/VerProductos")]
      public dynamic VerProductos(){
        try{
          return aux.VerProductos_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

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
          string queryDelete = "DELETE FROM VENTA_PRODUCTO WHERE Codigo_barras = @Codigo_barras";
          using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Codigo_barras", Int64.Parse(codigoBarras));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();

          // ELIMINAR DE PRODUCTO
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryDelete = "DELETE FROM PRODUCTO WHERE Codigo_barras = @Codigo_barras";
          using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
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

      [HttpPost]
      [Route("admin/AgregarTipoEquipo")]
      public dynamic AgregarTipoEquipo(string descripcion){
        try{
            if(string.IsNullOrEmpty(descripcion)){
              return new { message = "error" };}

            // INSERTAR TIPO DE EQUIPO EN LA BASE DE DATOS
            dynamic existeTipoEquipo = aux.VerificarExistenciaTipoEquipo_aux(descripcion);
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

      [HttpPost]
      [Route("admin/AgregarEmpleado")]
      public dynamic AgregarEmpleado(string cedula, string nombre, string apellido1, string apellido2, string distrito, string canton, string provincia, string correo, string contrasena, string salario, int id_puesto, int id_planilla, string codigo_suc){
        try{
          if(string.IsNullOrEmpty(cedula) || cedula.Length != 9 ||string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2) || string.IsNullOrEmpty(distrito) || string.IsNullOrEmpty(canton) || string.IsNullOrEmpty(provincia) || string.IsNullOrEmpty(correo) || !correo.Contains('@') || !correo.Contains('.') || string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(salario) || id_puesto == 0 || id_planilla == 0 || string.IsNullOrEmpty(codigo_suc)){
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
            comando.Parameters.AddWithValue("@Id_Puesto", id_puesto);
            comando.Parameters.AddWithValue("@Id_Planilla", id_planilla);
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
                        Identificador = reader.GetInt32(0),
                        Sucursal = reader.GetInt32(1),
                        CedulaEmpleado = reader.GetString(2),
                        Nombre = reader.GetString(3),
                        Primer_apellido = reader.GetString(4),
                        Segundo_apellido = reader.GetString(5),
                        Horas_laboradas = reader.GetString(6),
                        Clases_impartidas = reader.GetString(7),
                        Monto_total = reader.GetString(8)
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

      [HttpPost]
      [Route("admin/AgregarPlanilla")]
        public dynamic AgregarPlanilla(string descripcionPlanilla){
          try{

              // VERIFICACION DE DATOS
              if(string.IsNullOrEmpty(descripcionPlanilla)){
                  return new { message = "error" };}

              // INSERTAR PLANILLA EN LA BASE DE DATOS
              dynamic existePlanilla = aux.VerificarExistenciaPlanilla_aux(descripcionPlanilla);
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

      [HttpPost]
      [Route("admin/EliminarPlanilla")]
      public dynamic EliminarPlanilla(string descripcionPlanilla){
        try{
            // VERIFICACION DE DATOS
            if(string.IsNullOrEmpty(descripcionPlanilla)){
                return new { message = "error" };}

            // ELIMINAR PLANILLA EN LA BASE DE DATOS
            dynamic existePlanilla = aux.VerificarExistenciaPlanilla_aux(descripcionPlanilla);
            if(!existePlanilla){
                return new { message = "No existe esta planilla en la BD" };
            }

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryDelete = "DELETE FROM PLANILLA WHERE Descripcion = @Descripcion";
            using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
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
            string queryDelete = "DELETE FROM PUESTO WHERE Id_puesto = @Id_puesto";
            using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Descripcion", Id_puesto);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }
            return new { message = "ok" };
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }      

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

      [HttpPost]
      [Route("admin/AgregarTratamiento")]
      public dynamic AgregarTratamiento(string nombreTratamiento){
        try{
          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(nombreTratamiento)) {
              return new { message = "error" };}

          // INSERTAR TRATAMIENTO EN LA BASE DE DATOS
          dynamic existeTratamiento = aux.VerificarExistenciaTratamiento_aux(nombreTratamiento);
            if (existeTratamiento) {
                return new { message = "Tratamiento ya existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO TRATAMIENTO (Nombre) VALUES (@Nombre)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Nombre", nombreTratamiento);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }
            return new { message = "ok" };
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }

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

      [HttpGet]
      [Route("admin/VerTratamientosSPA")]
      public dynamic VerTratamientosSPA(){
        try{
          return aux.VerTratamientosSPA_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/AgregarTratamientoSPA")]
      public dynamic AgregarTratamientoSPA(string codigo_sucursal, int idTratamiento){
        try{
          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(codigo_sucursal) || idTratamiento == 0) {
              return new { message = "error" };}

          // INSERTAR TRATAMIENTO_SPA EN LA BASE DE DATOS
          dynamic existeTratamientoSPA = aux.VerificarExistenciaTratamientoSPA_aux(codigo_sucursal, idTratamiento);
            if (existeTratamientoSPA) {
                return new { message = "Tratamiento ya existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO TRATAMIENTO_SPA VALUES (@Codigo, @IdTratamiento)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Codigo", Int64.Parse(codigo_sucursal));
                comando.Parameters.AddWithValue("@IdTratamiento", idTratamiento);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }

          return new { message = "ok" };

        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }



      [HttpPost]
      [Route("admin/EliminarTratamientoSPA")]
      public dynamic EliminarTratamientoSPA(string codigo_sucursal, int idTratamiento){
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
            string queryDelete = "DELETE FROM TRATAMIENTO_SPA WHERE Codigo_sucursal = @Codigo";
            using (SqlCommand comando2 = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
                comando2.Parameters.AddWithValue("@Codigo", Int64.Parse(codigo_sucursal));
                comando2.ExecuteNonQuery();
                Console.WriteLine("Tratamiento eliminado exitosamente");
                DB_Handler.CerrarConexion();
            }

            return aux.VerTratamientosSPA_aux();  // JSON
                  
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("login/SignUpCliente")]
      public dynamic SignUpCliente(string cedula, string nombre, string apellido1, string apellido2, string fechaNacimiento, string peso, string direccion, string correoElectronico, string contrasena) {
          try {              
              // VERIFICACION DE DATOS
              if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre)|| string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2) || string.IsNullOrEmpty(fechaNacimiento)|| string.IsNullOrEmpty(peso) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correoElectronico)|| string.IsNullOrEmpty(contrasena)) {
                  return new { message = "error" };}
              if (cedula.Length != 9 || cedula[0] == '0' || !correoElectronico.Contains("@") || !correoElectronico.Contains(".")) {
                  return new { message = "error" };}

              // SEPARAR FECHA DE NACIMIENTO
              string[] fechaNSeparada = fechaNacimiento.Split("/");
              string mes = fechaNSeparada[0];
              string dia = fechaNSeparada[1];
              string anio = fechaNSeparada[2];

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

      [HttpPost]
      [Route("login/LoginCliente")]
      public dynamic LoginCliente(string cedula, string contrasena){
        try{
          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(contrasena)) {
              return new { message = "error" };}
          if (cedula.Length != 9 || cedula[0] == '0') {
              return new { message = "error" };}

          // VERIFICAR QUE NO SEA UN EMPLEADO
          dynamic esEmpleado = aux.VerificarExistenciaEmpleado_aux(cedula);
          if (esEmpleado) {
             DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string querySelect = "SELECT * FROM EMPLEADO WHERE Cedula = @Cedula AND Contrasena = @Contrasena";
            using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
                comando.Parameters.AddWithValue("@Contrasena", aux.EncriptarContrasenaMD5_aux(contrasena));
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows) {
                    DB_Handler.CerrarConexion();
                    return new { message = "empleado" };
                } else {
                    DB_Handler.CerrarConexion();
                    return new { message = "error" };
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
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows) {
                    DB_Handler.CerrarConexion();
                    return new { message = "cliente" };
                } else {
                    DB_Handler.CerrarConexion();
                    return new { message = "error" };
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