import {Component, OnInit} from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  mostrar = false
  constructor(private auth:AuthService) { }

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
    this.auth.login(parseInt(valor.cedula, 10), valor.password);
  }

  register(form:any){
    const valor = form.value;
    // @ts-ignore
    this.auth.register(parseInt(valor.cedula, 10), valor.nombre, valor.apellido1, valor.apellido2, valor.fechaNacimiento, parseInt(valor.peso, 10), valor.direccion, valor.email, valor.password);
  }
}
