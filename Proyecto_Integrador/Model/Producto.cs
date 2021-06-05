using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class Producto
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("PRODUCTO")]
        public string nombre { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("STOCK")]
        public int unidades { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }

        [DisplayName("PROVEEDOR")]
        public string proveedor { get; set; }

        [DisplayName("CATEGORIA")]
        public string categoria { get; set; }

        [DisplayName("STOCK")]
        public int stock { get; set; }

        [DisplayName("MARCA")]
        public string marca { get; set; }



    }
}