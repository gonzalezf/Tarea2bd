using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
namespace foro2.Controllers
{
    public class prueba3Controller : Controller
    {
        //
        // GET: /prueba3/
        public ActionResult Index()
        {
            var persona = new Persona();
            
            return View(persona);
        }
        [HttpPost] //este metodo recibe informacion, por eso va el httppost
        public ActionResult Index(Persona persona)
        {
            ViewBag.Error = "FUNCIONAAAA!!";
            return View(persona);
        }

	}
}