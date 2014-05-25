using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace foro2.Models
{
    public class ModeloComentario
    {
        public int id_comentario;
        public int id_tema;
        public int id_usuario;
        public string comentario;
        public string id_tema_string;

        public ModeloComentario()
        {
            comentario = string.Empty;
            id_tema_string = string.Empty;
        }
    }
}