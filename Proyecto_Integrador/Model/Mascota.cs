using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class Mascota
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }


        [DisplayName("NOMBRE")]
        public string nombre { get; set; }


        [DisplayName("RAZA")]
        public string raza { get; set; }


        [DisplayName("SEXO")]
        public string sexo { get; set; }


        [DisplayName("FECHA_NAC")]
        public DateTime fechanac { get; set; }


        [DisplayName("TIPO MASCOTA")]
        public string tipomascota { get; set; }


        [DisplayName("CLIENTE")]
        public string cliente { get; set; }


        [DisplayName("SERVICIO")]
        public string servicio { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }

    }
}