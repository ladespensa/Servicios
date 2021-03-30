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
    public class TiendasByTipoController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]TiposTiendasModel tipom)
        {

            int result = 0;
            string msj = "¡Error al obtener tiendas intente más tarde!";
            List<TiendasModel> lista = null;
            try
            {
                TiendasModel tienda = new TiendasModel();
                lista = tienda.getTiendasByTipos(tipom.PK);
                result = 1;
                msj = "¡Tiendas obtenidas!";
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener tiendas intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                tiendas = lista
            });
        }
    }
}