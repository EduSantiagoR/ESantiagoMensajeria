using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ContactosController : Controller
    {
        // GET: Contactos
        public ActionResult GetContactos(string token)
        {
            if(token != null)
            {
                bool correct = Models.Tokens.ValidateToken(token);
                if(correct)
                {
                    ML.Usuario usuario = (ML.Usuario)Session["Usuario"];
                    ML.Result result = BL.Contacto.GetByIdRemitente(usuario.IdUsuario);
                    usuario.Usuarios = result.Objects;
                    return View(usuario);
                }
                else
                {
                    ViewBag.Mensaje = "Acceso no autorizado. Inicia sesión primero.";
                    return PartialView("ModalError");
                }
            }
            else
            {
                ViewBag.Mensaje = "Acceso no autorizado. Inicia sesión primero.";
                return PartialView("ModalError");
            }
            
        }
        [HttpPost]
        public ActionResult Add(int idRemitente, string username)
        {
            ML.Result resultNewUsuario = BL.Usuario.GetByUsername(username);
            if (resultNewUsuario.Correct)
            {
                ML.Usuario newContact = (ML.Usuario) resultNewUsuario.Object;
                ML.Result result = BL.Contacto.Add(idRemitente, newContact.IdUsuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Contacto agregado correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error al agregar el contacto.";
                }
            }
            else
            {
                ViewBag.Mensaje = "El usuario que ingresaste no existe.";
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int idContacto)
        {
            ML.Usuario usuario = (ML.Usuario)Session["Usuario"];
            ML.Result result = BL.Contacto.Delete(usuario.IdUsuario, idContacto);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Contacto eliminado correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar el contacto.";
            }
            return PartialView("Modal");
        }

        //Métodos para AJAX
        [HttpGet]
        [Route("/Contactos/GetMensajes/{idUsuarioRemitente}/{idUsuarioDestinatario}")]
        public ActionResult GetMensajes(int idusuarioRemitente, int idUsuarioDestinatario)
        {
            ML.Result result = BL.Mensaje.GetMessagges(idusuarioRemitente, idUsuarioDestinatario);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Route("/Contactos/SendMessage/{idUsuarioRemitente}/{idUsuarioDestinatario}/{mensaje}")]
        public ActionResult SendMessage(int idUsuarioRemitente, int idUsuarioDestinatario, string mensaje)
        {
            ML.Mensaje mensajeNuevo = new ML.Mensaje();
            mensajeNuevo.IdUsuarioRemitente = idUsuarioRemitente;
            mensajeNuevo.IdUsuarioDestinatario = idUsuarioDestinatario;
            mensajeNuevo.Message = mensaje;
            ML.Result result = BL.Mensaje.EnviarMensaje(mensajeNuevo);
            return Json(result.Correct, JsonRequestBehavior.AllowGet);
        }
    }
}