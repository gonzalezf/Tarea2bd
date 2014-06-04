using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace foro2.Controllers
{
    public class EliminarTemaController : Controller
    {
        //REVISADO!
        //
        // GET: /EliminarTema/
        public ActionResult Index(string id, string id2)
        {
            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();

            


            ManipularDatos.EjecutarSql("DELETE FROM tema WHERE( [nombre ] = '" + id + "') "); //elimina el tema q lleva ese nombre
            ManipularDatos.Desconectar();

           
        //    return RedirectToAction("Categoria", new RouteValueDictionary( new { controller = "Categoria" , action = "Index", id = id2 }));
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Categoria", action = "Index", id = id2 }));

            // return RedirectToAction( "Categoria","Pokemon");
            //return View();
        }
	}
}