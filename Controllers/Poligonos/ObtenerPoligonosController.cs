using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models.Poligonos;

namespace acmarkert.Controllers.Poligonos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtenerPoligonosController : ControllerBase
    {

        [HttpPost]
        public ActionResult Post()
        {

            int result = 0;
            string msj = "¡Error al obtener poligonos intente más tarde!";
            List<PoligonoModel> lista = null;
            PoligonoModel aux = new PoligonoModel();
            try
            {
                lista = aux.ObtenerPoligonosList();
                result = 1;
                msj = "¡Poligonos obtenidos!";
            }
            catch
            {
                result = 0;
                msj = "¡Error al obtener poligonos intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                poligonos = lista
            });
        }
    }
}