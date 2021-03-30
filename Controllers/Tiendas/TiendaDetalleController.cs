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
    public class TiendaDetalleController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]TiendasModel tiendam)
        {

            int result = 0;
            string msj = "¡Error al obtener detalle de la tienda intente más tarde!";
            try
            {
                if (tiendam.obtenerTiendaPorPK())
                {
                    result = 1;
                    msj = "¡Tiendas obtenidas!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener detalle de tienda intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                tienda = tiendam
            });
        }
    }
}