using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Web.Routing;
using System.Data.SqlClient;
namespace foro2.Controllers
{
    public class EditarPerfilController : Controller
    {

        //REVISADO
        public ActionResult Index(string id, string id2)
        {

            //obtener id_grupo del usuario!
            SqlConnection sqlCnn0;
            SqlCommand sqlCmd0;
            string sql0 = null;
            string connectionString = null;
            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            sql0 = "select id_grupo from usuario where id_usuario ='" +id + "'";
            sqlCnn0 = new SqlConnection(connectionString);
            sqlCnn0.Open();
            sqlCmd0 = new SqlCommand(sql0, sqlCnn0);
            var id_grupo= sqlCmd0.ExecuteScalar();
            string string_id_grupo = "0"; //por defecto, siempre deberia cambiar

            tablasforo modeloforo = new tablasforo();

            if (id_grupo != null) //esto siempre debiese cumplirse a menos de un error extraño
            {
                string_id_grupo = id_grupo.ToString();
            } //ya obtuvimos el id_grupo 

            ViewBag.id_grupo_encontrado = string_id_grupo; //LISTO ! AHORA PUEDES TRABAJAR CON EL VIEW BAG

            if (int.Parse(string_id_grupo) != 1 && int.Parse((String)Session["UserId"]) != int.Parse(id))
            {
                return Redirect("/");
            }
            var editarusuario = new ModeloEditarUsuario();

            ViewBag.id_usuario_editar = id;
            ViewBag.avatar_url_editar = modeloforo.RetornarAvatarUrlUsuario(Int32.Parse(id));
            ViewBag.fecha_nacimiento_editar = id2;
            sqlCmd0.Dispose();
            sqlCnn0.Close();
            return View(editarusuario);
        }
        [HttpPost]
        public ActionResult Index(ModeloEditarUsuario usuario)
        {
          

            tablasforo ManipularDatos = new tablasforo();

            if (String.Compare(usuario.editarcontrasenna, usuario.editarrepetircontrasenna) == 0)
            {
                ManipularDatos.Conectar();


                if (int.Parse(usuario.id_usuario_string) > 0) // el usuario.id_grupo_usuario ES EL Q VA A RECIBIR DESPUES DE HABERLO CAMBIADO , POR LO TANTO SI QUIERO QUE UN ADMIN DEJE DE SERLO, NO VA A ENTRAR EN ESTE IF Y NUNCA SE VA A PODER REALIZAR, 
                    //OJO! PORQUE ID_GRUPO_USUARIO NO ES NECESARIAMENTE MI GRUPO SINO EL GRUPO QUE QUIERO ASIGNAR A LA PERSONA Q LE ESTOY EDITANDO EL PERFIL
                {
                    ManipularDatos.EjecutarSql("UPDATE usuario SET [contrasenna] = '" + usuario.editarcontrasenna + "', [avatar_url] = '" + usuario.editaravatarurl + "',  [fecha_nacimiento] = '" + usuario.editarfechanacimiento + "', [id_grupo] = '" + usuario.id_grupo_string + "'WHERE [id_usuario] =  '" + usuario.id_usuario_string + "'");
                }
                /*
                if (int.Parse((String)Session["GroupId"]) == 1 && int.Parse(usuario.id_grupo) > 0)
                {
                    ManipularDatos.EjecutarSql("UPDATE usuario SET [contrasenna] = '" + usuario.editarcontrasenna + "', [avatar_url] = '" + usuario.editaravatarurl + "',  [fecha_nacimiento] = '" + usuario.editarfechanacimiento + "', [id_grupo] = '"+usuario.id_grupo_string+"'WHERE [id_usuario] =  '" + usuario.id_usuario_string + "'");
                }
                 * */
                else
                {
                    ManipularDatos.EjecutarSql("UPDATE usuario SET [contrasenna] = '" + usuario.editarcontrasenna + "', [avatar_url] = '" + usuario.editaravatarurl + "',  [fecha_nacimiento] = '" + usuario.editarfechanacimiento + "' WHERE [id_usuario] =  '" + usuario.id_usuario_string + "'");
                   // ManipularDatos.EjecutarSql("UPDATE usuario SET [contrasenna] = '" + usuario.editarcontrasenna + "', [avatar_url] = ' grupo = " + usuario.id_grupo_string + "',  [fecha_nacimiento] = 'id = " + usuario.id_usuario_string + "' WHERE [id_usuario] =  '" + usuario.id_usuario_string + "'");

                }
                ManipularDatos.Desconectar();
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "VerPerfil", action = "Index", id = usuario.id_usuario_string }));

            }
            else
            {
                ViewBag.EditarUsuarioError = "Las contrasennas no coinciden";
                return View(usuario);
            }
         
            



        }


	}
}