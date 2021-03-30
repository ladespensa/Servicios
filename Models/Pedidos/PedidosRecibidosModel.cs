using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Pedidos
{
    public class PedidosRecibidosModel
    {
        public long PK { get; set; }
        public long PK_PEDIDO { get; set; }
        public string RECIBIDO { get; set; }
        public string CALCULADO { get; set; }

		private database db;
		public PedidosRecibidosModel()
		{
			db = new database();
		}
		public bool agregar()
		{

			try
			{

				string sql = "INSERT INTO PEDIDOS_RECIBIDOS (PK_PEDIDO,RECIBIDO,CALCULADO) "
							+ "VALUES(@PK_PEDIDO,@RECIBIDO,@CALCULADO) ";

				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK_PEDIDO", PK_PEDIDO);
				db.command.Parameters.AddWithValue("@RECIBIDO", RECIBIDO);
				db.command.Parameters.AddWithValue("@CALCULADO", CALCULADO);

				if (db.execute()) {
					return true;
				}

			}
			catch { }

			return false;
		}

	}
}
