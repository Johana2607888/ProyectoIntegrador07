using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class Proveedor
    {
        [DisplayName("CÓDIGO")]
        public int codigo { get; set; }
        [MaxLength(length: 20, ErrorMessage = "Maximo 20 letras")]
        [DisplayName("NOMBRE")]
        public string nombre { get; set; }
        [MaxLength(length: 30, ErrorMessage = "Maximo 30 letras")]
        [DisplayName("DIRECCIÓN")]
        public string direccion { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [MaxLength(length: 9, ErrorMessage = "Maximo 9 dígitos")]
        [DisplayName("TELÉFONO")]
        public string telefono { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [DisplayName("CORREO")]
        public string correo { get; set; }

        [DisplayName("DISTRITO")]
        public string distrito { get; set; }
    }
}