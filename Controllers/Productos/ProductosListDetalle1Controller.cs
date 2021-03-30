using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Productos;
using acmarkert.Models;

namespace acmarkert.Controllers.Productos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosListDetalle1Controller : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]Productos1Model productosList1)
        {
            List<ProductosModel> productosList = productosList1.productosList;
            int result = 0;
            /*double fact=0.029;
            double sum=2.5;*/
            double fact = double.Parse( VariablesModel.getVariableValue("FACTOR"));
            double sum = double.Parse( VariablesModel.getVariableValue("SUMA"));
            string msj = "¡Error al obtener lista de productos intente más tarde!";
            try
            {
                foreach (ProductosModel producto in productosList)
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
                factor=fact,
                suma=sum,
                productos = productosList
            });
        }
    }
}