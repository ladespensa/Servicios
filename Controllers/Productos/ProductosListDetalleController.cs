using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Productos;

namespace acmarkert.Controllers.Productos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosListDetalleController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]List<ProductosModel> productosList)
        {
            int result = 0;
            string msj = "¡Error al obtener lista de productos intente más tarde!";
            try
            {
                foreach(ProductosModel producto in productosList)
                {
                    producto.getProductoByPK();
                }
                result = 1;
                msj = "¡Productos obtenidos!";
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener lista de productos intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                productos = productosList
            });
        }
    }
}