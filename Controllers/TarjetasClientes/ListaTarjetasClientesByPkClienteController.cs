using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Openpay.Entities;
using acmarkert.Models.Tarjetas;

namespace acmarkert.Controllers.TarjetasClientes
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaTarjetasClientesByPkClienteController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]TarjetasModel tarjetam)
        {

            int result = 0;
            string msj = "¡Error al obtener tarjetas intente más tarde!";
            List<Card> listatar = new List<Card>();
            try
            {

                listatar = tarjetam.obneterTargetasPorPkCliente1();
                result = 1;
                msj = "¡Tarjetas obtenidas!";

            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener tarjetas intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                tarjetas = listatar
            });
        }
    }
}