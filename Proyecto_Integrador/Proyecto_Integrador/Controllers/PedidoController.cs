using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Proyecto_Integrador.Model;

namespace Proyecto_Integrador.Controllers
{
    public class PedidoController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        // GET: Pedido
        public ActionResult Index()
        {
            return View();
        }

        //lista de pedidos
        List<Pedido> listPedidos()
        {
            int id = int.Parse(Session["idUser"].ToString());
            List<Pedido> aPedidos = new List<Pedido>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOTRACKING", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ide_usu", id);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedidos.Add(new Pedido()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    fechapedido = dr[1].ToString(),
                    //descripcion = dr[2].ToString(),
                    cantidad = int.Parse(dr[2].ToString()),
                    //precio = double.Parse(dr[4].ToString()),
                    //monto = double.Parse(dr[5].ToString()),
                    cliente = dr[3].ToString(),
                    tracking = dr[4].ToString(),
                    //foto = dr[8].ToString()
                });

            }
            cn.Close();
            return aPedidos;
        }

        public ActionResult ListadoPedidos()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View(listPedidos());

        }


        List<Pedido> listPedidosDetalle(int id)
        {
            
            List<Pedido> aPedidos = new List<Pedido>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOTRACKING_DET", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ide_ped", id);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedidos.Add(new Pedido()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    fechapedido = dr[1].ToString(),
                    descripcion = dr[2].ToString(),
                    cantidad = int.Parse(dr[3].ToString()),
                    precio = double.Parse(dr[4].ToString()),
                    monto = double.Parse(dr[5].ToString()),
                    foto = dr[6].ToString()
                });

            }
            cn.Close();
            return aPedidos;
        }

        public ActionResult ListadoPedidosDetalle(int id)
        {
            return View(listPedidosDetalle(id));
        }


        List<Pedido> listdePedidos()
        {
            List<Pedido> aPedidos = new List<Pedido>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOPEDIDOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedidos.Add(new Pedido()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    fechapedido = dr[1].ToString(),
                    //descripcion = dr[2].ToString(),
                    cantidad = int.Parse(dr[2].ToString()),
                    //precio = double.Parse(dr[4].ToString()),
                    //monto = double.Parse(dr[5].ToString()),
                    cliente = dr[3].ToString(),
                    tracking = dr[4].ToString(),
                    //foto = dr[8].ToString()
                });

            }
            cn.Close();
            return aPedidos;
        }

        public ActionResult ListadodePedidos()
        {
            ViewBag.Message = "Bienvenido(a) " + Session["User"];
            return View(listdePedidos());

        }
        public ActionResult ListadodePedidosDetalle(int id)
        {
            return View(listPedidosDetalle(id));
        }

        List<Estado> listEstados()
        {
            List<Estado> aEstados = new List<Estado>();
            SqlCommand cmd = new SqlCommand("SP_LISTAESTADOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aEstados.Add(new Estado()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    descripcion = dr[1].ToString()
                });

            }
            cn.Close();
            return aEstados;
        }


        public ActionResult AtenderPedido(int id)
        {
            Pedido objP = listdePedidos().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.estado = new SelectList(listEstados(), "codigo", "descripcion");
            return View(objP);
        }

        [HttpPost]
        public ActionResult AtenderPedido(Pedido objP)
        {
            ViewBag.estado = new SelectList(listEstados(), "codigo", "descripcion");
            ViewBag.Success = "";
            ViewBag.Error = "";
            ViewBag.Info = "";

            if (objP.estado == 0)
            {
                ViewBag.Info = "Seleccione el estado";
                return View(objP);
            }
            if (objP.comentario == null)
            {
                ViewBag.Info = "Ingrese el comentario";
                return View(objP);
            }

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {

                SqlCommand cmd = new SqlCommand("SP_GRABARESTADOPEDIDO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide_ped", objP.codigo);
                cmd.Parameters.AddWithValue("@ide_est", objP.estado);
                cmd.Parameters.AddWithValue("@comenta", objP.comentario);
                int x = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.Success = " Pedido Actualizado..!!!";

            }
            catch
            {
                ViewBag.Error = " Registro incorrecto, verificar en llenar todos los campos";
                tr.Rollback();
                cn.Close();
                ViewBag.estado = new SelectList(listEstados(), "codigo", "descripcion");
                return View("~/Views/Pedido/AtenderPedido.cshtml");
            }
            finally
            {
                cn.Close();
            }

            ViewBag.estado = new SelectList(listEstados(), "codigo", "descripcion");
            return View("~/Views/Pedido/AtenderPedido.cshtml");

        }


    }
}