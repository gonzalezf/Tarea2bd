using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foro2.Models;
using System.Web.Routing;
namespace foro2.Controllers
{
    public class EditarPerfilController : Controller
    {
        public ActionResult Index(string id,string id2,string id3, string id4)
        {
            if (int.Parse((String)Session["GroupId"]) != 1 && int.Parse( (String)Session["UserId"]) != int.Parse(id))
            {
                return Redirect("/");
            }
            var editarusuario = new ModeloEditarUsuario();

            ViewBag.id_usuario_editar = id;
            ViewBag.avatar_url_editar = id2;
            ViewBag.fecha_nacimiento_editar = id3;
            ViewBag.group_id = id4;
            return View(editarusuario);
        }

        [HttpPost]
        public ActionResult Index(ModeloEditarUsuario usuario)
        {


            tablasforo ManipularDatos = new tablasforo();

            if (String.Compare(usuario.editarcontrasenna, usuario.editarrepetircontrasenna) == 0)
            {
                ManipularDatos.Conectar();
                if (int.Parse((String)Session["GroupId"]) == 1 && int.Parse(usuario.id_grupo) > 0)
                {
                    ManipularDatos.EjecutarSql("UPDATE usuario SET [contrasenna] = '" + usuario.editarcontrasenna + "', [avatar_url] = '" + usuario.editaravatarurl + "',  [fecha_nacimiento] = '" + usuario.editarfechanacimiento + "', [id_grupo] = '"+usuario.id_grupo+"'WHERE [id_usuario] =  '" + usuario.id_usuario_string + "'");
                }
                else
                {
                    ManipularDatos.EjecutarSql("UPDATE usuario SET [contrasenna] = '" + usuario.editarcontrasenna + "', [avatar_url] = '" + usuario.editaravatarurl + "',  [fecha_nacimiento] = '" + usuario.editarfechanacimiento + "' WHERE [id_usuario] =  '" + usuario.id_usuario_string + "'");
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