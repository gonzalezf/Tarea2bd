using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace foro2.Controllers
{
    public class CerrarSesionController : Controller
    {
        //
        // GET: /CerrarSesion/
        public ActionResult Index()
        {
            Session["LoggedIn"] = "No";
            return Redirect("/");
        }
	}
}