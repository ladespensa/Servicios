using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.EstatusPedidos
{
    public class EstatusPedidosModel
    {
        public string PK { get; set; }
        public string ESTATUS { get; set; }
        public string BORRADO { get; set; }
        public string FECHA_C { get; set; }
        public string FECHA_M { get; set; }
        public string FECHA_D { get; set; }
        public string USUARIO_C { get; set; }
        public string USUARIO_M { get; set; }
        public string USUARIO_D { get; set; }
        database db;

        public EstatusPedidosModel() {
            db = new database();
        }

        public List<EstatusPedidosModel> obtenerEstatusPedidos() {
			List<EstatusPedidosModel> lista = new List<EstatusPedidosModel>();
			EstatusPedidosModel estatus;
			try
			{
				string sql = "SELECT * FROM ESTATUS_PEDIDOS";
				db.PreparedSQL(sql);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					estatus = new EstatusPedidosModel();
					estatus.PK = res.Get("PK");
					estatus.ESTATUS= res.Get("ESTATUS");
					estatus.BORRADO = res.Get("BORRADO");
					estatus.FECHA_C = res.Get("FECHA_C");
					estatus.FECHA_M = res.Get("FECHA_M");
					estatus.FECHA_D = res.Get("FECHA_D");
					estatus.USUARIO_C = res.Get("USUARIO_C");
					estatus.USUARIO_M = res.Get("USUARIO_M");
					estatus.USUARIO_D = res.Get("USUARIO_D");
					lista.Add(estatus);
				}

			}
			catch { }

			return lista;
		}
		
		public List<EstatusPedidosModel> obtenerEstatusPedidosRepartidor() {
			List<EstatusPedidosModel> lista = new List<EstatusPedidosModel>();
			EstatusPedidosModel estatus;
			try
			{
				string sql = "SELECT * FROM ESTATUS_PEDIDOS " +
							 "WHERE NOT ESTATUS IN('Cancelado','Reembolso','Nuevo')";
				db.PreparedSQL(sql);
				ResultSet res = db.getTable();
				while (res.Next())
				{
					estatus = new EstatusPedidosModel();
					estatus.PK = res.Get("PK");
					estatus.ESTATUS= res.Get("ESTATUS");
					estatus.BORRADO = res.Get("BORRADO");
					estatus.FECHA_C = res.Get("FECHA_C");
					estatus.FECHA_M = res.Get("FECHA_M");
					estatus.FECHA_D = res.Get("FECHA_D");
					estatus.USUARIO_C = res.Get("USUARIO_C");
					estatus.USUARIO_M = res.Get("USUARIO_M");
					estatus.USUARIO_D = res.Get("USUARIO_D");
					lista.Add(estatus);
				}

			}
			catch { }

			return lista;
		}

    }
}
