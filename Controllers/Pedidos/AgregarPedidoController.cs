using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acmarkert.Models;
using acmarkert.Models.Cientes;
using acmarkert.Models.Direcciones;
using acmarkert.Models.Notificaciones;
using acmarkert.Models.Tiendas;
using System.Globalization;
using acmarkert.Models.Pedidos;
using acmarkert.Models.Productos;
using acmarkert.Models.CostosEnvios;

namespace acmarkert.Controllers.Pedidos
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgregarPedidoController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]PedidosModel pedidom)
        {
            PedidosModel pedidoClone = new PedidosModel();
            string listaProductosRecibidos = "";
            string listaProductosRecibidosCalculado = "";

            int result = 0;
            string msj = "¡Error al agregar pedido intente más tarde!",fecha_entrega="";
            DateTime DAT;
            try
            {
                TiendasModel tienda1 = new TiendasModel();
                tienda1.PK = pedidom.PK_TIENDA;
                tienda1.obtenerTiendaPorPK();

                try
                { //Formato "martes 5 de mayo 2020"
                    DAT = DateTime.Parse(pedidom.FECHA_ENTREGA, CultureInfo.CreateSpecificCulture("es-MX"));
                }
                catch (Exception e) {
                    LogModel.registra("Error Agregar pedido al convertir fecha:", e.ToString()+" {FECHA_ENTREGA="+pedidom.FECHA_ENTREGA+"}");
                    try//Formato "martes 5 de mayo 2020"
                    {
                        string[] fecha = pedidom.FECHA_ENTREGA.Split(" ");
                        DAT = DateTime.Parse(fecha[4] + "-" + fecha[3] + "-" + fecha[1], CultureInfo.CreateSpecificCulture("es-MX"));
                    }
                    catch (Exception e1) {//en caso de no poder convertir la fecha que recibo tengo que calcularla
                        LogModel.registra("Error Agregar pedido al convertir fecha:", e1.ToString());

                        if (pedidom.PK_COSTO_ENVIO.Equals("2"))//ESPRESS
                        {
                            DAT = DateTime.Now.AddDays(1);
                        }
                        else {//calculo entrega normal
                            DAT = obtenerFechaEntrega(tienda1, pedidom.PK_POLIGONO);
                        }

                    }
                }
                
                fecha_entrega = DAT.ToString("dddd dd ",CultureInfo.CreateSpecificCulture("es-MX"))+"de"+DAT.ToString(" MMMM yyyy",CultureInfo.CreateSpecificCulture("es-MX"));
                pedidom.FECHA_ENTREGA = DAT.ToString("yyyy-MM-dd");

                //COPIA TODO EL CONTENIDO A UN NUEVO PEDIDO PARA VOLVER A CALCULAR PRECIOS DE PRODUCTO, SUBTOTAL, TOTAL DEL PEDIDO
                pedidoClone.PK_CLIENTE = pedidom.PK_CLIENTE;
                pedidoClone.DIRECCION = pedidom.DIRECCION;
                pedidoClone.LATITUD = pedidom.LATITUD;
                pedidoClone.LONGITUD = pedidom.LONGITUD;
                pedidoClone.PK_TIENDA = pedidom.PK_TIENDA;
                pedidoClone.ENVIO = pedidom.ENVIO;
                pedidoClone.SUBTOTAL = pedidom.SUBTOTAL;
                pedidoClone.COMISION_TARJETA = pedidom.COMISION_TARJETA;
                pedidoClone.TOTAL = pedidom.TOTAL;
                pedidoClone.METODO_PAGO = pedidom.METODO_PAGO;
                pedidoClone.PK_COSTO_ENVIO = pedidom.PK_COSTO_ENVIO;
                pedidoClone.FECHA_ENTREGA = pedidom.FECHA_ENTREGA;
                pedidoClone.CODIGO_DESCUENTO = pedidom.CODIGO_DESCUENTO;
                pedidoClone.DESCUENTO = pedidom.DESCUENTO;
                pedidoClone.SOURCE_ID = pedidom.SOURCE_ID;
                pedidoClone.DEVICE_SESSION_ID = pedidom.DEVICE_SESSION_ID;
                pedidoClone.CVV2 = pedidom.CVV2;
                pedidoClone.COSTUMER_ID = pedidom.COSTUMER_ID;
                pedidoClone.LISTA = new List<PedidoDetalleModel>();
                
                if (pedidom.LISTA != null)
                {
                    double Total = 0;
                    PedidoDetalleModel aux;
                    ProductosModel producto = new ProductosModel();
                    foreach (PedidoDetalleModel detalle in pedidom.LISTA)
                    {
                        
                        producto.PK = detalle.PK_PRODUCTO;

                        aux = new PedidoDetalleModel();
                        aux.PK = detalle.PK;
                        aux.PK_PEDIDO = detalle.PK_PEDIDO;
                        aux.PK_PRODUCTO = detalle.PK_PRODUCTO;
                        aux.PRODUCTO = detalle.PRODUCTO;
                        aux.DESCRIPCION = detalle.DESCRIPCION;
                        aux.TIENDA = detalle.TIENDA;
                        if (producto.getPrecioByPK()){
                            try
                            {
                                aux.PRECIO = double.Parse(producto.PRECIO);
                            }
                            catch (Exception e) {
                                LogModel.registra("Agregar pedido error al obtener precio de producto", e.ToString());
                                aux.PRECIO = 0;
                            }
                        }
                        else {
                            LogModel.registra("Precio del producto no encotrado", "PK_PRODUCTO:"+detalle.PK_PRODUCTO+", PRECIO:"+detalle.PRECIO);
                            aux.PRECIO = detalle.PRECIO;
                        }
                        aux.CANTIDAD = detalle.CANTIDAD;
                        aux.DETALLES = detalle.DETALLES;
                        aux.IMAGEN = detalle.IMAGEN;
                        aux.BORRADO = detalle.BORRADO;
                        aux.FECHA_C = detalle.FECHA_C;
                        aux.FECHA_M = detalle.FECHA_M;
                        aux.FECHA_D = detalle.FECHA_D;
                        aux.USUARIO_C = detalle.USUARIO_C;
                        aux.USUARIO_M = detalle.USUARIO_M;
                        aux.USUARIO_D = detalle.USUARIO_D;

                        Total += (aux.CANTIDAD * aux.PRECIO);

                        pedidoClone.LISTA.Add(aux);

                        listaProductosRecibidos += "{PK_PRODUCTO:" + detalle.PK_PRODUCTO
                                                  + ",PRECIO:" + detalle.PRECIO
                                                  + ",CANTIDAD:" + detalle.CANTIDAD
                                                  + ",DETALLES:" + detalle.DETALLES
                                                  + "},";

                        listaProductosRecibidosCalculado += "{PK_PRODUCTO:" + aux.PK_PRODUCTO
                                                  + ",PRECIO:" + aux.PRECIO
                                                  + ",CANTIDAD:" + aux.CANTIDAD
                                                  + ",DETALLES:" + aux.DETALLES
                                                  + "},";
                    }
                    pedidoClone.SUBTOTAL = Total;
                    CostosEnviosModel ce = new CostosEnviosModel();
                    ce.PK = pedidoClone.PK_COSTO_ENVIO;
                    
                    if (ce.getCostoByPk()) {
                        Total += ((ce.COSTO) - (ce.COSTO * pedidoClone.DESCUENTO / 100));
                    } else {
                        Total += ce.COSTO;
                    }
                    
                    double factor = double.Parse(VariablesModel.getVariableValue("FACTOR"));
                    double suma = double.Parse(VariablesModel.getVariableValue("SUMA"));
                    double COMISION_TARJETA = 0;

                    if (factor > 0)
                    {
                        COMISION_TARJETA = Total * factor;
                    }
                    COMISION_TARJETA += suma;
                    pedidoClone.COMISION_TARJETA = COMISION_TARJETA;
                    pedidoClone.TOTAL = Total+COMISION_TARJETA;
                }


                if (pedidoClone.agregar())
                {

                    try {

                        PedidosRecibidosModel prm = new PedidosRecibidosModel();
                        prm.PK_PEDIDO = long.Parse(pedidoClone.PK);
                        prm.RECIBIDO = "{PK:"+pedidom.PK+",PK_CLIENTE:"+pedidom.PK_CLIENTE
                            + ",DIRECCION:\"" + pedidom.DIRECCION+ "\",LATITUD:\"" + pedidom.LATITUD
                            + "\",LONGITUD:\"" + pedidom.LONGITUD+ "\",SUBTOTAL:" + pedidom.SUBTOTAL
                            +",ENVIO:"+pedidom.ENVIO+",COMISION_TARJETA:"+pedidom.COMISION_TARJETA
                            +",TOTAL:"+pedidom.TOTAL+ ",METODO_PAGO:\"" + pedidom.METODO_PAGO
                            + "\",PK_TIENDA:" + pedidom.PK_TIENDA+ ",PK_COSTO_ENVIO:" + pedidom.PK_COSTO_ENVIO
                            + ",CODIGO_DESCUENTO:" + pedidom.CODIGO_DESCUENTO+ ",DESCUENTO:" + pedidom.DESCUENTO
                            + ",LISTA:[" + listaProductosRecibidos+"]"
                            +"}";
                        prm.CALCULADO = "{PK:"+pedidoClone.PK+",PK_CLIENTE:"+pedidoClone.PK_CLIENTE
                            + ",DIRECCION:\"" + pedidoClone.DIRECCION+ "\",LATITUD:\"" + pedidoClone.LATITUD
                            + "\",LONGITUD:\"" + pedidoClone.LONGITUD+ "\",SUBTOTAL:" + pedidoClone.SUBTOTAL
                            +",ENVIO:"+pedidoClone.ENVIO+",COMISION_TARJETA:"+pedidoClone.COMISION_TARJETA
                            +",TOTAL:"+pedidoClone.TOTAL+ ",METODO_PAGO:\"" + pedidoClone.METODO_PAGO
                            + "\",PK_TIENDA:" + pedidoClone.PK_TIENDA+ ",PK_COSTO_ENVIO:" + pedidoClone.PK_COSTO_ENVIO
                            + ",CODIGO_DESCUENTO:" + pedidoClone.CODIGO_DESCUENTO+ ",DESCUENTO:" + pedidoClone.DESCUENTO
                            + ",LISTA:[" + listaProductosRecibidos + "]"
                            + "}";
                        prm.agregar();
                    } catch(Exception exc) { 
                        LogModel.registra("Error al registrar PedidosRecibidos AgregarPedidoController", exc.ToString()); 
                    }

                    try {
                        DireccionesClientesModel dir = new DireccionesClientesModel();
                        dir.LATITUD = pedidoClone.LATITUD;
                        dir.LONGITUD = pedidoClone.LONGITUD;
                        dir.PK_CLIENTE = pedidoClone.PK_CLIENTE;
                        dir.DIRECCION = pedidoClone.DIRECCION;

                        if (dir.existeDireccion())
                        {
                            dir.CONTADOR++;
                            dir.actualizaContador();
                        }
                        else {
                            dir.CONTADOR = 1;
                            dir.creaDireccion();
                        }
                    } catch (Exception e) {
                        LogModel.registra("Error al guardar dirección AgregarPedidoController", e.ToString());
                    }

                    if (!pedidoClone.SOURCE_ID.Equals("EFECTIVO") && !pedidoClone.SOURCE_ID.Equals("TERMINAL"))
                    {
                        if (pedidoClone.pagar())
                        {
                            try
                            {
                                NotificacionesModel notifica = new NotificacionesModel();
                                TiendasModel tienda = new TiendasModel();
                                tienda.PK = pedidoClone.PK_TIENDA;
                                ClientesModel cliente = new ClientesModel();
                                cliente.PK = pedidoClone.PK_CLIENTE;

                                if (tienda.obtenerTiendaPorPK() && cliente.getTokenClienteByPk())
                                {
                                    notifica.TITLE = "Nuevo pedido";
                                    notifica.MESSAGE = cliente.NOMBRE + " ha registrado un nuevo pedido: " + pedidoClone.PK;
                                    notifica.TOKENS.Add(tienda.TOKEN);
                                    await notifica.sendNotificationAsync();
                                }
                            }
                            catch (Exception e)
                            {
                                LogModel.registra("Error al enviar notificación", e.ToString());
                            }

                            result = 1;
                            msj = "¡Pedido agregado!";
                        }
                        else
                        {
                            result = 0;
                            if (!string.IsNullOrEmpty(pedidoClone.ERROR))
                            {
                                msj = pedidoClone.ERROR;
                            }
                            else
                            {
                                msj = "¡Ocurriò un error al procesar pago intente más tarde!";
                            }
                        }
                    }
                    else
                    {

                        try
                        {
                            NotificacionesModel notifica = new NotificacionesModel();
                            TiendasModel tienda = new TiendasModel();
                            tienda.PK = pedidoClone.PK_TIENDA;
                            ClientesModel cliente = new ClientesModel();
                            cliente.PK = pedidoClone.PK_CLIENTE;

                            if (tienda.obtenerTiendaPorPK() && cliente.getTokenClienteByPk())
                            {
                                notifica.TITLE = "Nuevo pedido";
                                notifica.MESSAGE = cliente.NOMBRE + " ha registrado un nuevo pedido: " + pedidoClone.PK;
                                notifica.TOKENS.Add(tienda.TOKEN);
                                await notifica.sendNotificationAsync();
                            }
                        }
                        catch (Exception e)
                        {
                            LogModel.registra("Error al enviar notificación", e.ToString());
                        }

                        result = 1;
                        msj = "¡Pedido agregado!";
                    }
                }

            }
            catch(Exception e)
            {
                result = 0;
                msj = "¡Error al agregar pedido intente más tarde!";
                LogModel.registra("Error al agregar pedido", e.ToString());
            }
            
            return Ok(new
            {
                resultado = result,
                mensaje = msj,
                pedido = pedidoClone,
                entrega=fecha_entrega
            });
        }

        public DateTime obtenerFechaEntrega(TiendasModel tiendam,string PK_POLIGONO){

            DayOfWeek dia= new DayOfWeek();

            if(PK_POLIGONO.Equals(tiendam.LUNES.ToString())) {
                dia = DayOfWeek.Monday;
            }
            else if(PK_POLIGONO.Equals(tiendam.MARTES.ToString())) {
                dia = DayOfWeek.Tuesday;
            }
            else if(PK_POLIGONO.Equals(tiendam.MIERCOLES.ToString())) {
                dia = DayOfWeek.Wednesday;
            }
            else if(PK_POLIGONO.Equals(tiendam.JUEVES.ToString())) {
                dia = DayOfWeek.Thursday;
            }
            else if(PK_POLIGONO.Equals(tiendam.VIERNES.ToString())) {
                dia = DayOfWeek.Friday;
            }
            else if(PK_POLIGONO.Equals(tiendam.SABADO.ToString())) {
                dia = DayOfWeek.Saturday;
            }
            else if(PK_POLIGONO.Equals(tiendam.DOMINGO.ToString())) {
                dia = DayOfWeek.Sunday;
            }

            double valor = Double.Parse(VariablesModel.getVariableValue("HORAS_GRACIA"));
            DateTime date = DateTime.Now.AddHours(valor);

            if ((int)dia == (int)date.DayOfWeek) {
                date=date.AddDays(1);
            }
            while (date.DayOfWeek != dia) {
                date = date.AddDays(1);
            }

            return date;

        }


    }
}