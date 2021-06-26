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
        [DisplayName("CÓDIGO")]
        public int codigo { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(20)]
        [MaxLength(length: 20, ErrorMessage = "Maximo 20 letras")]
        [DisplayName("PRODUCTO")]
        public string nombre { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [DisplayName("CANTIDAD")]
        public int unidades { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }

        [DisplayName("PROVEEDOR")]
        public string proveedor { get; set; }

        [DisplayName("CATEGORÍA")]
        public string categoria { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [DisplayName("STOCK")]
        public int stock { get; set; }

        [DisplayName("MARCA")]
        public string marca { get; set; }



    }
}