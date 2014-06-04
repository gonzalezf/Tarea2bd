using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Controllers;

namespace foro2.Controllers
{
    public class EliminarCategoriaController : Controller
    {
        //REVISADO!
        // GET: /EliminarCategoria/
        public ActionResult Index(string id)
        {
            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();


       


            ManipularDatos.EjecutarSql("DELETE FROM categoria WHERE( [nombre ] = '"+id+"') "); //elimina la categoria q lleva ese nombre
            ManipularDatos.Desconectar();


          
            return RedirectToAction("Index", "Inicio");
            //return View();
        }
	}
}