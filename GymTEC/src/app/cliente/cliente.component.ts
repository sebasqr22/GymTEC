import {Component, Renderer2, ElementRef, OnInit} from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { GetApiService } from '../get-api.service';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit{
  constructor(private renderer: Renderer2, private el: ElementRef, private auth: AuthService,private router:Router,private route: ActivatedRoute, private api:GetApiService ) { }

  dropdown = 0

  nombre = "Sebatian Quesada"
  activo = 'principal'
  cedula = ""
  ngOnInit() {
    const busqueda = document.getElementById("busqueda") as HTMLInputElement
    const registro = document.getElementById("registro") as HTMLInputElement
    busqueda.style.display = 'none'
    registro.style.display = 'none'

    //this.auth.capaSeguridad(this.extraerCedula())
    this.funcionesPrimeras();
    //@ts-ignore
    this.cedula = localStorage.getItem("cedula");
    //@ts-ignore
    this.nombre = localStorage.getItem("nombre");
  }
  
  extraerCedula():string{
    var cedula = ""
    this.route.params.subscribe(params => {
      cedula = params['cedula'];
    })
    return cedula;
  }


  desbloquear(vista:string){
    const activo = document.getElementById(this.activo) as HTMLInputElement
    activo.style.display = 'none'
    //buscamos el nuevo
    const nuevo = document.getElementById(vista) as HTMLInputElement
    nuevo.style.display = 'block'
    this.activo = vista
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

  cambiarPantalla(pantalla:string){
    this.desbloquear(pantalla)
  }

  clase(tipo:string){
    this.cambiarPantalla('registro')
    const titulo = document.getElementById('registroName') as HTMLInputElement
    titulo.innerHTML = `Registro de ${tipo}`

  }

  crearCartaClase(tipo:string, src:string, caption:string){
    const carta = document.createElement('figure')
    carta.className = 'cartaClase'
    carta.addEventListener("click", () => {
      this.clase(tipo);
    });
    const imagen = document.createElement('img');
    imagen.src = src
    carta.appendChild(imagen)

    const figcap = document.createElement('figcaption')
    figcap.innerHTML = caption
    carta.appendChild(figcap)

    return carta
  }

  buscarClases(){
    const inicio = document.getElementById('fechaInicio') as HTMLInputElement
    const final = document.getElementById('fechaFinaliza') as HTMLInputElement
    const sedes = document.getElementById('sede') as HTMLInputElement

    if(inicio && final && sedes){
      const claseCartas = document.getElementById('clasesCartas') as HTMLInputElement
      claseCartas.innerHTML = ''
      const fi = inicio.value
      const ff = final.value
      const sede = sedes.value
      if(fi != "" && ff != ""){
        const div = document.getElementById('clasesCartas') as HTMLInputElement
        div.appendChild(this.crearCartaClase('natacion', 'https://img.freepik.com/premium-vector/continuous-line-drawing-young-professional-swimmer-practicing-swimming-indoor-pool-sports_500861-500.jpg', 'Natacion'))
        div.appendChild(this.crearCartaClase('pilates', 'https://t3.ftcdn.net/jpg/03/72/43/12/360_F_372431283_rhVo7VWm2Fmgf2K0CBo99cbZuvfoCdau.jpg', 'Pilates'))
      }
      else{
        alert("Los datos de Fecha de Inicio, Fecha de Finalización y Sede no deben estar vacíos!!")
      }
    }
  }

  funcionesPrimeras(){
    this.api.verClasesConCupo().subscribe((data) => {
      const llegada = JSON.parse(JSON.stringify(data));
      console.log(llegada)
      const tmp = document.getElementById("registroAClase") as HTMLInputElement;
      for(const i in llegada){
        const opcion = document.createElement('option');
        opcion.value = llegada[i].num_clase;
        opcion.textContent = llegada[i].num_clase;
        tmp.appendChild(opcion);
      }
    })
  }

  registrar(){
    const num_clase = document.getElementById("registroAClase") as HTMLInputElement;
    this.api.registrarClienteEnClase(this.cedula, num_clase.value).subscribe((data) => {
      const llegada = JSON.parse(JSON.stringify(data));
      alert(llegada.message)
    })
  }

  cargarDatos(data:any){
    const cargar = document.getElementById("") as HTMLInputElement;
    cargar.value = data

  }

  porSucursal(){
    const info = document.getElementById("busquedasede") as HTMLInputElement;
    this.api.buscarClasePorSucursal(info.value).subscribe((data) => {
      const llegada = JSON.parse(JSON.stringify(data));
      alert("Ver Consola")
      console.log(llegada)
      //this.cargarDatos(llegada)
    })
  }

  porPeriodo(){
    const info2 = document.getElementById("busquedafechaInicio") as HTMLInputElement;
    const info3 = document.getElementById("busquedafechaFinaliza") as HTMLInputElement;

    this.api.buscarClasePorPeriodos(info2.value, info3.value).subscribe((data) => {
      const llegada = JSON.parse(JSON.stringify(data));
      alert("Ver Consola")
      console.log(llegada)
    })
  }

  porTipoClase(){
    const info2 = document.getElementById("busquedatipoDeClase") as HTMLInputElement;

    this.api.buscarClasePorServicio(info2.value).subscribe((data) => {
      const llegada = JSON.parse(JSON.stringify(data));
      alert("Ver Consola")
      console.log(llegada)
    })
  }

  logout(){
    alert("logout")
  }
}
