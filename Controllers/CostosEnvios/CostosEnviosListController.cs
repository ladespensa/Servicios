using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.CostosEnvios;

namespace acmarkert.Controllers.CostosEnvios
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostosEnviosListController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post()
        {
            List<CostosEnviosModel> listacostos = null;
            CostosEnviosModel aux = new CostosEnviosModel();
            int result = 0;
            string msj = "¡Error al obtener costos de envìo intente más tarde!";
            try
            {
                listacostos = aux.obtenerCostosEnvios();
                result = 1;
                msj = "¡Costos obtenidos!";

            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener costos de envìo intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                costos = listacostos
            });
        }
    }
}