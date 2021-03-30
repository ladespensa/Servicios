using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Tiendas
{
    public class TiposTiendasModel
    {
        public string PK { get; set; }
      public string TIPO{get;set;}
      public string IMAGEN{get;set;}
      public string BORRADO{get;set;}
      public string FECHA_C{get;set;}
      public string FECHA_M{get;set;}
      public string FECHA_D{get;set;}
      public string USUARIO_C{get;set;}
      public string USUARIO_M{get;set;}
      public string USUARIO_D{get;set;}
        private database db;
        public TiposTiendasModel() {
            db = new database();
        }
        public List<TiposTiendasModel> obtenerTiposTienda() {
            List<TiposTiendasModel> lista = new List<TiposTiendasModel>();
            TiposTiendasModel aux;
            try {
                string sql = "SELECT * FROM TIPOS_TIENDAS";
                db.PreparedSQL(sql);
                ResultSet res = db.getTable();
                while (res.Next()) {
                    aux = new TiposTiendasModel();
                    aux.PK = res.Get("PK");
                    aux.TIPO = res.Get("TIPO");
                    aux.IMAGEN = res.Get("IMAGEN");
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
        
        public List<TiposTiendasModel> obtenerTiposByPkTienda(string pktienda) {
            List<TiposTiendasModel> lista = new List<TiposTiendasModel>();
            TiposTiendasModel aux;
            try {
                string sql = "SELECT * FROM TIPOS_TIENDAS " +
                             "WHERE PK IN(select PK_TIPO from TIENDAS_TIPOS WHERE PK_TIENDA = @PK_TIENDA) order by TIPO";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("PK_TIENDA", pktienda);
                ResultSet res = db.getTable();
                while (res.Next()) {
                    aux = new TiposTiendasModel();
                    aux.PK = res.Get("PK");
                    aux.TIPO = res.Get("TIPO");
                    aux.IMAGEN = res.Get("IMAGEN");
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
