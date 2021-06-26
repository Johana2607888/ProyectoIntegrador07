using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Proyecto_Integrador.Model;
using System.Web.UI;

namespace Proyecto_Integrador.Controllers
{
    public class ClienteController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public List<Distrito> ListDistritos()
        {
            
            List<Distrito> aDistrito = new List<Distrito>();
            SqlCommand cmd = new SqlCommand("SP_LISTADISTRITO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aDistrito.Add(new Distrito()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()

                });
            }

            cn.Close();
            return aDistrito;
        }

        public List<Cliente> ListCliente()
        {
            List<Cliente> aCliente = new List<Cliente>();
            SqlCommand cmd = new SqlCommand("SP_LISTACLIENTE", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCliente.Add(new Cliente()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    apellido = dr[2].ToString(),
                    dni = dr[3].ToString(),
                    direccion = dr[4].ToString(),
                    correo = dr[5].ToString(),
                    telefono = dr[6].ToString(),
                    desDistrito = dr[7].ToString()
                });
            }

            cn.Close();
            return aCliente;
        }

        public ActionResult ListadoCliente()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View(ListCliente());

        }

        public ActionResult registrarCliente()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");
            return View(new Cliente());
        }

        public ActionResult _RegistraCliente()
        {
            ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");

            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult registrarCliente(Cliente objC)
        {
            ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");
            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";

            //if (!ModelState.IsValid)
            //{
            //    //ViewBag.Info = "Revisar las validaciones";
            //    return View(objC);
            //}
            if (objC.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View("~/Views/Acceso/Login.cshtml");
            }

            if (objC.apellido == null)
            {
                ViewBag.Info = "Ingrese el apellido";
                return View("~/Views/Acceso/Login.cshtml");
            }

            if (objC.dni == null)
            {
                ViewBag.Info = "Ingrese el dni";
                return View("~/Views/Acceso/Login.cshtml");
            }

            if (objC.direccion == null)
            {
                ViewBag.Info = "Ingrese la direccion";
                return View("~/Views/Acceso/Login.cshtml");
            }

            if (objC.telefono == null)
            {
                ViewBag.Info = "Ingrese el telefono";
                return View("~/Views/Acceso/Login.cshtml");
            }



            if (objC.distrito == 0)
            {
                ViewBag.Info = "Seleccione el distrito";
                return View("~/Views/Acceso/Login.cshtml");
            }

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {

                SqlCommand cmd = new SqlCommand("SP_NUEVOCLIENTE", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nom_cli", objC.nombre);
                cmd.Parameters.AddWithValue("@ape_cli", objC.apellido);
                cmd.Parameters.AddWithValue("@dni_cli", objC.dni);
                cmd.Parameters.AddWithValue("@dir_cli", objC.direccion);
                cmd.Parameters.AddWithValue("@fon_cli", objC.telefono);
                cmd.Parameters.AddWithValue("@ide_dis", objC.distrito);
                cmd.Parameters.AddWithValue("@cor_cli", objC.correo);
                cmd.Parameters.AddWithValue("@pas_cli", objC.password);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = " Registro Correcto..!!!";

            }
            catch
            {
                ViewBag.Error = " Registro incorrecto, verificar en llenar todos los campos";
                tr.Rollback();
                cn.Close();
                ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");
                return View("~/Views/Acceso/Login.cshtml");
            }
            finally
            {
                cn.Close();
            }

            ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");
            return View("~/Views/Acceso/Login.cshtml");

        }

        public ActionResult eliminarCliente(int id)
        {
            Cliente objP = ListCliente().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.Success = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINACLIENTE", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_cli", objP.codigo);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Proveedor Eliminado..!!!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                tr.Rollback();
            }
            finally
            {
                cn.Close();
            }
            return RedirectToAction("ListadoCliente");
        }


        public ActionResult modificarCliente(int id)
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            Cliente objP = ListCliente().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");
            return View(objP);
        }

        [HttpPost]
        public ActionResult modificarCliente(Cliente objC)
        {
            ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");



            
            if (objC.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objC);
            }

            if (objC.apellido == null)
            {
                ViewBag.Info = "Ingrese el apellido";
                return View(objC);
            }

            if (objC.dni == null)
            {
                ViewBag.Info = "Ingrese el dni";
                return View(objC);
            }

            if (objC.direccion == null)
            {
                ViewBag.Info = "Ingrese la direccion";
                return View(objC);
            }

            if (objC.telefono == null)
            {
                ViewBag.Info = "Ingrese el telefono";
                return View(objC);
            }


            if (objC.distrito == 0)
            {
                ViewBag.Info = "Seleccione el distrito";
                return View(objC);
            }

            if (!ModelState.IsValid)
            {
                //ViewBag.Info = "Revisar las validaciones";
                return View(objC);
            }

            ViewBag.Success = "";
            ViewBag.Error = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZACLIENTE", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_cli", objC.codigo);
                cmd.Parameters.AddWithValue("@nom_cli", objC.nombre);
                cmd.Parameters.AddWithValue("ape_cli", objC.apellido);
                cmd.Parameters.AddWithValue("@dni_cli", objC.dni);
                cmd.Parameters.AddWithValue("@dir_cli", objC.direccion);
                cmd.Parameters.AddWithValue("@fon_cli", objC.telefono);
                cmd.Parameters.AddWithValue("@ide_dis", objC.distrito);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Cliente Actualizado..!!!";
            }
            catch (Exception ex)
            {
                
                ViewBag.Error = ex.Message;
                tr.Rollback();
                ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");
                return View("~/Views/Cliente/modificarCliente.cshtml");
            }
            finally
            {
                cn.Close();
            }

            ViewBag.distrito = new SelectList(ListDistritos(), "codigo", "nombre");
            return View("~/Views/Cliente/modificarCliente.cshtml");
        }

    }
}