import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GetApiService {

  constructor(private http:HttpClient) {}
  

   call_EliminarTratamientoSPA(){
     return this.http.get("https://localhost:7194/usuarios/cliente/EliminarTratamientoSPA");
   }

   call_AgregarTratamientoSPA(){
      return this.http.post("https://localhost:7194/usuarios/cliente/AgregarTratamientoSPA", {});
  }

  call_RegistrarCliente(){
    return this.http.post("https://localhost:7194/usuarios/cliente/RegistrarCliente", {});
  }

}
