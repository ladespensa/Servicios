using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acmarkert.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acmarkert.Controllers.Tiendas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtenerPedidosPasadosPorPkTiendaController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]PedidosModel pedidom)
        {
            List<PedidosModel> listaPedi = null;
            int result = 0;
            string msj = "¡Error al obtener mis pedidos intente más tarde!";
            try
            {
                listaPedi = pedidom.obtenerPedidosPasadosByPkTienda();
                result = 1;
                msj = "¡Pedidos obtenidos!";

            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener mis pedidos intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                pedidos = listaPedi
            });
        }
    }
}