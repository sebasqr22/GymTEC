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
    return this.http.post(`https://localhost:7194/usuarios/login/LoginCliente?cedula=${cedula}&contrasena=${contrasena}`, {});
  }

  //ADMIN COMPONENT
  call_VerTratamientoSPA(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTratamientoSPA`);
  }
  call_AgregarTratamientoSPA(nombreSucursal:string, numSpa: number){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarTratamientoSPA?nombreSucursal=${nombreSucursal}&numSpa=${numSpa}`, {});
  }

  call_EliminarTratamientoSPA(nombreSucursal:string, numSpa: number){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarTratamientoSPA?nombreSucursal=${nombreSucursal}&numSpa=${numSpa}`, {});
  }

  call_VerPuestos(descripcionPuesto:string){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerPuestos?descripcionPuesto=${descripcionPuesto}`);
  }

  call_AgregarPuesto(descripcionPuesto:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarPuesto?descripcionPuesto=${descripcionPuesto}`, {});
  }

  call_EliminarPuesto(descripcionPuesto:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarPuesto?descripcionPuesto=${descripcionPuesto}`, {});
  }

  call_VerPlanillas(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerPlanillas`);
  }

  call_AgregarPlanilla(descripcionPlanilla:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarPlanilla?descripcionPlanilla=${descripcionPlanilla}`, {});
  }

  call_EliminarPlanilla(descripcionPlanilla:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarPlanilla?descripcionPlanilla=${descripcionPlanilla}`, {});
  }

  call_VerEmpleados(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerEmpleados`);
  }

  call_AgregarEmpleados(cedula:string, nombre:string, apellido1:string, apellido2:string, distrito:string, canton:string, provincia:string, correo:string, contrasena:string, salario:string, id_puesto:number, id_planilla:number, nombre_suc:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarEmpleados?cedula=${cedula}&nombre=${nombre}&apellido1=${apellido1}&apellido2=${apellido2}&distrito=${distrito}&canton=${canton}&provincia=${provincia}&correo=${correo}&contrasena=${contrasena}&salario=${salario}&id_puesto=${id_puesto}&id_planilla=${id_planilla}&nombre_suc=${nombre_suc}`, {});
  }

  call_EliminarEmpleados(cedula:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarEmpleados?cedula=${cedula}`, {});
  }

  call_VerTiposEquipo(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTiposEquipo`);
  }

  call_AgregarTipoEquipo(descripcionTipoEquipo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarTipoEquipo?descripcionTipoEquipo=${descripcionTipoEquipo}`, {});
  }

  call_EliminarTipoEquipo(descripcionTipoEquipo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarTipoEquipo?descripcionTipoEquipo=${descripcionTipoEquipo}`, {});
  }

  agregarProductos(codigoBarras:string, nombreProducto:string, Descripcion:string, costo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarProducto?codigoBarras=${codigoBarras}&nombreProducto=${nombreProducto}&Descripcion=${Descripcion}&costo=${costo}`, {})
  }  activarSpa(nombreSucursal:string, numSpa:number){
    return this.http.post(`https://localhost:7194/usuarios/admin/ActivarSPA?nombreSucursal=${nombreSucursal}&numSpa=${numSpa}`, {});
  }

  activarTienda(nombreSucursal:string, numTienda:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/ActivarTienda?nombreSucursal=${nombreSucursal}&numTienda=${numTienda}`, {});
  }

  verProductos(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerProductos`);
  }

  eliminarProductos(codigoBarras:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarProducto?codigoBarras=${codigoBarras}`, {})
  }

  crearClase(servicioClase:string, cedulaInstructor:string, modalidad:string, capacidad:string, fecha:string, horaInicio:string, horaFinal:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/CrearClase?servicioClase=${servicioClase}&cedulaInstructor=${cedulaInstructor}&modalidad=${modalidad}&capacidad=${capacidad}&fecha=${fecha}&horaInicio=${horaInicio}&horaFinal=${horaFinal}`, {})
  }
  
  eliminarClase(Id_servicio:string, cedulaInstructor:string, modalidad:string, fecha:string, horaInicio:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarClase?Id_servicio=${Id_servicio}&cedulaInstructor=${cedulaInstructor}&modalidad=${modalidad}&fecha=${fecha}&horaInicio=${horaInicio}`, {});
  }

  verClases(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerClases`);
  }

  agregarInventario(numSerie:string, marca:string, nombreSucursal:string,  idTipoEquipo:string,  descripcion:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarInventario?numSerie=${numSerie}&marca=${marca}&nombreSucursal=${nombreSucursal}&idTipoEquipo=${idTipoEquipo}&descripcion=${descripcion}`, {})
  }

  verInventario(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerInventario`);
  }

  eliminarInventario(Numero_serie:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarInventario?Numero_serie=${Numero_serie}`, {})
  }


  copiarCalendarioActividades(Id_servicio:string, fechaInicio:string, fechaFin:string, Hora_inicio:string, Hora_fin:string, Modalidad:string, Capacidad:string, Cedula_instructor:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/CopiarCalendarioActividades?Id_servicio=${Id_servicio}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}&Hora_inicio=${Hora_inicio}&Hora_fin=${Hora_fin}&Modalidad=${Modalidad}&Capacidad=${Capacidad}&Cedula_instructor=${Cedula_instructor}&`, {})
  }

  asociarServicioSpaASucursal(nombreSucursal:string, idServicio:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AsociarServiciosASucursal?nombreSucursal=${nombreSucursal}&idServicio=${idServicio}`, {})
  }

  asociarTratamientoASpa(numSpa:string, nombreSucursal:string, idTratamiento:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AsociarTratamientoASpa?numSpa=${numSpa}&nombreSucursal=${nombreSucursal}&idTratamiento=${idTratamiento}`, {})
  }

  agregarSucursal(Nombre:string, Distrito:string, Provincia:string, Fecha_apertura:string, Hora_apertura:string, Hora_cierre:string, Max_capacidad:string, Cedula_administrador:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarSucursal?Nombre=${Nombre}&Distrito=${Distrito}&Provincia=${Provincia}&Fecha_apertura=${Fecha_apertura}&Hora_apertura=${Hora_apertura}&Hora_cierre=${Hora_cierre}&Max_capacidad=${Max_capacidad}&Cedula_administrador=${Cedula_administrador}&`, {})
  }

  eliminarSucursal(nombreSucursal:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarSucursal?nombreSucursal=${nombreSucursal}`, {})
  }

  verSucursales(){
    return this.http.post(`https://localhost:7194/usuarios/admin/VerSucursal`, {})
  }

  asociarInventario(nombre_sucursal:string, num_serie:string, costo:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AsociarInventario?nombre_sucursal=${nombre_sucursal}&num_serie=${num_serie}&costo=${costo}`, {})
  }

  asociarProductosATienda(nombreSucursal:string, numTienda:string, codigoProducto:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AsociarProductosATienda?nombreSucursal=${nombreSucursal}&numTienda=${numTienda}&codigoProducto=${codigoProducto}`, {})
  }

  copiarGimnasio(gym_nuevo:string, gym_viejo:string, num_spa:string, num_tienda:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/CopiarGimnasio?gym_nuevo=${gym_nuevo}&gym_viejo=${gym_viejo}&num_spa=${num_spa}&num_tienda=${num_tienda}`, {});
  }

  eliminarServicio(Id_servicio:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarServicio?Id_servicio=${Id_servicio}`, {});
  }

  verSericios(){
    return this.http.post(`https://localhost:7194/usuarios/admin/EliminarServicios`, {})
  }

  generarPlanillasTodos(){
    return this.http.get(`https://localhost:7194/usuarios/admin/GenerarPlanillasTodos`, {})
  }

  verTratamientos(){
    return this.http.get(`https://localhost:7194/usuarios/admin/VerTratamientos`, {})
  }

  agregarTratamiento(nombreTratamiento:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/AgregarTratamiento?nombreTratamiento=${nombreTratamiento}`, {})
  }

  buscarCliente(){
    //falta la ruta para este metodo
    //return this.http.get()
  }

  registrarClienteEnClase(cedulaClient:string, Num_clase:string, Id_servicio:string, Fecha:string, Hora_inicio:string, Modalidad:string, Cedula_instructor:string){
    return this.http.post(`https://localhost:7194/usuarios/admin/CopiarGRegistrarClienteEnClaseimnasio?cedulaClient=${cedulaClient}&Num_clase=${Num_clase}&Id_servicio=${Id_servicio}&Fecha=${Fecha}&Hora_inicio=${Hora_inicio}&Modalidad=${Modalidad}&Cedula_instructor=${Cedula_instructor}`, {})
  }

  buscarClase(Nombre_sucursal:string, Id_servicio:string, fechaInicio:string, fecha_fin:string){
    return this.http.get(`https://localhost:7194/usuarios/admin/BuscarClase?Nombre_sucursal=${Nombre_sucursal}&Id_servicio=${Id_servicio}&fechaInicio=${fechaInicio}&fecha_fin=${fecha_fin}`, {})
  }
}
