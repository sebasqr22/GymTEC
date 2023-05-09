import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GetApiService {

  constructor(private http:HttpClient) {}

  // LOGIN COMPONENT
  call_SignUpCliente(cedula:string, nombre:string, apellido1:string, apellido2:string, fechaNacimiento:string, peso:number, direccion:string, correo:string, contrasena:string){
    return this.http.post(`https://localhost:7194/usuarios/login/SignUpCliente?cedula=${cedula}&nombre=${nombre}&apellido1=${apellido1}&apellido2=${apellido2}&fechaNacimiento=${fechaNacimiento}&peso=${peso}&direccion=${direccion}&correoElectronico=${correo}&contrasena=${contrasena}`, {});
  }

  call_LoginCliente(cedula:string, contrasena:string){
    return this.http.post(`https://localhost:7194/usuarios/login/Login?cedula=${cedula}&contrasena=${contrasena}`, {});
  }


  //ADMIN COMPONENT

  // AgregarTratamiento(string nombreTratamiento)
  call_AgregarTratamiento(nombreTratamiento:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarTratamiento?nombreTratamiento=${nombreTratamiento}`, {});
  }

  // EliminarTratamiento(string idTratamiento)
  call_EliminarTratamiento(idTratamiento:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarTratamiento?idTratamiento=${idTratamiento}`, {});
  }

  call_VerTratamientos(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTratamientos`);
  }

  call_VerPuestos(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerPuestos`);
  }

  call_AgregarPuesto(descripcionPuesto:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarPuesto?descripcionPuesto=${descripcionPuesto}`, {});
  }

  //EliminarPuesto(string Id_puesto){
  call_EliminarPuesto(idPuesto:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarPuesto?Id_puesto=${idPuesto}`, {});
  }

  call_VerPlanillas(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerPlanillas`);
  }

  call_AgregarPlanilla(descripcionPlanilla:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarPlanilla?descripcionPlanilla=${descripcionPlanilla}`, {});
  }

  call_EliminarPlanilla(idPlanilla:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarPlanilla?idPlanilla=${idPlanilla}`, {});
  }

  call_VerEmpleados(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerEmpleados`);
  }

  call_AgregarEmpleados(cedula:string, nombre:string, apellido1:string, apellido2:string, distrito:string, canton:string, provincia:string, correo:string, contrasena:string, salario:string, id_puesto:number, id_planilla:number, nombre_suc:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarEmpleado?cedula=${cedula}&nombre=${nombre}&apellido1=${apellido1}&apellido2=${apellido2}&distrito=${distrito}&canton=${canton}&provincia=${provincia}&correo=${correo}&contrasena=${contrasena}&salario=${salario}&id_puesto=${id_puesto}&id_planilla=${id_planilla}&codigo_suc=${nombre_suc}`, {});
  }

  call_EliminarEmpleados(cedula:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarEmpleado?cedula=${cedula}`, {});
  }

  call_VerTiposEquipo(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTiposEquipo`);
  }

  call_AgregarTipoEquipo(descripcionTipoEquipo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarTipoEquipo?descripcion=${descripcionTipoEquipo}`, {});
  }

  // EliminarTipoEquipo(string idTipoEquipo)
  call_EliminarTipoEquipo(idTipoEquipo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarTipoEquipo?idTipoEquipo=${idTipoEquipo}`, {});
  }

  // ActivarSPA(string codigo_sucursal)
  activarSpa(codigoSucursal:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/ActivarSPA?codigo_sucursal=${codigoSucursal}`, {});
  }

  // ActivarTienda(string codigo_sucursal)
  activarTienda(codigo_sucursal:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/ActivarTienda?codigo_sucursal=${codigo_sucursal}`, {});
  }

  verProductos(codigo_gym:string){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerProductos?codigo_gym=${codigo_gym}`);
  }

  agregarProductos(codigoBarras:string, nombreProducto:string, Descripcion:string, costo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarProducto?codigoBarras=${codigoBarras}&nombreProducto=${nombreProducto}&Descripcion=${Descripcion}&costo=${costo}`, {})
  }

  eliminarProductos(codigoBarras:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarProducto?codigoBarras=${codigoBarras}`, {})
  }

  crearClase(Id_servicio:string, cedulaInstructor:string, modalidad:string, capacidad:string, fecha:string, horaInicio:string, horaFinal:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/CrearClase?idServicio=${Id_servicio}&cedulaInstructor=${cedulaInstructor}&modalidad=${modalidad}&capacidad=${capacidad}&fecha=${fecha}&horaInicio=${horaInicio}&horaFinal=${horaFinal}`, {})
  }

  eliminarClase(Id_servicio:string, cedulaInstructor:string, modalidad:string, fecha:string, horaInicio:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarClase?Id_servicio=${Id_servicio}&cedulaInstructor=${cedulaInstructor}&modalidad=${modalidad}&fecha=${fecha}&horaInicio=${horaInicio}`, {});
  }

  verClases(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerClases`);
  }

  verClasesConCupo(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerClasesConCupo`);
  }

  //AgregarInventario(string numSerie, string marca, string idTipoEquipo){
  agregarInventario(numSerie:string, marca:string, idTipoEquipo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarInventario?numSerie=${numSerie}&marca=${marca}&idTipoEquipo=${idTipoEquipo}`, {})
  }

  verInventario(codigoSucursal:string){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerInventario?codigo_suc=${codigoSucursal}`);
  }

  eliminarInventario(Numero_serie:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarInventario?Numero_serie=${Numero_serie}`, {})
  }


  copiarCalendarioActividades(fechaInicio:string, fechaFin:string, semanasAMover:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/CopiarCalendarioActividades?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}&semanasAMover=${semanasAMover}`, {}) //VERIFICAR QUE LA COPIA ESTE BIEN
  }

  // string Codigo_sucursal, string idServicio
  asociarServicioASucursal(codigoSucursal:string, idServicio:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AsociarServiciosASucursal?codigoSucursal=${codigoSucursal}&idServicio=${idServicio}`, {})
  }

  //AsociarTratamientosASPA(string Codigo_sucursal, string Id_tratamiento){
  asociarTratamientoASpa(Codigo_sucursal:string, idTratamiento:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AsociarTratamientoASPA?Codigo_sucursal=${Codigo_sucursal}&Id_tratamiento=${idTratamiento}`, {})
  }

  //AgregarSucursal(string Nombre, string Distrito, string Canton, string Provincia, string Fecha_apertura, string Hora_apertura, string Hora_cierre, string Max_capacidad, string Cedula_administrador){
  agregarSucursal( Codigo_sucursal:string, Nombre:string, Distrito:string, Canton:string, Provincia:string, Fecha_apertura:string, Hora_apertura:string, Hora_cierre:string, Max_capacidad:string, Cedula_administrador:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarSucursal?Codigo_sucursal=${Codigo_sucursal}&Nombre=${Nombre}&Distrito=${Distrito}&Canton=${Canton}&Provincia=${Provincia}&Fecha_apertura=${Fecha_apertura}&Hora_apertura=${Hora_apertura}&Hora_cierre=${Hora_cierre}&Max_capacidad=${Max_capacidad}&Cedula_administrador=${Cedula_administrador}`, {})
  }

  //EliminarSucursal(string codigo_suc){
  eliminarSucursal(codigoSucursal:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarSucursal?codigo_suc=${codigoSucursal}`, {})
  }

  verSucursales(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerSucursal`)
  }

  //AsociarInventario(string Codigo_sucursal, string num_serie, string costo) {
  asociarInventario(codigoSucursal:string, num_serie:string, costo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AsociarInventario?Codigo_sucursal=${codigoSucursal}&num_serie=${num_serie}&costo=${costo}`, {})
  }

//AsociarProductosATienda(string Codigo_sucursal, string Codigo_producto){
  asociarProductosATienda(codigoSucursal:string, codigoProducto:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AsociarProductosATienda?Codigo_sucursal=${codigoSucursal}&Codigo_producto=${codigoProducto}`, {})
  }

  //CopiarGimnasio(string new_gym, string copied_gym) {
  copiarGimnasio(gym_nuevo:string, gym_viejo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/CopiarGimnasio?new_gym=${gym_nuevo}&copied_gym=${gym_viejo}`, {})
  }


  eliminarServicio(Id_servicio:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarServicio?Id_servicio=${Id_servicio}`, {});
  }

  // antes decia VerSericios, fue un typo
  verServicios(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerServicios`)
  }

  agregarServicios(descripcion:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarServicio?descripcion=${descripcion}`, {})
  }

  eliminarServicios(idServicio:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarServicios?idServicio=${idServicio}`, {})
  }



  generarPlanillasTodos(){
    return this.http.get(`https://localhost:7194/usuarios/admin/GenerarPlanillasTodos`, {})
  }

  verTratamientos(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTratamientos`, {})
  }

  agregarTratamiento(nombreTratamiento:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarTratamiento?descripcionTratamiento=${nombreTratamiento}`, {})
  }



  //VerClienteEspecifico(string cedula){
  buscarCliente(cedula:string){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerClienteEspecifico?cedula=${cedula}`, {})
  }

  registrarClienteEnClase(cedulaClient:string, Num_clase:string){
    return this.http.post(`https://localhost:7194/usuarios/cliente/RegistrarClienteEnClase?cedulaClient=${cedulaClient}&Num_clase=${Num_clase}`, {})
  }

  //BuscarClase(string Codigo_sucursal,string Id_servicio, string fechaInicio, string fecha_fin){
  buscarClase(codigoSucursal:string, Id_servicio:string, fechaInicio:string, fecha_fin:string){
    return this.http.get(`https://localhost:7194/usuarios/admin/BuscarClase?codigoSucursal=${codigoSucursal}&Id_servicio=${Id_servicio}&fechaInicio=${fechaInicio}&fecha_fin=${fecha_fin}`, {})
  }

  //BuscarClasePorSucursal(string Codigo_sucursal){
  buscarClasePorSucursal(codigoSucursal:string){
      return this.http.get(`https://localhost:7194/usuarios/admin/BuscarClasePorSucursal?codigoSucursal=${codigoSucursal}`, {})
  }

  //BuscarClasePorServicio(string Id_servicio){
  buscarClasePorServicio(Id_servicio:string){
      return this.http.get(`https://localhost:7194/usuarios/admin/BuscarClasePorServicio?Id_servicio=${Id_servicio}`, {})
  }

  //BuscarClasePorPeriodos(string fechaInicio, string fecha_fin){
  buscarClasePorPeriodos(fechaInicio:string, fecha_fin:string){
    return this.http.get(`https://localhost:7194/usuarios/admin/BuscarClasePorPeriodos?fechaInicio=${fechaInicio}&fecha_fin=${fecha_fin}`, {})
  }

  verTotalidadInventario(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTotalidadInventario`)
  }

  verTotalidadProductos(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTotalidadProductos`)
  }

  verTratamientosAsociados(codigo_sucursal:string){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTratamientosAsociados?codigo_sucursal=${codigo_sucursal}`, {})
  }

  verTotalidadSpa(){
    //return this.http.get(`https://localhost:7194/usuarios/admin/VerTotalidadProductos`)
  }
}
