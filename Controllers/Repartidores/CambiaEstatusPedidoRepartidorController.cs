using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models;
using acmarkert.Models.Cientes;
using acmarkert.Models.Notificaciones;
using acmarkert.Models.Repartidores;

namespace acmarkert.Controllers.Repartidores
{
    [Route("api/[controller]")]
    [ApiController]
    public class CambiaEstatusPedidoRepartidorController : ControllerBase
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
                            RepartidoresModel repartidor = new RepartidoresModel();
                            repartidor.PK = pedidom.PK_REPARTIDOR;
                            if (cliente.getClienteByPk() && repartidor.obtenerUsuarioByPK())
                            {
                                if (!string.IsNullOrEmpty(cliente.TOKEN))
                                {
                                    
                                    NotificacionesModel notificacion = new NotificacionesModel();
                                    switch (int.Parse(pedidom.PK_ESTATUS)){
                                        case 3:
                                            notificacion.TITLE = "¡Tu repartidor esta en negocio!";
                                            notificacion.MESSAGE = "¡" + repartidor.NOMBRE + "' está esperando la entrega de tu pedido!";
                                            break;
                                        case 4:
                                            notificacion.TITLE = "¡Tu pedido está en camino!";
                                            notificacion.MESSAGE = "¡" + repartidor.NOMBRE + "' está en camino con tu pedido!";
                                            break;
                                        case 5:
                                            notificacion.TITLE = "¡Tu pedido se entrego!";
                                            notificacion.MESSAGE = "¡Gracias por utilizar acmarket!";
                                            break;
                                    }
                                    notificacion.TOKENS.Add(cliente.TOKEN);
                                    await notificacion.sendNotificationAsync();
                                }
                            }
                        }
                    }
                    catch (Exception e) { LogModel.registra("Error al enviar notificación set estatus Pedido Tienda", e.ToString()); }
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