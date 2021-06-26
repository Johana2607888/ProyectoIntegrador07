using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class Cliente
    {
        [DisplayName("CÓDIGO")]
        public int codigo { get; set; }

        [MaxLength(length: 20, ErrorMessage = "Maximo 20 letras")]
        [DisplayName("NOMBRE")]
        public string nombre { get; set; }

        [MaxLength(length: 20, ErrorMessage = "Maximo 20 letras")]
        [DisplayName("APELLIDO")]
        public string apellido { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [MaxLength(length: 8, ErrorMessage = "Ingresar solo 8 números")]
       
        [DisplayName("DNI")]
        public string dni { get; set; }

        [MaxLength(length: 30, ErrorMessage = "Maximo 30 letras")]
        [DisplayName("DIRECCIÓN")]
        public string direccion { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [MaxLength(length: 9, ErrorMessage = "Ingresar solo 9 números")]
        [DisplayName("CELULAR")]
        public string telefono { get; set; }

        [DisplayName("DISTRITO")]
        public int distrito { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail es no válido")]
        [DisplayName("CORREO")]
        public string correo { get; set; }


        [DisplayName("CONTRASEÑA")]
        public string password { get; set; }

        [DisplayName("DISTRITO")]
        public string desDistrito { get; set; }

        public int idRol { get; set; }


    }
}