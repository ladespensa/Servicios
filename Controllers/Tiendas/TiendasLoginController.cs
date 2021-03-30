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
    public class TiendasLoginController : ControllerBase
    {

        [HttpPost]
        public ActionResult Post([FromBody]TiendasModel tiendam)
        {

            int result = 0;
            string msj = "¡Error al obtener tienda intente más tarde!";

            try
            {
                TiendasModel aux = new TiendasModel();
                aux.PASSWORD = tiendam.PASSWORD;
                if (tiendam.obtenerTiendaPorTelefono())
                {
                    if (tiendam.PASSWORD.Equals(aux.PASSWORD))
                    {
                        result = 1;
                        msj = "¡Sesion iniciada!";
                    }
                    else
                    {
                        result = 0;
                        msj = "¡Contraseña incorrecta!";
                    }
                }
                else
                {
                    result = 0;
                    msj = "¡Usuario no existe!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener usuario intente más tarde!";
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