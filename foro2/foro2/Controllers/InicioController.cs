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
                int numero = sqlreader.GetInt32(2);
                int valor = -1;
                int valor2 = listadecategorias.RetornarCantidadMensajes(numero);

                SqlDataReader sqlreader3 = listadecategorias.RetornarCantidadTemas(numero);
                SqlDataReader sqlreader4 = listadecategorias.RetornarUltimoMensaje(sqlreader.GetInt32(2));

                string ultimo_mensaje_obtenido = null;
                string ultimo_tema_obtenido = null;
                int id_ultimo_usuario_obtenido = -1;
                while (sqlreader4.Read())
                {
                    id_ultimo_usuario_obtenido = sqlreader4.GetInt32(3);
                    ultimo_tema_obtenido = sqlreader4.GetString(4);
                    ultimo_mensaje_obtenido = sqlreader4.GetString(6);
                }
                while (sqlreader3.Read())
                {
                    valor = sqlreader3.GetInt32(0);
                }

                list.Add(new ColumnaCategorias { nombre = sqlreader.GetString(0), descripcion = sqlreader.GetString(1), id_categoria = sqlreader.GetInt32(2), cantidad_temas = valor, cantidad_mensajes = valor2, id_ultimo_usuario = id_ultimo_usuario_obtenido, ultimo_tema = ultimo_tema_obtenido, ultimo_mensaje = ultimo_mensaje_obtenido}); //revisar si funciona el Get String

            }
            //hacemos lo mismo que en el caso anterior , pero ahora para TODAS las categorias (publicas y privadas)
            while (sqlreader2.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                int numero = sqlreader2.GetInt32(2);
                SqlDataReader sqlreader3 = listadecategorias.RetornarCantidadTemas(numero);
                int valor2 = listadecategorias.RetornarCantidadMensajes(numero);
                int valor = -1;
                SqlDataReader sqlreader4 = listadecategorias.RetornarUltimoMensaje(sqlreader2.GetInt32(2));
                string ultimo_mensaje_obtenido = null;
                string ultimo_tema_obtenido = null;
                int id_ultimo_usuario_obtenido = -1;
                while (sqlreader4.Read())
                {
                    id_ultimo_usuario_obtenido = sqlreader4.GetInt32(3);
                    ultimo_tema_obtenido = sqlreader4.GetString(4);
                    ultimo_mensaje_obtenido = sqlreader4.GetString(6);
                }
         

                while (sqlreader3.Read())
                {
                    valor = sqlreader3.GetInt32(0);
                }

                list2.Add(new ColumnaCategorias { nombre = sqlreader2.GetString(0), descripcion = sqlreader2.GetString(1), id_categoria = sqlreader2.GetInt32(2), cantidad_temas = valor,cantidad_mensajes = valor2,ultimo_tema = ultimo_tema_obtenido, ultimo_mensaje = ultimo_mensaje_obtenido, id_ultimo_usuario = id_ultimo_usuario_obtenido }); //revisar si funciona el Get String

            }

            registros = list.ToArray();
            registrosdetodaslascategorias = list2.ToArray();

            ViewBag.ListaCategoriasPublicas = registros; // Este viewbag toma un arreglo el cual es imprimido 
            ViewBag.ListaCategoriasPublicasYPrivadas = registrosdetodaslascategorias; //se ocupara al iniciar sesion! //INVOCAR SI SE INICIA SESION!
            //
           
            sqlreader.Close();
            sqlreader2.Close();
            return View();
            


        }
	}
}