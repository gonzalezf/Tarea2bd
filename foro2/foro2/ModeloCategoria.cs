using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using foro2.Models;

namespace foro2
{
    public class ModeloCategoria
    {
        public string nombrecategoria;
        public string descripcioncategoria;
        public bool publico_categoria;



        public ModeloCategoria()
        {
            nombrecategoria = string.Empty;
            descripcioncategoria = string.Empty;
        }
    }
}