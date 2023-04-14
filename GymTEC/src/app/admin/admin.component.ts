import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit{
  nombre = 'Sebas';
  tipo = 'SPA';
  dropdown = 0;

  pantallaActual = 'principal'

  pantallas = ['gestSucSpa', 'gestSucTienda', 'gestTratSpaP', 'gestPuest', 'gestTipPlaP', 'gestEmplP', 'gestServP', 'gestTipEquipP'
    , 'gestInvetP', 'gestProductP', 'confGymPSpa' , 'confGymPProduc' , 'confGymPInventario' , 'confGymPCrear', 'genPlanPComo' , 'genPlanPMensuales' ,
    'genPlanPHoras' , 'genPlanPClase', 'copCalenP', 'copGymp']


  ngOnInit() {
    for (let i = 0; i < this.pantallas.length; i++) {
      const tmp = document.getElementById(this.pantallas[i]) as HTMLInputElement
      tmp.style.display = 'none'
    }
  }

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

  mostrarPantalla(pantalla:string){
    const act = document.getElementById(this.pantallaActual) as HTMLInputElement
    act.style.display = 'none'

    const tmp = document.getElementById(pantalla) as HTMLInputElement
    tmp.style.display = 'block'

    this.pantallaActual = tmp.id
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
