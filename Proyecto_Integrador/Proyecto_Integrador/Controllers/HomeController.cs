
using Proyecto_Integrador.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Integrador.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View();
        }

        public ActionResult Principal()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];

            return View();
        }

    }
}