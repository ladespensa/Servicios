using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acmarkert.Models.Tiendas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acmarkert.Controllers.Tiendas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtenerTiendaByPkController : ControllerBase
    {
        [HttpPost("[action]")]
        public ActionResult TiendaLightByPk([FromBody] TiendasModel tiendaM)
        {

            int result = 0;
            string msj = "¡Error al obtener tienda intente más tarde!";
            try
            {
                tiendaM.getTiendasLightByPk();
                result = 1;
                msj = "¡Tienda obtenida!";
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener tienda intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                tienda = tiendaM,
            });
        }

    }
}