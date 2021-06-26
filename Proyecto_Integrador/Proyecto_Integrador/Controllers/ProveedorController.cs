using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Proyecto_Integrador.Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Proyecto_Integrador.Controllers
{
    public class ProveedorController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        List<Proveedor> listProveedor()
        {
            List<Proveedor> aProveedor = new List<Proveedor>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOPROVEEDOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProveedor.Add(new Proveedor()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    direccion = dr[2].ToString(),
                    telefono = dr[3].ToString(),
                    correo = dr[4].ToString(),
                    distrito = dr[5].ToString()

                });

            }
            cn.Close();
            return aProveedor;
        }

        List<ProveedorO> listProveedorO()
        {
            List<ProveedorO> aProveedorO = new List<ProveedorO>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPROVEEDOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProveedorO.Add(new ProveedorO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    direccion = dr[2].ToString(),
                    telefono = dr[3].ToString(),
                    correo = dr[4].ToString(),
                    distrito = int.Parse(dr[5].ToString())

                });

            }
            cn.Close();
            return aProveedorO;
        }

        public ActionResult listadoProveedores()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View(listProveedor());
        }



        List<Distrito> listDistritos()
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

        public ActionResult eliminarProveedor(int id)
        {
            ProveedorO objP = listProveedorO().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.mensaje = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINAPROVEEDOR", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide", objP.codigo);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = x.ToString() + " Proveedor Eliminado..!!!";
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                tr.Rollback();
            }
            finally
            {
                cn.Close();
            }
            return RedirectToAction("listadoProveedores");
        }


        public ActionResult modificarProveedor(int id)
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            ProveedorO objP = listProveedorO().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.distrito = new SelectList(listDistritos(), "codigo", "nombre");
            return View(objP);
        }

        [HttpPost]
        public ActionResult modificarProveedor(ProveedorO objP)
        {
            ViewBag.distrito = new SelectList(listDistritos(), "codigo", "nombre");
            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";

            

            if (objP.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objP);
            }

            if (objP.direccion == null)
            {
                ViewBag.Info = "Ingrese la direccion";
                return View(objP);
            }

            if (objP.telefono == null)
            {
                ViewBag.Info = "Ingrese el telefono";
                return View(objP);
            }

            if (objP.correo == null)
            {
                ViewBag.Info = "Ingrese el correo";
                return View(objP);
            }

            if (objP.distrito == 0)
            {
                ViewBag.Info = "Seleccione el distrito";
                return View(objP);
            }

            if (!ModelState.IsValid)
            {
                //ViewBag.Info = "Revisar las validaciones";
                return View(objP);
            }

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAPROVEEDOR", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide", objP.codigo);
                cmd.Parameters.AddWithValue("@nom", objP.nombre);
                cmd.Parameters.AddWithValue("@dir_prov", objP.direccion);
                cmd.Parameters.AddWithValue("@fon_prov", objP.telefono);
                cmd.Parameters.AddWithValue("@cor_prov", objP.correo);
                cmd.Parameters.AddWithValue("@ide_dis", objP.distrito);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Proveedor Actualizado..!!!";
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

            ViewBag.distrito = new SelectList(listDistritos(), "codigo", "nombre");
            return View(objP);
        }


        public ActionResult registrarProveedor()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            ViewBag.distrito = new SelectList(listDistritos(), "codigo", "nombre");
            return View(new ProveedorO());
        }

        [HttpPost]
        public ActionResult registrarProveedor(ProveedorO objP)
        {
            ViewBag.distrito = new SelectList(listDistritos(), "codigo", "nombre");
            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";

            

            if (objP.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objP);
            }

            if (objP.direccion == null)
            {
                ViewBag.Info = "Ingrese la direccion";
                return View(objP);
            }

            if (objP.telefono == null)
            {
                ViewBag.Info = "Ingrese el telefono";
                return View(objP);
            }

            if (objP.correo == null)
            {
                ViewBag.Info = "Ingrese el correo";
                return View(objP);
            }

            if (objP.distrito == 0)
            {
                ViewBag.Info = "Seleccione el distrito";
                return View(objP);
            }

            if (!ModelState.IsValid)
            {
                //ViewBag.Info = "Revisar las validaciones";
                return View(objP);
            }

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_NUEVOPROVEEDOR", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nom", objP.nombre);
                cmd.Parameters.AddWithValue("@dir_prov", objP.direccion);
                cmd.Parameters.AddWithValue("@fon_prov", objP.telefono);
                cmd.Parameters.AddWithValue("@cor_prov", objP.correo);
                cmd.Parameters.AddWithValue("@ide_dis", objP.distrito);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Proveedor Registrado..!!!";
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

            ViewBag.distrito = new SelectList(listDistritos(), "codigo", "nombre");
            return View(objP);

        }

    }
}