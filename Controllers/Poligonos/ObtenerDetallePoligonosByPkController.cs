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
    public class ObtenerDetallePoligonosByPkController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]PoligonosModelList listaP)
        {

            int result = 0;
            string msj = "¡Error al obtener poligonos intente más tarde!";
            List<PoligonoModel> lista = new List<PoligonoModel>();
            PoligonoModel aux = new PoligonoModel();
            try
            {
                foreach (PoligonoModel poligo in listaP.POLIGONOS) {
                    poligo.ObtenerPoligono();
                    lista.Add(poligo);
                }
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