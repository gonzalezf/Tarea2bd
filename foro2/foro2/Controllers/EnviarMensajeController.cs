using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Globalization;
using System.Threading;

namespace foro2.Controllers
{
    public class EnviarMensajeController : Controller
    {
        //
        // GET: /EnviarMensaje/
        public ActionResult Index()
        {
            EnviarMensaje mensaje = new EnviarMensaje();
            return View(mensaje);
        }

        [HttpPost] //este metodo recibe informacion, por eso va el httppost
        public ActionResult Index(EnviarMensaje mensaje)
        {
            tablasforo tf = new tablasforo();
            int id_usuario = tf.ObtenerIdUsuario(mensaje.para);
            if(id_usuario == -1)
            {
                //No existe el usuario espeficiado, morir!
                ViewBag.Error = "No existe tal usuario ("+mensaje.para+")!";
                return View(mensaje);
            }

            List<String> inbox_info = tf.RetornarInboxInfo(id_usuario.ToString());

            if(inbox_info.Count() <= 0)
            {
                //Morir, no existe tal buzon!
                 ViewBag.Error = "No encontre el buzon de ese usuario";
                 return View(mensaje);
            }
            else if(int.Parse(inbox_info[0]) == -1)
            {
                 ViewBag.Error = "No encontre el buzon de ese usuario";
                //Morir, no existe tal buzon!
                 return View(mensaje);
            }
            int id_buzon = int.Parse(inbox_info[0]);

            string fecha_envio = DateTime.Now.ToString("dd/MM/yyyy");
            tf.Conectar();
            if(tf.EjecutarSql("INSERT INTO mensaje_privado VALUES('"+int.Parse((String) Session["UserId"])+"', '"+id_buzon.ToString()+"', 'False', '"+mensaje.mensaje+"', '"+fecha_envio+"')") <= 0)
            {
                ViewBag.Error = "Error al ejecutar query!";
            }
            else
            {
                ViewBag.Error = "";
            }
            tf.Desconectar();
            return View(mensaje);
        }
	}
}