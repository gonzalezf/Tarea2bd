using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Data.SqlClient;

namespace foro2.Controllers
{
    public class CategoriaController : Controller
    {
  
        public ActionResult Index(string id) // Recuperar el id!y mostrar todos los temas!
        {
            ViewBag.TemaSeleccionado = id;
           
            string nombrecategoria = id; //nombre categoria recibe el nombre  de la categoria deseada, para buscar los temas mas adelante
            ModeloTema[] registros = null;
            tablasforo listadetemas = new tablasforo();
            SqlDataReader sqlreader = listadetemas.RetornarTemas(nombrecategoria); //tenemos listado de temas
            var list = new List<ModeloTema>();
            
            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
  
//                list.Add(new ModeloTema { nombre = sqlreader.GetString(0), id_usuario_string = sqlreader.GetString(1), descripcion = sqlreader.GetString(2) }); //revisar si funciona el Get String
                list.Add(new ModeloTema { nombre = sqlreader.GetString(0), id_usuario = sqlreader.GetInt32(1), descripcion = sqlreader.GetString(2) }); //revisar si funciona el Get String


            }
            registros = list.ToArray();
            ViewBag.ListaTemas = registros; // Este viewbag toma un arreglo el cual es imprimido 

            
            return View();
        }
	}
}