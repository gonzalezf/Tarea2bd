//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace foro2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tema
    {
        public tema()
        {
        //    this.comentario = new HashSet<comentario>();
        }
    
        public int id_tema { get; set; }
        public Nullable<int> id_categoria { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string mensaje { get; set; }
        public Nullable<bool> publico { get; set; }
    
        public virtual categoria categoria { get; set; }
     //   public virtual ICollection<comentario> comentario { get; set; }
   //     public virtual usuario usuario { get; set; }
    }
}
