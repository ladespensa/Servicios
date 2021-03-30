using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Tiendas
{
    public class TiendasTotalesDetallesModel
    {
        public string PK_TIENDA { get; set; }
        public string FOLIO_PAGO { get; set; }
        public double MONTO { get; set; }
        public int NUM_PEDIDOS { get; set; }
        public string FECHA_PAGO_TIENDA { get; set; }
        public List<PedidosModel> ListaPedidos { get; set; }
        private database db;
        public TiendasTotalesDetallesModel() {
            db = new database();
            ListaPedidos = new List<PedidosModel>();
        }
        public bool obtenerPagadoDetalleByFolio()
        {

            PedidosModel aux;
            try
            {

                string sql = @"SELECT PK,PK_TIENDA,SUM(SUBTOTAL)MONTO,FOLIO_PAGO,FECHA_PAGO_TIENDA
                            from PEDIDOS
                            WHERE PK_TIENDA =@PK_TIENDA AND FOLIO_PAGO=@FOLIO_PAGO AND PK_ESTATUS=5 AND BORRADO=0
                            GROUP BY PK_TIENDA, FOLIO_PAGO, FECHA_PAGO_TIENDA,PK";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_TIENDA", PK_TIENDA);
                db.command.Parameters.AddWithValue("@FOLIO_PAGO", FOLIO_PAGO);
                ResultSet res = db.getTable();
                while (res.Next())
                {
                    PK_TIENDA = res.Get("PK_TIENDA");
                    FECHA_PAGO_TIENDA = res.Get("FECHA_PAGO_TIENDA");
                    aux = new PedidosModel();
                    aux.PK = res.Get("PK");
                    MONTO += res.GetDouble("MONTO");
                    if (aux.obtenerPedidoByPkSinDetalle()) {
                        ListaPedidos.Add(aux);
                        NUM_PEDIDOS += 1;
                    }

                }
                return true;
            }
            catch (Exception e) { LogModel.registra("Error al obtener total detalle list", e.ToString()); }

            return false;
        }

    }
}
