using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Pedidos
{
    public class PedidoDetalleModel
    {
		public string PK { get; set; }
	public string PK_PEDIDO{get;set;}
	public string PK_PRODUCTO{get;set;}
	public string PRODUCTO{get;set;}
	public string DESCRIPCION{get;set;}
	public string TIENDA{get;set;}
	public double PRECIO{get;set;}
	public double CANTIDAD{get;set;}
	public string DETALLES{get;set;}
	public string IMAGEN{get;set;}
	public string BORRADO{get;set;}
	public string FECHA_C{get;set;}
	public string FECHA_M{get;set;}
	public string FECHA_D{get;set;}
	public string USUARIO_C{get;set;}
	public string USUARIO_M{get;set;}
	public string USUARIO_D{get;set;}
		private database db;
		public PedidoDetalleModel() {
			db = new database();
		}
		public bool agregar() {
			try
			{
				string sql = "INSERT INTO PEDIDO_DETALLE (PK_PEDIDO,PK_PRODUCTO,PRECIO,CANTIDAD,DETALLES) " +
							 "VALUES(@PK_PEDIDO,@PK_PRODUCTO,@PRECIO,@CANTIDAD,@DETALLES)";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_PEDIDO", PK_PEDIDO);
				db.command.Parameters.AddWithValue("@PK_PRODUCTO", PK_PRODUCTO);
				db.command.Parameters.AddWithValue("@PRECIO", PRECIO);
				db.command.Parameters.AddWithValue("@CANTIDAD", CANTIDAD);
				db.command.Parameters.AddWithValue("@DETALLES", DETALLES);

				PK = db.executeId();

				if (!string.IsNullOrEmpty(PK))
				{
					return true;
				}

			}
			catch { }

			return false;

		}
		public List<PedidoDetalleModel> getByPkPedido() {
			List<PedidoDetalleModel> pedidos = new List<PedidoDetalleModel>();
			PedidoDetalleModel aux;
			try
			{
				string sql = "SELECT * FROM VPEDIDO_DETALLE WHERE PK_PEDIDO=@PK_PEDIDO";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_PEDIDO", PK_PEDIDO);

				ResultSet res = db.getTable();

				while (res.Next())
				{
					aux = new PedidoDetalleModel();
					aux.PK = res.Get("PK");
					aux.PK_PEDIDO = res.Get("PK_PEDIDO");
					aux.PK_PRODUCTO = res.Get("PK_PRODUCTO");
					aux.PRODUCTO = res.Get("PRODUCTO");
					aux.DESCRIPCION = res.Get("DESCRIPCION");
					aux.TIENDA = res.Get("NOMBRE");
					aux.PRECIO = res.GetDouble("PRECIO");
					aux.CANTIDAD = res.GetDouble("CANTIDAD");
					aux.DETALLES = res.Get("DETALLES");
					aux.IMAGEN = res.Get("IMAGEN");
					aux.BORRADO = res.Get("BORRADO");
					aux.FECHA_C = res.Get("FECHA_C");
					aux.FECHA_M = res.Get("FECHA_M");
					aux.FECHA_D = res.Get("FECHA_D");
					aux.USUARIO_C = res.Get("USUARIO_C");
					aux.USUARIO_M = res.Get("USUARIO_M");
					aux.USUARIO_D = res.Get("USUARIO_D");
					pedidos.Add(aux);
				}

			}
			catch { }

			return pedidos;

		}
	}
}
