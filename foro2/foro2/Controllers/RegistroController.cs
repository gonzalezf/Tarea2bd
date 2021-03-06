﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Globalization;
using System.Threading;
using System.Web.Routing;

namespace foro2.Controllers
{
    public class RegistroController : Controller
    {
        //REVISADO
        // GET: /Registro/
        public ActionResult Index() //se utiliza al principio cuando aun no se ha enviado alguna informacion
        {
            if (!tablasforo.IsLoggedIn(Session))
            {
                return Redirect("/");
            }
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
                if(ManipularDatos.ObtenerIdUsuario(usuario.nombre) != -1)
                {
                    ViewBag.ErrorRegistro = "El usuario ya existe";
                    return View(usuario);
                }
                ManipularDatos.EjecutarSql("INSERT INTO usuario VALUES(" + 1 + ",'" + usuario.nombre + "','" + usuario.contrasenna + "','" + cantidadcomentarios + "','" + usuario.avatarurl + "','" + usuario.fechadenacimiento + "','" + usuario.sexo + "','" + fecha_registro + "')");
                int id_usuario = ManipularDatos.RetornarIdUsuario(usuario.nombre);
                ManipularDatos.EjecutarSql("INSERT INTO buzon_entrada VALUES(" + id_usuario+ ","+0+","+0+ ")");

                ManipularDatos.Desconectar();
                //return RedirectToAction("/Inicio/Index");
                //return RedirectToRoute("/Inicio/Index");  <!ARREEGLAR ESTO! Lograr que redireccione bien!
                ManipularDatos.Desconectar();
              //  return View(usuario);

                ViewBag.registro_exitoso = "Se registro exitosamente!";
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Inicio", action = "Index", id=id_usuario }));

            }
            else
            {
                ViewBag.ErrorRegistro = "Las contrasennas no coinciden";
                ManipularDatos.Desconectar();
                return View(usuario);
            }
        }
	}
}