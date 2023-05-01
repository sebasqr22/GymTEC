import {Component, OnInit} from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit{
  constructor(private auth:AuthService) { }
  nombre = 'Sebas';
  tipo = 'SPA';
  dropdown = 0;

  pantallaActual = 'principal'

  pantallas = ['gestSucSpa', 'gestSucTienda', 'gestTratSpaP', 'gestTipPlaP', 'gestEmplP', 'gestServP', 'gestTipEquipP'
    , 'gestInvetP', 'gestProductP', 'confGymPSpa' , 'confGymPProduc' , 'confGymPInventario' , 'confGymPCrear', 'genPlanPComo' , 'genPlanPMensuales' ,
    'genPlanPHoras' , 'genPlanPClase', 'copCalenP', 'copGymp', 'gestPuestP']


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
    console.log(tmp)

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

toNum(dato:string):number{
  return parseInt(dato, 10);
}

agregarSedes(){
  
}

//pantalla AGREGAR SPA
activacionSpa(){
  const sede = document.getElementById('sedegestSucSpaSELECT') as HTMLInputElement;
  const nombre = document.getElementById('gestSucSpaNOMBRE') as HTMLInputElement;
  const direccion = document.getElementById('gestSucSpaDIRECCION') as HTMLInputElement;
  const fechaDeApertura = document.getElementById('gestSucSpaFECHAAPERTURA') as HTMLInputElement;
  const horarioDeAtencion = document.getElementById('gestSucSpaHORARIOATENCION') as HTMLInputElement;
  const empleadoAdmin = document.getElementById('gestSucSpaEMPLEADOADMINISTRADOR') as HTMLInputElement;
  const capacidad = document.getElementById('gestSucSpaCAPACIDAD') as HTMLInputElement;
  const numerosTelefono = document.getElementById('gestSucTiendaNUMEROS2') as HTMLInputElement;
  const estadoActivo = document.getElementById('gestSucTiendaESTADOACTIVO2') as HTMLInputElement;
  
  //this.auth.agregarSpa
}

//pantalla activar tienda!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
activacionTienda(){
  const sede = document.getElementById('gestSucTiendaSELECT') as HTMLInputElement;
  const nombre = document.getElementById('gestSucTiendaNOMBRE') as HTMLInputElement;
  const direccion = document.getElementById('gestSucTiendaDIRECCION') as HTMLInputElement;
  const fechaDeApertura = document.getElementById('gestSucTiendaFECHAAPERTURA') as HTMLInputElement;
  const horarioDeAtencion = document.getElementById('gestSucTiendaHORARIODEATENCION') as HTMLInputElement;
  const empleadoAdministrador = document.getElementById('gestSucTiendaEMPLEADOADMINISTRADOR') as HTMLInputElement;
  const numerosDeTelefono = document.getElementById('gestSucTiendaNUMEROS') as HTMLInputElement;
  const estadoActivo = document.getElementById('gestSucTiendaESTADOACTIVO') as HTMLInputElement;
}

// pantalla GESTION DE TRATAMIENTOS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
agregarTratamiento(){
  const tratamiento = document.getElementById('gestTratSpaPSELECT') as HTMLInputElement;
  const IDunico = document.getElementById('gestTratSpaID') as HTMLInputElement;
  const nombre = document.getElementById('gestTratSpaNOMNRE') as HTMLInputElement;
}

eliminarTratamiento(){
  //llamada a eliminar tratamiento
}

agregarNuevoTratamiento(){
  const nombreNuevo = document.getElementById('gestTratSpaNOOMBRENUEVO') as HTMLInputElement;

}

//pantall de gestion de puestos!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

guardarPuesto(){
  const puesto = document.getElementById('gestPuestPSELECT') as HTMLInputElement;
  const ID = document.getElementById('gestPuestPID') as HTMLInputElement;
  const descripcion = document.getElementById('gestPuestPDESCRIPCION') as HTMLInputElement;
  
}

eliminarPuesto(){
  //llamada a eliminar puesto
}

agregarNuevoPuesto(){
  const nombre = document.getElementById('gestPuestPNOMBREAGREGAR') as HTMLInputElement;
  const descripcion = document.getElementById('gestPuestPDESCRIPCIONAGREGAR') as HTMLInputElement;
}

//pantalla GESTION DE TIPOS DE PLANILLA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
gestionDePlanilla(){
  const tipoPlanilla = document.getElementById('gestTipPlaPSELECT') as HTMLInputElement;
  const id = document.getElementById('gestTipPlaPID') as HTMLInputElement;
  const pagoMensual = document.getElementById('gestTipPlaPPAGOMENSUAL') as HTMLInputElement;
  const pagoHoras = document.getElementById('gestTipPlaPPAGOPORHORAS') as HTMLInputElement;
  const pagoClase = document.getElementById('gestTipPlaPPAGOPORCLASE') as HTMLInputElement;
  const descripcion = document.getElementById('gestTipPlaPDESCRIPCION') as HTMLInputElement;

  //this.auth
}

//pantalla de gestion de empleados!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
gestionEmpleados(){
  const empleado = document.getElementById('gestEmplPSELECT') as HTMLInputElement;
  const cedula = document.getElementById('gestEmplPNUMEROCEDULA') as HTMLInputElement; 
  const nombre = document.getElementById('gestEmplPNOMBRE') as HTMLInputElement; 
  const direccion = document.getElementById('gestEmplPDIRECCION') as HTMLInputElement;
  const puestoQueDesem = document.getElementById('gestEmplPPUESTO') as HTMLInputElement;
  const sucursalQueTrabaja = document.getElementById('gestEmplPSUCURSAL') as HTMLInputElement;
  const tipoDePlanilla = document.getElementById('gestEmplPLANILLA') as HTMLInputElement;
  const salario = document.getElementById('gestEmplPSALARIO') as HTMLInputElement;
  const correo = document.getElementById('gestEmplPCORREO') as HTMLInputElement;
  const contrasena = document.getElementById('gestEmplPPASSWORD') as HTMLInputElement;
  const apellido1 = "";
  const apellido2 = "";
  const distrito = "";
  const canton = "";
  const provincia = "";
  //modificarEmpleado
  //@ts-ignore
  this.auth.agregarEmpleados(this.toNum(cedula.value), nombre.value,apellido1.value, apellido2.value, distrito.value, canton.value, provincia.value, correo.value, contrasena.value, salario.value, puestoQueDesem.value, tipoDePlanilla.value, sucursalQueTrabaja.value)
}

eliminarEmpleado(){
  const cedula = document.getElementById('gestEmplPNUMEROCEDULA') as HTMLInputElement; 
  //@ts-ignore
  this.auth.eliminarEmpleados(this.toNum(cedula.value));
}

agregarEmpleado(){
  const cedula = document.getElementById('gestEmplPNUMEROCEDULA22') as HTMLInputElement; 
  const nombre = document.getElementById('gestEmplPNOMBRE2') as HTMLInputElement; 
  const direccion = document.getElementById('gestEmplPDIRECCION2') as HTMLInputElement;
  const puestoQueDesem = document.getElementById('gestEmplPPUESTO2') as HTMLInputElement;
  const sucursalQueTrabaja = document.getElementById('gestEmplPSUCURSAL2') as HTMLInputElement;
  const tipoDePlanilla = document.getElementById('gestEmplPLANILLA2') as HTMLInputElement;
  const salario = document.getElementById('gestEmplPSALARIO2') as HTMLInputElement;
  const correo = document.getElementById('gestEmplPCORREO2') as HTMLInputElement;
  const contrasena = document.getElementById('gestEmplPPASSWORD2') as HTMLInputElement;
  const apellido1 = "";
  const apellido2 = "";
  const distrito = "";
  const canton = "";
  const provincia = "";
  //@ts-ignore
  this.auth.agregarEmpleados(this.toNum(cedula.value), nombre.value,apellido1.value, apellido2.value, distrito.value, canton.value, provincia.value, correo.value, contrasena.value, salario.value, puestoQueDesem.value, tipoDePlanilla.value, sucursalQueTrabaja.value)
}

agregarNuevoEmpleado(){

}



//pantalla de gestion de servicios!!!!!!!!!!!!!!!!!!!!!!!!!!!

guardarServicio(){
  const servicio = document.getElementById('gestServPSELECT') as HTMLInputElement;
  const IDunico = document.getElementById('gestServPID') as HTMLInputElement;
  const nombre = document.getElementById('gestServPNOMBRE') as HTMLInputElement;
  const pagoHoras = document.getElementById('gestServPDESCRIPCION') as HTMLInputElement;
}

eliminarServicio(){
  //llamada a eliminar servicio
}

agregarNuevoServicio(){
  const nombre = document.getElementById('gestServPNOMBRE2') as HTMLInputElement;
  const descripcion = document.getElementById('gestServPDESCRIPCION2') as HTMLInputElement;
  
}

//pantalla de gestion de tipos de equipos!!!!!!!!!!!!!!!!!!!!!!!!!!!
guardarTipoEquipo(){
  const gym = document.getElementById('gestTipEquipPGYM') as HTMLInputElement;
  const id = document.getElementById('gestTipEquipPID') as HTMLInputElement;
  const descripcion = document.getElementById('gestTipEquipPDESCRIPCION') as HTMLInputElement;
}

eliminarTipoEquipo(){
  //llamada a eliminar equipo
}

agregarNuevoEquipo(){
  const id = document.getElementById('gestTipEquipPIDNUEVO') as HTMLInputElement;
  const descripcion = document.getElementById('gestTipEquipPDESCRIPCIONNUEVO') as HTMLInputElement;
}


//PANTALLA DE GESTION DE INVENTARIO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

guardarInventario(){
  const gym = document.getElementById('gestInvetPGYM') as HTMLInputElement;
  const maquina = document.getElementById('gestInvetPMAQUINA') as HTMLInputElement; 
  const tipoDeEquipo = document.getElementById('gestInvetPTIPO') as HTMLInputElement; 
  const marca = document.getElementById('gestInvetPMARCA') as HTMLInputElement;
  const numeroDeSerie = document.getElementById('gestInvetPNUMEROSERIE') as HTMLInputElement;
  const costo = document.getElementById('gestInvetPCOSTOSUCURSAL') as HTMLInputElement;
  const asignadaAGym = document.getElementById('gestInvetPASIGNADA') as HTMLInputElement;
}

eliminarInventario(){
  //llamada a eliminar inventario
}

agregarNuevoInventario(){
  const tipoDeEquipo = document.getElementById('gestInvetPTIPO2') as HTMLInputElement; 
  const marca = document.getElementById('gestInvetPMARCA2') as HTMLInputElement;
  const numeroDeSerie = document.getElementById('gestInvetPNUMEROSERIE2') as HTMLInputElement;
  const costo = document.getElementById('gestInvetPCOSTOSUCURSAL2') as HTMLInputElement;
  const asignadaAGym = document.getElementById('gestInvetPASIGNADA2') as HTMLInputElement;
}



}//bracket que cierra


