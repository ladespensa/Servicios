using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models
{
    public class ErroresOpenPayModel
    {

        public static string error(int codigo) {

            string error="";

            switch (codigo)
            {
                case 3001:
                    error = "La tarjeta fue declinada.";
                    break;
                case 3002:
                    error = "La tarjeta ha expirado.";
                    break;
                case 3003:
                    error = "La tarjeta no tiene fondos suficientes.";
                    break;
                case 3004:
                    error = "La tarjeta ha sido identificada como una tarjeta robada.";
                    break;
                case 3005:
                    error = "La tarjeta ha sido rechazada por el sistema antifraudes.";
                    break;

                case 3006:
                    error = "La operación no esta permitida para este cliente o esta transacción.";
                    break;
                case 3007:
                    error = "Deprecado. La tarjeta fue declinada.";
                    break;
                case 3008:
                    error = "La tarjeta no es soportada en transacciones en línea.";
                    break;
                case 3009:
                    error = "La tarjeta fue reportada como perdida.";
                    break;
                case 3010:
                    error = "El banco ha restringido la tarjeta.";
                    break;
                case 3011:
                    error = "El banco ha solicitado que la tarjeta sea retenida. Contacte al banco.";
                    break;
                case 3012:
                    error = "Se requiere solicitar al banco autorización para realizar este pago.";
                    break;
                case 2006:
                    error = "El código de seguridad de la tarjeta (CVV2) no fue proporcionado.";
                    break;
                case 2007:
                    error = "El número de tarjeta es de prueba, solamente puede usarse en Sandbox.";
                    break;
                case 2009:
                    error = "El código de seguridad de la tarjeta (CVV2) es inválido.";
                    break;
                default:
                    error = "Error al hacer el cargo.";
                    break;
            }
            return error;
        }

    }
}
