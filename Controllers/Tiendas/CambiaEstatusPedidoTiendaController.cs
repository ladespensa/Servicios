using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models;
using acmarkert.Models.Cientes;
using acmarkert.Models.Notificaciones;

namespace acmarkert.Controllers.Tiendas
{
    [Route("api/[controller]")]
    [ApiController]
    public class CambiaEstatusPedidoTiendaController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]PedidosModel pedidom)
        {
            int result = 0;
            string msj = "¡Error al actualizar estatus intente más tarde!";
            try
            {
                if (pedidom.setEstatus())
                {
                    try
                    {
                        if (pedidom.obtenerPedidoByPk())
                        {
                            ClientesModel cliente = new ClientesModel();
                            cliente.PK = pedidom.PK_CLIENTE;
                            if (cliente.getClienteByPk())
                            {
                                if (!string.IsNullOrEmpty(cliente.TOKEN)) {
                                    NotificacionesModel notificacion = new NotificacionesModel();
                                    notificacion.TITLE = "¡Se está preparando el pedido!";
                                    notificacion.MESSAGE = "¡La tienda '"+pedidom.TIENDA+"' está preparando tu pedido!";
                                    notificacion.TOKENS.Add(cliente.TOKEN);
                                    await notificacion.sendNotificationAsync();
                                }
                            }
                        }
                    }
                    catch (Exception e) { LogModel.registra("Error al enviar notificación set estatus Pedido Tienda",e.ToString()); }
                    result = 1;
                    msj = "¡Estatus actualizado!";
                }
            }
            catch
            {
                result = 0;
                msj = "¡Error al actualizar estatus intente más tarde!";
            }

            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                pedido = pedidom
            });
        }
    }
}