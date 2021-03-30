using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Productos;

namespace acmarkert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosByTiendaAndCategoriaController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]ProductosModel productos)
        {

            int result = 0;
            string msj = "¡Error al obtener productos intente más tarde!";
            List<ProductosModel> lista = null;
            try
            {
                lista = productos.getProductosByTiedaAndCategoria();
                result = 1;
                msj = "¡Productos obtenidos!";
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener productos intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                productos = lista
            });
        }
    }
}