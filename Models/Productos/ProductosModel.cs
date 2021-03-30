using ConnectDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Productos
{
    public class ProductosModel
    {
        public string PK { get; set; }
        public string PK_CATEGORIA { get; set; }
        public string CATEGORIA { get; set; }
        public string PK_TIENDA { get; set; }
        public string TIENDA { get; set; }
        public string PK_TIPO_TIENDA { get; set; }
        public string TIPO { get; set; }
        public string IMAGEN_TIPO { get; set; }
        public string IMAGEN_CATEGORIA { get; set; }
        public string PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string STOCK { get; set; }
        public string PK_MEDIDA { get; set; }
        public string MEDIDA { get; set; }
        public string MEDIDA_DESCRIPCION { get; set; }
        public string IMAGEN { get; set; }
        public string IMAGEN_TIENDA { get; set; }
        public string PRECIO { get; set; }
        public string BORRADO { get; set; }
        public string FECHA_C { get; set; }
        public string FECHA_M { get; set; }
        public string FECHA_D { get; set; }
        public string USUARIO_C { get; set; }
        public string USUARIO_M { get; set; }
        public string USUARIO_D { get; set; }
        public long TIENDA_LUNES { get; set; }
        public long TIENDA_MARTES { get; set; }
        public long TIENDA_MIERCOLES { get; set; }
        public long TIENDA_JUEVES { get; set; }
        public long TIENDA_VIERNES { get; set; }
        public long TIENDA_SABADO { get; set; }
        public long TIENDA_DOMINGO { get; set; }
        public string ENTREGA_LUNES { get; set; }
        public string ENTREGA_MARTES { get; set; }
        public string ENTREGA_MIERCOLES { get; set; }
        public string ENTREGA_JUEVES { get; set; }
        public string ENTREGA_VIERNES { get; set; }
        public string ENTREGA_SABADO { get; set; }
        public string ENTREGA_DOMINGO { get; set; }
        public string ENTREGA_EXPRESS { get; set; }
        private database db;
        public ProductosModel()
        {
            db = new database();
        }
        public List<ProductosModel> getProductosByTiedaAndCategoria()
        {
            List<ProductosModel> lista = new List<ProductosModel>();
            ProductosModel aux;
            string sql = "SELECT PR.*,ME.MEDIDA, ME.DESCRIPCION MEDIDA_DESCRIPCION " +
                         "FROM PRODUCTOS PR INNER JOIN MEDIDAS ME ON(PR.PK_MEDIDA= ME.PK) " +
                         "WHERE PR.PK_TIENDA =@PK_TIENDA AND PR.PK_CATEGORIA=@PK_CATEGORIA AND STOCK >0 ORDER BY PRODUCTO";

            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@PK_TIENDA", PK_TIENDA);
            db.command.Parameters.AddWithValue("@PK_CATEGORIA", PK_CATEGORIA);
            ResultSet res = db.getTable();

            while (res.Next())
            {
                aux = new ProductosModel();
                aux.PK = res.Get("PK");
                aux.PK_CATEGORIA = res.Get("PK_CATEGORIA");
                aux.PK_TIENDA = res.Get("PK_TIENDA");
                aux.PRODUCTO = res.Get("PRODUCTO");
                aux.DESCRIPCION = res.Get("DESCRIPCION");
                aux.STOCK = res.Get("STOCK");
                aux.PK_MEDIDA = res.Get("PK_MEDIDA");
                aux.MEDIDA = res.Get("MEDIDA");
                aux.MEDIDA_DESCRIPCION = res.Get("MEDIDA_DESCRIPCION");
                aux.PRECIO = res.Get("PRECIO");
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

            return lista;
        }
        public List<ProductosModel> getProductosByPromocion()
        {
            List<ProductosModel> lista = new List<ProductosModel>();
            ProductosModel aux;
            string sql = @"SELECT PR.*,TI.NOMBRE TIENDA,ME.MEDIDA,ME.DESCRIPCION MEDIDA_DESCRIPCION,
                           TI.IMAGEN IMAGEN_TIENDA,TI.LUNES,TI.MARTES,TI.MIERCOLES,TI.JUEVES,TI.VIERNES,TI.SABADO,TI.DOMINGO,
                           CA.CLASIFICACION CATEGORIA,CA.IMAGEN IMAGEN_CATEGORIA,TP.PK PK_TIPO_TIENDA,TP.TIPO,TP.IMAGEN IMAGEN_TIPO	
                           FROM PRODUCTOS PR
                           INNER JOIN VTIENDAS TI ON(TI.PK= PR.PK_TIENDA)
                           INNER JOIN MEDIDAS ME ON(PR.PK_MEDIDA = ME.PK)
						   INNER JOIN CATEGORIAS CA ON(CA.PK=PR.PK_CATEGORIA) 
						   INNER JOIN TIPOS_TIENDAS TP ON(TP.PK=CA.PK_TIPO_TIENDA)
                           WHERE PROMOCION=1 ";

            db.PreparedSQL(sql); 
            ResultSet res = db.getTable();

            while (res.Next())
            {
                aux = new ProductosModel();
                aux.PK = res.Get("PK");
                aux.PK_CATEGORIA = res.Get("PK_CATEGORIA");
                aux.CATEGORIA = res.Get("CATEGORIA");
                aux.PK_TIENDA = res.Get("PK_TIENDA");
                aux.TIENDA = res.Get("TIENDA");
                aux.PK_TIPO_TIENDA = res.Get("PK_TIPO_TIENDA");
                aux.TIPO = res.Get("TIPO");
                aux.PRODUCTO = res.Get("PRODUCTO");
                aux.DESCRIPCION = res.Get("DESCRIPCION");
                aux.STOCK = res.Get("STOCK");
                aux.PK_MEDIDA = res.Get("PK_MEDIDA");
                aux.MEDIDA = res.Get("MEDIDA");
                aux.MEDIDA_DESCRIPCION = res.Get("MEDIDA_DESCRIPCION");
                aux.PRECIO = res.Get("PRECIO");
                aux.IMAGEN = res.Get("IMAGEN_PROMO");
                aux.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
                aux.IMAGEN_CATEGORIA = res.Get("IMAGEN_CATEGORIA");
                aux.IMAGEN_TIPO = res.Get("IMAGEN_TIPO");

                aux.TIENDA_LUNES = res.GetLong("LUNES");
                aux.TIENDA_MARTES = res.GetLong("MARTES");
                aux.TIENDA_MIERCOLES = res.GetLong("MIERCOLES");
                aux.TIENDA_JUEVES = res.GetLong("JUEVES");
                aux.TIENDA_VIERNES = res.GetLong("VIERNES");
                aux.TIENDA_SABADO = res.GetLong("SABADO");
                aux.TIENDA_DOMINGO = res.GetLong("DOMINGO");

                DateTime DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Monday);
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

                aux.BORRADO = res.Get("BORRADO");
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
        public List<ProductosModel> getProductosByTiendaAndNombre()
        {
            List<ProductosModel> lista = new List<ProductosModel>();
            ProductosModel aux;
            string sql = @"SELECT PR.*,TI.NOMBRE TIENDA,ME.MEDIDA,ME.DESCRIPCION MEDIDA_DESCRIPCION,
                           TI.IMAGEN IMAGEN_TIENDA,TI.LUNES,TI.MARTES,TI.MIERCOLES,TI.JUEVES,TI.VIERNES,TI.SABADO,TI.DOMINGO,
                           CA.CLASIFICACION CATEGORIA,CA.IMAGEN IMAGEN_CATEGORIA,TP.PK PK_TIPO_TIENDA,TP.TIPO,TP.IMAGEN IMAGEN_TIPO	
                           FROM PRODUCTOS PR
                           INNER JOIN VTIENDAS TI ON(TI.PK= PR.PK_TIENDA)
                           INNER JOIN MEDIDAS ME ON(PR.PK_MEDIDA = ME.PK)
						   INNER JOIN CATEGORIAS CA ON(CA.PK=PR.PK_CATEGORIA) 
						   INNER JOIN TIPOS_TIENDAS TP ON(TP.PK=CA.PK_TIPO_TIENDA)
                           WHERE PR.PRODUCTO LIKE @PRODUCTO AND TI.PK=@PK_TIENDA ";

            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@PRODUCTO", "%"+PRODUCTO+"%");
            db.command.Parameters.AddWithValue("@PK_TIENDA", PK_TIENDA);
            ResultSet res = db.getTable();

            while (res.Next())
            {
                aux = new ProductosModel();
                aux.PK = res.Get("PK");
                aux.PK_CATEGORIA = res.Get("PK_CATEGORIA");
                aux.CATEGORIA = res.Get("CATEGORIA");
                aux.PK_TIENDA = res.Get("PK_TIENDA");
                aux.TIENDA = res.Get("TIENDA");
                aux.PK_TIPO_TIENDA = res.Get("PK_TIPO_TIENDA");
                aux.TIPO = res.Get("TIPO");
                aux.PRODUCTO = res.Get("PRODUCTO");
                aux.DESCRIPCION = res.Get("DESCRIPCION");
                aux.STOCK = res.Get("STOCK");
                aux.PK_MEDIDA = res.Get("PK_MEDIDA");
                aux.MEDIDA = res.Get("MEDIDA");
                aux.MEDIDA_DESCRIPCION = res.Get("MEDIDA_DESCRIPCION");
                aux.PRECIO = res.Get("PRECIO");
                aux.IMAGEN = res.Get("IMAGEN");
                aux.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
                aux.IMAGEN_CATEGORIA = res.Get("IMAGEN_CATEGORIA");
                aux.IMAGEN_TIPO = res.Get("IMAGEN_TIPO");

                aux.TIENDA_LUNES = res.GetLong("LUNES");
                aux.TIENDA_MARTES = res.GetLong("MARTES");
                aux.TIENDA_MIERCOLES = res.GetLong("MIERCOLES");
                aux.TIENDA_JUEVES = res.GetLong("JUEVES");
                aux.TIENDA_VIERNES = res.GetLong("VIERNES");
                aux.TIENDA_SABADO = res.GetLong("SABADO");
                aux.TIENDA_DOMINGO = res.GetLong("DOMINGO");

                DateTime DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Monday);
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

                aux.BORRADO = res.Get("BORRADO");
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
        public List<ProductosModel> getProductosByPromocionByPkTienda()
        {
            List<ProductosModel> lista = new List<ProductosModel>();
            ProductosModel aux;
            string sql = @"SELECT PR.*,TI.NOMBRE TIENDA,ME.MEDIDA,ME.DESCRIPCION MEDIDA_DESCRIPCION,
                           TI.IMAGEN IMAGEN_TIENDA,TI.LUNES,TI.MARTES,TI.MIERCOLES,TI.JUEVES,TI.VIERNES,TI.SABADO,TI.DOMINGO,
                           CA.CLASIFICACION CATEGORIA,CA.IMAGEN IMAGEN_CATEGORIA,TP.PK PK_TIPO_TIENDA,TP.TIPO,TP.IMAGEN IMAGEN_TIPO	
                           FROM PRODUCTOS PR
                           INNER JOIN VTIENDAS TI ON(TI.PK= PR.PK_TIENDA)
                           INNER JOIN MEDIDAS ME ON(PR.PK_MEDIDA = ME.PK)
						   INNER JOIN CATEGORIAS CA ON(CA.PK=PR.PK_CATEGORIA) 
						   INNER JOIN TIPOS_TIENDAS TP ON(TP.PK=CA.PK_TIPO_TIENDA)
                           WHERE PROMOCION=1 AND PK_TIENDA=@PK_TIENDA";

            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@PK_TIENDA", PK_TIENDA);
            ResultSet res = db.getTable();

            while (res.Next())
            {
                aux = new ProductosModel();
                aux.PK = res.Get("PK");
                aux.PK_CATEGORIA = res.Get("PK_CATEGORIA");
                aux.CATEGORIA = res.Get("CATEGORIA");
                aux.PK_TIENDA = res.Get("PK_TIENDA");
                aux.TIENDA = res.Get("TIENDA");
                aux.PK_TIPO_TIENDA = res.Get("PK_TIPO_TIENDA");
                aux.TIPO = res.Get("TIPO");
                aux.PRODUCTO = res.Get("PRODUCTO");
                aux.DESCRIPCION = res.Get("DESCRIPCION");
                aux.STOCK = res.Get("STOCK");
                aux.PK_MEDIDA = res.Get("PK_MEDIDA");
                aux.MEDIDA = res.Get("MEDIDA");
                aux.MEDIDA_DESCRIPCION = res.Get("MEDIDA_DESCRIPCION");
                aux.PRECIO = res.Get("PRECIO");
                aux.IMAGEN = res.Get("IMAGEN_PROMO");
                aux.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
                aux.IMAGEN_CATEGORIA = res.Get("IMAGEN_CATEGORIA");
                aux.IMAGEN_TIPO = res.Get("IMAGEN_TIPO");

                aux.TIENDA_LUNES = res.GetLong("LUNES");
                aux.TIENDA_MARTES = res.GetLong("MARTES");
                aux.TIENDA_MIERCOLES = res.GetLong("MIERCOLES");
                aux.TIENDA_JUEVES = res.GetLong("JUEVES");
                aux.TIENDA_VIERNES = res.GetLong("VIERNES");
                aux.TIENDA_SABADO = res.GetLong("SABADO");
                aux.TIENDA_DOMINGO = res.GetLong("DOMINGO");

                DateTime DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Monday);
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

                aux.BORRADO = res.Get("BORRADO");
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
        public List<ProductosModel> getProductosByCategoria()
        {
            List<ProductosModel> lista = new List<ProductosModel>();
            ProductosModel aux;
            string sql = @"SELECT PR.*,TI.NOMBRE TIENDA,ME.MEDIDA,ME.DESCRIPCION MEDIDA_DESCRIPCION,TI.IMAGEN IMAGEN_TIENDA,TI.LUNES,TI.MARTES,TI.MIERCOLES,TI.JUEVES,TI.VIERNES,TI.SABADO,TI.DOMINGO,
						   CA.CLASIFICACION CATEGORIA,CA.IMAGEN IMAGEN_CATEGORIA,CA.PK_TIPO_TIENDA,TP.TIPO,TP.IMAGEN IMAGEN_TIPO
                           FROM PRODUCTOS PR
                           INNER JOIN TIENDAS TI ON(TI.PK= PR.PK_TIENDA)
                           INNER JOIN MEDIDAS ME ON(PR.PK_MEDIDA = ME.PK)
						   INNER JOIN CATEGORIAS CA ON(CA.PK=PR.PK_CATEGORIA) 
						   INNER JOIN TIPOS_TIENDAS TP ON(TP.PK=CA.PK_TIPO_TIENDA)
                           WHERE PR.PK_CATEGORIA=@PK_CATEGORIA ORDER BY PRODUCTO";//TODO_SERGIO and convert(varchar(5), getdate(),8) BETWEEN TI.APERTURA AND TI.CIERRE";

            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@PK_CATEGORIA", PK_CATEGORIA);
                
            ResultSet res = db.getTable();

            while (res.Next())
            {
                aux = new ProductosModel();
                aux.PK = res.Get("PK");
                aux.PK_CATEGORIA = res.Get("PK_CATEGORIA");
                aux.CATEGORIA = res.Get("CATEGORIA");
                aux.PK_TIENDA = res.Get("PK_TIENDA");
                aux.TIENDA = res.Get("TIENDA");
                aux.PK_TIPO_TIENDA = res.Get("PK_TIPO_TIENDA");
                aux.TIPO = res.Get("TIPO");
                aux.PRODUCTO = res.Get("PRODUCTO");
                aux.DESCRIPCION = res.Get("DESCRIPCION");
                aux.STOCK = res.Get("STOCK");
                aux.PK_MEDIDA = res.Get("PK_MEDIDA");
                aux.MEDIDA = res.Get("MEDIDA");
                aux.MEDIDA_DESCRIPCION = res.Get("MEDIDA_DESCRIPCION");
                aux.PRECIO = res.Get("PRECIO");
                aux.IMAGEN = res.Get("IMAGEN");
                aux.IMAGEN_CATEGORIA = res.Get("IMAGEN_CATEGORIA");
                aux.IMAGEN_TIENDA = res.Get("IMAGEN_TIENDA");
                aux.IMAGEN_TIPO = res.Get("IMAGEN_TIPO");
                aux.BORRADO = res.Get("BORRADO");

                aux.TIENDA_LUNES=res.GetLong("LUNES");
                aux.TIENDA_MARTES=res.GetLong("MARTES");
                aux.TIENDA_MIERCOLES=res.GetLong("MIERCOLES");
                aux.TIENDA_JUEVES=res.GetLong("JUEVES");
                aux.TIENDA_VIERNES=res.GetLong("VIERNES");
                aux.TIENDA_SABADO=res.GetLong("SABADO");
                aux.TIENDA_DOMINGO=res.GetLong("DOMINGO");

                DateTime DAT = Utilerias.UtilsModel.obtenerFechaEntrega(DayOfWeek.Monday);
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

        public bool getProductoByPK()
        {
            try
            {
                string sql = "SELECT PR.*,ME.MEDIDA, ME.DESCRIPCION MEDIDA_DESCRIPCION " +
                             "FROM PRODUCTOS PR INNER JOIN MEDIDAS ME ON(PR.PK_MEDIDA= ME.PK) " +
                             "WHERE PR.PK =@PK";

                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK", PK);
                ResultSet res = db.getTable();

                if (res.Next())
                {
                    PK = res.Get("PK");
                    PK_CATEGORIA = res.Get("PK_CATEGORIA");
                    PK_TIENDA = res.Get("PK_TIENDA");
                    PRODUCTO = res.Get("PRODUCTO");
                    DESCRIPCION = res.Get("DESCRIPCION");
                    STOCK = res.Get("STOCK");
                    PK_MEDIDA = res.Get("PK_MEDIDA");
                    MEDIDA = res.Get("MEDIDA");
                    MEDIDA_DESCRIPCION = res.Get("MEDIDA_DESCRIPCION");
                    PRECIO = res.Get("PRECIO");
                    IMAGEN = res.Get("IMAGEN");
                    BORRADO = res.Get("BORRADO");
                    FECHA_C = res.Get("FECHA_C");
                    FECHA_M = res.Get("FECHA_M");
                    FECHA_D = res.Get("FECHA_D");
                    USUARIO_C = res.Get("USUARIO_C");
                    USUARIO_M = res.Get("USUARIO_M");
                    USUARIO_D = res.Get("USUARIO_D");
                    return true;
                }
            }
            catch { }
            return false;
        }

        public bool getPrecioByPK()
        {
            try
            {
                string sql = "SELECT PR.PRECIO " +
                             "FROM PRODUCTOS PR " +
                             "WHERE PR.PK =@PK ";

                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK", PK);
                ResultSet res = db.getTable();

                if (res.Next())
                {
                    PK = res.Get("PK");
                    PRECIO = res.Get("PRECIO");
                    return true;
                }
            }
            catch { }
            return false;
        }


    }
    public class Productos1Model { 
        public String PK { get; set; }
        public List<ProductosModel> productosList { get; set; }
    }
}