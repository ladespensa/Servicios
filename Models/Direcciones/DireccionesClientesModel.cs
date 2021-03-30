using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Direcciones
{
    public class DireccionesClientesModel
    {
        public long PK { get; set; }
      public string PK_CLIENTE{get;set;}
      public string DIRECCION{get;set;}
      public string LATITUD{get;set;}
      public string LONGITUD{get;set;}
      public long CONTADOR { get;set;}
      public string BORRADO{get;set;}
      public string FECHA_C{get;set;}
      public string FECHA_M{get;set;}
      public string FECHA_D{get;set;}
      public string USUARIO_C{get;set;}
      public string USUARIO_M{get;set;}
      public string USUARIO_D{get;set;}

        private database db;
        public string ERROR { get; set; }

        public DireccionesClientesModel() {
            db = new database();
        }
        public bool existeDireccion() {

            try
            {
                string sql = "SELECT * FROM DIRECCIONES_ENTREGA " +
                             "WHERE DIRECCION =@DIRECCION OR " +
                             "(LATITUD=@LATITUD AND LONGITUD=@LONGITUD)";

                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@DIRECCION", DIRECCION);
                db.command.Parameters.AddWithValue("@LATITUD", LATITUD);
                db.command.Parameters.AddWithValue("@LONGITUD", LONGITUD);

                ResultSet res = db.getTable();
                if (res.Next()) {
                    PK = res.GetLong("PK");
                    CONTADOR = res.GetLong("PK");
                    return true;
                }

            } catch (Exception e) {
                ERROR = e.ToString();
            }

            return false;
        }
        
        public bool creaDireccion() {

            try
            {
                string sql = "INSERT INTO DIRECCIONES_ENTREGA (PK_CLIENTE,DIRECCION,LATITUD,LONGITUD,CONTADOR) " +
                              "VALUES(@PK_CLIENTE,@DIRECCION,@LATITUD,@LONGITUD,@CONTADOR)";

                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
                db.command.Parameters.AddWithValue("@DIRECCION", DIRECCION);
                db.command.Parameters.AddWithValue("@LATITUD", LATITUD);
                db.command.Parameters.AddWithValue("@LONGITUD", LONGITUD);
                db.command.Parameters.AddWithValue("@CONTADOR", CONTADOR);
                
                if (db.execute()) {
                    return true;
                }

            } catch (Exception e) {
                ERROR = e.ToString();
            }

            return false;
        }
        
        public bool actualizaContador() {

            try
            {
                string sql = "UPDATE DIRECCIONES_ENTREGA SET CONTADOR=@CONTADOR WHERE PK=@PK";

                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK", PK);
                db.command.Parameters.AddWithValue("@CONTADOR", CONTADOR);
                
                if (db.execute()) {
                    return true;
                }

            } catch (Exception e) {
                ERROR = e.ToString();
            }

            return false;
        }
        public List<DireccionesClientesModel> obtieneDireccionesByPkCliente() {

            List<DireccionesClientesModel> lista = new List<DireccionesClientesModel>();
            DireccionesClientesModel aux;
            try
            {
                string sql = "SELECT * FROM DIRECCIONES_ENTREGA " +
                             "WHERE PK_CLIENTE=@PK_CLIENTE ORDER BY CONTADOR ";

                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
                ResultSet res = db.getTable();
                while(res.Next()) {
                    aux = new DireccionesClientesModel();
                    aux.PK = res.GetLong("PK");
                    aux.DIRECCION = res.Get("DIRECCION");
                    aux.LATITUD = res.Get("LATITUD");
                    aux.LONGITUD = res.Get("LONGITUD");
                    aux.CONTADOR = res.GetLong("CONTADOR");
                    lista.Add(aux);
                }

            } catch (Exception e) {
                ERROR = e.ToString();
            }

            return lista;
        }

    }
}
