﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;

namespace foro2.Controllers
{
    public class IniciarSesionController : Controller
    {
        //REVISADO
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

            List<String> l = ManipularDatos.LogIn(usuario.nombre, usuario.contrasenna);
            //si valor recibe un valor mayor a 1, significa que existe un usuario con ese nombre y esa contrasenna. ejecutarsql retorna el numero de filas afectadas por la query.

            if (l.Count <= 0)
            {
                ViewBag.ErrorIniciarSesion = "Nickname o contraseña incorrecta";
                ManipularDatos.Desconectar();
                Session["LoggedIn"] = "No";
                ManipularDatos.Desconectar(); //AGREGUE ESTA LINEA
                usuario.nombre = null;
                usuario.contrasenna = null;
                return View(usuario);
            }
            else
            {
                int valor = int.Parse(l[0]);
                if (valor == -1)
                {
                    ViewBag.ErrorIniciarSesion = "Nickname o contraseña incorrecta";
                    ManipularDatos.Desconectar();
                    Session["LoggedIn"] = "No";
                    ManipularDatos.Desconectar(); //AGREGUE ESTA LINEA
                    usuario.nombre = null;
                    usuario.contrasenna = null;
                    return View(usuario);
                }
                else
                {
                    ViewBag.ErrorIniciarSesion = "Ha iniciado sesion";
                    ManipularDatos.Desconectar();
                    Session["LoggedIn"] = "Yes";
                    Session["UserId"] = valor.ToString();
                    Session["UserName"] = usuario.nombre;
                    Session["ImageURL"] = l[1];
                    Session["GroupId"] = l[2];
                    ManipularDatos.Desconectar(); //Agregue esta linea
                    return Redirect("/Inicio/Index");
                }
            }
        }
	}
}