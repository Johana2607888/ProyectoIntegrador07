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
                    descripcion = dr[2].ToString(),
                    cantidad = int.Parse(dr[3].ToString()),
                    precio = double.Parse(dr[4].ToString()),
                    monto = double.Parse(dr[5].ToString()),
                    cliente = dr[6].ToString(),
                    tracking = dr[7].ToString(),
                    foto = dr[8].ToString()
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


    }
}