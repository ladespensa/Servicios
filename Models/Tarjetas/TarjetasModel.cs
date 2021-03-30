using ConnectDB;
using Openpay;
using Openpay.Entities;
using Openpay.Entities.Request;
using acmarkert.Models.Cientes;
using acmarkert.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Tarjetas
{
    public class TarjetasModel
    {
        public string PK { get; set; }
        public string PK_CLIENTE { get; set; }
        public string ID_TARJETA { get; set; }
        public string DIGITOS { get; set; }
        public string MARCA { get; set; }
        public string TYPE { get; set; }
        public string TITULAR { get; set; }
        public string AÑO { get; set; }
        public string MES { get; set; }
        public string BANCO { get; set; }
        public string DEVICE_SESSION_ID { get; set; }
        public string BORRADO { get; set; }
        public string FECHA_C { get; set; }
        public string FECHA_M { get; set; }
        public string FECHA_D { get; set; }
        public string USUARIO_C { get; set; }
        public string USUARIO_M { get; set; }
        public string USUARIO_D { get; set; }
        private database db;
        private OpenpayAPI api;
        public Card card{get;set;}
        public string ERROR { get; set; }

        public TarjetasModel() {
            db = new database();
        }
        public bool agregarTarjeta() {
            try
            {
                ClientesModel cliente = new ClientesModel();
                cliente.PK = PK_CLIENTE;
                cliente.getClienteByPk();
                if (!String.IsNullOrEmpty(cliente.OPENID))
                {
                    if (Resources.DEVELOP.ToUpper().Equals("TRUE"))
                    {
                        api = new OpenpayAPI(Resources.SK_OPEN_PAY_DEV, Resources.ID_OPEN_PAY_DEV, false);
                    }
                    else
                    {
                        api = new OpenpayAPI(Resources.SK_OPEN_PAY, Resources.ID_OPEN_PAY, true);
                    }
                    
                    Card request = new Card();
                    request.TokenId = ID_TARJETA;
                    request.DeviceSessionId = DEVICE_SESSION_ID;
                    request = api.CardService.Create(cliente.OPENID, request);
                    MARCA = request.Brand;
                    MARCA = request.CardNumber;
                    card = request;
                    return !String.IsNullOrEmpty(request.Brand);
                }
                /*
                string sql = "INSERT INTO TARJETAS_CLIENTES(PK_CLIENTE,ID_TARJETA,DIGITOS,MARCA)" +
                             "VALUES (@PK_CLIENTE,@ID_TARJETA,@DIGITOS,@MARCA)";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
                db.command.Parameters.AddWithValue("@ID_TARJETA", ID_TARJETA);
                db.command.Parameters.AddWithValue("@DIGITOS", request.CardNumber);
                db.command.Parameters.AddWithValue("@MARCA", request.Brand);
                PK = db.executeId();
                if (!string.IsNullOrEmpty(PK)) {
                    return true;
                }
                */

            }
            catch (OpenpayException e1)
            {
                string error1 = e1.ToString();
                LogModel.registra("error al agregar tajera Openpay ", error1);
                ERROR = ErroresOpenPayModel.error(e1.ErrorCode);
            }
            catch (Exception e)
            {
                string error = e.ToString();
                LogModel.registra("error al agregar tajera", error);
            }
            return false;

        }

        public List<TarjetasModel> obneterTargetasPorPkCliente()
        {
            List<TarjetasModel> tarjetasmodel = new List<TarjetasModel>();
            TarjetasModel aux;
            try
            {

                string sql = "SELECT * FROM TARJETAS_CLIENTES WHERE PK_CLIENTE=@PK_CLIENTE";
                db.PreparedSQL(sql);
                db.command.Parameters.AddWithValue("@PK_CLIENTE", PK_CLIENTE);
                ResultSet res = db.getTable();
                while (res.Next()) {
                    aux = new TarjetasModel();
                    aux.PK = res.Get("PK");
                    aux.PK_CLIENTE = res.Get("PK_CLIENTE");
                    aux.ID_TARJETA = res.Get("ID_TARJETA");
                    aux.DIGITOS = res.Get("DIGITOS");
                    aux.MARCA = res.Get("MARCA");
                    aux.BORRADO = res.Get("BORRADO");
                    aux.FECHA_C = res.Get("FECHA_C");
                    aux.FECHA_M = res.Get("FECHA_M");
                    aux.FECHA_D = res.Get("FECHA_D");
                    aux.USUARIO_C = res.Get("USUARIO_C");
                    aux.USUARIO_M = res.Get("USUARIO_M");
                    aux.USUARIO_D = res.Get("FECHA_D");
                    tarjetasmodel.Add(aux);
                }


            }
            catch { }
            return tarjetasmodel;

        }

        
        public List<Card> obneterTargetasPorPkCliente1()
        {
            List<Card> cards = new List<Card>();
            TarjetasModel aux;
            try
            {
                ClientesModel cliente = new ClientesModel();
                cliente.PK = PK_CLIENTE;
                cliente.getClienteByPk();
                if (Resources.DEVELOP.ToUpper().Equals("TRUE"))
                {
                    api = new OpenpayAPI(Resources.SK_OPEN_PAY_DEV, Resources.ID_OPEN_PAY_DEV, false);
                }
                else
                {
                    api = new OpenpayAPI(Resources.SK_OPEN_PAY, Resources.ID_OPEN_PAY, true);
                }
                
                SearchParams request = new SearchParams();
                //request.CreationGte = new DateTime(2100, 5, 1);
                //request.CreationLte = new DateTime(2014, 5, 15);
                //request.Offset = 0;
                request.Limit = 100;
                cards = api.CardService.List(cliente.OPENID, request);

            }
            catch (Exception e){
                LogModel.registra("Error al obtener tarjetas", e.ToString());
            }
            return cards;

        }



    }
}
