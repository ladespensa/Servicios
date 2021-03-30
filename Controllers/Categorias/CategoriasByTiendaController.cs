using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Categorias;
using acmarkert.Models.Tiendas;

namespace acmarkert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasByTiendaController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]TiendasModel tienda)
        {

            int result = 0;
            string msj = "¡Error al obtener categorias intente más tarde!";
            List<CategoriasModel> lista = null;
            try
            {
                CategoriasModel categoria = new CategoriasModel();
                lista = categoria.getCategoriasByTienda(tienda.PK);
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