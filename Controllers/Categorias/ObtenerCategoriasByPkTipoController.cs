using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acmarkert.Models.Categorias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acmarkert.Controllers.Categorias
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtenerCategoriasByPkTipoController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]CategoriasModel categoriaM)
        {
            int result = 0;
            string msj = "¡Error al obtener categorias intente más tarde!";
            List<CategoriasModel> lista = new List<CategoriasModel>();
            try
            {
                lista = categoriaM.getCategoriasByPkTipo();
                result = 1;
                msj = "¡Categorias obtenidas!";
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener categorias intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                categorias = lista
            });
        }
    }
}