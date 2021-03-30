using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Tiendas;

namespace acmarkert.Controllers.Tiendas
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalesTiendaController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]TiendasTotalesModel tiendam)
        {

            int result = 0;
            string msj = "¡Error al obtener totales, intente más tarde!";
            try
            {
                if (tiendam.obtenerPagadoListAndMontoNoPagado())
                {
                    result = 1;
                    msj = "¡Totales obtenidos!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener totales, intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                totales = tiendam
            });
        }
    }
}