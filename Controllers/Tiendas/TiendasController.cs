using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Tiendas;
using acmarkert.Models.Productos;
using acmarkert.Models;

namespace acmarkert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendasController : ControllerBase
    {

        [HttpPost]
        public ActionResult Post() {

            int result = 0;
            string msj = "¡Error al obtener tiendas intente más tarde!";
            string liga = "",apiOpen="",produccion="", apiOpenCharId="";
            List<TiendasModel> lista=null;
            List<ProductosModel> listaPromos = null;
            ProductosModel aux1 = new ProductosModel();
            try
            {
                TiendasModel tienda = new TiendasModel();
                lista = tienda.getTiendasLight();
                listaPromos = aux1.getProductosByPromocion();
                liga = "Conoce ACMarket app en: " + VariablesModel.getVariableValue("LIGA_COMPARTIR");
                apiOpen = VariablesModel.getVariableValue("PUBLICAOPEN");
                apiOpenCharId = VariablesModel.getVariableValue("IDOPEN");
                produccion = VariablesModel.getVariableValue("API_PRODUCTIVA_OPEN_PAY");
                result = 1;
                msj = "¡Tiendas obtenidas!";
            }
            catch {
                result = 0;
                msj = "¡Error al obtener tiendas intente más tarde!";
            }

            return Ok(new{ 
                resultado=result,
                mensaje = msj,
                tiendas=lista,
                link = liga,
                publicaopen=apiOpen,
                openproduccion=produccion,
                idopen= apiOpenCharId,
                promos = listaPromos
            }); 
        }

    }
}