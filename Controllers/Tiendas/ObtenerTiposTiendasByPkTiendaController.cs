using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acmarkert.Models.Productos;
using acmarkert.Models.Tiendas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acmarkert.Controllers.Tiendas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtenerTiposTiendasByPkTiendaController : ControllerBase
    {


        [HttpPost]
        public ActionResult Post([FromBody]TiendasModel tiendam)
        {

            int result = 0;
            string msj = "¡Error al obtener tipos de tiendas intente más tarde!";
            List<TiposTiendasModel> lista = new List<TiposTiendasModel>();
            List<ProductosModel> listaPromos = null;
            try
            {
                ProductosModel aux1 = new ProductosModel();
                TiposTiendasModel tipo = new TiposTiendasModel();
                aux1.PK_TIENDA = tiendam.PK;
                lista = tipo.obtenerTiposByPkTienda(tiendam.PK);
                listaPromos = aux1.getProductosByPromocionByPkTienda();
                result = 1;
                msj = "¡Tipos de tiendas obtenidas!";
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
                tipos = lista,
                promos = listaPromos
            });
        }

    }
}