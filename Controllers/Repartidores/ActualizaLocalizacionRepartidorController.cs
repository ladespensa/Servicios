using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Repartidores;

namespace acmarkert.Controllers.Repartidores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActualizaLocalizacionRepartidorController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]RepartidoresModel repartidorm)
        {
            int result = 0;
            string msj = "¡Error al actualizar localización intente más tarde!";
            try
            {
                if (repartidorm.actualizaLocalizacion())
                {
                    result = 1;
                    msj = "¡Localización actualizada!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al actualizar localización intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                repartidor = repartidorm
            });
        }
    }
}