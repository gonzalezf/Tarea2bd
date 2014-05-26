using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Data.SqlClient;
using System.Web.Routing;
namespace foro2.Controllers
{
    public class EditarComentarioController : Controller
    {
        public ActionResult Index(string id, string id2) // recibo el id_comentario
        {


            ViewBag.TemaAModificar = id2;

            var coment = new ModeloComentario();

            //obtener el mensaje del comentario...
            SqlConnection sqlCnn0;
            SqlCommand sqlCmd0;
            string sql0 = null;
            string connectionString = null;
            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";


            sql0 = "use XE select mensaje from comentario where id_comentario ='"+id+"'";
            sqlCnn0 = new SqlConnection(connectionString);
            sqlCnn0.Open();
            sqlCmd0 = new SqlCommand(sql0, sqlCnn0);
            var mensajeobtenido = sqlCmd0.ExecuteScalar();
            string mensajedecomentario = "NO EXISTE UN MENSAJE ANTERIOR"; // por defecto

            if (mensajeobtenido != null) //esto siempre debiese cumplirse a menos de un error extraño
            {
                mensajedecomentario = mensajeobtenido.ToString();
            } //ya obtuvimos la id categoria... ahora obtener lo demas

            @ViewBag.mensajeanterior = mensajedecomentario; 





            ViewBag.id_comentario_seleccionado = id;
            return View(coment);
        }


        [HttpPost]
        public ActionResult Index(ModeloComentario coment)
        {

                 

            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();




            ManipularDatos.EjecutarSql("UPDATE comentario SET [mensaje] = '"+coment.comentarioeditado+"' WHERE [id_comentario] =  '"+coment.id_comentario_string+"'");

            ManipularDatos.Desconectar();
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "MostrarComentarios", action = "Index", id = coment.id_tema_string }));

           // return View(coment);

        }
	}
}