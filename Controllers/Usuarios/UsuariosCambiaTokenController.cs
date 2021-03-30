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
    public class UsuariosCambiaTokenController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]UsuariosPortalModel usuariom)
        {
            int result = 0;
            string msj = "¡Error al actualizar token intente más tarde!";
            try
            {
                if (usuariom.NuevoToken())
                {
                    result = 1;
                    msj = "¡Token actualizado!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al actualizar token intente más tarde!";
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