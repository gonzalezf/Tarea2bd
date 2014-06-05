using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foro2.Models
{
    public class EnviarMensaje
    {
        public string para { get; set; }
        public string asunto {get;set;}
        public string mensaje {get;set;}
        public EnviarMensaje()
        {
            para = string.Empty;
            asunto = string.Empty;
            mensaje = string.Empty;
        }        
    }
}