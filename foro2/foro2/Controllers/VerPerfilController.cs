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

            tablasforo infousuario = new tablasforo();

            registros = infousuario.RetornarInfoUsuario2(id); //tenemos listado de comentarios

            
            ViewBag.InfoUsuario = registros; // Este viewbag toma un arreglo el cual es imprimido 
            return View();
        }
	}
}