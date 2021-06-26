using Proyecto_Integrador.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Proyecto_Integrador.Controllers
{
    public class MascotaController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

        // GET: Mascota
        public ActionResult Index()
        {
            ViewBag.servicio = new SelectList(listServicios(), "codigo", "nombre");
            ViewBag.tipomascota = new SelectList(listTipoMascota(), "codigo", "nombre");
            return View();
        }

        List<Mascota> listMascotas()
        {
            cn.Open();
            int id = int.Parse(Session["idUser"].ToString());
            List<Mascota> aMascotas = new List<Mascota>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOMASCOTANEW",cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ide_usu", id);

            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            aMascotas.Add(new Mascota()
            {
                codigo = int.Parse(dr[0].ToString()),
                nombre = dr[1].ToString(),
                raza = dr[2].ToString(),
                sexo = dr[3].ToString(),
                tipomascota = dr[4].ToString(),
                foto = dr[5].ToString()

            });

            cn.Close();
            return aMascotas;

        }
        
        List<MascotaO> listMascotasO()
        {
            
            
            List<MascotaO> aMascotasO = new List<MascotaO>();
            SqlCommand cmd = new SqlCommand("SP_LISTAMASCOTA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                aMascotasO.Add(new MascotaO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    raza = dr[2].ToString(),
                    sexo = dr[3].ToString(),
                    fechanac = dr[4].ToString(),
                    tipomascota = int.Parse(dr[5].ToString()),
                    servicio = int.Parse(dr[6].ToString()),
                    foto = dr[7].ToString()
                });

            cn.Close();
            return aMascotasO;

        }

        public ActionResult listadoMascotas()
        {
            
            return View(listMascotas());
        }

        public ActionResult MascotasClientes()
        {
            if (Session["mascotas"] == null)
            {
                Session["mascotas"] = new List<Item>();
            }
            return View(listMascotas());
        }

        public ActionResult eliminarMascota(int id)
        {
            MascotaO objP = listMascotasO().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.Success = "";
            ViewBag.Error = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINAMASCOTA", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_mas", objP.codigo);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Mascota Eliminada..!!!";
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
            return RedirectToAction("listadoMascotas","Mascota");
        }

        List<Servicios> listServicios()
        {
            List<Servicios> aServicio = new List<Servicios>();
            SqlCommand cmd = new SqlCommand("SP_LISTASERVICIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aServicio.Add(new Servicios()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }
            cn.Close();
            return aServicio;
        }


        List<TipoMascota> listTipoMascota()
        {
            List<TipoMascota> aServicio = new List<TipoMascota>();
            SqlCommand cmd = new SqlCommand("SP_LISTATIPOMASCOTA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aServicio.Add(new TipoMascota()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }
            cn.Close();
            return aServicio;
        }

        public ActionResult registrarMascota()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            ViewBag.servicio = new SelectList(listServicios(), "codigo", "nombre");
            ViewBag.tipomascota = new SelectList(listTipoMascota(), "codigo", "nombre");
            return View(new MascotaO());
        }

        [HttpPost]
        public ActionResult registrarMascota(MascotaO objM, HttpPostedFileBase f)
        {
            ViewBag.servicio = new SelectList(listServicios(), "codigo", "nombre");
            ViewBag.tipomascota = new SelectList(listTipoMascota(), "codigo", "nombre");

            

            if (objM.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objM);
            }

            if (objM.raza == null)
            {
                ViewBag.Info = "Ingrese la raza";
                return View(objM);
            }


            if (objM.sexo == null)
            {
                ViewBag.Info = "Ingrese el sexo";
                return View(objM);
            }

            if (objM.fechanac == null)
            {
                ViewBag.Info = "Ingrese la fecha";
                return View(objM);
            }

            if (objM.tipomascota == 0)
            {
                ViewBag.Info = "Seleccione el tipo de Mascota";
                return View(objM);
            }

            if (objM.servicio == 0)
            {
                ViewBag.Info = "Seleccione el servicio";
                return View(objM);
            }

            if (f == null)
            {
                ViewBag.Info = "Seleccione una imagen";
                return View(objM);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.Info = "Debe ser .JPG";
                return View(objM);
            }

            if (!ModelState.IsValid)
            {
                //ViewBag.Info = "Revisar las validaciones";
                return View(objM);
            }

            ViewBag.Success = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_NUEVOMASCOTA", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_usu", int.Parse(Session["idUser"].ToString()));
                cmd.Parameters.AddWithValue("@nom_mas", objM.nombre);
                cmd.Parameters.AddWithValue("@raz_mas", objM.raza);
                cmd.Parameters.AddWithValue("@sex_mas", objM.sexo);
                cmd.Parameters.AddWithValue("@fec_mas", objM.fechanac);
                cmd.Parameters.AddWithValue("@ide_tipomas", objM.tipomascota);
                cmd.Parameters.AddWithValue("@id_ser", objM.servicio);
                cmd.Parameters.AddWithValue("@img_mas", "~/fotos_mascotas/" + Path.GetFileName(f.FileName));
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Mascota Registrado..!!!";
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

            ViewBag.servicio = new SelectList(listServicios(), "codigo", "nombre");
            ViewBag.tipomascota = new SelectList(listTipoMascota(), "codigo", "nombre");

            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_mascotas/"),
                Path.GetFileName(f.FileName)));
            //return RedirectToAction("listadoMascotas");
            return View("~/Views/Mascota/registrarMascota.cshtml");

        }

        public ActionResult modificarMascota(int id)
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            MascotaO objM = listMascotasO().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.servicio = new SelectList(listServicios(), "codigo", "nombre");
            ViewBag.tipomascota = new SelectList(listTipoMascota(), "codigo", "nombre");
            return View(objM);
        }

        [HttpPost]
        public ActionResult modificarMascota(MascotaO objM, HttpPostedFileBase f)
        {
            ViewBag.servicio = new SelectList(listServicios(), "codigo", "nombre");
            ViewBag.tipomascota = new SelectList(listTipoMascota(), "codigo", "nombre");

            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";

            

            if (objM.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objM);
            }

            if (objM.raza == null)
            {
                ViewBag.Info = "Ingrese la raza";
                return View(objM);
            }


            if (objM.sexo == null)
            {
                ViewBag.Info = "Ingrese el sexo";
                return View(objM);
            }

            if (objM.fechanac == null)
            {
                ViewBag.Info = "Ingrese la fecha";
                return View(objM);
            }

            if (objM.tipomascota == 0)
            {
                ViewBag.Info = "Seleccione el tipo de Mascota";
                return View(objM);
            }

            if (objM.servicio == 0)
            {
                ViewBag.Info = "Seleccione el servicio";
                return View(objM);
            }

            if (f == null)
            {
                ViewBag.Info = "Seleccione una imagen";
                return View(objM);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.Info = "Debe ser .JPG";
                return View(objM);
            }

            if (!ModelState.IsValid)
            {
                //ViewBag.Info = "Revisar las validaciones";
                return View(objM);
            }

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAMASCOTA", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_mas", objM.codigo);
                cmd.Parameters.AddWithValue("@nom_mas", objM.nombre);
                cmd.Parameters.AddWithValue("@raz_mas", objM.raza);
                cmd.Parameters.AddWithValue("@sex_mas", objM.sexo);
                cmd.Parameters.AddWithValue("@fec_mas", objM.fechanac);
                cmd.Parameters.AddWithValue("@ide_tipomas", objM.tipomascota);
                cmd.Parameters.AddWithValue("@ide_ser", objM.servicio);
                cmd.Parameters.AddWithValue("@img_mas", "~/fotos_mascotas/" + Path.GetFileName(f.FileName));
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Mascota Actualizado..!!!";
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

            ViewBag.servicio = new SelectList(listServicios(), "codigo", "nombre");
            ViewBag.tipomascota = new SelectList(listTipoMascota(), "codigo", "nombre");
            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_mascotas/"),
               Path.GetFileName(f.FileName)));

            return View("~/Views/Mascota/modificarMascota.cshtml");
        }

        List<Historial> listHistorial(int id)
        {
            cn.Open();
            List<Historial> aHistorial = new List<Historial>();
            SqlCommand cmd = new SqlCommand("SP_LISTAHISTORIAL", cn);
            cmd.Parameters.AddWithValue("@ide_mas", int.Parse(Session["idUser"].ToString()));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                aHistorial.Add(new Historial()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    mascota = dr[1].ToString(),
                    tipomascota = dr[2].ToString(),
                    cliente = dr[3].ToString(),
                    incidencia = dr[4].ToString(),
                    fechaincidencia = DateTime.Parse(dr[5].ToString()),
                    observacion = dr[6].ToString()
                });
            cn.Close();
            return aHistorial;
        }

        public ActionResult listadoHistorial(int id)
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View(listHistorial(id));
        }
    }
}