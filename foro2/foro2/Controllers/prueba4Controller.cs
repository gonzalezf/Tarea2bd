using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;

namespace foro2.Controllers
{
    public class prueba4Controller : Controller
    {
        //
        // GET: /prueba4/
        public ActionResult Index()
        {
            var usuario = new Usuario2();

            return View(usuario);
        }
        [HttpPost] //este metodo recibe informacion, por eso va el httppost
        public ActionResult Index(Usuario2 usuario)
        {
            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();

          // ManipularDatos.EjecutarSql("INSERT INTO prueba VALUES('felipe','pelife10')");
           ManipularDatos.EjecutarSql("INSERT INTO prueba VALUES('"+usuario.name+"','"+usuario.contrasenna+"')");
         

           // ManipularDatos.EjecutarSql("INSERT INTO prueba("probando);
             //ManipularDatos.EjecutarSql("INSERT INTO prueba("+ViewData["name"]+","+ViewData["contrasenna"]+")");

            //    return RedirectToAction("About");            
            ManipularDatos.Desconectar();
          //  ViewBag.Error4 = "INSERT INTO prueba(" + usuario.name + "','" + usuario.contrasenna + "')'";
            //ViewBag.Error4 = "FUNCIONA ..."+usuario.name+"!!!";
            return View(usuario);
        }

    }
}