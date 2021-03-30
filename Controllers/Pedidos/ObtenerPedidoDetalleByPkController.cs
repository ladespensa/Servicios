using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models;

namespace acmarkert.Controllers.Pedidos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtenerPedidoDetalleByPkController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]PedidosModel pedidom)
        {

            int result = 0;
            string msj = "¡Error al obtener detalle de pedido, intente más tarde!";
            try
            {
                if (pedidom.obtenerPedidoByPk())
                {
                    result = 1;
                    msj = "¡Detalle de pedido obtenido!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener detalle de pedido, intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                pedido = pedidom
            });
        }
    }
}