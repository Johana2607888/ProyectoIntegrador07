using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Proyecto_Integrador.Model;

namespace Proyecto_Integrador.Controllers
{
    public class TiendaController : Controller
    {

        SqlConnection cn = new SqlConnection(ConfigurationManager.
                                 ConnectionStrings["cn"].ConnectionString);

        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        List<Producto> ListProducto()
        {
            List<Producto> aProductos = new List<Producto>();
            SqlCommand cmd = new SqlCommand("SP_LISTPRODUCTOSDET", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    aProductos.Add(new Producto()
                    {
                        codigo = int.Parse(dr[0].ToString()),
                        nombre = dr[1].ToString(),
                        precio = double.Parse(dr[2].ToString()),
                        stock = int.Parse(dr[3].ToString()),
                        foto = dr[4].ToString()
                    });
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
            }
            return aProductos;
        }

        public ActionResult carritoCompras()
        {

            if(Session["mensaje"] != null)
            {
                ViewBag.Success = Session["mensaje"];
            }

            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            if (Session["carrito"] == null)
            {
                Session["carrito"] = new List<Item>();
            }
            return View(ListProducto());
        }

        public ActionResult seleccionaProducto(int id)
        {
            Producto objP = ListProducto().Where(p => p.codigo == id).FirstOrDefault();
            return View(objP);
        }

        public ActionResult agregarProducto(int id, int cant = 0)
        {
            var miProducto = ListProducto().Where(p => p.codigo == id).FirstOrDefault();

            Item objI = new Item()
            {
                codigo = miProducto.codigo,
                nombre = miProducto.nombre,
                precio = miProducto.precio,
                cantidad = cant,
                foto = miProducto.foto
            };

            var miCarrito = (List<Item>)Session["carrito"];
            miCarrito.Add(objI);
            Session["carrito"] = miCarrito;
            Session["mensaje"] = null;
            return RedirectToAction("carritoCompras");
        }

        public ActionResult comprar()
        {
            if (Session["carrito"] == null)
            {
                return RedirectToAction("carritoCompras");
            }

            var miCarrito = (List<Item>)Session["carrito"];
            ViewBag.total = miCarrito.Sum(s => s.subtotal);
            return View(miCarrito);
        }

        //Metodo que elimina un producto de la compra
        public ActionResult eliminaProducto(int id = 0)
        {
            if (id == null) return RedirectToAction("carritoCompras");
            var miCarrito = (List<Item>)Session["carrito"];
            var item = miCarrito.Where(i => i.codigo == id).FirstOrDefault();
            miCarrito.Remove(item);

            Session["carrito"] = miCarrito;
            return RedirectToAction("comprar");
        }


        public ActionResult comprarProducto()
        {
            var miCarrito = (List<Item>)Session["carrito"];
            ViewBag.Success = "";
            ViewBag.Error = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                foreach (var item in miCarrito)
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAPEDIDO", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ide_pro", item.codigo);
                    cmd.Parameters.AddWithValue("@pre_ped", item.precio);
                    cmd.Parameters.AddWithValue("@can_ped", item.cantidad);
                    cmd.Parameters.AddWithValue("@des_ped", item.nombre);
                    cmd.Parameters.AddWithValue("@ide_cli", int.Parse(Session["idUser"].ToString()));

                    int x = cmd.ExecuteNonQuery();
                }
                tr.Commit();
                ViewBag.Success = " Pedido Registrado..!!!";

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
            Session["carrito"] = null;
            ListProducto();
            Session["mensaje"] = ViewBag.Success;
            return RedirectToAction("carritoCompras", "Tienda");


        }
    }
}