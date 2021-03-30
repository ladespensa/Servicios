using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acmarkert.Models;
using acmarkert.Models.Cientes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acmarkert.Controllers.Registro
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgregaOpenPayIdController : ControllerBase
    {
        /*
        [HttpPost]
        public ActionResult Post()
        {

            int result = 0;
            string msj = "¡Error al registrar cliente intente más tarde!";

            ClientesModel aux = new ClientesModel();
            foreach (ClientesModel clientem in aux.getClientes())
            {
                try
                {
                    clientem.registraOpenPay();
                    result = 1;
                    msj = "¡Cliente registrado!";
                }
                catch (Exception e)
                {
                    LogModel.registra("Error al agregar cliente a Openpay", e.ToString());
                    result = 0;
                    msj = "¡Error al registrar en open pay cliente intente más tarde!";
                }
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                cliente = aux
            });
        }
        */
    }
}