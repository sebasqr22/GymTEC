import {Component, OnInit} from '@angular/core';
import { AuthService } from '../auth.service';
import { GetApiService } from '../get-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  mostrar = false
  constructor(private auth:AuthService, private api:GetApiService, private router:Router) { }

  ngOnInit() {
    const signin = document.getElementById("signin") as HTMLInputElement
    signin.style.display = 'none'

  }

  mostrarContra(){
    const input = document.getElementById('floatingPassword') as HTMLInputElement
    if(this.mostrar){
      input.type = 'password'
    }
    else{
      input.type = 'text'
    }
    this.mostrar = !this.mostrar
  }

  mostrarContra2(){
    const input = document.getElementById('floatingPassword1') as HTMLInputElement
    if(this.mostrar){
      input.type = 'password'
    }
    else{
      input.type = 'text'
    }
    this.mostrar = !this.mostrar
  }

  cambiarPantalla(type:number, event:Event){
    event.preventDefault();
    const signin = document.getElementById("signin") as HTMLInputElement
    const login = document.getElementById("login") as HTMLInputElement

    if(type === 0){
      login.style.display = 'none'
      signin.style.display = 'block'
      console.log("a")
    }
    else{
      signin.style.display = 'none'
      login.style.display = 'block'


      /** this.getApi.call_RegistrarCliente(login.value.ced, string nombre, string apellido1, string apellido2, ).subscribe((data)=>{
        console.log(data);
      });*/
    }
  }
  login(form:any){
    const valor = form.value;
    //call_LoginCliente(parseInt(valor.cedula, 10), valor.password)
    this.api.call_LoginCliente(valor.cedula, valor.password).subscribe((data) => {
      const llegada = JSON.parse(JSON.stringify(data));
      if(llegada.message == "cliente"){
        this.router.navigate(['/cliente'])
      }

    })
  }

  register(form:any){
    const valor = form.value;
    console.log(valor)
    // @ts-ignore
    //this.auth.register(parseInt(valor.cedula, 10), valor.nombre, valor.apellido1, valor.apellido2, valor.fechaNacimiento, parseInt(valor.peso, 10), valor.direccion, valor.email, valor.password);
    this.api.call_SignUpCliente(valor.cedula.toString(), valor.nombre, valor.apellido1, valor.apellido2, valor.fechaNacimiento, valor.peso, valor.direccion, valor.email, valor.password).subscribe((data) => {
      const llegada = JSON.parse(JSON.stringify(data));
      if(llegada.message == "ok"){
        this.router.navigate(['/cliente'])
      }
    })
  }
}
