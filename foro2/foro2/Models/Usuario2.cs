using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foro2.Models
{
    public class Usuario2
    {
        public string id_prueba{get;set;}
        public string name { get; set; }
        public string contrasenna { get; set; }

        public Usuario2() //constructor...
        {
            id_prueba = String.Empty;
            name = String.Empty;
            contrasenna = String.Empty;

        }


    }
}