using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using foro2.Models;

public class ColumnaCategorias /* Clase hecha para la funcion retornar categorias. ATENCION! CREO QUE ESTA CLASE NO LA LEE, LA LEE DESDE EL MODELO */
{
    public string nombre;
    public string descripcion;

}

namespace foro2.Controllers
{
    public class InicioController : Controller
    {
        //
        // GET: /Inicio/
        public ActionResult Index()
        {

            ColumnaCategorias[] registros = null; // solo categorias publicas
            ColumnaCategorias[] registrosdetodaslascategorias = null; //categorias publicas y privadas
            tablasforo listadecategorias = new tablasforo();

            SqlDataReader sqlreader =  listadecategorias.RetornarCategoriasPublicas(); //tenemos listado de categorias..!
            SqlDataReader sqlreader2 = listadecategorias.RetornarTodasLasCategorias(); //tenemos listado de categorias..!

            var list = new List<ColumnaCategorias>();
            var list2 = new List<ColumnaCategorias>();

            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                ViewBag.Categoria =  sqlreader.GetValue(0);
                list.Add(new ColumnaCategorias { nombre = sqlreader.GetString(0), descripcion = sqlreader.GetString(1) }); //revisar si funciona el Get String
   

            }
            //hacemos lo mismo que en el caso anterior , pero ahora para TODAS las categorias (publicas y privadas)
            while (sqlreader2.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                list2.Add(new ColumnaCategorias { nombre = sqlreader2.GetString(0), descripcion = sqlreader2.GetString(1) }); //revisar si funciona el Get String
                
            }

            registros = list.ToArray();
            registrosdetodaslascategorias = list2.ToArray();

            ViewBag.ListaCategoriasPublicas = registros; // Este viewbag toma un arreglo el cual es imprimido 
            ViewBag.ListaCategoriasPublicasYPrivadas = registrosdetodaslascategorias; //se ocupara al iniciar sesion! //INVOCAR SI SE INICIA SESION!
            return View();
            


        }
	}
}