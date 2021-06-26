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
        [DisplayName("CÓDIGO")]
        public int codigo { get; set; }

        [MaxLength(length: 20, ErrorMessage = "Maximo 20 letras")]
        [DisplayName("PRODUCTO")]
        public string nombre { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [DisplayName("STOCK")]
        public int stock { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }

        [DisplayName("PROVEEDOR")]
        public int proveedor { get; set; }

        [DisplayName("CATEGORÍA")]
        public int categoria { get; set; }

        [DisplayName("MARCA")]
        public int marca { get; set; }
    }
}