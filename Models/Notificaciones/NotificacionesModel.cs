using Newtonsoft.Json;
using acmarkert.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace acmarkert.Models.Notificaciones
{
    public class NotificacionesModel
    {

        public string TITLE { get; set; }
        public string MESSAGE { get; set; }
        public string DATA { get; set; }
        public string TOKEN { get; set; }
        public string TELEFONOUSUARIO { get; set; }
        public List<string> TOKENS { get; set; }

        public string API_KEY { get; set; }
        public string resultl { get; set; }

        public NotificacionesModel()
        {
            API_KEY = Resources.API_FIREBASE;
            TOKENS = new List<string>();
        }

        public async Task<bool> sendNotificationAsync()
        {

            try
            {

                var messageInformation = new Message()
                {
                    notification = new Notification()
                    {
                        title = TITLE,
                        body = MESSAGE
                    },

                    registration_ids = TOKENS.ToArray()
                };
                //Object to JSON STRUCTURE => using Newtonsoft.Json;
                string jsonMessage = JsonConvert.SerializeObject(messageInformation);


                var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
                request.Headers.TryAddWithoutValidation("Authorization", "key=" + API_KEY);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    resultl = result.ToString();
                }

                //   saveNotification();

                return true;
            }
            catch (Exception e)
            {

            }

            return false;
        }

        public class Message
        {
            public string[] registration_ids { get; set; }
            public Notification notification { get; set; }
            public object data { get; set; }
        }

        public class Notification
        {
            public string title { get; set; }
            public string body { get; set; }
            public string data { get; set; }
        }


    }

}

