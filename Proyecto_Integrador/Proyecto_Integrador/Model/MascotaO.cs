using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class MascotaO
    {
        [DisplayName("CÓDIGO")]
        public int codigo { get; set; }

        [MaxLength(length: 20, ErrorMessage = "Solo 20 caracteres como máximo")]
        [DisplayName("NOMBRE")]
        public string nombre { get; set; }
        [MaxLength(length: 20, ErrorMessage = "Solo 20 caracteres como máximo")]
        [DisplayName("RAZA")]
        public string raza { get; set; }
        [MaxLength(length: 20, ErrorMessage = "Solo 20 caracteres como máximo")]
        [DisplayName("SEXO")]
        public string sexo { get; set; }
        [MaxLength(length: 15, ErrorMessage = "Solo 15 caracteres como máximo")]
        [DisplayName("FECHA DE NACIMIENTO")]
        public string fechanac { get; set; }

        [DisplayName("TIPO MASCOTA")]
        public int tipomascota { get; set; }

        [DisplayName("CLIENTE")]
        public int cliente { get; set; }

        [DisplayName("SERVICIO")]
        public int servicio { get; set; }

        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}