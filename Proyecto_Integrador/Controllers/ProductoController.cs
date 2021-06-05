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
    public class ProductoController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        List<Producto> listProductos()
        {
            List<Producto> aProductos = new List<Producto>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOPRODUCTOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProductos.Add(new Producto()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    proveedor = dr[2].ToString(),
                    categoria = dr[3].ToString(),
                    precio = double.Parse(dr[4].ToString()),
                    stock = int.Parse(dr[5].ToString()),
                    marca = dr[6].ToString()
                });

            }
            cn.Close();
            return aProductos;
        }

        List<ProductoO> listProductosO()
        {
            List<ProductoO> aProductos = new List<ProductoO>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPRODUCTOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProductos.Add(new ProductoO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    precio = double.Parse(dr[2].ToString()),
                    stock = int.Parse(dr[3].ToString()),
                    proveedor = int.Parse(dr[4].ToString()),
                    categoria = int.Parse(dr[5].ToString()), 
                    marca = int.Parse(dr[6].ToString())
                });

            }
            cn.Close();
            return aProductos;
        }
        
        public ActionResult listadoProductos()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View(listProductos());
        }

        List<Proveedor> listProveedor()
        {
            List<Proveedor> aProveedor = new List<Proveedor>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPROVEEDOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProveedor.Add(new Proveedor()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }
            cn.Close();
            return aProveedor;
        }

        List<Categoria> listCategoria()
        {
            List<Categoria> aCategoria = new List<Categoria>();
            SqlCommand cmd = new SqlCommand("SP_LISTACATEGORIA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCategoria.Add(new Categoria()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }
            cn.Close();
            return aCategoria;
        }

        List<Marca> listMarca()
        {
            List<Marca> aMarca = new List<Marca>();
            SqlCommand cmd = new SqlCommand("SP_LISTAMARCA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMarca.Add(new Marca()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()
                });

            }
            cn.Close();
            return aMarca;
        }

        public ActionResult eliminarProducto(int id)
        {
            ProductoO objP = listProductosO().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.Success = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINAPRODUCTO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide", objP.codigo);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Producto Eliminado..!!!";
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
            return RedirectToAction("listadoProductos");
        }


        public ActionResult modificarProducto(int id)
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            ProductoO objP = listProductosO().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.proveedor = new SelectList(listProveedor(), "codigo", "nombre");
            ViewBag.categoria = new SelectList(listCategoria(), "codigo", "nombre");
            ViewBag.marca = new SelectList(listMarca(), "codigo", "nombre");
            return View(objP);
        }

        [HttpPost]
        public ActionResult modificarProducto(ProductoO objP, HttpPostedFileBase f)
        {
            ViewBag.Success = "";
            ViewBag.Error = "";

            ViewBag.proveedor = new SelectList(listProveedor(), "codigo", "nombre");
            ViewBag.categoria = new SelectList(listCategoria(), "codigo", "nombre");
            ViewBag.marca = new SelectList(listMarca(), "codigo", "nombre");

            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";
            if (objP.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objP);
            }

            if (objP.precio == 0)
            {
                ViewBag.Info = "Ingrese el precio";
                return View(objP);
            }

            if (objP.stock == 0)
            {
                ViewBag.Info = "Ingrese el stock";
                return View(objP);
            }

            if (objP.marca == 0)
            {
                ViewBag.Info = "Seleccione la marca";
                return View(objP);
            }

            if (objP.proveedor == 0)
            {
                ViewBag.Info = "Seleccione el proveedor";
                return View(objP);
            }

            if (objP.categoria == 0)
            {
                ViewBag.Info = "Seleccione la categoria";
                return View(objP);
            }
            if (f == null)
            {
                ViewBag.Info = "Seleccione una imagen";
                return View(objP);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.Info = "Debe ser .JPG";
                return View(objP);
            }

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAPRODUCTO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide", objP.codigo);
                cmd.Parameters.AddWithValue("@nom", objP.nombre);
                cmd.Parameters.AddWithValue("@prov", objP.proveedor);
                cmd.Parameters.AddWithValue("@cat", objP.categoria);
                cmd.Parameters.AddWithValue("@pre", objP.precio);
                cmd.Parameters.AddWithValue("@sto", objP.stock);
                cmd.Parameters.AddWithValue("@mar", objP.marca);
                cmd.Parameters.AddWithValue("@fot", "~/fotos_productos/" + Path.GetFileName(f.FileName));
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Producto Actualizado..!!!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.proveedor = new SelectList(listProveedor(), "codigo", "nombre");
                ViewBag.categoria = new SelectList(listCategoria(), "codigo", "nombre");
                ViewBag.marca = new SelectList(listMarca(), "codigo", "nombre");
                tr.Rollback();
                return View("~/Views/Producto/modificarProducto.cshtml");
            }
            finally
            {
                cn.Close();
            }


            ViewBag.proveedor = new SelectList(listProveedor(), "codigo", "nombre");
            ViewBag.categoria = new SelectList(listCategoria(), "codigo", "nombre");
            ViewBag.marca = new SelectList(listMarca(), "codigo", "nombre");

            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_productos/"),
               Path.GetFileName(f.FileName)));

            //return View(objP);
            return View("~/Views/Producto/modificarProducto.cshtml");
        }


        public ActionResult registrarProducto()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            ViewBag.proveedor = new SelectList(listProveedor(), "codigo", "nombre");
            ViewBag.categoria = new SelectList(listCategoria(), "codigo", "nombre");
            ViewBag.marca = new SelectList(listMarca(), "codigo", "nombre");
            return View(new ProductoO());
        }

        [HttpPost]
        public ActionResult registrarProducto(ProductoO objP, HttpPostedFileBase f)
        {
            

            ViewBag.proveedor = new SelectList(listProveedor(), "codigo", "nombre");
            ViewBag.categoria = new SelectList(listCategoria(), "codigo", "nombre");
            ViewBag.marca = new SelectList(listMarca(), "codigo", "nombre");

            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";
            if (objP.nombre == null)
            {
                ViewBag.Info = "Ingrese el nombre";
                return View(objP);
            }

            if (objP.precio == 0)
            {
                ViewBag.Info = "Ingrese el precio";
                return View(objP);
            }

            if (objP.stock == 0)
            {
                ViewBag.Info = "Ingrese el stock";
                return View(objP);
            }

            if (objP.marca == 0)
            {
                ViewBag.Info = "Seleccione la marca";
                return View(objP);
            }

            if (objP.proveedor == 0)
            {
                ViewBag.Info = "Seleccione el proveedor";
                return View(objP);
            }

            if (objP.categoria == 0)
            {
                ViewBag.Info = "Seleccione la categoria";
                return View(objP);
            }
            if (f == null)
            {
                ViewBag.Info = "Seleccione una imagen";
                return View(objP);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.Info = "Debe ser .JPG";
                return View(objP);
            }

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_NUEVOPRODUCTO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nom", objP.nombre);
                cmd.Parameters.AddWithValue("@prov", objP.proveedor);
                cmd.Parameters.AddWithValue("@cat", objP.categoria);
                cmd.Parameters.AddWithValue("@pre", objP.precio);
                cmd.Parameters.AddWithValue("@sto", objP.stock);
                cmd.Parameters.AddWithValue("@mar", objP.marca);
                cmd.Parameters.AddWithValue("@fot", "~/fotos_productos/" + Path.GetFileName(f.FileName));
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = x.ToString() + " Producto Registrado..!!!";
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

            ViewBag.proveedor = new SelectList(listProveedor(), "codigo", "nombre");
            ViewBag.categoria = new SelectList(listCategoria(), "codigo", "nombre");
            ViewBag.marca = new SelectList(listMarca(), "codigo", "nombre");

            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_productos/"),
                Path.GetFileName(f.FileName)));

            //return View(objP);
            return View("~/Views/Producto/modificarProducto.cshtml");

        }

    }
}