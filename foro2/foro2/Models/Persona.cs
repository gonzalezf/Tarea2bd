using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foro2.Models
{
    public class Persona
    {
        public string Saludo { get; set; }
        public string Nombre { get; set; }

        public Persona() //constructor...
        {
            Saludo = "Hola ";
            Nombre = string.Empty;

        }


    }
}