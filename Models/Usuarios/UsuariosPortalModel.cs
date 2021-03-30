using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Usuarios
{
    public class UsuariosPortalModel
    {

        public string PK { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOS { get; set; }
        public string USUARIO { get; set; }
        public string PASSWORD { get; set; }
        public string PK_ROL { get; set; }
        public string BORRADO { get; set; }
        public string FECHA_C { get; set; }
        public string FECHA_M { get; set; }
        public string FECHA_D { get; set; }
        public string USUARIO_C { get; set; }
        public string USUARIO_M { get; set; }
        public string USUARIO_D { get; set; }
        public string TOKEN { get; set; }

        private database db;

        public UsuariosPortalModel()
        {
            db = new database();
        }

        public bool obtenerUsuarioByUsuario()
        {

            try
            {
                string sql = "SELECT * FROM USUARIOS_PORTAL WHERE USUARIO=@USUARIO";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@USUARIO", USUARIO);
                ResultSet res = db.getTable();
                if (res.Next())
                {
                    PK = res.Get("PK");
                    NOMBRE = res.Get("NOMBRE");
                    APELLIDOS = res.Get("APELLIDOS");
                    USUARIO = res.Get("USUARIO");
                    PASSWORD = res.Get("PASSWORD");
                    PK_ROL = res.Get("PK_ROL");
                    BORRADO = res.Get("BORRADO");
                    FECHA_C = res.Get("FECHA_C");
                    FECHA_M = res.Get("FECHA_M");
                    FECHA_D = res.Get("FECHA_D");
                    USUARIO_C = res.Get("USUARIO_C");
                    USUARIO_M = res.Get("USUARIO_M");
                    USUARIO_D = res.Get("USUARIO_D");
                }
            }
            catch (Exception e) { }

            return false;
        }

        public bool NuevoToken()
        {
            try
            {
                string sql = "UPDATE USUARIOS_PORTAL SET TOKEN=@TOKEN " +
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
