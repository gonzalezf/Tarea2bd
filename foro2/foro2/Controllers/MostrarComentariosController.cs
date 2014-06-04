using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using foro2.Models;




namespace foro2.Controllers
{
    public class MostrarComentariosController : Controller
    {
        //REVISADO
        // GET: /MostrarComentarios/
        public ActionResult Index(string id)
        {
            ViewBag.TemadelComentario = id;
            foro2.tablasforo.ColumnaComentarios2[] registros = null; // solo categorias publicas
   
            tablasforo listadecomentarios = new tablasforo();

            registros = listadecomentarios.RetornarComentarios(id); //tenemos listado de comentarios
 


                


            ViewBag.ListaComentarios = registros; // Este viewbag toma un arreglo el cual es imprimido 
            return View();
        }
	}
}