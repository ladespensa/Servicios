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
    public class ActualizaTokenTiendasController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]TiendasModel tiendam)
        {
            int result = 0;
            string msj = "¡Error al actualizar token intente más tarde!";
            try
            {
                if (tiendam.NuevoToken())
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
                tienda = tiendam
            });
        }
    }
}