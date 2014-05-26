using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace foro2.Models
{
    public class ModeloComentario
    {
        public int id_comentario {get;set;}
        public int id_tema{get;set;}
        public int id_usuario {get;set;}
        public string comentario { get; set; }
        public string id_tema_string {get;set;}
        public string id_comentario_string { get; set; }
        public string comentarioeditado { get; set; }

        public ModeloComentario()
        {
            comentario = string.Empty;
          
        }
    }
}