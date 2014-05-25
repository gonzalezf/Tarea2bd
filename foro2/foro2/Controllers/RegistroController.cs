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
    public class RegistroController : Controller
    {
        //
        // GET: /Registro/
        public ActionResult Index() //se utiliza al principio cuando aun no se ha enviado alguna informacion
        {
            var usuario = new Usuario();
            return View(usuario);
        }

      
        [HttpPost] //este metodo recibe informacion, por eso va el httppost
        public ActionResult Index(Usuario usuario)
        {
            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();
            string cantidadcomentarios = "0"; //no ha ingresado ningun comentario todavia...

            string fecha_registro = DateTime.Now.ToString("dd/MM/yyyy");
            if (String.Compare(usuario.contrasenna, usuario.repetircontrasenna) == 0)
            {
                ManipularDatos.EjecutarSql("INSERT INTO usuario VALUES(" + 3 + ",'" + usuario.nombre + "','" + usuario.contrasenna + "','" + cantidadcomentarios + "','" + usuario.avatarurl + "','" + usuario.fechadenacimiento + "','" + usuario.sexo + "','" + fecha_registro + "')");

                ManipularDatos.Desconectar();
                //return RedirectToAction("/Inicio/Index");
                //return RedirectToRoute("/Inicio/Index");  <!ARREEGLAR ESTO! Lograr que redireccione bien!
                return View(usuario);

            }
            else
            {
                ViewBag.ErrorRegistro = "Las contrasennas no coinciden";
                return View(usuario);
            }





          
      
        
 
       
        }
	}
}