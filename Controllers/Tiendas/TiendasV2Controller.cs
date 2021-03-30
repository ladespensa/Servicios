using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acmarkert.Models;
using acmarkert.Models.ImagenesSlider;
using acmarkert.Models.Productos;
using acmarkert.Models.Tiendas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acmarkert.Controllers.Tiendas
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendasV2Controller : ControllerBase
    {
        [HttpPost("[action]")]
        public ActionResult Obtener([FromBody]PedidosModel pedido)
        {

            int result = 0, cantidadP=0,pk_falta_calificacion=0;
            string msj = "¡Error al obtener tiendas intente más tarde!";
            string liga = "", apiOpen = "", produccion = "", apiOpenCharId = "";
            List<TiendasModel> lista = null;
            List<ProductosModel> listaPromos = null;
            ProductosModel aux1 = new ProductosModel();
            ImagenesSliderModel imgSlider = new ImagenesSliderModel();
            List<ImagenesSliderModel> imagenesSlider=null;
            
            try
            {
                TiendasModel tienda = new TiendasModel();
                lista = tienda.getTiendasLight();
                listaPromos = aux1.getProductosByPromocion();
                liga = "Conoce ACMarket app en: " + VariablesModel.getVariableValue("LIGA_COMPARTIR");
                apiOpen = VariablesModel.getVariableValue("PUBLICAOPEN");
                apiOpenCharId = VariablesModel.getVariableValue("IDOPEN");
                produccion = VariablesModel.getVariableValue("API_PRODUCTIVA_OPEN_PAY");
                cantidadP = pedido.obtenerCantidadPedidosByPkCliente();
                if (pedido.faltaCalificacion()) {
                    pk_falta_calificacion = int.Parse(pedido.PK);
                }
                imagenesSlider = imgSlider.getList();
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
                tiendas = lista,
                link = liga,
                publicaopen = apiOpen,
                openproduccion = produccion,
                idopen = apiOpenCharId,
                promos = listaPromos,
                cantidadMisPedidos = cantidadP,
                faltaCalificacion=pk_falta_calificacion,
                sliderImg=imagenesSlider
            });
        }
    }
}