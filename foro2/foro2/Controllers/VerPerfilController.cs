using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;



public class ColumnaInfoUsuario
{
    public int id_grupo;
    public string  nombre; //ojo! parece q debe ser int!
    public int cantidad_comentarios;
    public string avatar_url;
    public string fecha_nacimiento;
    public string sexo;
    public string fecha_registro;
    public string id_usuario;
}

namespace foro2.Controllers
{
    public class VerPerfilController : Controller
    {
        //
        // GET: /VerPerfil/
        public ActionResult Index(string id) //recibimos el id del usuario!
        {
            ColumnaInfoUsuario[] registros = null; // solo categorias publicas

            tablasforo infousuario = new tablasforo();

            SqlDataReader sqlreader = infousuario.RetornarInfoUsuario(id); //tenemos listado de comentarios


            var list = new List<ColumnaInfoUsuario>();

            //No estoy seguro si va a funcionar que tome como string el id_usuario en vez de int.

            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                ViewBag.Categoria = sqlreader.GetValue(0);
                list.Add(new ColumnaInfoUsuario { id_grupo = sqlreader.GetInt32(0), nombre = sqlreader.GetString(1), cantidad_comentarios = sqlreader.GetInt32(2), avatar_url = sqlreader.GetString(3), fecha_nacimiento = sqlreader.GetString(4), sexo = sqlreader.GetString(5), fecha_registro = sqlreader.GetString(6), id_usuario = id}); //revisar si funciona el Get String


            }

            registros = list.ToArray();
            ViewBag.InfoUsuario = registros; // Este viewbag toma un arreglo el cual es imprimido 
            return View();
        }
	}
}