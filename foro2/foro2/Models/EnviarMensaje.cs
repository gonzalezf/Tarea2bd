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

            mensaje = string.Empty;
        }

        /*
        public EnviarMensaje(string p, string a, string m)
        {
            para = p;
            asunto = a;
            mensaje = m;
        }*/
        
    }
}