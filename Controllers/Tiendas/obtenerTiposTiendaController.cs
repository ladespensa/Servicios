using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models;
using acmarkert.Models.Productos;
using acmarkert.Models.Tiendas;

namespace acmarkert.Controllers.Tiendas
{
    [Route("api/[controller]")]
    [ApiController]
    public class obtenerTiposTiendaController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post()
        {
            List<TiposTiendasModel> lista = null;
            List<ProductosModel> listaPromos = null;
            string liga="";
            TiposTiendasModel aux = new TiposTiendasModel();
            ProductosModel aux1 = new ProductosModel();
            int result = 0;
            string msj = "¡Error al obtener tipos de tiendas intente más tarde!";
            try
            {
                lista = aux.obtenerTiposTienda();
                listaPromos = aux1.getProductosByPromocion();
                liga = "Conoce Polar app en: " + VariablesModel.getVariableValue("LIGA_COMPARTIR");
                result = 1;
                msj = "¡Tipos de tiendas obtenidos!";

            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener tipos de tiendas intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                link=liga,
                tipos = lista,
                promos=listaPromos
            });
        }
    }
}