using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.SqlClient;

namespace foro2.Controllers
{
    public class CrearCategoriaController : Controller
    {
        //
        // GET: /CrearCategoria/
        public ActionResult Index()
        {

            if (String.Compare((String)Session["LoggedIn"], "Yes") != 0)
            {
                return Redirect("/");
            }

            if (String.Compare((String)Session["GroupId"], "1") != 0)
            {
                return Redirect("/");
            }

            ModeloCategoria categoria = new ModeloCategoria();

          





            return View(categoria);
        }

        [HttpPost]
        public ActionResult Index(ModeloCategoria categoria)
        {

            ViewBag.nombre = categoria.nombrecategoria;
            ViewBag.descripcion = categoria.descripcioncategoria;
            ViewBag.publico_categoria = categoria.publico_categoria;
            
        
            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();


            ManipularDatos.EjecutarSql("INSERT INTO categoria VALUES('" + categoria.nombrecategoria + "','" + categoria.descripcioncategoria+ "','" + categoria.publico_categoria + "')");
            ManipularDatos.Desconectar();




          //  return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Inicio", action = "Index" }));
            return View(categoria);
        }

	}
}