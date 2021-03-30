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
    public class TotalesDetallesTiendaController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]TiendasTotalesDetallesModel tiendam)
        {

            int result = 0;
            string msj = "¡Error al obtener detalle de totales, intente más tarde!";
            try
            {
                if (tiendam.obtenerPagadoDetalleByFolio())
                {
                    result = 1;
                    msj = "¡Detalle de totales obtenidos!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener detalle de totales, intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                totalesDetalles = tiendam
            });
        }
    }
}