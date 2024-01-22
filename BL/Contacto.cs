using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Contacto
    {
        public static ML.Result GetByIdRemitente(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMensajeriaEntities context = new DL.ESantiagoMensajeriaEntities())
                {
                    var query = context.ContactosGetByIdUsuarioRemitente(idUsuario).ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var usuario in query)
                        {
                            ML.Usuario Usuario =  new ML.Usuario();
                            Usuario.Email = usuario.Email;
                            Usuario.IdUsuario = usuario.IdUsuario;
                            Usuario.Username = usuario.Username;

                            result.Objects.Add(Usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al recuperar los contactos.";
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
        public static ML.Result Add(int idRemitente, int idDestinatario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMensajeriaEntities context = new DL.ESantiagoMensajeriaEntities())
                {
                    int rowsAffected = context.ContactosAdd(idRemitente, idDestinatario);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Contacto agregado.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al agregar contacto.";
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
        public static ML.Result Delete(int idRemitente, int idDestinatario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMensajeriaEntities context = new DL.ESantiagoMensajeriaEntities())
                {
                    int rowsAffected = context.ContactosDelete(idRemitente, idDestinatario);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Contacto eliminado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar contacto.";
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
    }
}
