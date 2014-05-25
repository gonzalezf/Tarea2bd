using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using foro2.Models;


public class ColumnaComentarios
{
    public int id_comentario;
    public int id_usuario; //ojo! parece q debe ser int!
    public string mensaje;
}

namespace foro2.Controllers
{
    public class MostrarComentariosController : Controller
    {
        //
        // GET: /MostrarComentarios/
        public ActionResult Index(string id)
        {
            ViewBag.TemadelComentario = id;
            ColumnaComentarios[] registros = null; // solo categorias publicas
   
            tablasforo listadecomentarios = new tablasforo();

            SqlDataReader sqlreader = listadecomentarios.RetornarComentarios(id); //tenemos listado de comentarios
 

            var list = new List<ColumnaComentarios>();
           
            //No estoy seguro si va a funcionar que tome como string el id_usuario en vez de int.

            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                ViewBag.Categoria = sqlreader.GetValue(0);
                list.Add(new ColumnaComentarios { id_usuario = sqlreader.GetInt32(0), mensaje = sqlreader.GetString(1), id_comentario = sqlreader.GetInt32(2) }); //revisar si funciona el Get String


            }
                
            registros = list.ToArray();    
            ViewBag.ListaComentarios = registros; // Este viewbag toma un arreglo el cual es imprimido 
            return View();
        }
	}
}