using ConnectDB;
using Openpay;
using Openpay.Entities;
using acmarkert.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Cientes
{
    public class ClientesModel
    {
		public string PK { get; set; }
	public string OPENID{get;set;}
	public string TELEFONO{get;set;}
	public string PASSWORD{get;set;}
	public string NOMBRE{get;set;}
	public string APELLIDOS{get;set;}
	public string FECHA_NACIMIENTO{get;set;}
	public string GENERO{get;set;}
	public string CORREO{get;set;}
	public string CODIGO { get; set; }
	public string TOKEN { get; set; }
	public string FOTO { get; set; }
	public string PLATAFORMA{get;set;}
	public string BORRADO{get;set;}
	public string FECHA_C{get;set;}
	public string FECHA_M{get;set;}
	public string FECHA_D{get;set;}
	public string USUARIO_C{get;set;}
	public string USUARIO_M{get;set;}
	public string USUARIO_D{get;set;}
		public bool TENGO_UN_CODIGO { get; set; }
		public bool WHATSAPP { get; set; }
		private database db;
		private OpenpayAPI api = null;
		public ClientesModel()
		{
			db = new database();
		}
		public bool getUsuarioByTelefono() {
			try
			{
				string sql = "SELECT * FROM CLIENTES WHERE TELEFONO=@TELEFONO AND BORRADO =0";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@TELEFONO", TELEFONO);
				ResultSet res = db.getTable();
				if (res.Next()) {
					PK = res.Get("PK");
					TELEFONO = res.Get("TELEFONO");
					PASSWORD = res.Get("PASSWORD");
					NOMBRE = res.Get("NOMBRE");
					APELLIDOS = res.Get("APELLIDOS");
					FECHA_NACIMIENTO = res.Get("FECHA_NACIMIENTO");
					GENERO = res.Get("GENERO");
					CORREO = res.Get("CORREO");
					OPENID = res.Get("OPENID");
					FOTO = res.Get("FOTO");
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
			catch { 
			
			}

			return false;
		}


		public bool getClienteByPk() {
			try
			{
				string sql = "SELECT * FROM CLIENTES WHERE PK=@PK AND BORRADO =0";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK", PK);
				ResultSet res = db.getTable();
				if (res.Next()) {
					PK = res.Get("PK");
					TELEFONO = res.Get("TELEFONO");
					PASSWORD = res.Get("PASSWORD");
					NOMBRE = res.Get("NOMBRE");
					APELLIDOS = res.Get("APELLIDOS");
					FECHA_NACIMIENTO = res.Get("FECHA_NACIMIENTO");
					GENERO = res.Get("GENERO");
					CORREO = res.Get("CORREO");
					OPENID = res.Get("OPENID");
					FOTO = res.Get("FOTO");
					TOKEN = res.Get("TOKEN");
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
			catch { 
			
			}

			return false;
		}


		public List<ClientesModel> getClientes()
		{
			List<ClientesModel> lista = new List<ClientesModel>();


			string sql = "SELECT * FROM CLIENTES  where NOMBRE IS NOT NULL AND APELLIDOS IS NOT NULL AND NOT CORREO IS NULL";
			db.PreparedSQL(sql);
			ResultSet res = db.getTable();
			ClientesModel aux;
			while (res.Next())
			{
				try
				{
					aux = new ClientesModel();
					aux.PK = res.Get("PK");
					aux.NOMBRE = res.Get("NOMBRE");
					aux.APELLIDOS = res.Get("APELLIDOS");
					aux.CORREO = res.Get("CORREO");
					lista.Add(aux);
				}
				catch
				{

				}
			}
			return lista;
		}

		public bool getTokenClienteByPk() {
			try
			{
				string sql = "SELECT NOMBRE FROM CLIENTES WHERE PK=@PK AND BORRADO =0";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PK", PK);
				ResultSet res = db.getTable();
				if (res.Next()) {
					NOMBRE = res.Get("NOMBRE");
					return true;
				}
			}
			catch { 
			
			}

			return false;
		}

		public void GeneraCodigoVerificacion()
		{
			CODIGO = (new Random()).Next(10001, 99999).ToString();
		}

		public bool ObtenerCodigo()
		{
			try
			{
				string sql = "SELECT PK,CODIGO FROM CLIENTES WHERE TELEFONO='" + TELEFONO + "'";
				ResultSet res = db.getTable(sql);
				if (res.Next())
				{
					PK = res.Get("PK");
					CODIGO = res.Get("CODIGO");
					return true;
				}				
			}
			catch { }
			return false;
		}
		public bool Create()
		{
			try
			{
				string sql = "SELECT PK FROM CLIENTES WHERE TELEFONO='" + TELEFONO + "'";
				ResultSet res = db.getTable(sql);
				if (res.Next())
				{
					PK = res.Get("PK");
					sql = "UPDATE CLIENTES SET CODIGO='" + CODIGO + "' WHERE PK=" + PK;
					db.execute(sql);
				}
				else
				{
					sql = "INSERT INTO CLIENTES (TELEFONO,CODIGO) VALUES ('" + TELEFONO + "','" + CODIGO + "')";
					PK = db.executeId(sql);
				}

				return true;
			}
			catch { }
			return false;
		}

		public bool complementaDatos()
		{
			try
			{
				string sql = "UPDATE CLIENTES SET PASSWORD=@PASSWORD, NOMBRE=@NOMBRE,APELLIDOS=@APELLIDOS," +
							 "GENERO=@GENERO,FECHA_NACIMIENTO=@FECHA_NACIMIENTO,CORREO=@CORREO " +
							 "WHERE PK=@PK";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PASSWORD", PASSWORD);
				db.command.Parameters.AddWithValue("@NOMBRE", NOMBRE);
				db.command.Parameters.AddWithValue("@APELLIDOS", APELLIDOS);
				db.command.Parameters.AddWithValue("@GENERO", GENERO);
				db.command.Parameters.AddWithValue("@FECHA_NACIMIENTO", FECHA_NACIMIENTO);
				db.command.Parameters.AddWithValue("@CORREO", CORREO);
				db.command.Parameters.AddWithValue("@PK", PK);
				
				if (db.execute())
				{
					return true;
				}
			}
			catch { }
			return false;
		}
		public bool actualizaDatosPerfil()
		{
			try
			{
				string sql = "UPDATE CLIENTES SET NOMBRE=@NOMBRE, APELLIDOS=@APELLIDOS," +
							 "CORREO=@CORREO" +
							 (!string.IsNullOrEmpty(FOTO)?",FOTO=@FOTO ":" ") +
							 "WHERE PK=@PK";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@NOMBRE", NOMBRE);
				db.command.Parameters.AddWithValue("@APELLIDOS", APELLIDOS);
				db.command.Parameters.AddWithValue("@CORREO", CORREO);
				if (!string.IsNullOrEmpty(FOTO))
				{
					db.command.Parameters.AddWithValue("@FOTO", FOTO);
				}
				db.command.Parameters.AddWithValue("@PK", PK);
				
				if (db.execute())
				{
					return true;
				}
			}
			catch { }
			return false;
		}
		
		public bool NuevoPassword()
		{
			try
			{
				string sql = "UPDATE CLIENTES SET PASSWORD=@PASSWORD " +
							 "WHERE PK=@PK ";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@PASSWORD", PASSWORD);
				db.command.Parameters.AddWithValue("@PK", PK);
				
				if (db.execute())
				{
					return true;
				}
			}
			catch { }
			return false;
		}
		public bool NuevoToken()
		{
			try
			{
				string sql = "UPDATE CLIENTES SET TOKEN=@TOKEN, PLATAFORMA=@PLATAFORMA " +
							 "WHERE PK=@PK ";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@TOKEN", TOKEN);
				db.command.Parameters.AddWithValue("@PLATAFORMA", PLATAFORMA);
				db.command.Parameters.AddWithValue("@PK", PK);
				
				if (db.execute())
				{
					return true;
				}
			}
			catch { }
			return false;
		}
		
		public bool saveIdOpenPay()
		{
			try
			{
				string sql = "UPDATE CLIENTES SET OPENID=@OPENID " +
							 "WHERE PK=@PK ";
				db.PreparedSQL(sql);
				db.command.Parameters.AddWithValue("@OPENID", OPENID);
				db.command.Parameters.AddWithValue("@PK", PK);
				
				if (db.execute())
				{
					return true;
				}
			}
			catch { }
			return false;
		}

		public bool registraOpenPay() {

			if (Resources.DEVELOP.ToUpper().Equals("TRUE"))
			{
				api = new OpenpayAPI(Resources.SK_OPEN_PAY_DEV, Resources.ID_OPEN_PAY_DEV,false);
			}
			else
			{
				api = new OpenpayAPI(Resources.SK_OPEN_PAY, Resources.ID_OPEN_PAY,true);
			}
			Customer request = new Customer();
			//request.ExternalId = PK;
			request.Name = NOMBRE;
			request.LastName = APELLIDOS;
			request.Email =	CORREO;
			/*
			request.PhoneNumber = "4421432915";
			request.RequiresAccount = false;
			Address address = new Address();
			address.City = "Queretaro";
			address.CountryCode = "MX";
			address.State = "Queretaro";
			address.PostalCode = "79125";
			address.Line1 = "Av. Pie de la cuesta #12";
			address.Line2 = "Desarrollo San Pablo";
			address.Line3 = "Qro. Qro.";
			request.Address = address;
			*/
			request = api.CustomerService.Create(request);

			OPENID = request.Id;

			return saveIdOpenPay();
		}

	}
}
