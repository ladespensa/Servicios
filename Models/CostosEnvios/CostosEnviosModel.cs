using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.CostosEnvios
{
    public class CostosEnviosModel
    {

        public string PK { get; set; }
        public string DESCRIPCION { get; set; }
        public double COSTO { get; set; }
        public string BORRADO { get; set; }
        public string FECHA_C { get; set; }
        public string FECHA_M { get; set; }
        public string FECHA_D { get; set; }
        public string USUARIO_C { get; set; }
        public string USUARIO_M { get; set; }
        public string USUARIO_D { get; set; }

        public database db;

        public CostosEnviosModel()
        {
            db = new database();
        }

        public List<CostosEnviosModel> obtenerCostosEnvios() {
            List<CostosEnviosModel> lista = new List<CostosEnviosModel>();
            CostosEnviosModel aux;
            try {
                string sql = "SELECT * FROM COSTOS_ENVIOS";
                db.PreparedSQL(sql);
                ResultSet res = db.getTable();
                while (res.Next()) {
                    aux = new CostosEnviosModel();
                    aux.PK = res.Get("PK");
                    aux.DESCRIPCION = res.Get("DESCRIPCION");
                    aux.COSTO = res.GetDouble("COSTO");
                    aux.BORRADO = res.Get("BORRADO");
                    aux.FECHA_C = res.Get("FECHA_C");
                    aux.FECHA_M = res.Get("FECHA_M");
                    aux.FECHA_D = res.Get("FECHA_D");
                    aux.USUARIO_C = res.Get("USUARIO_C");
                    aux.USUARIO_M = res.Get("USUARIO_M");
                    aux.USUARIO_D = res.Get("USUARIO_D");
                    lista.Add(aux);
                }

            } catch { }


            return lista;
        }
        public bool getCostoByPk() {
            try {
                string sql = "SELECT COSTO FROM COSTOS_ENVIOS where PK=@PK";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK", PK);
                ResultSet res = db.getTable();
                if (res.Next()) {
                    COSTO = res.GetDouble("COSTO");
                    return true;
                }

            } catch { }


            return false;
        }


    }
}
