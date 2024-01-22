using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Helpers;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            byte[] passwordBytes = Encriptar(Encoding.UTF8.GetBytes(password));
            ML.Result result = BL.Usuario.IniciarSesion(username, passwordBytes);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario) result.Object;
                var token = Models.Tokens.GenerateJwtToken(usuario);
                usuario.Token = token;
                ViewBag.Mensaje = "Acceso concedido";
                ViewBag.Correct = true;
                Session["Usuario"] = usuario;
            }
            else
            {
                ViewBag.Mensaje = "Acceso denegado. Por favor, verifique sus credenciales.";
                ViewBag.Correct = false;
            }
            ViewBag.Direccion = "";
            return PartialView("Modal");
        }
        [HttpPost]
        public ActionResult Registrar(string username, string email, string password)
        {
            byte[] passwordBytes = Encriptar(Encoding.UTF8.GetBytes(password));
            ML.Usuario usuario = new ML.Usuario(username, email, passwordBytes);
            ML.Result result = BL.Usuario.Add(usuario);
            if(result.Correct)
            {
                ViewBag.Mensaje = "Usuario agregador correctamente. Ahora puedes iniciar sesión.";
                ViewBag.Correct = true;
            }
            else
            {
                ViewBag.Mensaje = "Error al agregar el usuario.";
                ViewBag.Correct = false;
            }
            ViewBag.Direccion = "Login";
            return PartialView("Modal");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }
        private static byte[] Encriptar(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] datosEncriptados = sha256.ComputeHash(data);
                return datosEncriptados;
            }
        }
        private static void ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    // Encabezado
                    foreach (var claim in jsonToken.Header)
                    {
                        Console.WriteLine($"{claim.Key}: {claim.Value}");
                    }

                    // Carga útil
                    foreach (var claim in jsonToken.Payload)
                    {
                        Console.WriteLine($"{claim.Key}: {claim.Value}");
                    }

                    // Firma
                    Console.WriteLine(jsonToken.RawSignature);
                }
            }
            else
            {
                Console.WriteLine("No se pudo leer el token.");
            }
        }
    }
}