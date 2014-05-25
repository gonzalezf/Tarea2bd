using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;

namespace foro2.Controllers
{
    public class IniciarSesionController : Controller
    {
        //
        // GET: /IniciarSesion/
        public ActionResult Index()
        {
            if (String.Compare((String)Session["LoggedIn"], "Yes") == 0)
            {
                return Redirect("/");
            }
            Usuario usuario = new Usuario();
            return View(usuario);


            
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            //hacer login...!
            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();

            int valor = ManipularDatos.LogIn(usuario.nombre, usuario.contrasenna);
            //si valor recibe un valor mayor a 1, significa que existe un usuario con ese nombre y esa contrasenna. ejecutarsql retorna el numero de filas afectadas por la query.
            
            
            
            if (valor == -1)
            {
                ViewBag.ErrorIniciarSesion = "Nickname o contrasenna incorrecta";
                ManipularDatos.Desconectar();
                Session["LoggedIn"] = "No";
                return View(usuario);
            }
            else
            {
                ViewBag.ErrorIniciarSesion = "Ha iniciado sesion";
                ManipularDatos.Desconectar();
                Session["LoggedIn"] = "Yes";
                Session["UserId"] = valor.ToString();
                Session["UserName"] = usuario.nombre;
                return View(usuario); //USUARIO LOGUEADO, REDIRECCIONAR DONDE CORRESPONDA.
            }

        }
	}
}