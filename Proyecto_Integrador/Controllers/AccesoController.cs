using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Integrador.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                using (Models.BD_PROYECTO_INTEGRADOREntities db = new Models.BD_PROYECTO_INTEGRADOREntities())
                {
                    var oUser = (from d in db.usuario
                                 
                                 where d.email == User.Trim() && d.password == Pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();
                    }

                    Session["User"] = oUser.nombre;
                    Session["idUser"] = oUser.id;
                    ViewBag.Message = Session["User"];
                    ViewBag.idUser = Session["idUser"];
                }

                return RedirectToAction("Principal", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}