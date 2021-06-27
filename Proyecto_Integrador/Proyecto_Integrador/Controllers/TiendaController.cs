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

        //listado de productos
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
        //carrito de compras
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
        //seleccionar producto
        public ActionResult seleccionaProducto(int id)
        {
            Producto objP = ListProducto().Where(p => p.codigo == id).FirstOrDefault();
            return View(objP);
        }

        //agregar producto
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

        //comprar
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

        //comprar producto
        public ActionResult comprarProducto()
        {
            int x = 0;
            var miCarrito = (List<Item>)Session["carrito"];
            ViewBag.Success = "";
            ViewBag.Error = "";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_REGISTRAPEDIDO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_cli", int.Parse(Session["idUser"].ToString()));
                cmd.Parameters.AddWithValue("@can_ped", miCarrito.Count);
                //cmd.Parameters.AddWithValue("@id_ped", DbType.Int32).Direction= ParameterDirection.Output;

                SqlParameter inc_ID = new SqlParameter("@id_ped", DbType.Int32);
                inc_ID.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(inc_ID);

                //cmd.Parameters.Add("@id_ped", SqlDbType.Int);
                //cmd.Parameters["@id_ped"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int idPedido = int.Parse(cmd.Parameters["@id_ped"].Value.ToString());

                if (idPedido > 0)
                {
                    foreach (var item in miCarrito)
                    {
                        SqlCommand cmd2 = new SqlCommand("SP_REGISTRAPEDIDO_DET", cn, tr);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@ide_ped", idPedido);
                        cmd2.Parameters.AddWithValue("@ide_pro", item.codigo);
                        cmd2.Parameters.AddWithValue("@pre_ped", item.precio);
                        cmd2.Parameters.AddWithValue("@can_ped", item.cantidad);

                        x = cmd2.ExecuteNonQuery();
                    }
                }

                if (x > 0)
                {
                    tr.Commit();
                    ViewBag.Success = " Pedido Registrado..!!!";
                }
                else
                {
                    ViewBag.Error = "No se pudo registrar los pedidos";
                    tr.Rollback();
                }

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