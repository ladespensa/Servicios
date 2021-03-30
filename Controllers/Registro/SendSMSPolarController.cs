using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Nexmo.Api;
using acmarkert.Models;
using acmarkert.Models.Cientes;
using RestSharp;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace acmarkert.Controllers.Registro
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendSMSPolarController : ControllerBase
    {

		// POST: api/SendSMS
		[HttpPost]
		public IActionResult Post([FromBody]ClientesModel model)
		{
			int result = 0;
			string msj = "¡Error al enviar codigo de veriicación!";
			try
			{
				if (model.TENGO_UN_CODIGO) {

					if (model.ObtenerCodigo()) {
						result = 1;
						msj = "¡Código obtenido!";
					}
				}
				else
				{

					model.GeneraCodigoVerificacion();
					if (model.Create())
					{
						if (EnviarCodigoSMS(model))
						{
							result = 1;
							msj = "¡Código enviado!";
						}
						/*
						if (model.WHATSAPP)
						{
							enviaWhatTWilio(model);
							result = 1;
							msj = "¡Codigo enviado!";
						}
						else
						{
							EnviarCodigoSMS(model);
							result = 1;
							msj = "¡Codigo enviado!";
						}*/
					}
				}
			}
			catch (Exception ex)
			{
				model.CODIGO = null;
			}
			return Ok(new { 
				resultado=result,
				mensaje=msj,
				cliente=model 
			});
		}

		private bool EnviarCodigoSMS(ClientesModel model)
		{
			try
			{
				var client = new Nexmo.Api.Client(creds: new Nexmo.Api.Request.Credentials
				{
					ApiKey = VariablesModel.getVariableValue("ApiKeyNexmo"), //"513e39de",
					ApiSecret = VariablesModel.getVariableValue("ApiSecretNexmo"), //"9CaNjhJGjDW9pp0q"
				});
				var results = client.SMS.Send(request: new SMS.SMSRequest
				{
					from = "ACMarket",
					to = "+52" + model.TELEFONO,
					text = "Tu còdigo de activaciòn acmarket es: " + model.CODIGO
				});
				return true;
			}
			catch (Exception e) {
				LogModel.registra("Error al enviar sms código de verificacíon",e.ToString());
			}
			return false;
		}

		public bool enviaWhatTWilio(ClientesModel model) {

			try
			{
				const string accountSid = "AC78d3900f6c583e7d1f9e4df1352d066e";
				const string authToken = "681557b7d2b0f8a1637aaafc440b0c05";

				TwilioClient.Init(accountSid, authToken);

				MessageResource message = MessageResource.Create(
				   from: new PhoneNumber("whatsapp:+14155238886"),
				   to: new PhoneNumber("whatsapp:+521"+model.TELEFONO),
				   body: "Tu código de verificación Polar es:\n" + model.CODIGO +
						 "\n\n" +
						 "_*Polar nunca te pedirá tu código de verificación fuera de la aplicación_\n" +
						 "*Nunca lo compartas por mensaje o teléfono*"
			   );

				if (message.Status.Equals("queued"))
				{
					return true;
				}
			}
			catch (Twilio.Exceptions.TwilioException ex)
			{
				LogModel.registra("Errora al enviar còdigo whatsapp", ex.ToString());
			}
			catch (Exception e) {
				LogModel.registra("Errora al enviar còdigo whatsapp", e.ToString());
			}

			return false;
		}

		/*
		private void GenerarCodigoWhatsResh(ClientesModel model) {

			var client = new RestClient("https://whatsmsapi.com/api/send_sms");
			var request = new RestRequest(Method.POST);
			request.AddHeader("cache-control", "no-cache");
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddHeader("x-api-key", "5e97de925d1fb");
			request.AddParameter("undefined", "phone=+522225496022&text=Hello%20World!", ParameterType.RequestBody);
			IRestResponse response = client.Execute(request);

		}
		private void GenerarCodigoWhats(ClientesModel model)
		{
			var client = new Nexmo.Api.Client(creds: new Nexmo.Api.Request.Credentials
			{
				ApiKey = "513e39de",
				ApiSecret = "9CaNjhJGjDW9pp0q",
				ApplicationId= "98a39a29-b761-4d0f-bc99-b95587ce11b6"
				//ApplicationKey=@"",
				

			});
			//client. 
			
			Dictionary<string, string> datos = new Dictionary<string, string>();
			JObject obj = new JObject();
			obj.Add("type", "whatsapp");
			obj.Add("number", "14157386170");
			
			JObject objTo = new JObject();
			objTo.Add("type", "whatsapp");
			objTo.Add("number", "+5222254960222");

			JObject obj2 = new JObject();
			obj2.Add("type", "text");
			obj2.Add("text", "This is a WhatsApp Message sent from the Messages API");
			obj2.Add("title", "ejemplo");
			
			JObject obj1 = new JObject();
			obj1.Add("content", obj2);

			datos.Add("from", obj.ToString());
			datos.Add("to", objTo.ToString());
			datos.Add("message", obj1.ToString());
			//string token= Nexmo.Api.Jwt.CreateToken("cce06df5-2625-48af-997d-b4c805b056a0", "9CaNjhJGjDW9pp0q");
			var resu =Nexmo.Api.Request.ApiRequest.DoRequest(new Uri("https://api.nexmo.com/v0.1/messages/"),datos,client.Credentials);
			/*
			var results = client.SMS.Send(request: new SMS.SMSRequest
			{

				from = "Polar",
				to = "+522225496022",
				text = "Tu codigo de activacion polar es: " + model.CODIGO,
				type = "wappush",
				url= "https://polar.house/",
				title="Polar"

			}) ;
		}*/

	}
}