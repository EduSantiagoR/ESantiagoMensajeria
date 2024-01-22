using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Mensaje
    {
        public static ML.Result GetMessagges(int idUsuarioRemitente, int idUsuarioDestino)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMensajeriaEntities context = new DL.ESantiagoMensajeriaEntities())
                {
                    var query = context.MensajeGetMessages(idUsuarioRemitente, idUsuarioDestino).ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Mensaje mensaje = new ML.Mensaje();
                            mensaje.IdMensaje = item.IdMensaje;
                            mensaje.IdUsuarioRemitente = item.IdUsuarioRemitente;
                            mensaje.IdUsuarioDestinatario = item.IdUsuarioDestinatario;
                            mensaje.Fecha = item.Fecha.Value;
                            mensaje.Message = item.Mensaje;
                            result.Objects.Add(mensaje);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se han podido recuperar los mensajes.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result EnviarMensaje(ML.Mensaje mensaje)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMensajeriaEntities context = new DL.ESantiagoMensajeriaEntities())
                {
                    int rowsAffected = context.MensajeSendMessage(mensaje.IdUsuarioRemitente, mensaje.IdUsuarioDestinatario, mensaje.Message);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Mensaje enviado.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al enviar mensaje";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
