using ConnectDB;
using Openpay;
using Openpay.Entities;
using Openpay.Entities.Request;
using acmarkert.Models.Cientes;
using acmarkert.Models.Pedidos;
using acmarkert.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace acmarkert.Models
{
	public class PedidosModel
	{
		public string PK { get; set; }
		public string PK_CLIENTE { get; set; }
		public string CLIENTE { get; set; }
		public string TELEFONO_CLIENTE { get; set; }
		public string PK_TIENDA { get; set; }
		public string TIENDA { get; set; }
		public string IMAGEN_TIENDA { get; set; }
		public string DIRECCION { get; set; }
		public string DIRECCION_TIENDA { get; set; }
		public string LATITUD { get; set; }
		public string LONGITUD { get; set; }
		public string LATITUD_TIENDA { get; set; }
		public string LONGITUD_TIENDA { get; set; }
		public string PK_REPARTIDOR { get; set; }
		public string PK_ESTATUS { get; set; }
		public string ESTATUS { get; set; }
		public string BORRADO { get; set; }
		public double SUBTOTAL { get; set; }
		public double ENVIO { get; set; }
		public double COMISION_TARJETA { get; set; }
		public double TOTAL { get; set; }
		public string METODO_PAGO { get; set; }
		public string PAGO_EFECTIVO { get; set; }
		public string FECHA_PAGO_EFECTIVO { get; set; }
		public string COSTUMER_ID { get; set; }
		public string SOURCE_ID { get; set; }
		public string DEVICE_SESSION_ID { get; set; }
		public string CVV2 { get; set; }
		public string PK_POLIGONO { get; set; }
		public string FECHA_C { get; set; }
		public string FECHA_M { get; set; }
		public string FECHA_D { get; set; }
		public string USUARIO_C { get; set; }
		public string USUARIO_M { get; set; }
		public string USUARIO_D { get; set; }
		public string ERROR { get; set; }
		public string REPARTIDOR { get; set; }
		public string PK_COSTO_ENVIO { get; set; }
		public string FECHA_ENTREGA { get; set; }
		public int DESCUENTO { get; set; }
		public string CODIGO_DESCUENTO { get; set; }
		public int CANTIDAD_DECUENTOS { get; set; }

		private OpenpayAPI api = null;
		public CargosModel cargo { get; set; }
		public List<PedidoDetalleModel> LISTA { get; set; }

		private database db;
		public PedidosModel()
		{
			db = new database();
		}
		public bool agregar()
		{

			try
			{
				string sql = "INSERT INTO PEDIDOS (PK_CLIENTE,PK_TIENDA,DIRECCION,LATITUD,LONGITUD,ENVIO," +
							 "SUBTOTAL,COMISION_TARJETA,TOTAL,METODO_PAGO,PK_COSTO_ENVIO,FECHA_ENTREGA";
				
				if (!string.IsNullOrEmpty(CODIGO_DESCUENTO)) { sql += ",CODIGO_DESCUENTO,DESCUENTO)"; }
				else { sql += ") "; }
				sql += "VALUES(@PK_CLIENTE,@PK_TIENDA,@DIRECCION,@LATITUD,@LONGITUD,@ENVIO,@SUBTOTAL" +
						",@COMISION_TARJETA,@TOTAL,@METODO_PAGO,@PK_COSTO_ENVIO,@FECHA_ENTREGA";

				if (!string.IsNullOrEmpty(CODIGO_DESCUENTO)) {sql+=",@CODIGO_DESCUENTO,@DESCUENTO)";}
				else {sql += ")";}

				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
				db.command.Parameters.AddWithValue("@DIRECCION", DIRECCION);
				db.command.Parameters.AddWithValue("@LATITUD", LATITUD);
				db.command.Parameters.AddWithValue("@LONGITUD", LONGITUD);
				db.command.Parameters.AddWithValue("@PK_TIENDA", PK_TIENDA);
				db.command.Parameters.AddWithValue("@ENVIO", ENVIO);
				db.command.Parameters.AddWithValue("@SUBTOTAL", SUBTOTAL);
				db.command.Parameters.AddWithValue("@COMISION_TARJETA", COMISION_TARJETA);
				db.command.Parameters.AddWithValue("@TOTAL", TOTAL);
				db.command.Parameters.AddWithValue("@METODO_PAGO", METODO_PAGO);
				db.command.Parameters.AddWithValue("@PK_COSTO_ENVIO", PK_COSTO_ENVIO);
				db.command.Parameters.AddWithValue("@FECHA_ENTREGA", FECHA_ENTREGA);
				if (!string.IsNullOrEmpty(CODIGO_DESCUENTO))
				{
					db.command.Parameters.AddWithValue("@CODIGO_DESCUENTO", CODIGO_DESCUENTO);
					db.command.Parameters.AddWithValue("@DESCUENTO", DESCUENTO);
				}
				PK = db.executeId();

				if (!string.IsNullOrEmpty(PK))
				{
					if (LISTA != null)
					{
						foreach (PedidoDetalleModel detalle in LISTA)
						{
							detalle.PK_PEDIDO = PK;
							detalle.agregar();
						}
					}
					return true;
				}

			}
			catch { }

			return false;
		}

		public bool obtenerPedidoByPk()
		{
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK=@PK AND BORRADO=0";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK", PK);
				ResultSet res = db.getTable();
				if (res.Next())
				{
					PK = res.Get("PK");
					PK_CLIENTE = res.Get("PK_CLIENTE");
					CLIENTE = res.Get("CLIENTE");
					TELEFONO_CLIENTE = res.Get("TELEFONO"); 
					PK_TIENDA = res.Get("PK_TIENDA");
					TIENDA = res.Get("TIENDA");
					DIRECCION_TIENDA = res.Get("TIENDA_DIRECCION");
					LATITUD_TIENDA = res.Get("LATITUD_TIENDA");
					LONGITUD_TIENDA = res.Get("LONGITUD_TIENDA");
					DIRECCION = res.Get("DIRECCION");
					LATITUD = res.Get("LATITUD");
					LONGITUD = res.Get("LONGITUD");
					PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					REPARTIDOR = res.Get("REPARTIDOR");
					PK_ESTATUS = res.Get("PK_ESTATUS");
					ESTATUS = res.Get("ESTATUS");
					SUBTOTAL = res.GetDouble("SUBTOTAL");
					ENVIO = res.GetDouble("ENVIO");
					COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					TOTAL = res.GetDouble("TOTAL");
					METODO_PAGO = res.Get("METODO_PAGO");
					PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					BORRADO = res.Get("BORRADO");
					FECHA_C = res.Get("FECHA_C");
					FECHA_M = res.Get("FECHA_M");
					FECHA_D = res.Get("FECHA_D");
					USUARIO_C = res.Get("USUARIO_C");
					USUARIO_M = res.Get("USUARIO_M");
					USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = PK;
					LISTA = aux.getByPkPedido();
					if (METODO_PAGO.Equals("T"))
					{
						cargo = new CargosModel();
						cargo.PK_PEDIDO = PK;
						cargo.obtenerCargoByPK();
					}
					return true;
				}

			}
			catch { }

			return false;
		}
		public bool obtenerPedidoByPkSinDetalle()
		{
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK=@PK AND BORRADO=0";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK", PK);
				ResultSet res = db.getTable();
				if (res.Next())
				{
					PK = res.Get("PK");
					PK_CLIENTE = res.Get("PK_CLIENTE");
					CLIENTE = res.Get("CLIENTE"); 
					PK_TIENDA = res.Get("PK_TIENDA");
					TIENDA = res.Get("TIENDA");
					DIRECCION = res.Get("DIRECCION");
					LATITUD = res.Get("LATITUD");
					LONGITUD = res.Get("LONGITUD");
					PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					REPARTIDOR = res.Get("REPARTIDOR");
					PK_ESTATUS = res.Get("PK_ESTATUS");
					ESTATUS = res.Get("ESTATUS");
					SUBTOTAL = res.GetDouble("SUBTOTAL");
					ENVIO = res.GetDouble("ENVIO");
					COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					TOTAL = res.GetDouble("TOTAL");
					METODO_PAGO = res.Get("METODO_PAGO");
					PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					BORRADO = res.Get("BORRADO");
					FECHA_C = res.Get("FECHA_C");
					FECHA_M = res.Get("FECHA_M");
					FECHA_D = res.Get("FECHA_D");
					USUARIO_C = res.Get("USUARIO_C");
					USUARIO_M = res.Get("USUARIO_M");
					USUARIO_D = res.Get("USUARIO_D");
					return true;
				}

			}
			catch { }

			return false;
		}
		
		public List<PedidosModel> obtenerPedidosByPkCliente()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK_CLIENTE=@PK_CLIENTE AND BORRADO=0";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE"); 
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();			

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}
		public List<PedidosModel> obtenerPedidosByPkClienteNoEntregados()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = @"SELECT * FROM VPEDIDOS 
							WHERE PK_CLIENTE = @PK_CLIENTE AND BORRADO = 0
							and(convert(varchar(10), FECHA_ENTREGA, 120) >= convert(varchar(10), getdate(), 120) and PK_ESTATUS < 5)
							order by FECHA_ENTREGA desc
							";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
				ResultSet res = db.getTable();
				while (res.Next())
				{

					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE");
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					try
					{
						pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + res.GetDateTime("FECHA_ENTREGA").ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
					}
					catch (Exception e) { }
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}
		public List<PedidosModel> obtenerPedidosByPkClienteEntregados()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = @"SELECT * FROM VPEDIDOS 
							WHERE PK_CLIENTE=@PK_CLIENTE AND BORRADO=0 
							and (convert(varchar(10),FECHA_ENTREGA,120)<convert(varchar(10),getdate(),120) OR PK_ESTATUS=5)
							order by FECHA_ENTREGA desc";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
				ResultSet res = db.getTable();
				while (res.Next())
				{

					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE");
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					try
					{
						pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + res.GetDateTime("FECHA_ENTREGA").ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
					}
					catch (Exception e) { }
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}
		public List<PedidosModel> obtenerPedidosByPkClienteHoy()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK_CLIENTE=@PK_CLIENTE AND BORRADO=0 and convert(varchar(10),FECHA_C,120)=convert(varchar(10),getdate(),120) order by FECHA_C desc";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE"); 
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					try
					{
						pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + res.GetDateTime("FECHA_ENTREGA").ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
					}
					catch (Exception e) { }
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();			

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}
		public List<PedidosModel> obtenerPedidosByPkClienteAntesDeHoy()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK_CLIENTE=@PK_CLIENTE AND BORRADO=0 and convert(varchar(10),FECHA_C,120)<convert(varchar(10),getdate(),120) order by FECHA_C desc";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE"); 
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					try
					{
						pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + res.GetDateTime("FECHA_ENTREGA").ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
						//pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("yyyy-MM-dd");
					}
					catch (Exception e) { }
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();			

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}

		public List<PedidosModel> obtenerPedidosByPkTienda()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK_TIENDA=@PK_TIENDA AND PK_ESTATUS<5 AND BORRADO=0 ORDER BY FECHA_C DESC";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_TIENDA", PK_TIENDA);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE");
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();			

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}
		public List<PedidosModel> obtenerPedidosPasadosByPkTienda()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK_TIENDA=@PK_TIENDA AND PK_ESTATUS=5 AND BORRADO=0 AND FECHA_ENTREGA=@FECHA_ENTREGA ORDER BY FECHA_C DESC";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_TIENDA", PK_TIENDA);
				db.command.Parameters.AddWithValue("@FECHA_ENTREGA", FECHA_ENTREGA);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE");
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();			

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}
		public List<PedidosModel> obtenerPedidosByPkRepartidor()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK_REPARTIDOR=@PK_REPARTIDOR AND PK_ESTATUS<5 AND BORRADO=0 ORDER BY FECHA_C DESC";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_REPARTIDOR", PK_REPARTIDOR);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE");
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.DIRECCION_TIENDA = res.Get("TIENDA_DIRECCION");
					pedido.LATITUD_TIENDA = res.Get("LATITUD_TIENDA");
					pedido.LONGITUD_TIENDA = res.Get("LONGITUD_TIENDA");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					try
					{
						pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + res.GetDateTime("FECHA_ENTREGA").ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
						//pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("yyyy-MM-dd");
					}
					catch (Exception e) { }
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();			

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}
		public List<PedidosModel> obtenerPedidosByPkRepartidorEntregados()
		{
			List<PedidosModel> lista = new List<PedidosModel>();
			PedidosModel pedido;
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDOS WHERE PK_REPARTIDOR=@PK_REPARTIDOR AND PK_ESTATUS=5 AND BORRADO=0 AND FECHA_ENTREGA=@FECHA_ENTREGA ORDER BY FECHA_C DESC";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_REPARTIDOR", PK_REPARTIDOR);
				db.command.Parameters.AddWithValue("@FECHA_ENTREGA", FECHA_ENTREGA);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					pedido = new PedidosModel();
					pedido.PK = res.Get("PK");
					pedido.PK_CLIENTE = res.Get("PK_CLIENTE");
					pedido.CLIENTE = res.Get("CLIENTE");
					pedido.PK_TIENDA = res.Get("PK_TIENDA");
					pedido.DIRECCION = res.Get("DIRECCION");
					pedido.DIRECCION_TIENDA = res.Get("TIENDA_DIRECCION");
					pedido.LATITUD_TIENDA = res.Get("LATITUD_TIENDA");
					pedido.LONGITUD_TIENDA = res.Get("LONGITUD_TIENDA");
					pedido.LATITUD = res.Get("LATITUD");
					pedido.LONGITUD = res.Get("LONGITUD");
					pedido.PK_REPARTIDOR = res.Get("PK_REPARTIDOR");
					pedido.REPARTIDOR = res.Get("REPARTIDOR");
					pedido.PK_ESTATUS = res.Get("PK_ESTATUS");
					pedido.ESTATUS = res.Get("ESTATUS");
					pedido.SUBTOTAL = res.GetDouble("SUBTOTAL");
					pedido.ENVIO = res.GetDouble("ENVIO");
					pedido.COMISION_TARJETA = res.GetDouble("COMISION_TARJETA");
					pedido.TOTAL = res.GetDouble("TOTAL");
					pedido.METODO_PAGO = res.Get("METODO_PAGO");
					pedido.PAGO_EFECTIVO = res.Get("PAGO_EFECTIVO");
					pedido.FECHA_PAGO_EFECTIVO = res.Get("FECHA_PAGO_EFECTIVO");
					pedido.TIENDA = res.Get("TIENDA");
					pedido.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
					try
					{
						pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + res.GetDateTime("FECHA_ENTREGA").ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
						//pedido.FECHA_ENTREGA = res.GetDateTime("FECHA_ENTREGA").ToString("yyyy-MM-dd");
					}
					catch (Exception e) { }
					pedido.BORRADO = res.Get("BORRADO");
					pedido.FECHA_C = res.Get("FECHA_C");
					pedido.FECHA_M = res.Get("FECHA_M");
					pedido.FECHA_D = res.Get("FECHA_D");
					pedido.USUARIO_C = res.Get("USUARIO_C");
					pedido.USUARIO_M = res.Get("USUARIO_M");
					pedido.USUARIO_D = res.Get("USUARIO_D");
					aux = new PedidoDetalleModel();
					aux.PK_PEDIDO = pedido.PK;
					pedido.LISTA = aux.getByPkPedido();			

					lista.Add(pedido);
				}

			}
			catch { }

			return lista;
		}

		public bool pagar()
		{


			try
			{

				ClientesModel cliente = new ClientesModel();
				cliente.PK = PK_CLIENTE;
				cliente.getClienteByPk();
				if (!String.IsNullOrEmpty(cliente.OPENID))
				{
					if (Resources.DEVELOP.ToUpper().Equals("TRUE"))
					{
						api = new OpenpayAPI(Resources.SK_OPEN_PAY_DEV, Resources.ID_OPEN_PAY_DEV, false);
					}
					else
					{
						api = new OpenpayAPI(Resources.SK_OPEN_PAY, Resources.ID_OPEN_PAY, true);
					}

					ChargeRequest request = new ChargeRequest();
					Customer customer = new Customer();
					customer.Name = cliente.NOMBRE;
					customer.LastName = cliente.APELLIDOS;
					customer.PhoneNumber = cliente.TELEFONO;
					customer.Email = cliente.CORREO;

					request.Method = "card";
					request.SourceId = SOURCE_ID;
					request.Amount = new Decimal(TOTAL);
					request.Currency = "MXN";
					request.Description = "Pedido polar " + PK;
					request.OrderId = PK;
					request.DeviceSessionId = DEVICE_SESSION_ID;
					//request.Customer = customer;>>>>>>>>>>>>
					request.Cvv2 = CVV2;
					try
					{
						Charge charge = api.ChargeService.Create(cliente.OPENID, request);

						cargo = new CargosModel();
						cargo.ID = charge.Id;
						cargo.AUTHORIZATION = charge.Authorization;
						cargo.AMOUNT = (double)charge.Amount;
						cargo.METHOD = charge.Method;
						cargo.ESTATUS = charge.Status;
						cargo.PK_PEDIDO = PK;
						cargo.ERROR_MESSAGE = charge.ErrorMessage;

						cargo.agregarCargo();

						if (cargo.ESTATUS.Equals("completed"))
						{
							return true;
						}
						else
						{
							ERROR = charge.ErrorMessage;
							delete();
						}
					}
					catch (OpenpayException e) {
						LogModel.registra("Error al pagar", e.ToString());
						CargosErroresModel aux1 = new CargosErroresModel();
						aux1.category = e.Category;
						aux1.description = e.Description;
						aux1.http_code = e.HResult;
						aux1.request_id = e.RequestId;
						aux1.PK_PEDIDO = PK;
						aux1.agregarCargoError();
						ERROR = ErroresOpenPayModel.error(e.ErrorCode);
						delete();
					}
					catch (Exception ex) {
						LogModel.registra("Error al pagar", ex.ToString());
						ERROR = ex.Message;
						delete();
					}
				}

			}
			catch (Exception e)
			{
				LogModel.registra("Error al pagar", e.ToString());
				ERROR = e.Message;
			}
			return false;
		}

		public bool delete()
		{

			try
			{
				string sql = "UPDATE PEDIDOS SET BORRADO=1 WHERE PK=@PK";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK", PK);
				return db.execute();
			}
			catch (Exception e)
			{
				LogModel.registra("Error al borrar pedido", e.ToString());
			}

			return false;
		}
		public bool setEstatus()
		{

			try
			{
				string sql = "UPDATE PEDIDOS SET PK_ESTATUS=@PK_ESTATUS WHERE PK=@PK";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK", PK);
				db.command.Parameters.AddWithValue("@PK_ESTATUS", PK_ESTATUS);
				return db.execute();
			}
			catch (Exception e)
			{
				LogModel.registra("Error al actualizar estatus", e.ToString());
			}

			return false;
		}

		public bool obtenerCantidadCodigosUsados() {

			try {
				string sql = "SELECT count(PK) as MAX FROM PEDIDOS WHERE CODIGO_DESCUENTO = @CODIGO_DESCUENTO AND PK_CLIENTE=@PK_CLIENTE";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@CODIGO_DESCUENTO", CODIGO_DESCUENTO);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
				ResultSet res = db.getTable();
				if (res.Next()) {
					CANTIDAD_DECUENTOS = res.GetInt("MAX");
					return true;
				}
			}catch(Exception e){ }

			return false;
		}

		public int obtenerCantidadPedidosByPkCliente()
		{
			int cantidad=0;
			try
			{
				string sql = "SELECT count(*)MAX FROM PEDIDOS WHERE PK_CLIENTE=@PK_CLIENTE AND BORRADO=0";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
				ResultSet res = db.getTable();
				if (res.Next()) {
					cantidad=res.GetInt("MAX");
				}

			}
			catch { }

			return cantidad;
		}

		public bool faltaCalificacion()
		{
			
			try
			{
				string sql = "SELECT top(1)PE.PK,EN.id " +
						     "FROM PEDIDOS PE " +
							 "left JOIN ENCUESTA EN ON(EN.id_pedido= PE.PK) " +
							 "where PE.PK_CLIENTE= @PK_CLIENTE and PE.PK_ESTATUS = 5 " +
							 "order by PE.FECHA_ENTREGA desc";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);

				ResultSet res = db.getTable();
				if (res.Next())
				{
					PK = res.Get("PK");
					string id_encuesta = res.Get("id");
					return string.IsNullOrEmpty(id_encuesta) || id_encuesta.Equals("0"); 
				}

			}
			catch { }

			return false;
		}

	}
}
