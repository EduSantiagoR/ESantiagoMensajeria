using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ML.Usuario usuario = (ML.Usuario)Session["Usuario"];
            if(usuario != null)
            {
                bool result = Models.Tokens.ValidateToken(usuario.Token);
                if(result)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Usuario");
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}