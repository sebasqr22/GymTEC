import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GetApiService {

  constructor(private http:HttpClient) {}

  // LOGIN COMPONENT
  call_SignUpCliente(cedula:number, nombre:string, apellido1:string, apellido2:string, fechaNacimiento:string, peso:number, direccion:string, correo:string, contrasena:string){
    return this.http.post(`https://localhost:7194/usuarios/cliente/SignUpCliente?cedula=${cedula}&nombre=${nombre}&apellido1=${apellido1}&apellido2=${apellido2}&fechaNacimiento=${fechaNacimiento}&peso=${peso}&direccion=${direccion}&correoElectronico=${correo}&contrasena=${contrasena}`, {});
  }

  call_LoginClient(cedula:number, contrasena:string){
    return this.http.get(`https://localhost:7194/usuarios/cliente/LoginCliente?cedula=${cedula}&contrasena=${contrasena}`);
  }

  //ADMIN COMPONENT
  call_AgregarTratamientoSPA(nombreSucursal:string, numSpa: number){
    return this.http.post(`https://localhost:7194/usuarios/cliente/AgregarTratamientoSPA?nombreSucursal=${nombreSucursal}&numSpa=${numSpa}`, {});
}
  call_EliminarTratamientoSPA(nombreSucursal:string, numSpa: number){
    return this.http.get(`https://localhost:7194/usuarios/cliente/EliminarTratamientoSPA?nombreSucursal=${nombreSucursal}&numSpa=${numSpa}`);
  }



}
