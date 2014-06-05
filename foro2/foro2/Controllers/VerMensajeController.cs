using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;

namespace foro2.Controllers
{
    public class VerMensajeController : Controller
    {
        //
        // GET: /VerMensaje/
        public ActionResult Index(string id_mensaje)
        {
            string[] error = {"", "", ""};
            ViewBag.Error = error;
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
            tf.Conectar();
            tf.EjecutarSql("UPDATE buzon_entrada SET mensajes_sin_leer =  (CASE WHEN ((SELECT mensaje_privado.leido FROM mensaje_privado WHERE mensaje_privado.id_mensaje = '"+l[1]+"') = 'False') THEN (mensajes_sin_leer - 1) ELSE (mensajes_sin_leer) END) WHERE id_buzon = '"+l[2]+"';UPDATE mensaje_privado SET leido = 'True' WHERE id_mensaje = '"+l[1]+"';");
            tf.Desconectar();
            var m = new EnviarMensaje();
            return View(m);
        }

        [HttpPost] //este metodo recibe informacion, por eso va el httppost
        public ActionResult Index(EnviarMensaje m)
        {
            string[] error = { "", "", "" };
            ViewBag.Error = error;
            ViewBag.probandopara = m.para;
            ViewBag.probandoasunto = m.asunto;
            ViewBag.probandomensaje = m.mensaje;

            tablasforo tf = new tablasforo();

            int id_usuario = int.Parse(ViewBag.IdRemitente);
            if (id_usuario == -1)
            {
                //No existe el usuario espeficiado, morir!
                ViewBag.Error = error;
                ViewBag.Error[0] = "No existe tal usuario (" + m.para + ")!";
                return View(m);
            }

            List<String> inbox_info = tf.RetornarInboxInfo(id_usuario.ToString());

            if (inbox_info.Count() <= 0)
            {
                //Morir, no existe tal buzon!
                ViewBag.Error = error;
                ViewBag.Error[0] = "No encontre el buzon de ese usuario";
                return View(m);
            }
            else if (int.Parse(inbox_info[0]) == -1)
            {
                ViewBag.Error = error;
                ViewBag.Error[0] = "No encontre el buzon de ese usuario";
                //Morir, no existe tal buzon!
                return View(m);
            }
            if (m.asunto.Length <= 0)
            {
                ViewBag.Error = error;
                ViewBag.Error[0] = "No especificó un asunto";
                return View(m);
            }
            if (m.mensaje.Length <= 0)
            {
                ViewBag.Error = error;
                ViewBag.Error[1] = "El mensaje es muy corto";
                return View(m);
            }
            else if (m.mensaje.Length > 350)
            {
                ViewBag.Error = error;
                ViewBag.Error[1] = "El mensaje es muy largo";
                return View(m);
            }
            int id_buzon = int.Parse(inbox_info[0]);

            string fecha_envio = DateTime.Now.ToString("dd/MM/yyyy");
            tf.Conectar();
            if (tf.EjecutarSql("INSERT INTO mensaje_privado VALUES('" + int.Parse((String)Session["UserId"]) + "', '" + id_buzon.ToString() + "', 'False', '" + m.mensaje + "', '" + fecha_envio + "', '" + m.asunto + "')") <= 0)
            {
                ViewBag.Error[0] = "Error al ejecutar query!";
            }
            else
            {
                ViewBag.Error[2] = "¡Mensaje Enviado!";
            }
            tf.Desconectar();
            return Redirect("/Inbox/Index");
        }
	}
}