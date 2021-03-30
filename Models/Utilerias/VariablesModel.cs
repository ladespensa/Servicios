using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models
{
    public class VariablesModel
    {
        public static string getVariableValue(string nombre) {
            database db = new database();
            string valor="";
            string sql = "SELECT * FROM VARIABLES WHERE NOMBRE=@NOMBRE";
            db.PreparedSQL(sql);
            db.command.Parameters.AddWithValue("@NOMBRE", nombre);

            ResultSet res = db.getTable();
            if (res.Next()) {
                valor = res.Get("VALOR");
            }
            return valor;
        }
    }
}
