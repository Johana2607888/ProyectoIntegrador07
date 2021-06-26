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
        [DisplayName("CÓDIGO")]
        public int codigo { get; set; }
        [MaxLength(length: 20, ErrorMessage = "Máximo 20 letras")]
        [DisplayName("NOMBRE")]
        public string nombre { get; set; }
        [MaxLength(length: 20, ErrorMessage = "Máximo 20 letras")]
        [DisplayName("DIRECCIÓN")]
        public string direccion { get; set; }
        [MaxLength(length: 9, ErrorMessage = "Máximo 9 dígitos")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [DisplayName("TELÉFONO")]
        public string telefono { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [MaxLength(length: 30, ErrorMessage = "Máximo 30 letras")]
        [DisplayName("CORREO")]
        public string correo { get; set; }

        [DisplayName("DISTRITO")]
        public int distrito { get; set; }
    }
}