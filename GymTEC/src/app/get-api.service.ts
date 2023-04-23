import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GetApiService {

  constructor(private http:HttpClient) {}

  // LOGIN COMPONENT
  call_SignUpCliente(cedula:number, nombre:string, apellido1:string, apellido2:string, fechaNacimiento:string, peso:number, direccion:string, correo:string, contrasena:string){
    return this.http.post(`https://localhost:7194/usuarios/login/SignUpCliente?cedula=${cedula}&nombre=${nombre}&apellido1=${apellido1}&apellido2=${apellido2}&fechaNacimiento=${fechaNacimiento}&peso=${peso}&direccion=${direccion}&correoElectronico=${correo}&contrasena=${contrasena}`, {});
  }

  call_LoginCliente(cedula:number, contrasena:string){
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


}
