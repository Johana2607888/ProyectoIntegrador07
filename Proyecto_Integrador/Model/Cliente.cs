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
        [DisplayName("CODIGO")]
        public int codigo { get; set; }

        [DisplayName("NOMBRE")]
        public string nombre { get; set; }

        [DisplayName("APELLIDO")]
        public string apellido { get; set; }

        [DisplayName("DNI")]
        public string dni { get; set; }

        [DisplayName("DIRECCION")]
        public string direccion { get; set; }

        [DisplayName("CELULAR")]
        public string telefono { get; set; }

        [DisplayName("DISTRITO")]
        public int distrito { get; set; }

        [DisplayName("CORREO")]
        public string correo { get; set; }


        [DisplayName("CONTRASEÑA")]
        public string password { get; set; }

        [DisplayName("DISTRITO")]
        public string desDistrito { get; set; }



    }
}