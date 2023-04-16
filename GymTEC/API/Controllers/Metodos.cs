using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Metodos{

    [ApiController]
    [Route("usuarios")]
    public class MetodosAPI: ControllerBase{

        [HttpPost]
        [Route("cliente/agregarTratamientoSPA")]
        public string AgregarTratamientoSPA(){
            return "{ 'status': 'ok' }";
        }

        [HttpGet]
        [Route("cliente/eliminarTratamientoSPA")]
        public string EliminarTratamientoSPA(){
            return "'status' AAAA 'ok' ";
        }
    }
}
