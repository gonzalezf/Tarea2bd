using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foro2.Models
{
	public class Usuario
	{
        public string nombre { get; set; }
        public string contrasenna { get; set; }
        public string fechadenacimiento { get; set; }
        public string sexo { get; set; }
        public string avatarurl { get; set; }
        public string repetircontrasenna { get; set; }
        public Usuario() //constructor
        {
            nombre = string.Empty;
            contrasenna = string.Empty;
            fechadenacimiento = string.Empty;
            sexo = string.Empty;
            avatarurl = string.Empty;
            repetircontrasenna = string.Empty;

        }
	}
}