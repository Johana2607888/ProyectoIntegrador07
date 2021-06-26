using Proyecto_Integrador.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Proyecto_Integrador.Controllers
{
    public class AccesoController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
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
               
                cn.Open();
                List<Cliente> aCliente = new List<Cliente>();
                SqlCommand cmd = new SqlCommand("SP_INGRESOUSUARIO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user", User.Trim());
                cmd.Parameters.AddWithValue("@clave", Pass.Trim());
                
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    aCliente.Add(new Cliente()
                    {
                        codigo = int.Parse(dr[0].ToString()),
                        nombre = dr[1].ToString(),
                        idRol = int.Parse(dr[2].ToString())
                    });
                }

                if (aCliente.Count == 0)
                {
                    ViewBag.Error = "Usuario o clave invalida";
                    return View();
                }

                Session["idUser"] = aCliente[0].codigo;
                Session["User"] = aCliente[0].nombre;
                Session["idRol"] = aCliente[0].idRol;
                ViewBag.Message = Session["User"];
                ViewBag.idUser = Session["idUser"];
                ViewBag.idRol = Session["idRol"];
                return RedirectToAction("Principal", "Home");
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Registro Incorrecto, verificar en llenar todos los campos";
                return View();
            }
            finally
            {
                cn.Close();
            }

        }
    }
}