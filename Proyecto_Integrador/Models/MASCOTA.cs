//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_Integrador.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MASCOTA
    {
        public int IDE_MAS { get; set; }
        public string NOM_MAS { get; set; }
        public string RAZ_MAS { get; set; }
        public string SEX_MAS { get; set; }
        public string FEC_MAS { get; set; }
        public string ESTADO { get; set; }
        public Nullable<int> IDE_TIPO_MASCOTA { get; set; }
        public Nullable<int> IDE_CLI { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual TIPO_MASCOTA TIPO_MASCOTA { get; set; }
    }
}