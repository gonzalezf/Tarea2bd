using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foro2.Models
{
    public class ModeloTema
    {
        public string id_categoria_string { get; set; }
        public int id_categoria { get; set; }
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string mensaje { get; set; }
        public bool publico { get; set; }
        public string id_usuario_string { get; set; }
        public int id_tema { get; set; }
        public int cantidad_mensajes { get; set; }


        public ModeloTema()
        {
            nombre = string.Empty;
            descripcion = string.Empty;
            mensaje = string.Empty;
            id_categoria_string = string.Empty;
            

        }
    }
}