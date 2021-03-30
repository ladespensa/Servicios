using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models
{
    public class LogModel
    {
        
        public static void registra(string error,string detalle) {
            try
            {
                database db = new database();
                string sql = "insert into LOG(ERROR,DETALLE) VALUES(@ERROR,@DETALLE)";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@ERROR", error);
                db.command.Parameters.AddWithValue("@DETALLE", detalle);

                db.execute();
            }
            catch (Exception e) {
                string er = e.ToString();
            }

        }
    }
}
