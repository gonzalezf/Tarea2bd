using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;

namespace foro2.Controllers
{
    public class CrearTemaController : Controller
    {
        //
        // GET: /CrearTema/
        public ActionResult Index(string id)
        {
            ModeloTema tema = new ModeloTema();
            ViewBag.seleccion = id;
            return View(tema);
        }
        [HttpPost]
        public ActionResult Index(ModeloTema tema)
        {


            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();
            //ESTOS DATOS DEBEN SER REEMPLAZADOS!!!
            int id_usuario = 1; //gonzalez_f
            int id_categoria = 1; //categoria pokemon
      
            ViewBag.ErrorCrearTema = "HOLA " + tema.publico;
            ViewBag.CategoriaMensaje = tema.mensaje;
            /*
            if (tema.publico == True)
                {
                    String booleanopublico = "True";
                }
                else {
                    String booleanopublico = "False";
                }*/
            string booleanopublico = "True";
            //FIN DE DATOS QUE DEBEN SER REEMPLAZADOS!
            ManipularDatos.EjecutarSql("INSERT INTO tema VALUES(" + id_categoria + "," + id_usuario + ",'" + tema.nombre + "','" + tema.descripcion + "','" + tema.mensaje + "','" + booleanopublico + "')");
            ManipularDatos.Desconectar();

            return View( tema);
        }
        [HttpPost]
        public ActionResult Index(string categoriaseleccionada,ModeloTema tema)
        {


            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();
            //ESTOS DATOS DEBEN SER REEMPLAZADOS!!!
            int id_usuario = 1; //gonzalez_f
            int id_categoria = 1; //categoria pokemon
            ViewBag.seleccion = categoriaseleccionada;
            ViewBag.ErrorCrearTema = "HOLA "+ tema.publico;
            ViewBag.CategoriaMensaje = tema.mensaje;
            /*
            if (tema.publico == True)
                {
                    String booleanopublico = "True";
                }
                else {
                    String booleanopublico = "False";
                }*/
            string booleanopublico = "True";
            //FIN DE DATOS QUE DEBEN SER REEMPLAZADOS!
            ManipularDatos.EjecutarSql("INSERT INTO tema VALUES("+id_categoria+","+id_usuario+",'"+tema.nombre+"','"+tema.descripcion+"','"+tema.mensaje+"','"+booleanopublico+"')");
            ManipularDatos.Desconectar();

            return View(categoriaseleccionada, tema);
        }



	}
}