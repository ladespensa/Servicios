using ConnectDB;
using acmarkert.Models.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace acmarkert.Models.Tiendas
{
    public class TiendasModel
    {
        public string PK { get; set; }
        public string NOMBRE { get; set; }
        public string DIRECCION { get; set; }
        public string LATITUD { get; set; }
        public string LONGITUD { get; set; }
        public string IMAGEN { get; set; }
        public string BORRADO { get; set; }
        public string FECHA_C { get; set; }
        public string FECHA_M { get; set; }
        public string FECHA_D { get; set; }
        public string USUARIO_C { get; set; }
        public string USUARIO_M { get; set; }
        public string USUARIO_D { get; set; }
        public string FOLIO { get; set; }
      public string TELEFONO {get;set;}
      public string CORREO {get;set;}
      public string PASSWORD {get;set;}
      public string BANCO {get;set;}
      public string CUENTA {get;set;}
      public string CLABE {get;set;}
      public string ENCARGADO {get;set;}
      public string TOKEN {get;set;}
      public long LUNES {get;set;}
      public long MARTES {get;set;}
      public long MIERCOLES {get;set;}
      public long JUEVES {get;set;}
      public long VIERNES {get;set;}
      public long SABADO {get;set;}
      public long DOMINGO {get;set;}
      public string ENTREGA_LUNES {get;set;}
      public string ENTREGA_MARTES { get;set;}
      public string ENTREGA_MIERCOLES { get;set;}
      public string ENTREGA_JUEVES { get;set;}
      public string ENTREGA_VIERNES { get;set;}
      public string ENTREGA_SABADO { get;set;}
      public string ENTREGA_DOMINGO { get;set;}
      public string ENTREGA_EXPRESS { get;set;}
        public List<CategoriasModel> TIPOS { get; set; }
        public List<CategoriasModel> CATEGORIAS { get; set; }
        public List<TiposTiendasModel> TIPOS_TIENDAS { get; set; }

        private database db;

        public TiendasModel() {
            db = new database();
        }

        public List<TiendasModel> getTiendas() {
            List<TiendasModel> lista = new List<TiendasModel>();
            TiendasModel aux;
            CategoriasModel aux2;

            string sql = "SELECT * FROM TIENDAS WHERE BORRADO=0";
            db.PreparedSQL(sql);
            ResultSet res = db.getTable();
            while (res.Next())
            {
                aux = new TiendasModel();
                aux.PK = res.Get("PK");
                aux.NOMBRE = res.Get("NOMBRE");
                aux.DIRECCION = res.Get("DIRECCION");
                aux.LATITUD = res.Get("LATITUD");
                aux.LONGITUD = res.Get("LONGITUD");
                aux.IMAGEN = res.Get("IMAGEN");
                aux.BORRADO = res.Get("BORRADO");
                aux.LUNES = res.GetLong("LUNES");
                aux.MARTES = res.GetLong("MARTES");
                aux.MIERCOLES= res.GetLong("MIERCOLES");
                aux.JUEVES = res.GetLong("JUEVES");
                aux.VIERNES = res.GetLong("VIERNES");
                aux.SABADO = res.GetLong("SABADO");
                aux.DOMINGO = res.GetLong("DOMINGO");
                aux.FECHA_C = res.Get("FECHA_C");
                aux.FECHA_M = res.Get("FECHA_M");
                aux.FECHA_D = res.Get("FECHA_D");
                aux.USUARIO_C = res.Get("USUARIO_C");
                aux.USUARIO_M = res.Get("USUARIO_M");
                aux.USUARIO_D = res.Get("USUARIO_D");
                aux2 = new CategoriasModel();
                aux.CATEGORIAS = aux2.getCategoriasByTienda(aux.PK);
                lista.Add(aux);
            }

            return lista;
        }
        public List<TiendasModel> getTiendasLight() {
            List<TiendasModel> lista = new List<TiendasModel>();
            TiendasModel aux;
            CategoriasModel aux2;

            string sql = "SELECT * FROM TIENDAS WHERE BORRADO=0";
            db.PreparedSQL(sql);
            ResultSet res = db.getTable();
            while (res.Next())
            {
                aux = new TiendasModel();
                aux.PK = res.Get("PK");
                aux.NOMBRE = res.Get("NOMBRE");
                aux.DIRECCION = res.Get("DIRECCION");
                aux.LATITUD = res.Get("LATITUD");
                aux.LONGITUD = res.Get("LONGITUD");
                aux.IMAGEN = res.Get("IMAGEN");
                aux.BORRADO = res.Get("BORRADO");
                aux.LUNES = res.GetLong("LUNES");
                aux.MARTES = res.GetLong("MARTES");
                aux.MIERCOLES = res.GetLong("MIERCOLES");
                aux.JUEVES = res.GetLong("JUEVES");
                aux.VIERNES = res.GetLong("VIERNES");
                aux.SABADO = res.GetLong("SABADO");
                aux.DOMINGO = res.GetLong("DOMINGO");

                DateTime DAT= Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Monday);
                aux.ENTREGA_LUNES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Tuesday);
                aux.ENTREGA_MARTES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Wednesday);
                aux.ENTREGA_MIERCOLES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Thursday);
                aux.ENTREGA_JUEVES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Friday);
                aux.ENTREGA_VIERNES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Saturday);
                aux.ENTREGA_SABADO = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Sunday);
                aux.ENTREGA_DOMINGO = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = DateTime.Now.AddDays(1);
                aux.ENTREGA_EXPRESS = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                
                aux.FECHA_C = res.Get("FECHA_C");
                aux.FECHA_M = res.Get("FECHA_M");
                aux.FECHA_D = res.Get("FECHA_D");
                aux.USUARIO_C = res.Get("USUARIO_C");
                aux.USUARIO_M = res.Get("USUARIO_M");
                aux.USUARIO_D = res.Get("USUARIO_D");
                lista.Add(aux);
            }

            return lista;
        }
        public bool getTiendasLightByPk() {

            string sql = "SELECT * FROM TIENDAS WHERE BORRADO=0 AND PK=@PK";
            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@PK", PK);
            ResultSet res = db.getTable();
            if (res.Next())
            {
                PK = res.Get("PK");
                NOMBRE = res.Get("NOMBRE");
                DIRECCION = res.Get("DIRECCION");
                LATITUD = res.Get("LATITUD");
                LONGITUD = res.Get("LONGITUD");
                IMAGEN = res.Get("IMAGEN");
                BORRADO = res.Get("BORRADO");
                LUNES = res.GetLong("LUNES");
                MARTES = res.GetLong("MARTES");
                MIERCOLES = res.GetLong("MIERCOLES");
                JUEVES = res.GetLong("JUEVES");
                VIERNES = res.GetLong("VIERNES");
                SABADO = res.GetLong("SABADO");
                DOMINGO = res.GetLong("DOMINGO");

                DateTime DAT= Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Monday);
                ENTREGA_LUNES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Tuesday);
                ENTREGA_MARTES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Wednesday);
                ENTREGA_MIERCOLES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Thursday);
                ENTREGA_JUEVES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Friday);
                ENTREGA_VIERNES = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Saturday);
                ENTREGA_SABADO = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Sunday);
                ENTREGA_DOMINGO = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                DAT = DateTime.Now.AddDays(1);
                ENTREGA_EXPRESS = DAT.ToString("dddd dd ", CultureInfo.CreateSpecificCulture("es-MX")) + "de" + DAT.ToString(" MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                
                FECHA_C = res.Get("FECHA_C");
                FECHA_M = res.Get("FECHA_M");
                FECHA_D = res.Get("FECHA_D");
                USUARIO_C = res.Get("USUARIO_C");
                USUARIO_M = res.Get("USUARIO_M");
                USUARIO_D = res.Get("USUARIO_D");
                return true;
            }

            return false;
        }

        public List<TiendasModel> getTiendasByTipos(string PK_TIPO)
        {
            List<TiendasModel> lista = new List<TiendasModel>();
            TiendasModel aux;
            CategoriasModel aux2;
            string sql = @"SELECT TI.*
                            FROM VTIENDAS TI
                            WHERE BORRADO = 0 AND
                            PK IN(SELECT PK_TIENDA
                            FROM TIENDAS_TIPOS
                            WHERE BORRADO = 0 AND PK_TIPO = @PK_TIPO GROUP BY PK_TIENDA)";
            /*
            string sql = @"SELECT *
                            FROM TIENDAS
                            WHERE BORRADO = 0 AND
                              PK IN(SELECT PK_TIENDA
                              FROM TIENDAS_TIPOS
                              WHERE BORRADO = 0 AND PK_TIPO = @PK_TIPO  GROUP BY PK_TIENDA)
                            ";*/
                            //AND convert(varchar(5), getdate(),8) BETWEEN APERTURA AND CIERRE";
            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@PK_TIPO",PK_TIPO);
            ResultSet res = db.getTable();
            while (res.Next())
            {
                aux = new TiendasModel();
                aux.PK = res.Get("PK");
                aux.NOMBRE = res.Get("NOMBRE");
                aux.DIRECCION = res.Get("DIRECCION");
                aux.LATITUD = res.Get("LATITUD");
                aux.LONGITUD = res.Get("LONGITUD");
                aux.IMAGEN = res.Get("IMAGEN");
                aux.FOLIO = res.Get("FOLIO");
                aux.TELEFONO= res.Get("TELEFONO");
                aux.CORREO = res.Get("CORREO");
                aux.BANCO = res.Get("BANCO");
                aux.CUENTA = res.Get("CUENTA");
                aux.CLABE = res.Get("CLABE");
                aux.ENCARGADO = res.Get("ENCARGADO");
                aux.LUNES = res.GetLong("LUNES");
                aux.MARTES = res.GetLong("MARTES");
                aux.MIERCOLES = res.GetLong("MIERCOLES");
                aux.JUEVES = res.GetLong("JUEVES");
                aux.VIERNES = res.GetLong("VIERNES");
                aux.SABADO = res.GetLong("SABADO");
                aux.DOMINGO = res.GetLong("DOMINGO");
                aux.BORRADO = res.Get("BORRADO");
                aux.FECHA_C = res.Get("FECHA_C");
                aux.FECHA_M = res.Get("FECHA_M");
                aux.FECHA_D = res.Get("FECHA_D");
                aux.USUARIO_C = res.Get("USUARIO_C");
                aux.USUARIO_M = res.Get("USUARIO_M");
                aux.USUARIO_D = res.Get("USUARIO_D");
                //aux2 = new CategoriasModel();
                //aux.CATEGORIAS = aux2.getCategoriasByTienda(aux.PK);
                lista.Add(aux);
            }

            return lista;
        }
        public bool obtenerTokenTiendaPorPK()
        {
            CategoriasModel aux2;
            string sql = "SELECT TOKEN " +
                         "FROM TIENDAS " +
                         "WHERE BORRADO=0 AND " +
                         "PK =@PK";
            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@PK",PK);
            ResultSet res = db.getTable();
            if (res.Next())
            {
                TOKEN = res.Get("TOKEN");
                return true;
            }

            return false;
        }
        public bool obtenerTiendaPorPK()
        {
            CategoriasModel aux2;
            string sql = "SELECT * " +
                         "FROM TIENDAS " +
                         "WHERE BORRADO=0 AND " +
                         "PK =@PK";
            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@PK",PK);
            ResultSet res = db.getTable();
            if (res.Next())
            {
                PK = res.Get("PK");
                NOMBRE = res.Get("NOMBRE");
                DIRECCION = res.Get("DIRECCION");
                LATITUD = res.Get("LATITUD");
                LONGITUD = res.Get("LONGITUD");
                IMAGEN = res.Get("IMAGEN");
                FOLIO = res.Get("FOLIO");
                TELEFONO = res.Get("TELEFONO");
                CORREO = res.Get("CORREO");
                BANCO = res.Get("BANCO");
                CUENTA = res.Get("CUENTA");
                CLABE = res.Get("CLABE");
                ENCARGADO = res.Get("ENCARGADO");
                BORRADO = res.Get("BORRADO");
                LUNES = res.GetLong("LUNES");
                MARTES = res.GetLong("MARTES");
                MIERCOLES = res.GetLong("MIERCOLES");
                JUEVES = res.GetLong("JUEVES");
                VIERNES = res.GetLong("VIERNES");
                SABADO = res.GetLong("SABADO");
                DOMINGO = res.GetLong("DOMINGO");
                FECHA_C = res.Get("FECHA_C");
                FECHA_M = res.Get("FECHA_M");
                FECHA_D = res.Get("FECHA_D");
                USUARIO_C = res.Get("USUARIO_C");
                USUARIO_M = res.Get("USUARIO_M");
                USUARIO_D = res.Get("USUARIO_D");
                TOKEN = res.Get("TOKEN");
                aux2 = new CategoriasModel();
                CATEGORIAS = aux2.getCategoriasByTienda(PK);
                return true;
            }

            return false;
        }
        public bool obtenerTiendaPorTelefono()
        {
            CategoriasModel aux2;
            string sql = "SELECT * " +
                         "FROM TIENDAS " +
                         "WHERE BORRADO=0 AND " +
                         "TELEFONO =@TELEFONO";
            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@TELEFONO",TELEFONO);
            ResultSet res = db.getTable();
            if (res.Next())
            {
                PK = res.Get("PK");
                NOMBRE = res.Get("NOMBRE");
                DIRECCION = res.Get("DIRECCION");
                LATITUD = res.Get("LATITUD");
                LONGITUD = res.Get("LONGITUD");
                IMAGEN = res.Get("IMAGEN");
                FOLIO = res.Get("FOLIO");
                TELEFONO = res.Get("TELEFONO");
                CORREO = res.Get("CORREO");
                BANCO = res.Get("BANCO");
                CUENTA = res.Get("CUENTA");
                CLABE = res.Get("CLABE");
                ENCARGADO = res.Get("ENCARGADO");
                BORRADO = res.Get("BORRADO");
                LUNES = res.GetLong("LUNES");
                MARTES = res.GetLong("MARTES");
                MIERCOLES = res.GetLong("MIERCOLES");
                JUEVES = res.GetLong("JUEVES");
                VIERNES = res.GetLong("VIERNES");
                SABADO = res.GetLong("SABADO");
                DOMINGO = res.GetLong("DOMINGO");
                FECHA_C = res.Get("FECHA_C");
                FECHA_M = res.Get("FECHA_M");
                FECHA_D = res.Get("FECHA_D");
                USUARIO_C = res.Get("USUARIO_C");
                USUARIO_M = res.Get("USUARIO_M");
                USUARIO_D = res.Get("USUARIO_D");
                aux2 = new CategoriasModel();
                CATEGORIAS = aux2.getCategoriasByTienda(PK);
            }

            return true;
        }

        public bool NuevoToken()
        {
            try
            {
                string sql = "UPDATE TIENDAS SET TOKEN=@TOKEN " +
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

    
    }
}
