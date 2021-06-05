using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class ProductoO
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("PRODUCTO")]
        public string nombre { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("STOCK")]
        public int stock { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }

        [DisplayName("PROVEEDOR")]
        public int proveedor { get; set; }

        [DisplayName("CATEGORIA")]
        public int categoria { get; set; }

        [DisplayName("MARCA")]
        public int marca { get; set; }
    }
}