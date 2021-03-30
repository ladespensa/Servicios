using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Poligonos
{
    public class PoligonoCoordenadasModel
    {

        public string PK { get; set; }
        public long PK_POLIGONO { get; set; }
        public string LATITUD { get; set; }
        public string LONGITUD { get; set; }
        public string FECHA_C { get; set; }
        private database db;
        string ERROR;
        public PoligonoCoordenadasModel() {
            db = new database();
        }
        public List<PoligonoCoordenadasModel> ObtenerCoordenadasByPkPoligono()
        {
            List<PoligonoCoordenadasModel> lista = new List<PoligonoCoordenadasModel>();
            PoligonoCoordenadasModel aux;
            try {
                string sql = "SELECT * FROM POLIGONO_COORDENADAS WHERE PK_POLIGONO=@PK_POLIGONO";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_POLIGONO", PK_POLIGONO);
                ResultSet res = db.getTable();
                while (res.Next()) {
                    aux = new PoligonoCoordenadasModel();
                    aux.PK = res.Get("PK");
                    aux.PK_POLIGONO = res.GetLong("PK_POLIGONO");
                    aux.LATITUD = res.Get("LATITUD");
                    aux.LONGITUD = res.Get("LONGITUD");
                    aux.FECHA_C = res.Get("FECHA_C");
                    lista.Add(aux);
                }
            } catch (Exception e) {
                ERROR = e.ToString();
            }

            return lista;
        }

    }
}
