using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result IniciarSesion(string username, byte[] password)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMensajeriaEntities context = new DL.ESantiagoMensajeriaEntities())
                {
                    var query = context.UsuarioCheck(username, password).FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Username = query.Username;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al recuperar el usuario.";
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
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMensajeriaEntities context = new DL.ESantiagoMensajeriaEntities())
                {
                    int rowsAffeccted = context.UsuarioAdd(usuario.Username, usuario.Email, usuario.Password);
                    if (rowsAffeccted > 0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario agregado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al agregar usuario.";
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
        public static ML.Result GetByUsername(string username)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoMensajeriaEntities context = new DL.ESantiagoMensajeriaEntities())
                {
                    var query = context.UsuarioGetByUsername(username).FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Username = query.Username;
                        usuario.Email = query.Email;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al recuperar al usuario.";
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
