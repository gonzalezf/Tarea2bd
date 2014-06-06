using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

namespace foro2.Controllers
{
    public class ComentarController : Controller
    {


        //REVISADO!
        public ActionResult Index(string id)
        {
            if(!tablasforo.IsLoggedIn(Session))
            {
                return Redirect("/Registro/Index");
            }

            var coment = new ModeloComentario();
            ViewBag.temaenseleccion= id;
            return View(coment);
        }


        [HttpPost]
        public ActionResult Index(ModeloComentario coment)
        {


            //obtener id_tema seleccionada!!
            SqlConnection sqlCnn0;
            SqlCommand sqlCmd0;
            string sql0 = null;
            string connectionString = null;
            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            ViewBag.TemaModelo = coment.id_tema_string;
            ViewBag.Comentario = coment.comentario;

     //       sql0 = "use XE select id_tema from tema where nombre ='" + comentario.id_tema_string+ "'";
            sql0 = "select id_tema from tema where nombre ='"+coment.id_tema_string+"'";
            sqlCnn0 = new SqlConnection(connectionString);
            sqlCnn0.Open();
            sqlCmd0 = new SqlCommand(sql0, sqlCnn0);
            var id_tema = sqlCmd0.ExecuteScalar();
            string string_id_tema = "1"; // por defecto

            if (id_tema != null) //esto siempre debiese cumplirse a menos de un error extraño
            {
                string_id_tema = id_tema.ToString();
            } //ya obtuvimos la id categoria... ahora obtener lo demas


            
            tablasforo ManipularDatos = new tablasforo();
            ManipularDatos.Conectar();
                 

            
            //ESTOS DATOS DEBEN SER REEMPLAZADOS!!!

            string id_usuario = Session["UserId"].ToString();
           
            //FIN DE DATOS QUE DEBEN SER REEMPLAZADOS


            ManipularDatos.EjecutarSql("INSERT INTO comentario VALUES('"+string_id_tema +"'," + id_usuario + ",'" + coment.comentario + "')");

            ManipularDatos.Desconectar();
            sqlCmd0.Dispose();
            sqlCnn0.Close();
            return Redirect("/");
        }

	}
}