using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace foro2.Controllers
{
    public class VerMensajeController : Controller
    {
        //
        // GET: /VerMensaje/
        public ActionResult Index(string id_mensaje)
        {
            if(id_mensaje == null)
            {
                return Redirect("/");
            }
            tablasforo tf = new tablasforo();
            List<String> l = tf.GetMessageInfo(id_mensaje);
            ViewBag.NombreRemitente = l[0];
            ViewBag.IdMensaje = l[1];
            ViewBag.IdBuzon = l[2];
            ViewBag.IdRemitente = l[3];
            ViewBag.Leido = l[4];
            ViewBag.Mensaje = l[5];
            ViewBag.Fecha = l[6];
            return View();
        }
	}
}