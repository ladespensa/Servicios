using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acmarkert.Models;
using acmarkert.Models.CodigosDescuento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acmarkert.Controllers.CodigoDescuento
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuscarCodigoDescuentoController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]CodigosDescuentoModel codigom)
        {
            int result = 0;
            string msj = "¡Error al obtener código de descuento intente más tarde!";
            try
            {
                if (codigom.buscarCodigoDescuento())
                {
                    if (codigom.VIGENTE)
                    {
                        PedidosModel pedidom = new PedidosModel();
                        pedidom.CODIGO_DESCUENTO = codigom.CODIGO;
                        pedidom.PK_CLIENTE = codigom.PK_CLIENTE;
                        if (pedidom.obtenerCantidadCodigosUsados())
                        {
                            if (pedidom.CANTIDAD_DECUENTOS < codigom.MAXIMO_USOS)
                            {
                                result = 1;
                                msj = "¡Código valido!";
                            }
                            else {
                                result = 0;
                                msj = "¡Código ya fue utilizado!";
                            }
                        }
                    }
                    else {
                        result = 0;
                        msj = "¡Código no vigente!";
                    }
                }
                else {
                    result = 0;
                    msj = "¡Código inválido!";
                }
                

            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener código de descuento intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                codigo=codigom
            });
        }
    }
}