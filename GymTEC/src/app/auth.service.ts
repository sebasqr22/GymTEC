import { Injectable } from '@angular/core';
import {GetApiService} from "./get-api.service";
import {core} from "@angular/compiler";
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private api: GetApiService, private router:Router, private route: ActivatedRoute) {}

  login(cedula:number, contrasena:string){
    this.api.call_LoginCliente(cedula.toString(), contrasena).subscribe((data) =>{
      if(data === "ok"){
        localStorage.setItem('userCedula', cedula.toString());
      }
    })
  }

  register(cedula:string, nombre:string, apellido1:string, apellido2:string, fechaNacimiento:string, peso:number, direccion:string, correo:string, contrasena:string){
    this.api.call_SignUpCliente(cedula, nombre, apellido1, apellido2, fechaNacimiento, peso, direccion, correo, contrasena).subscribe((data) => {
      console.log(data);
    })
  }

  // verTratamientosSPA(){
  //   this.api.call_VerTratamientoSPA().subscribe((data) =>{
  //     console.log(data);
  //   })
  // }
  //
  // agregarTratamientosSPA(nombreSucursal:string, numSpa: number){
  //   this.api.call_AgregarTratamientoSPA(nombreSucursal, numSpa).subscribe((data) => {
  //     console.log(data);
  //   })
  // }
  //
  // eliminarTratamientosSPA(nombreSucursal:string, numSpa: number){
  //   this.api.call_EliminarTratamientoSPA(nombreSucursal, numSpa).subscribe((data) => {
  //     console.log(data);
  //   })
  // }

  verPuestos(descripcionPuesto:string){
    this.api.call_VerPuestos(descripcionPuesto).subscribe((data)=>{
      console.log(data);
    })
  }

  agregarPuesto(descripcionPuesto:string){
    this.api.call_AgregarPuesto(descripcionPuesto).subscribe((data)=>{
      console.log(data);
    })
  }


  eliminarPuesto(descripcionPuesto:string){
    this.api.call_EliminarPuesto(descripcionPuesto).subscribe((data)=>{
      console.log(data);
    })
  }

  verPlanillas(){
    this.api.call_VerPlanillas().subscribe((data)=>{
      console.log(data);
    })
  }

  agregarPlanilla(descripcionPlanilla:string){
    this.api.call_AgregarPlanilla(descripcionPlanilla).subscribe((data)=>{
      console.log(data);
    })
  }

  eliminarPlanilla(descripcionPlanilla:string){
    this.api.call_EliminarPlanilla(descripcionPlanilla).subscribe((data)=>{
      console.log(data);
    })
  }

  verEmpleados(){
    this.api.call_VerEmpleados().subscribe((data)=>{
      console.log(data);
    })
  }

  agregarEmpleados(cedula:string, nombre:string, apellido1:string, apellido2:string, distrito:string, canton:string, provincia:string, correo:string, contrasena:string, salario:string, id_puesto:number, id_planilla:number, nombre_suc:string){
    this.api.call_AgregarEmpleados(cedula, nombre, apellido1, apellido2, distrito, canton, provincia, correo, contrasena, salario, id_puesto, id_planilla, nombre_suc).subscribe((data) =>{
      console.log(data);
    })
  }

  eliminarEmpleados(cedula:string){
    this.api.call_EliminarEmpleados(cedula).subscribe((data) =>{
      console.log(data);
    })
  }

  verTiposEquipos(){
    this.api.call_VerTiposEquipo().subscribe((data) =>{
      console.log(data);
    })
  }

  agregarTipoEquipo(descripcionTipoEquipo:string){
    this.api.call_AgregarTipoEquipo(descripcionTipoEquipo).subscribe((data) =>{
      console.log(data);
    })
  }

  eliminarTipoEquipo(descripcionTipoEquipo:string){
    this.api.call_EliminarTipoEquipo(descripcionTipoEquipo).subscribe((data) =>{
      console.log(data);
    })
  }

  capaSeguridad(cedula:string){
    const cedulaGuardada = localStorage.getItem('userCedula');

    if(cedula === cedulaGuardada){
      return 'ok';
    }
    else{
      return 'error';
    }
  }

}
