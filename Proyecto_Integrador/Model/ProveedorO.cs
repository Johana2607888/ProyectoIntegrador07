using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class ProveedorO
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("NOMBRE")]
        public string nombre { get; set; }

        [DisplayName("DIRECCION")]
        public string direccion { get; set; }

        [DisplayName("TELEFONO")]
        public string telefono { get; set; }

        [DisplayName("CORREO")]
        public string correo { get; set; }

        [DisplayName("DISTRITO")]
        public int distrito { get; set; }
    }
}