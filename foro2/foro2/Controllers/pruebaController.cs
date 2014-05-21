using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;


namespace foro2.Controllers
{
    public class pruebaController : Controller
    {
        //
        // GET: /prueba/
        XEEntities prue = new XEEntities();

        public ActionResult Index()
        {
            return View(prue.prueba.ToList()); /*NOMBRE DE LA tabla, y luego convertirla en lista*/
        }
	}
}