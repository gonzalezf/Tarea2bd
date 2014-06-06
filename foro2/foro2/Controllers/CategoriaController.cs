using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Data.SqlClient;

namespace foro2.Controllers
{
    public class CategoriaController : Controller
    {
        //REVISADO!
        public ActionResult Index(string id) // Recuperar el id!y mostrar todos los temas!
        {
            ViewBag.TemaSeleccionado = id;
           
            string nombrecategoria = id; //nombre categoria recibe el nombre  de la categoria deseada, para buscar los temas mas adelante
            ModeloTema[] registros = null;
            tablasforo listadetemas = new tablasforo();
            registros = listadetemas.RetornarTemas(nombrecategoria, tablasforo.IsLoggedIn(Session)); //tenemos listado de temas
           

            ViewBag.ListaTemas = registros; // Este viewbag toma un arreglo el cual es imprimido 

            
            return View();
        }
	}
}