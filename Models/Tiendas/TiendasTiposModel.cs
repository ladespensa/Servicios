using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Tiendas
{
    public class TiendasTiposModel
    {
        public string PK { get; set; }
      public string PK_TIENDA{get;set;}
      public string PK_TIPO{get;set;}
      public string BORRADO{get;set;}
      public string FECHA_C{get;set;}
      public string FECHA_M{get;set;}
      public string FECHA_D{get;set;}
      public string USUARIO_C{get;set;}
      public string USUARIO_M{get;set;}
      public string USUARIO_D{get;set;}
        private database db;

        public TiendasTiposModel() {
            db = new database();
        }

        public List<TiendasTiposModel> obtenerTiendasTiposModel() {
            List<TiendasTiposModel> lista = new List<TiendasTiposModel>();
            TiendasTiposModel aux;
            try {
                string sql = "SELECT * FROM TIENDAS_TIPOS WHERE PK_TIENDA=@PK_TIENDA";

                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_TIENDA", PK_TIENDA);

                ResultSet res = db.getTable();
                while (res.Next()) {
                    aux = new TiendasTiposModel();
                    aux.PK = res.Get("PK");
                    aux.PK_TIENDA = res.Get("PK_TIENDA");
                    aux.PK_TIPO = res.Get("PK_TIPO");
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


    }
}
