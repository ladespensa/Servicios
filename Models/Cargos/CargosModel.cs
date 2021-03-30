using ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models
{
    public class CargosModel
    {
        public string PK { set; get; }
        public string ID { set; get; }
        public string AUTHORIZATION { set; get; }
        public double AMOUNT { set; get; }
        public string METHOD{ set; get; }
        public string ESTATUS { set; get; }
        public string CURRENCY { set; get; }
        public string PK_PEDIDO { set; get; }
        public string ERROR_MESSAGE{ set; get; }
        public string BORRADO{ set; get; }
        public string FECHA_C { get; set; }

        database db;

        public CargosModel() {
            db = new database();
        }

        public bool agregarCargo() {
            try {
                if (string.IsNullOrEmpty(ERROR_MESSAGE))
                {
                    string sql = "INSERT INTO " +
                                 "CARGOS(ID,AMOUNT,AUTHORIZATION1,METHOD,ESTATUS,PK_PEDIDO) " +
                                 "VALUES(@ID,@AMOUNT,@AUTHORIZATION1,@METHOD,@ESTATUS,@PK_PEDIDO)";

                    db.PreparedSQL(sql);
                    db.command.Parameters.AddWithValue("@ID", ID);
                    db.command.Parameters.AddWithValue("@AUTHORIZATION1", AUTHORIZATION);
                    db.command.Parameters.AddWithValue("@AMOUNT", AMOUNT);
                    db.command.Parameters.AddWithValue("@METHOD", METHOD);
                    db.command.Parameters.AddWithValue("@ESTATUS", ESTATUS);
                    //db.command.Parameters.AddWithValue("@CURRENCY", CURRENCY);
                    db.command.Parameters.AddWithValue("@PK_PEDIDO", PK_PEDIDO);

                    //db.command.Parameters.AddWithValue("@ERROR_MESSAGE", ERROR_MESSAGE);

                    PK = db.executeId();
                    if (!string.IsNullOrEmpty(PK))
                    {
                        return true;
                    }
                }
                else {
                    string sql = "INSERT INTO " +
                                     "CARGOS(ESTATUS,ERROR_MESSAGE) " +
                                     "VALUES(@ESTATUS,@ERROR_MESSAGE)";

                    db.PreparedSQL(sql);
                    db.command.Parameters.AddWithValue("@ESTATUS", ESTATUS);
                    db.command.Parameters.AddWithValue("@ERROR_MESSAGE", ERROR_MESSAGE);

                    PK = db.executeId();
                    if (!string.IsNullOrEmpty(PK))
                    {
                        return true;
                    }
                }

            } catch (Exception e) {
                LogModel.registra("Error al guardar el cargo en la base", e.ToString()+ " DATOS{ID:"+ID+ ",AUTHORIZATION:" + AUTHORIZATION+ ", METHOD:" + METHOD+ ", STATUS:" + ESTATUS+ ", PK_PEDIDO:" + PK_PEDIDO + ", ERROR_MESSAGE:" + ERROR_MESSAGE+"}");
            }
            return false;
        }
        public bool obtenerCargoByPK() {
            try
            {

                string sql = "SELECT * FROM CARGOS WHERE PK_PEDIDO=@PK_PEDIDO";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_PEDIDO", PK_PEDIDO);

                ResultSet res = db.getTable();

                if (res.Next())
                {
                    PK = res.Get("PK");
                    ID = res.Get("ID");
                    AUTHORIZATION = res.Get("AUTHORIZATION1");
                    AMOUNT = res.GetDouble("AMOUNT");
                    METHOD = res.Get("METHOD");
                    ESTATUS = res.Get("ESTATUS");
                    BORRADO = res.Get("BORRADO");
                    FECHA_C = res.Get("FECHA_C");
                    return true;
                }

            }
            catch (Exception e)
            {
                LogModel.registra("Error al obtener el cargo ", e.ToString() + " DATOS{PK_PEDIDO:" + PK_PEDIDO + "}");
            }
            return false;
        }


    }

}
