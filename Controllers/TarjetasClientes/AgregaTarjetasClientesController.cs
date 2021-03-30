using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Tarjetas;

namespace acmarkert.Controllers.TarjetasClientes
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgregaTarjetasClientesController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]TarjetasModel tarjetam)
        {
            int result = 0;
            string msj = "¡Error al guardar tarjeta intente más tarde!";
            try
            {
                if (tarjetam.agregarTarjeta())
                {
                    result = 1;
                    msj = "¡Tarjeta guardada!";
                }
                else
                {
                    result = 0;
                    if (!string.IsNullOrEmpty(tarjetam.ERROR) && !tarjetam.ERROR.Equals("Error al hacer el cargo."))
                    {
                        msj = tarjetam.ERROR;
                    }
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al guardar tarjeta intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                tarjeta = tarjetam.card
            });
        }
    }
}