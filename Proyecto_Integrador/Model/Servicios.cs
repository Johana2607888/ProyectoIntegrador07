using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class Servicios
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("NOMBRE")]
        public string nombre { get; set; }

        [DisplayName("DESCRIPCION")]
        public string descripcion { get; set; }

        [DisplayName("FECHA_SERVICIO")]
        public DateTime fechaservicio { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}