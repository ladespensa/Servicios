using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Categorias
{
    public class CategoriasModel
    {
        public string PK { get; set; }
public long PK_GRUPO{get;set;}
public string GRUPO{get;set;}
public string DESCRIPCION_GRUPO{get;set;}
public string CLASIFICACION{get;set;}
public string DESCRIPCION{get;set;}
public string IMAGEN{get;set;}
public long PK_TIPO_TIENDA{get;set;}
public string TIPO{get;set;}
public string TIPO_IMAGEN{get;set;}
public string BORRADO{get;set;}
public string FECHA_C{get;set;}
public string FECHA_M{get;set;}
public string FECHA_D{get;set;}
public string USUARIO_C{get;set;}
public string USUARIO_M{get;set;}
public string USUARIO_D{get;set;}

        public database db;

        public CategoriasModel()
        {
            db = new database();
        }

        public List<CategoriasModel> getCategoriasByTienda(string pk_tienda) {
            List<CategoriasModel> categorias = new List<CategoriasModel>();
            CategoriasModel aux;
            string sql = "SELECT * FROM CATEGORIAS WHERE PK IN ( SELECT PK_CATEGORIA FROM PRODUCTOS WHERE PK_TIENDA=@PK_TIENDA)";

            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue ("@PK_TIENDA", pk_tienda);
            ResultSet res = db.getTable();

            while (res.Next())
            {
                aux = new CategoriasModel();
                aux.PK = res.Get("PK");
                aux.CLASIFICACION = res.Get("CLASIFICACION");
                aux.DESCRIPCION = res.Get("DESCRIPCION");
                aux.IMAGEN = res.Get("IMAGEN");
                aux.BORRADO = res.Get("BORRADO");
                aux.FECHA_C = res.Get("FECHA_C");
                aux.FECHA_M = res.Get("FECHA_M");
                aux.FECHA_D = res.Get("FECHA_D");
                aux.USUARIO_C = res.Get("USUARIO_C");
                aux.USUARIO_M = res.Get("USUARIO_M");
                aux.USUARIO_D = res.Get("USUARIO_D");
                categorias.Add(aux);
            }

            return categorias;
        }

        public List<CategoriasModel> getCategorias() {
            List<CategoriasModel> categorias = new List<CategoriasModel>();
            CategoriasModel aux;
            string sql = "SELECT * FROM VCATEGORIAS order by TIPO ASC";

            db.PreparedSQL(sql);
            ResultSet res = db.getTable();

            while (res.Next())
            {
                aux = new CategoriasModel();
                aux.PK = res.Get("PK");
                aux.PK_GRUPO = res.GetLong("PK_TIPO_TIENDA");
                aux.GRUPO = res.Get("TIPO");
                aux.DESCRIPCION_GRUPO = res.Get("GRUPO");
                aux.CLASIFICACION = res.Get("CLASIFICACION");
                aux.DESCRIPCION = res.Get("DESCRIPCION");
                aux.IMAGEN = res.Get("IMAGEN");
                aux.PK_TIPO_TIENDA = res.GetLong("PK_TIPO_TIENDA");
                aux.TIPO = res.Get("TIPO");
                aux.TIPO_IMAGEN = res.Get("TIPO_IMAGEN");
                aux.BORRADO = res.Get("BORRADO");
                aux.FECHA_C = res.Get("FECHA_C");
                aux.FECHA_M = res.Get("FECHA_M");
                aux.FECHA_D = res.Get("FECHA_D");
                aux.USUARIO_C = res.Get("USUARIO_C");
                aux.USUARIO_M = res.Get("USUARIO_M");
                aux.USUARIO_D = res.Get("USUARIO_D");
                categorias.Add(aux);
            }

            return categorias;
        }

        public List<CategoriasModel> getCategoriasByPkTipo() {
            List<CategoriasModel> categorias = new List<CategoriasModel>();
            CategoriasModel aux;
            string sql = "SELECT * FROM VCATEGORIAS " +
                         "where PK_TIPO_TIENDA=@PK_TIPO_TIENDA " +
                         "order by PK_GRUPO ASC";

            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("PK_TIPO_TIENDA", PK_TIPO_TIENDA);
            ResultSet res = db.getTable();

            while (res.Next())
            {
                aux = new CategoriasModel();
                aux.PK = res.Get("PK");
                aux.PK_GRUPO = res.GetLong("PK_GRUPO");
                aux.GRUPO = res.Get("GRUPO");
                aux.DESCRIPCION_GRUPO = res.Get("GRUPO");
                aux.CLASIFICACION = res.Get("CLASIFICACION");
                aux.DESCRIPCION = res.Get("DESCRIPCION");
                aux.IMAGEN = res.Get("IMAGEN");
                aux.PK_TIPO_TIENDA = res.GetLong("PK_TIPO_TIENDA");
                aux.TIPO = res.Get("TIPO");
                aux.TIPO_IMAGEN = res.Get("TIPO_IMAGEN");
                aux.BORRADO = res.Get("BORRADO");
                aux.FECHA_C = res.Get("FECHA_C");
                aux.FECHA_M = res.Get("FECHA_M");
                aux.FECHA_D = res.Get("FECHA_D");
                aux.USUARIO_C = res.Get("USUARIO_C");
                aux.USUARIO_M = res.Get("USUARIO_M");
                aux.USUARIO_D = res.Get("USUARIO_D");
                categorias.Add(aux);
            }

            return categorias;
        }

    }
}
