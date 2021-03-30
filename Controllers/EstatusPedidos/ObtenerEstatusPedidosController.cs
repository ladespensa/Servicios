using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.EstatusPedidos;

namespace acmarkert.Controllers.EstatusPedidos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtenerEstatusPedidosController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post()
        {
            EstatusPedidosModel estatus = new EstatusPedidosModel();
            List<EstatusPedidosModel> listaEstatus = null;
            int result = 0;
            string msj = "¡Error al obtener estatus pedidos intente más tarde!";
            try
            {
                listaEstatus = estatus.obtenerEstatusPedidos();
                result = 1;
                msj = "¡Estatus obtenidos!";

            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener estatus pedidos intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                estatus = listaEstatus
            });
        }
    }
}