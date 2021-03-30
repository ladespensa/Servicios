using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Cientes;

namespace acmarkert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesLoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]ClientesModel clientem)
        {

            int result = 0;
            string msj = "¡Error al obtener cliente intente más tarde!";
            List<ClientesModel> lista = null;
            try
            {
                ClientesModel aux = new ClientesModel();
                aux.PASSWORD = clientem.PASSWORD;
                if (clientem.getUsuarioByTelefono())
                {
                    if (clientem.PASSWORD.Equals(aux.PASSWORD))
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
                else {
                    result = 0;
                    msj = "¡Cliente no existe!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener cliente intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                cliente = clientem
            });
        }
    }
}