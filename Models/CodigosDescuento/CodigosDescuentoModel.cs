using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.CodigosDescuento
{
    public class CodigosDescuentoModel
    {
        public string PK { get; set; }
      public string CODIGO{get;set;}
      public string POCENTAJE_DESCUENTO{get;set;}
      public int MAXIMO_USOS{get;set;}
      public string FECHA_INICIO{get;set;}
      public string FECHA_TERMINO{get;set;}
      public string BORRADO{get;set;}
      public string FECHA_C{get;set;}
      public string FECHA_M{get;set;}
      public string FECHA_D{get;set;}
      public string USUATIO_C{get;set;}
      public string USUARIO_M{get;set;}
      public string USUARIO_D{get;set;}
      public bool VIGENTE{get;set;}
      public string PK_CLIENTE{get;set;}
      public string ERROR{get;set;}
        public database db;
        public CodigosDescuentoModel() {
            db = new database();
        }

        public bool buscarCodigoDescuento() {
            try
            {
                string sql = @"select *,
                            CASE WHEN getdate() between FECHA_INICIO AND FECHA_TERMINO 
                            THEN 'TRUE' ELSE 'FALSE' END AS VIGENTE
                            from CODIGOS_DESCUENTO WHERE CODIGO=@CODIGO";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@CODIGO", CODIGO);
                ResultSet res = db.getTable();
                if (res.Next())
                {
                    PK = res.Get("PK");
                    CODIGO = res.Get("CODIGO");
                    POCENTAJE_DESCUENTO = res.Get("POCENTAJE_DESCUENTO");
                    MAXIMO_USOS = res.GetInt("MAXIMO_USOS");
                    FECHA_INICIO = res.Get("FECHA_INICIO");
                    FECHA_TERMINO = res.Get("FECHA_TERMINO");
                    BORRADO = res.Get("BORRADO");
                    FECHA_C = res.Get("FECHA_C");
                    FECHA_M = res.Get("FECHA_M");
                    FECHA_D = res.Get("FECHA_D");
                    USUATIO_C = res.Get("USUATIO_C");
                    USUARIO_M = res.Get("USUARIO_M");
                    USUARIO_D = res.Get("USUARIO_D");
                    VIGENTE = res.GetBool("VIGENTE");
                    return true;
                }
            }
            catch (Exception e) {
                ERROR = e.ToString();
            }
            return false;

        }

    }
}
