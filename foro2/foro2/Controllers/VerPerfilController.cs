using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;





namespace foro2.Controllers
{
    public class VerPerfilController : Controller
    {
        //REVISADO
        // GET: /VerPerfil/
        public ActionResult Index(string id) //recibimos el id del usuario!
        {
            foro2.tablasforo.ColumnaInfoUsuario2[] registros = null; // solo categorias publicas
            foro2.tablasforo.ColumnaUltimos5Temas[] registros2 = null;
            foro2.tablasforo.ColumnaUltimos5Comentarios[] registros3 = null;
            tablasforo infousuario = new tablasforo();

            registros = infousuario.RetornarInfoUsuario2(id); //tenemos listado de comentarios
            registros2 = infousuario.RetornarUltimos5TemasCreados(id);
            registros3 = infousuario.RetornarUltimos5Comentarios(id);
            ViewBag.InfoUsuario = registros; // Este viewbag toma un arreglo el cual es imprimido 

            ViewBag.Ultimos5TemasCreados = registros2;
            ViewBag.Ultimos5Comentarios = registros3;
            return View();
        }
	}
}