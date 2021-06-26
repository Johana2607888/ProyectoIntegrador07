using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class Historial
    {
        [DisplayName("CÓDIGO")]
        public int codigo { get; set; }
        [DisplayName("ID MASCOTA")]
        public int id_mascota { get; set; }
        [DisplayName("NOMBRE MASCOTA")]
        public string mascota { get; set; }
        [DisplayName("TIPO MASCOTA")]
        public string tipomascota { get; set; }
        [DisplayName("CLIENTE")]
        public string cliente { get; set; }
        [DisplayName("INCIDENCIA")]
        public string incidencia { get; set; }
        [DisplayName("FECHA INCIDENCIA")]
        public DateTime fechaincidencia { get; set; }
        [DisplayName("OBSERVACION")]
        public string observacion { get; set; }
    }
}