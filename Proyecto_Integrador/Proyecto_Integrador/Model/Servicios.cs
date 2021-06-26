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
        [DisplayName("CÓDIGO")]
        public int codigo { get; set; }
        [MaxLength(length: 20, ErrorMessage = "Maximo 20 letras")]
        [DisplayName("NOMBRE")]
        public string nombre { get; set; }

        [MaxLength(length: 20, ErrorMessage = "Maximo 20 dígitos")]
        [DisplayName("DESCRIPCIÓN")]
        public string descripcion { get; set; }
        [MaxLength(length: 15, ErrorMessage = "Maximo 15 dígitos")]
        [DisplayName("FECHA_SERVICIO")]
        public DateTime fechaservicio { get; set; }

        [DisplayName("PRECIO")]
        public double precio { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}