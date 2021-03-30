using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Tiendas
{
    public class TiendasTotalesModel
    {
        public string PK_TIENDA { get; set; }
        public double NO_PAGADO { get; set; }
        public int NUM_PEDIDOS { get; set; }
        public List<encabezadoPagado> ListaPagado { get; set; }
        private database db;
        public TiendasTotalesModel() {
            db = new database();
            ListaPagado = new List<encabezadoPagado>();
        }

        public bool obtenerPagadoListAndMontoNoPagado() {

            encabezadoPagado aux;
            try {

                string sql = @"SELECT PK_TIENDA,SUM(SUBTOTAL)MONTO,FOLIO_PAGO,FECHA_PAGO_TIENDA,count(PK) NO_PEDIDOS
                            from PEDIDOS
                            WHERE PK_TIENDA =@PK_TIENDA AND PK_ESTATUS=5 AND BORRADO=0
                            GROUP BY PK_TIENDA, FOLIO_PAGO, FECHA_PAGO_TIENDA";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_TIENDA",PK_TIENDA);
                ResultSet res = db.getTable();
                while (res.Next()) {
                    aux = new encabezadoPagado();
                    aux.PK_TIENDA = res.Get("PK_TIENDA");
                    aux.MONTO = res.GetDouble("MONTO");
                    aux.FOLIO_PAGO = res.Get("FOLIO_PAGO");
                    aux.FECHA_PAGO_TIENDA = res.Get("FECHA_PAGO_TIENDA");
                    aux.NUM_PEDIDOS = res.GetInt("NO_PEDIDOS");
                    if (string.IsNullOrEmpty(aux.FOLIO_PAGO))
                    {
                        NO_PAGADO = aux.MONTO;
                        NUM_PEDIDOS = aux.NUM_PEDIDOS;
                    }
                    else {
                        ListaPagado.Add(aux);
                    }
                }
                return true;
            } catch (Exception e) { LogModel.registra("Error al obtener pago list and monto no pagado",e.ToString()); }

            return false;
        }

    }
    public class encabezadoPagado { 

        public string PK_TIENDA { get; set; }
        public double MONTO { get; set; }
        public string FOLIO_PAGO { get; set; }
        public string FECHA_PAGO_TIENDA { get; set; }
        public int NUM_PEDIDOS { get; set; }
    

    }
}
