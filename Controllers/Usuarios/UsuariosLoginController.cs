using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Usuarios;

namespace acmarkert.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosLoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]UsuariosPortalModel usuariom)
        {

            int result = 0;
            string msj = "¡Error al obtener usuario intente más tarde!";
            
            try
            {
                UsuariosPortalModel aux = new UsuariosPortalModel();
                aux.PASSWORD = usuariom.PASSWORD;
                if (usuariom.obtenerUsuarioByUsuario())
                {
                    if (usuariom.PASSWORD.Equals(aux.PASSWORD))
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
                usuario = usuariom
            });
        }
    }
}