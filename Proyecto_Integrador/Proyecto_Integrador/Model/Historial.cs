using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Model
{
    public class Historial
    {
        public int codigo { get; set; }
        public int id_mascota { get; set; }
        public string mascota { get; set; }
        public string tipomascota { get; set; }
        public string cliente { get; set; }
        public string incidencia { get; set; }
        public DateTime fechaincidencia { get; set; }
        public string observacion { get; set; }
    }
}