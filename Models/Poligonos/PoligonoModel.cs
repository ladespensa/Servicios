using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Poligonos
{
    public class PoligonoModel
    {
        public long PK { get; set; }
        public string NOMBRE { get; set; }
        public int POLIGONO_VERSION { get; set; }
        public string FECHA_C { get; set; }
        string ERROR;
        public List<PoligonoCoordenadasModel> COORDENADAS { get; set; }
        private database db;
        public PoligonoModel() {
            db = new database();
            COORDENADAS = new List<PoligonoCoordenadasModel>();
        }
        public bool ObtenerPoligono() {

            try {
                string sql = "SELECT * FROM POLIGONO WHERE PK=@PK";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK", PK);
                ResultSet res = db.getTable();
                if (res.Next()) {
                    PK = res.GetLong("PK");
                    NOMBRE = res.Get("NOMBRE");
                    POLIGONO_VERSION = res.GetInt("POLIGONO_VERSION");
                    FECHA_C = res.Get("FECHA_C");
                    PoligonoCoordenadasModel aux = new PoligonoCoordenadasModel();
                    aux.PK_POLIGONO = PK;
                    COORDENADAS = aux.ObtenerCoordenadasByPkPoligono();
                }

            } catch (Exception e) {
                ERROR = e.ToString();
            }

            return false;
        }
        public List<PoligonoModel> ObtenerPoligonosList() {

            List<PoligonoModel> lista = new List<PoligonoModel>();
            PoligonoModel aux;
            try {
                string sql = "SELECT * FROM POLIGONO WHERE BORRADO=0";
                db.PreparedSQL(sql);
                ResultSet res = db.getTable();
                while (res.Next()) {
                    aux = new PoligonoModel();
                    aux.PK = res.GetLong("PK");
                    aux.NOMBRE = res.Get("NOMBRE");
                    aux.POLIGONO_VERSION = res.GetInt("POLIGONO_VERSION");
                    aux.FECHA_C = res.Get("FECHA_C");
                    lista.Add(aux);
                }

            } catch (Exception e) {
                ERROR = e.ToString();
            }

            return lista;
        }

    }

    public class PoligonosModelList { 
    
        public List<PoligonoModel> POLIGONOS { get; set; }

    }

}
