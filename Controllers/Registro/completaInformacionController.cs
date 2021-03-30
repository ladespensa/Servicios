using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models;
using acmarkert.Models.Cientes;

namespace acmarkert.Controllers.Registro
{
    [Route("api/[controller]")]
    [ApiController]
    public class completaInformacionController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]ClientesModel clientem)
        {
            int result = 0;
            string msj = "¡Error al registrar cliente intente más tarde!";
            try
            {
                if (clientem.complementaDatos())
                {

                    try
                    {
                        clientem.registraOpenPay();
                    }
                    catch (Exception er) { 
                        LogModel.registra("Conflicto al crear cliente en Openpay", er.ToString());
                    }

                    result = 1;
                    msj = "¡Cliente registrado!";
                }
            }
            catch(Exception e)
            {
                LogModel.registra("Error al agregar cliente", e.ToString());
                result = 0;
                msj = "¡Error al registrar cliente intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                cliente = clientem
            });
        }
    }
}