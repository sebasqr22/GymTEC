import { Component } from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  nombre = 'Sebas';
  tipo = 'SPA';
  dropdown = 0;

  mostrar(idPrincipal:string, idDrop:string){
    const principal = document.getElementById(idPrincipal) as HTMLInputElement
    const drop = document.getElementById(idDrop) as HTMLInputElement

    if(principal.getAttribute('aria-expanded') === 'true'){
      principal.className = 'nav-link collapsed';
      principal.setAttribute('aria-expanded', 'false')
      drop.className = 'collapse'
    }
    else{ //se debe desplegar el dropdown
      principal.className = 'nav-link';
      principal.setAttribute('aria-expanded', 'true')
      drop.className = 'collapse show'
    }
  }
mostrarDropdown(){
  const drop = document.getElementById("dropdown") as HTMLInputElement
  if(this.dropdown === 0){
    drop.className = "dropdown-menu dropdown-menu-right shadow animated--grow-in show"
    this.dropdown = 1
  }
  else{
    drop.className = "dropdown-menu dropdown-menu-right shadow animated--grow-in"
    this.dropdown = 0
  }
}
}
