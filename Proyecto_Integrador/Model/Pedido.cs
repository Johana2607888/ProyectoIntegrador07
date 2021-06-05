using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Integrador.Model
{
    public class Pedido
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("FECHA PEDIDO")]
        public string fechapedido { get; set; }
        [DisplayName("DESCRIPCION")]
        public string descripcion { get; set; }
        [DisplayName("CANTIDAD")]
        public int cantidad { get; set; }
        [DisplayName("PRECIO")]
        public double precio { get; set; }
        [DisplayName("MONTO")]
        public double monto { get; set; }
        [DisplayName("CLIENTE")]
        public string cliente { get; set; }
        [DisplayName("TRACKING")]
        public string tracking { get; set; }
        [DisplayName("IMAGEN")]
        public string foto { get; set; }
    }
}