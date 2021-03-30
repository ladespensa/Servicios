using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Direcciones;

namespace acmarkert.Controllers.Direcciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtenerDireccionesByPkClienteController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]DireccionesClientesModel dir)
        {
            int result = 0;
            string msj = "¡Error al obtener direcciones intente más tarde!";
            List<DireccionesClientesModel> lista = null;
            try
            {
                lista = dir.obtieneDireccionesByPkCliente();
                result = 1;
                msj = "¡Direcciones obtenidas!";
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener direcciones intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                direcciones = lista
            });
        }
    }
}