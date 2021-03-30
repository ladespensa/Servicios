using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Repartidores;

namespace acmarkert.Controllers.Repartidores
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRepartidorController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]RepartidoresModel repartidorm)
        {

            int result = 0;
            string msj = "¡Error al obtener usuario intente más tarde!";

            try
            {
                RepartidoresModel aux = new RepartidoresModel();
                aux.PASSWORD = repartidorm.PASSWORD;
                if (repartidorm.obtenerUsuarioByTelefono())
                {
                    if (repartidorm.PASSWORD.Equals(aux.PASSWORD))
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
                repartidor = repartidorm
            });
        }
    }
}