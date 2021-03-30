using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Repartidores
{
    public class RepartidoresModel
    {
        public string PK { get; set; }
      public string NOMBRE{get;set;}
      public string APATERNO{get;set;}
      public string AMATERNO{get;set;}
      public string ESTADO{get;set;}
      public string MUNICIPIO{get;set;}
      public string COLONIA{get;set;}
      public string CALLE{get;set;}
      public string NUMERO{get;set;}
      public string TELEFONO{get;set;}
      public string CORREO{get;set;}
      public string IMAGEN{get;set;}
      public string BASICO{get;set;}
      public string URGENTE{get;set;}
      public string FOLIO{get;set;}
      public string PASSWORD{get;set;}
      public string FECHA_C{get;set;}
      public string FECHA_M{get;set;}
      public string TOKEN { get;set;}
      public string LATITUD { get;set;}
      public string LONGITUD { get;set;}
        database db;
        public RepartidoresModel() {
            db = new database();
        }

        public bool obtenerUsuarioByTelefono()
        {

            try
            {
                string sql = "SELECT * FROM REPARTIDORES WHERE TELEFONO=@TELEFONO";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@TELEFONO", TELEFONO);
                ResultSet res = db.getTable();
                if (res.Next())
                {
                    PK = res.Get("PK");
                    NOMBRE = res.Get("NOMBRE");
                    APATERNO = res.Get("APATERNO");
                    AMATERNO = res.Get("AMATERNO");
                    ESTADO = res.Get("ESTADO");
                    MUNICIPIO = res.Get("MUNICIPIO");
                    COLONIA = res.Get("COLONIA");
                    CALLE = res.Get("CALLE");
                    NUMERO = res.Get("NUMERO");
                    TELEFONO = res.Get("TELEFONO");
                    CORREO = res.Get("CORREO");
                    IMAGEN = res.Get("IMAGEN");
                    FOLIO = res.Get("FOLIO");
                    PASSWORD = res.Get("PASSWORD");
                    FECHA_C = res.Get("FECHA_C");
                    FECHA_M = res.Get("FECHA_M");
                    return true;
                }
            }
            catch (Exception e) { }

            return false;
        }
        public bool obtenerUsuarioByPK()
        {

            try
            {
                string sql = "SELECT * FROM REPARTIDORES WHERE PK=@PK";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK", PK);
                ResultSet res = db.getTable();
                if (res.Next())
                {
                    PK = res.Get("PK");
                    NOMBRE = res.Get("NOMBRE");
                    APATERNO = res.Get("APATERNO");
                    AMATERNO = res.Get("AMATERNO");
                    ESTADO = res.Get("ESTADO");
                    MUNICIPIO = res.Get("MUNICIPIO");
                    COLONIA = res.Get("COLONIA");
                    CALLE = res.Get("CALLE");
                    NUMERO = res.Get("NUMERO");
                    TELEFONO = res.Get("TELEFONO");
                    CORREO = res.Get("CORREO");
                    IMAGEN = res.Get("IMAGEN");
                    FOLIO = res.Get("FOLIO");
                    PASSWORD = res.Get("PASSWORD");
                    FECHA_C = res.Get("FECHA_C");
                    FECHA_M = res.Get("FECHA_M");
                    return true;
                }
            }
            catch (Exception e) { }

            return false;
        }

        public bool NuevoToken() {
            try
            {
                string sql = "UPDATE REPARTIDORES SET TOKEN=@TOKEN " +
                             "WHERE PK=@PK ";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@TOKEN", TOKEN);
                db.command.Parameters.AddWithValue("@PK", PK);

                if (db.execute())
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
        public bool actualizaLocalizacion() {
            try
            {
                string sql = "UPDATE REPARTIDORES SET LATITUD=@LATITUD,LONGITUD=@LONGITUD " +
                             "WHERE PK=@PK ";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@LATITUD", LATITUD);
                db.command.Parameters.AddWithValue("@LONGITUD", LONGITUD);
                db.command.Parameters.AddWithValue("@PK", PK);

                if (db.execute())
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

    }
}
