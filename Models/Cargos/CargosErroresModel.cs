using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models
{
    public class CargosErroresModel
    {

        public string PK { get; set; }
        public string PK_PEDIDO { get; set; }
        public string category {get;set;}
public string description { get; set; }
public long http_code { get; set; }
        public long error_code { get; set; }
    public string request_id { get; set; }
public string FECHA_C { get; set; }

        database db;

        public CargosErroresModel()
        {
            db = new database();
        }

        public bool agregarCargoError()
        {
            try
            {
                    string sql = "INSERT INTO " +
                                 "CARGOS_ERRORES(category,description,http_code,error_code,request_id,PK_PEDIDO) " +
                                 "VALUES(@category,@description,@http_code,@error_code,@request_id,@PK_PEDIDO) ";

                    db.PreparedSQL(sql);
                    db.command.Parameters.AddWithValue("@category", category);
                    db.command.Parameters.AddWithValue("@description", description);
                    db.command.Parameters.AddWithValue("@http_code", http_code);
                    db.command.Parameters.AddWithValue("@error_code", error_code);
                    db.command.Parameters.AddWithValue("@request_id", request_id);
                    db.command.Parameters.AddWithValue("@PK_PEDIDO", PK_PEDIDO);
                   
                    PK = db.executeId();
                    if (!string.IsNullOrEmpty(PK))
                    {
                        return true;
                    }

            }
            catch (Exception e)
            {
                LogModel.registra("Error al guardar el error del cargo en la base", e.ToString() + " DATOS{category:" + category + ",description:" + description + ", http_code:" + http_code + ", error_code:" + error_code + ", request_id:" + request_id + "}");
            }
            return false;
        }


    }
}
