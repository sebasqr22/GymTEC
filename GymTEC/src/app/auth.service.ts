import { Injectable } from '@angular/core';
import {GetApiService} from "./get-api.service";
import {core} from "@angular/compiler";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private api: GetApiService) {}

  login(cedula:number, contrasena:string){
    this.api.call_LoginCliente(cedula.toString(), contrasena).subscribe((data) =>{
      console.log(data);
    })
  }

  register(cedula:string, nombre:string, apellido1:string, apellido2:string, fechaNacimiento:string, peso:number, direccion:string, correo:string, contrasena:string){
    this.api.call_SignUpCliente(cedula, nombre, apellido1, apellido2, fechaNacimiento, peso, direccion, correo, contrasena).subscribe((data) => {
      console.log(data);
    })
  }
}
