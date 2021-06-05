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
    public class ServicioController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        // GET: Servicio
        public ActionResult Index()
        {
            return View();
        }

        List<Servicios> listServicios()
        {
            List<Servicios> aServicios = new List<Servicios>();
            SqlCommand cmd = new SqlCommand("SP_LISTASERVICIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aServicios.Add(new Servicios()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    descripcion = dr[2].ToString(),
                    fechaservicio = DateTime.Parse(dr[3].ToString()),
                    precio = double.Parse(dr[4].ToString())
                });

            }
            cn.Close();
            return aServicios;
        }

        public ActionResult listadoServicios()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View(listServicios());
        }

        public ActionResult eliminarServicio(int id)
        {
            Servicios objP = listServicios().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.Success = "";
            ViewBag.Error = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINASERVICIO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_ser", objP.codigo);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Servicio Eliminado..!!!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                tr.Rollback();
                return RedirectToAction("listadoServicios");
            }
            finally
            {
                cn.Close();
            }
            return RedirectToAction("listadoServicios");
        }


        public ActionResult modificarServicio(int id)
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            Servicios objS = listServicios().Where(p => p.codigo == id).FirstOrDefault();
            return View(objS);
        }

        [HttpPost]
        public ActionResult modificarServicio(Servicios objS, HttpPostedFileBase f)
        {
            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";
            if (objS.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objS);
            }
            if (objS.descripcion == null)
            {
                ViewBag.Info = "Ingrese la descripcion";
                return View(objS);
            }
            if (objS.fechaservicio == null)
            {
                ViewBag.Info = "Ingrese la fecha";
                return View(objS);
            }
            if (objS.precio == 0)
            {
                ViewBag.Info = "Ingrese el precio";
                return View(objS);
            }

            if (f == null)
            {
                ViewBag.Info = "Seleccione una imagen";
                return View(objS);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.Info = "Debe ser .JPG";
                return View(objS);
            }

            
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZASERVICIO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_ser", objS.codigo);
                cmd.Parameters.AddWithValue("@nom_ser", objS.nombre);
                cmd.Parameters.AddWithValue("@des_ser", objS.descripcion);
                cmd.Parameters.AddWithValue("@fec_ser", objS.fechaservicio);
                cmd.Parameters.AddWithValue("@pre_ser", objS.precio);
                cmd.Parameters.AddWithValue("@img_ser", "~/fotos_servicios/" + Path.GetFileName(f.FileName));
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Servicio Actualizado..!!!";
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

            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_servicios/"),
               Path.GetFileName(f.FileName)));

            return View(objS);
        }


        public ActionResult registrarServicio()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View(new Servicios());
        }

        [HttpPost]
        public ActionResult registrarServicio(Servicios objS, HttpPostedFileBase f)
        {

            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";
            if (objS.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objS);
            }
            if (objS.descripcion == null)
            {
                ViewBag.Info = "Ingrese la descripcion";
                return View(objS);
            }
            if (objS.fechaservicio == null)
            {
                ViewBag.Info = "Ingrese la fecha";
                return View(objS);
            }
            if (objS.precio == 0)
            {
                ViewBag.Info = "Ingrese el precio";
                return View(objS);
            }

            if (f == null)
            {
                ViewBag.Info = "Seleccione una imagen";
                return View(objS);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.Info = "Debe ser .JPG";
                return View(objS);
            }

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_NUEVOSERVICIO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ide_ser", objS.codigo);
                cmd.Parameters.AddWithValue("@nom_ser", objS.nombre);
                cmd.Parameters.AddWithValue("@des_ser", objS.descripcion);
                cmd.Parameters.AddWithValue("@fec_ser", objS.fechaservicio);
                cmd.Parameters.AddWithValue("@pre_ser", objS.precio);
                cmd.Parameters.AddWithValue("@img_ser", "~/fotos_servicios/" + Path.GetFileName(f.FileName));
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Servicio Registrado..!!!";
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


            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_servicios/"),
                Path.GetFileName(f.FileName)));

            return View(objS);
            //return View("~/Views/Servicio/registrarServicio.cshtml");
        }
    }
}