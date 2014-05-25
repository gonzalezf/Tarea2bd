using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foro2.Models
{
    public class ModeloTema
    {
        public int id_categoria { get; set; }
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string mensaje { get; set; }
        public bool publico { get; set; }
        public string id_usuario_string { get; set; }


        public ModeloTema()
        {
            nombre = string.Empty;
            descripcion = string.Empty;
            mensaje = string.Empty;
            

        }
    }
}