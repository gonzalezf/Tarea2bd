using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using foro2.Models;

public class ColumnaCategorias2 /* Clase hecha para la funcion retornar categorias. ATENCION! CREO QUE ESTA CLASE NO LA LEE, LA LEE DESDE EL MODELO */
{
    public string nombre;
    public string descripcion;
    public int id_categoria;
    public int cantidad_temas;
    public int cantidad_mensajes;
    public int id_ultimo_usuario;
    public string ultimo_mensaje;
    public string ultimo_tema;
}

namespace foro2.Controllers
{
    public class InicioController : Controller
    {
        //
        // GET: /Inicio/
        public ActionResult Index(string id)
        {

            
            tablasforo listadecategorias = new tablasforo();
            foro2.tablasforo.ColumnaCategorias2[] registros = null; // solo categorias publicas



            if (String.Compare((String)Session["LoggedIn"], "Yes") == 0)
            {
                registros = listadecategorias.RetornarTodasLasCategorias(); //tenemos listado de categorias..!
                ViewBag.ListaCategorias = registros; //se ocupara al iniciar sesion! //INVOCAR SI SE INICIA SESION!
                if (String.Compare((String)id, "1") == 0)
                {
                    ViewBag.registro_exitoso = "exitoso"; //se ocupara al iniciar sesion! //INVOCAR SI SE INICIA SESION!

                }

                return View();
            }

            else
            {
                registros = listadecategorias.RetornarCategoriasPublicas(); //tenemos listado de categorias..!

                ViewBag.ListaCategorias = registros; // Este viewbag toma un arreglo el cual es imprimido 

                if (String.Compare((String)id, "1") == 0)
                {
                    ViewBag.registro_exitoso = "exitoso"; //se ocupara al iniciar sesion! //INVOCAR SI SE INICIA SESION!

                }
                return View();
            

            }


        }



        
	}
}