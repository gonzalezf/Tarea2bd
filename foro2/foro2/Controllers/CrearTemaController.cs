using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Data.SqlClient;

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
            ViewBag.id_categoria_string = tema.id_categoria_string;

            //obtener id_categoria seleccionada!!
            SqlConnection sqlCnn0;
            SqlCommand sqlCmd0;
            string sql0 = null; 
            string connectionString = null;
            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            sql0 = "select id_categoria from categoria where nombre ='" + tema.id_categoria_string+ "'";
            sqlCnn0 = new SqlConnection(connectionString);
            sqlCnn0.Open();
            sqlCmd0 = new SqlCommand(sql0, sqlCnn0);
            var id_categoria = sqlCmd0.ExecuteScalar();
            string string_id_categoria = "2";

            if (id_categoria != null) //esto siempre debiese cumplirse a menos de un error extraño
            {
                string_id_categoria = id_categoria.ToString();
            } //ya obtuvimos la id categoria... ahora obtener lo demas

            
            //guardar todo lo del modelo en la base de datos.

            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();
          
            
            
            //ESTOS DATOS DEBEN SER REEMPLAZADOS!!!
            int id_usuario = 1; //gonzalez_f
        //    int id_categoria = 1; //categoria pokemon
            ViewBag.ErrorCrearTema = "Booleano publico ---> " + tema.publico;
            ViewBag.CategoriaMensaje = "Mensaje del tema " + tema.mensaje;
          
            /*
            if (tema.publico == True) //Hay que ver como obtener si fue chequeado el radio button de publico.
                {
                    String booleanopublico = "True";
                }
                else {
                    String booleanopublico = "False";
                }*/
            
            string booleanopublico = "True";
            //FIN DE DATOS QUE DEBEN SER REEMPLAZADOS!
//            ManipularDatos.EjecutarSql("INSERT INTO tema VALUES('" + string_id_categoria + "'," + id_usuario + ",'" + tema.nombre + "','" + tema.descripcion + "','" + tema.mensaje + "','" + booleanopublico + "')");

            ManipularDatos.EjecutarSql("INSERT INTO tema VALUES('"+string_id_categoria+ "'," + id_usuario + ",'" + tema.nombre + "','" + tema.descripcion + "','" + tema.mensaje + "','" + booleanopublico + "')");
            ManipularDatos.Desconectar();

            return View( tema);
        }

        /*

        [HttpPost] //la funcion que continua, creo q no sirve.
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
  /*          string booleanopublico = "True";
            //FIN DE DATOS QUE DEBEN SER REEMPLAZADOS!
            ManipularDatos.EjecutarSql("INSERT INTO tema VALUES("+id_categoria+","+id_usuario+",'"+tema.nombre+"','"+tema.descripcion+"','"+tema.mensaje+"','"+booleanopublico+"')");
            ManipularDatos.Desconectar();

            return View(categoriaseleccionada, tema);
        }

        */

	}
}