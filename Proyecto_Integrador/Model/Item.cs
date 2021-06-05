using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class Item
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }
        [DisplayName("PRODUCTO")]
        public string nombre { get; set; }
        [DisplayName("PRECIO")]
        public double precio { get; set; }
        [DisplayName("CANTIDAD")]
        public int cantidad { get; set; }
        [DisplayName("SUBTOTAL")]
        public double subtotal { get { return cantidad * precio; } }
        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}