using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Data.SqlClient;

namespace foro2.Controllers
{
    public class InboxController : Controller
    {
        //
        // GET: /Inbox/
        public ActionResult Index()
        {
            if (!tablasforo.IsLoggedIn(Session))
            {
                return Redirect("/");
            }
            tablasforo tf = new tablasforo();

            //Aca obtengo la informacion del inbox
            List<String> ret = tf.RetornarInboxInfo((String)Session["UserId"]);
            if(ret.Count() <= 0)
            {
                ViewBag.Error = "No inbox with the id "+(String)Session["UserId"];
                return View();
            }
            ViewBag.InboxId = ret[0];
            ViewBag.TotalMessages = ret[1];
            ViewBag.TotalUnreadMessages = ret[2];

            List<ModeloPM> ret2 = tf.RetornarMensajesPrivados((String)Session["UserId"]);
            ModeloPM[] mensajes = ret2.ToArray();
            ViewBag.Mensajes = mensajes;
            ViewBag.Error = "";
            return View();
        }
	}
}