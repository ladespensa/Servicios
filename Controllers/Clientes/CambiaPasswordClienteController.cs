using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Cientes;

namespace acmarkert.Controllers.Clientes
{
    [Route("api/[controller]")]
    [ApiController]
    public class CambiaPasswordClienteController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]ClientesModel clientem)
        {
            int result = 0;
            string msj = "¡Error al actualizar contraseña intente más tarde!";
            try
            {
                if (clientem.NuevoPassword())
                {
                    result = 1;
                    msj = "¡Contraseña actualizada!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al actualizar contraseña intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                cliente = clientem
            });
        }
    }
}