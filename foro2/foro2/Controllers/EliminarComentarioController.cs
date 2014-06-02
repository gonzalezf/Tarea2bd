using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace foro2.Controllers
{
    public class EliminarComentarioController : Controller
    {
        //REVISADO
        // GET: /EliminarComentario/
        public ActionResult Index(string id,string id2)
        {
            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();





            ManipularDatos.EjecutarSql("DELETE FROM comentario WHERE( [id_comentario ] = '" + id + "') "); //eliminar comentario
            ManipularDatos.Desconectar();


            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "MostrarComentarios", action = "Index", id = id2 }));

            //return RedirectToAction("Index", "Inicio");
            //return View();
        }
	}
}