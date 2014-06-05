using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using foro2.Models;

namespace foro2
{
    public class tablasforo
    {
        SqlConnection MiConexion;

        public void Conectar()
        {
            MiConexion = new SqlConnection("Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"); /* pide cada de conexion */
            MiConexion.Open();

        }

        public void Desconectar()
        {
            MiConexion.Close();
        }

        public class ColumnaCategorias /* Clase hecha para la funcion retornar categorias */
        {
            public string nombre;
            public string descripcion;

        }


        public class UltimoMensaje /* Clase hecha para la funcion retornar categorias. ATENCION! CREO QUE ESTA CLASE NO LA LEE, LA LEE DESDE EL MODELO */
        {

            public int id_ultimo_usuario;
            public string ultimo_mensaje;
            public string ultimo_tema;
        }


        public class ColumnaComentarios
        {
            public string id_usuario; //ojo! parece q debe ser int!
            public string mensaje;
        }
        /*
        public void RetornarCategorias()
        {
            ColumnaCategorias[] registros = null;
            string sql = @"USE XE SELECT nombre, descripcion FROM categoria";
            using (var command = new SqlCommand(sql,con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    var list = new List<ColumnaCategorias>();
                    while (reader.Read())
                        list.Add(new ColumnaCategorias {nombre = reader.GetString(0),descripcion = reader.GetString(1) })
                }
            }


        }*/
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
            public string nombre_usuario;
        }


        public ColumnaCategorias2[] RetornarCategoriasPublicas()
        {
            ColumnaCategorias2[] registros = null; // solo categorias publicas
            var list = new List<ColumnaCategorias2>();
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select nombre, descripcion, id_categoria from categoria where publico = 'True'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();


            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {

                int numero = sqlreader.GetInt32(2);

                int valor2 = RetornarCantidadMensajes(numero);

                int valor_cantidad_temas = RetornarCantidadTemas(numero);
                List<UltimoMensaje> registros2 = RetornarUltimoMensaje(sqlreader.GetInt32(2));

                int id_ultimo_usuario_obtenido = registros2[0].id_ultimo_usuario;
                string ultimo_tema_obtenido = registros2[0].ultimo_tema;
                string ultimo_mensaje_obtenido = registros2[0].ultimo_mensaje;



                list.Add(new ColumnaCategorias2 { nombre = sqlreader.GetString(0), descripcion = sqlreader.GetString(1), id_categoria = sqlreader.GetInt32(2), cantidad_temas = valor_cantidad_temas, cantidad_mensajes = valor2, id_ultimo_usuario = id_ultimo_usuario_obtenido, ultimo_tema = ultimo_tema_obtenido, ultimo_mensaje = ultimo_mensaje_obtenido, nombre_usuario = RetornarNombreUsuario2(id_ultimo_usuario_obtenido) }); //revisar si funciona el Get String


            }

            registros = list.ToArray();


            sqlreader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();

            return registros;


        }

        public List<String> GetMessageInfo(String message_id)
        {
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "SELECT u.nombre as nombre_remitente, m.* FROM mensaje_privado m, (SELECT us.id_usuario, us.nombre FROM usuario us) u WHERE m.id_mensaje = '" + message_id + "' AND u.id_usuario = m.id_remitente";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            var list = new List<String>();
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            if (sqlreader.Read())
            {
                list.Add(sqlreader.GetString(0));
                list.Add(sqlreader.GetInt32(1).ToString());
                list.Add(sqlreader.GetInt32(2).ToString());
                list.Add(sqlreader.GetInt32(3).ToString());
                list.Add(sqlreader.GetBoolean(4).ToString());
                list.Add(sqlreader.GetString(5));
                list.Add(sqlreader.GetString(6));
            }

            //agregue estas lineas de cierre
            sqlCmd.Dispose();
            sqlCnn.Close();
            sqlreader.Close();
            return list;
        }

        public List<String> RetornarInboxInfo(String userid)
        {
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "SELECT id_buzon, mensajes, mensajes_sin_leer FROM buzon_entrada WHERE id_usuario ='" + userid + "'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            List<String> list = new List<String>();
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            if (sqlreader.Read())
            {
                list.Add(sqlreader.GetInt32(0).ToString());
                list.Add(sqlreader.GetInt32(1).ToString());
                list.Add(sqlreader.GetInt32(2).ToString());
            }
            sqlreader.Close();
            sqlCnn.Close();
            /*Agregue esta linea */
            sqlCmd.Dispose();

            return list;
        }

        public List<ModeloPM> RetornarMensajesPrivados(String inboxid)
        {
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "SELECT u.nombre as nombre_remitente, m.* FROM mensaje_privado m, (SELECT us.id_usuario, us.nombre FROM usuario us) u WHERE m.id_buzon = '"+inboxid+"' AND u.id_usuario = m.id_remitente ORDER BY id_mensaje DESC";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            var list = new List<ModeloPM>();
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            while (sqlreader.Read())
            {
                list.Add(new ModeloPM(sqlreader.GetString(0), sqlreader.GetInt32(1), sqlreader.GetInt32(2), sqlreader.GetInt32(3), sqlreader.GetBoolean(4), sqlreader.GetString(5), sqlreader.GetString(6), sqlreader.GetString(7)));
            }
            //agregue estas lineas
            sqlCmd.Dispose();
            sqlCnn.Close();
            sqlreader.Close();
            return list;
        }

        public ColumnaCategorias2[] RetornarTodasLasCategorias()
        {
            ColumnaCategorias2[] registros = null;
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;
            var list = new List<ColumnaCategorias2>();
            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select nombre, descripcion, id_categoria from categoria";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();

            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {

                int numero = sqlreader.GetInt32(2);

                int valor2 = RetornarCantidadMensajes(numero);

                int valor_cantidad_temas = RetornarCantidadTemas(numero);
                List<UltimoMensaje> registros2 = RetornarUltimoMensaje(sqlreader.GetInt32(2));

                int id_ultimo_usuario_obtenido = registros2[0].id_ultimo_usuario;
                string ultimo_tema_obtenido = registros2[0].ultimo_tema;
                string ultimo_mensaje_obtenido = registros2[0].ultimo_mensaje;



                list.Add(new ColumnaCategorias2 { nombre_usuario = RetornarNombreUsuario2(id_ultimo_usuario_obtenido), nombre = sqlreader.GetString(0), descripcion = sqlreader.GetString(1), id_categoria = sqlreader.GetInt32(2), cantidad_temas = valor_cantidad_temas, cantidad_mensajes = valor2, id_ultimo_usuario = id_ultimo_usuario_obtenido, ultimo_tema = ultimo_tema_obtenido, ultimo_mensaje = ultimo_mensaje_obtenido }); //revisar si funciona el Get String


            }

            registros = list.ToArray();
            sqlreader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();

            return registros;


        }


        public ModeloTema[] RetornarTemas(string nombrecategoria)
        {
            ModeloTema[] registros = null;
            string connectionString = null;
            SqlConnection sqlCnn;
            SqlConnection sqlCnn0;
            var list = new List<ModeloTema>();
            SqlCommand sqlCmd;
            SqlCommand sqlCmd0;
            string sql0 = null;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql0 = "select id_categoria from categoria where nombre ='" + nombrecategoria + "'";
            sqlCnn0 = new SqlConnection(connectionString);
            sqlCnn0.Open();
            sqlCmd0 = new SqlCommand(sql0, sqlCnn0);
            var id_categoria = sqlCmd0.ExecuteScalar();
            string string_id_categoria = "";
            if (id_categoria != null) //esto siempre debiese cumplirse a menos de un error extraño
            {
                string_id_categoria = id_categoria.ToString();
            } //ya obtuvimos la id categoria... ahora obtener lo demas

            //OJO! CON EL USUARIO! Hay que retornar el nombre usuario, no el id_usuario.

            sql = "select nombre, id_usuario, descripcion, id_tema from tema where id_categoria = '" + string_id_categoria + "'"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();


            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {

                int valor_id_tema = sqlreader.GetInt32(3);
                //                list.Add(new ModeloTema { nombre = sqlreader.GetString(0), id_usuario_string = sqlreader.GetString(1), descripcion = sqlreader.GetString(2) }); //revisar si funciona el Get String

                int cantidad_mensajes_por_tema = RetornarCantidadMensajesPorTema(valor_id_tema);

                list.Add(new ModeloTema { nombre = sqlreader.GetString(0),id_usuario_string=RetornarNombreUsuario2(sqlreader.GetInt32(1)), id_usuario = sqlreader.GetInt32(1), descripcion = sqlreader.GetString(2), id_tema = sqlreader.GetInt32(3), cantidad_mensajes = cantidad_mensajes_por_tema });


            }
            registros = list.ToArray();
            sqlreader.Close();
            sqlCnn.Close();
            sqlCnn0.Close();
            sqlCmd.Dispose();
            sqlCmd0.Dispose();

            return registros;


        }

        public int ObtenerIdUsuario(String usuario)
        {
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "SELECT id_usuario FROM usuario WHERE nombre ='" + usuario + "'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            int entero = -1;
            if (sqlreader.Read())
            {
                //agregue algunas lineas
                entero = sqlreader.GetInt32(0);

            }
            sqlreader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return entero;

        }


        public SqlDataReader RetornarInfoUsuario(string idusuario)
        {

            string connectionString = null;
            SqlConnection sqlCnn;


            SqlCommand sqlCmd;

            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select id_grupo , nombre, cantidad_comentarios,avatar_url,fecha_nacimiento,sexo,fecha_registro  from usuario where id_usuario = '" + idusuario + "'"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();



            return sqlreader;


        }


        public string RetornarNombreUsuario2(int idusuario)
        {

            string connectionString = null;
            SqlConnection sqlCnn;


            SqlCommand sqlCmd;

            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select  nombre  from usuario where id_usuario = '" + idusuario + "'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            string nombre_obtenido = null;
            while (sqlreader.Read())
            {
                nombre_obtenido = sqlreader.GetString(0);
            }
            sqlreader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return nombre_obtenido;


        }

        public int RetornarIdUsuario(string nombre)
        {

            string connectionString = null;
            SqlConnection sqlCnn;


            SqlCommand sqlCmd;

            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select  id_usuario  from usuario where nombre = '" + nombre + "'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            int id_usuario_obtenido = -1;
            while (sqlreader.Read())
            {
                id_usuario_obtenido = sqlreader.GetInt32(0);
            }

            sqlreader.Close();
            sqlCnn.Close();
            sqlCmd.Dispose();
            return id_usuario_obtenido;


        }
        public string RetornarAvatarUrlUsuario(int idusuario)
        {

            string connectionString = null;
            SqlConnection sqlCnn;


            SqlCommand sqlCmd;

            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select  avatar_url  from usuario where id_usuario = '" + idusuario + "'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            string nombre_obtenido = null;
            while (sqlreader.Read())
            {
                nombre_obtenido = sqlreader.GetString(0);
            }

            //agregue estas lineas
            sqlreader.Close();
            sqlCnn.Close();
            sqlCmd.Dispose();
            return nombre_obtenido;


        }

        public class ColumnaInfoUsuario2
        {
            public int id_grupo;
            public string nombre; //ojo! parece q debe ser int!
            public int cantidad_comentarios;
            public string avatar_url;
            public string fecha_nacimiento;
            public string sexo;
            public string fecha_registro;
            public string id_usuario;
        }

        public ColumnaInfoUsuario2[] RetornarInfoUsuario2(string idusuario)
        {

            ColumnaInfoUsuario2[] registros = null; // solo categorias publicas

            string connectionString = null;
            SqlConnection sqlCnn;


            SqlCommand sqlCmd;

            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select id_grupo , nombre, cantidad_comentarios,avatar_url,fecha_nacimiento,sexo,fecha_registro  from usuario where id_usuario = '" + idusuario + "'"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();

            var list = new List<ColumnaInfoUsuario2>();


            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                //    ViewBag.Categoria = sqlreader.GetValue(0); Comente esta linea en la revision.
                list.Add(new ColumnaInfoUsuario2 { id_grupo = sqlreader.GetInt32(0), nombre = sqlreader.GetString(1), cantidad_comentarios = sqlreader.GetInt32(2), avatar_url = sqlreader.GetString(3), fecha_nacimiento = sqlreader.GetString(4), sexo = sqlreader.GetString(5), fecha_registro = sqlreader.GetString(6), id_usuario = idusuario }); //revisar si funciona el Get String


            }

            registros = list.ToArray();

            sqlreader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return registros;


        }



        public class ColumnaUltimos5Temas
        {
            public int id_tema;
            public int id_categoria;
            public int id_usuario;
            public string nombre; //ojo! parece q debe ser int!
            public string descripcion;
            public string mensaje;

        }

        public ColumnaUltimos5Temas[] RetornarUltimos5TemasCreados(string idusuario)
        {

            ColumnaUltimos5Temas[] registros = null; // solo categorias publicas
            string connectionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;


            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select TOP 5 * from tema where id_usuario = '" + idusuario + "' order by id_tema DESC"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();

            var list = new List<ColumnaUltimos5Temas>();


            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                //    ViewBag.Categoria = sqlreader.GetValue(0); Comente esta linea en la revision.
                list.Add(new ColumnaUltimos5Temas { id_tema = sqlreader.GetInt32(0), id_categoria = sqlreader.GetInt32(1), id_usuario = Int32.Parse(idusuario), nombre = sqlreader.GetString(3), descripcion = sqlreader.GetString(4), mensaje = sqlreader.GetString(5) }); //revisar si funciona el Get String


            }

            registros = list.ToArray();

            sqlreader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return registros;


        }


        public class ColumnaUltimos5Comentarios
        {
            public int id_comentario;
            public int id_tema;
            public int id_usuario;
            public string mensaje;

        }


        public ColumnaUltimos5Comentarios[] RetornarUltimos5Comentarios(string idusuario)
        {

            ColumnaUltimos5Comentarios[] registros = null; // solo categorias publicas
            string connectionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;


            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select TOP 5 * from comentario where id_usuario = '" + idusuario + "' order by id_comentario DESC"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();

            var list = new List<ColumnaUltimos5Comentarios>();


            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                //    ViewBag.Categoria = sqlreader.GetValue(0); Comente esta linea en la revision.
                list.Add(new ColumnaUltimos5Comentarios { id_comentario = sqlreader.GetInt32(0), id_tema = sqlreader.GetInt32(1), id_usuario = Int32.Parse(idusuario), mensaje = sqlreader.GetString(3) }); //revisar si funciona el Get String


            }

            registros = list.ToArray();

            sqlreader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return registros;


        }

        public int RetornarCantidadTemas(int id_categoria) //modificada
        {

            string connectionString = null;
            SqlConnection sqlCnn;


            SqlCommand sqlCmd;

            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";



            sql = "select COUNT(id_tema) from tema  where id_categoria = '" + id_categoria + "'"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();

            int numero = -1;
            while (sqlreader.Read())
            {
                numero = sqlreader.GetInt32(0);
            }
            //agregue estas lineas
            sqlreader.Close();
            sqlCnn.Close();
            sqlCmd.Dispose();

            return numero;


        }


        public List<UltimoMensaje> RetornarUltimoMensaje(int id_categoria)
        {
            UltimoMensaje[] registros = null; // solo categorias publicas
            var list = new List<UltimoMensaje>();
            string connectionString = null;
            SqlConnection sqlCnn;


            SqlCommand sqlCmd;

            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";



            sql = "SELECT TOP 1 c.id_comentario, t.* FROM (SELECT co.id_comentario, co.id_tema FROM comentario co) c,(SELECT cat.id_categoria FROM categoria cat) ca,tema t WHERE c.id_tema = t.id_tema AND t.id_categoria = ca.id_categoria AND ca.id_categoria = '" + id_categoria + "'  ORDER BY c.id_comentario DESC;"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            int id_ultimo_usuario_obtenido = -1;
            string ultimo_tema_obtenido = null;
            string ultimo_mensaje_obtenido = null;

            while (sqlreader.Read())
            {
                id_ultimo_usuario_obtenido = sqlreader.GetInt32(3);
                ultimo_tema_obtenido = sqlreader.GetString(4);
                ultimo_mensaje_obtenido = sqlreader.GetString(6);
            }
            list.Add(new UltimoMensaje { id_ultimo_usuario = id_ultimo_usuario_obtenido, ultimo_tema = ultimo_tema_obtenido, ultimo_mensaje = ultimo_mensaje_obtenido });
            registros = list.ToArray();
            sqlCmd.Dispose();
            sqlCnn.Close();
            sqlreader.Close();
            return list;


        }


        public int RetornarCantidadMensajesPorTema(int id_tema) //TERMINAR
        {


            string connectionString = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            SqlConnection sqlCnn2;


            SqlCommand sqlCmd2;

            string sql2 = null;



            sql2 = "select COUNT(id_comentario) from comentario  where id_tema = '" + id_tema + "'"; //queremos obtener el id de la categoria
            sqlCnn2 = new SqlConnection(connectionString);
            sqlCnn2.Open();
            sqlCmd2 = new SqlCommand(sql2, sqlCnn2);
            int suma = 0;
            SqlDataReader sqlreader2 = sqlCmd2.ExecuteReader();

            while (sqlreader2.Read())
            {
                suma += sqlreader2.GetInt32(0);
            }

            //agregue estas lineas!
            sqlreader2.Close();
            sqlCmd2.Dispose();
            sqlCnn2.Close();
            return suma;
        }



        public int RetornarCantidadMensajes(int id_categoria)  //por categoria
        {


            string connectionString = null;
            SqlConnection sqlCnn;


            SqlCommand sqlCmd;

            string sql = null;


            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";



            sql = "select id_tema from tema  where id_categoria = '" + id_categoria + "'"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            SqlDataReader sqlreader2 = null;
            int suma = 0;
            while (sqlreader.Read())
            {
                SqlConnection sqlCnn2;


                SqlCommand sqlCmd2;

                string sql2 = null;


                if (sqlreader.GetValue(0) is int)
                {
                    sql2 = "select COUNT(id_comentario) from comentario  where id_tema = '" + sqlreader.GetValue(0) + "'"; //queremos obtener el id de la categoria
                    sqlCnn2 = new SqlConnection(connectionString);
                    sqlCnn2.Open();
                    sqlCmd2 = new SqlCommand(sql2, sqlCnn2);

                    sqlreader2 = sqlCmd2.ExecuteReader();
                    while (sqlreader2.Read())
                    {
                        suma += sqlreader2.GetInt32(0);
                    }
                    sqlreader2.Close();

                }



            }
            sqlreader.Close();

            sqlCnn.Close();
            sqlCnn.Close();
            sqlCmd.Dispose();
            sqlCmd.Dispose();
            return suma;





        }






        public List<String> LogIn(String usuario, String password)
        {
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "SELECT id_usuario, avatar_url, id_grupo FROM usuario WHERE nombre ='" + usuario + "' AND contrasenna = '" + password + "'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            List<String> l = new List<String>();
            if (sqlreader.Read())
            {
                l.Add(sqlreader.GetInt32(0).ToString());
                l.Add(sqlreader.GetString(1));
                l.Add(sqlreader.GetInt32(2).ToString());
            }
            //agregue estas lineas
            sqlreader.Close();
            sqlCnn.Close();
            sqlCmd.Dispose();
            return l;
        }

        public class ColumnaComentarios2
        {
            public int id_comentario;
            public int id_usuario; //ojo! parece q debe ser int!
            public string mensaje;
            public string nombre_usuario;
            public string avatar_url;
        }

        public ColumnaComentarios2[] RetornarComentarios(string nombretema)
        {
            ColumnaComentarios2[] registros = null;
            string connectionString = null;
            SqlConnection sqlCnn;
            SqlConnection sqlCnn0;

            SqlCommand sqlCmd;
            SqlCommand sqlCmd0;
            string sql0 = null;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql0 = "select id_tema from tema where nombre ='" + nombretema + "'";
            sqlCnn0 = new SqlConnection(connectionString);
            sqlCnn0.Open();
            sqlCmd0 = new SqlCommand(sql0, sqlCnn0);
            var id_tema = sqlCmd0.ExecuteScalar();
            string string_id_tema = "";
            if (id_tema != null) //esto siempre debiese cumplirse a menos de un error extraño
            {
                string_id_tema = id_tema.ToString();
            } //ya obtuvimos la id tema... ahora obtener lo demas



            sql = "select  id_usuario, mensaje, id_comentario from comentario where id_tema = '" + string_id_tema + "'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            var list = new List<ColumnaComentarios2>();

            //No estoy seguro si va a funcionar que tome como string el id_usuario en vez de int.

            while (sqlreader.Read()) //guardamos en una lista el nombre y descripcion de todas las categorias publicas, luego se pasan a un arreglo y ese arreglo a un viewbag
            {
                // ViewBag.Categoria = sqlreader.GetValue(0); Comente esta linea en la revision.
                string nombre_usuario_obtenido = RetornarNombreUsuario2(sqlreader.GetInt32(0));
                string avatar_url_obtenido = RetornarAvatarUrlUsuario(sqlreader.GetInt32(0));
                list.Add(new ColumnaComentarios2 { id_usuario = sqlreader.GetInt32(0), mensaje = sqlreader.GetString(1), id_comentario = sqlreader.GetInt32(2), nombre_usuario = nombre_usuario_obtenido, avatar_url = avatar_url_obtenido }); //revisar si funciona el Get String


            }

            registros = list.ToArray();
            sqlreader.Close();
            sqlCnn0.Close();
            sqlCnn.Close();
            sqlCmd0.Dispose();
            sqlCmd.Dispose();
            return registros;


        }


        public int EjecutarSql(String Query) // funcion para ejecutar todas nuestras querys...
        {
            SqlCommand MiComando = new SqlCommand(Query, this.MiConexion); //recibe cadena de texto

            int FilasAfectadas = MiComando.ExecuteNonQuery(); // se ejecuta la query. Retorna un valor entero
            /*
                        if (FilasAfectadas > 0) //se realizo exitosamente
                        {
                            //ver como mandar mensaje al usuario!
                            //MessageBox.Show("")......
                        }
                        else
                        {
                            //ver como mandar mensaje al usuario...
                            //error!

                        }*/
            return FilasAfectadas;
        }



        /*public void ActualizarGrid(DataGridView dg, String Query){
         this.Conectar(); //se conecta a la base de datos..
         System.Data.DataSet MiDataSet = new System.Data.DataSet(); // crear data set
             
         SqlDataAdapter MiDataAdapter = new SqlDataAdapter(Query,MiConexion); //crear adaptador de datos
             
            
         * 
         * 
             
         //Llenar el data set, solo se puede hacer a traves de un data adapter
         *
         MiDataAdapter.Fill(MiDataSet,"alumno");
         dg.DataSource = MiDataSet;
         dg.DataMember = "alumno"; //llenar el valor adecuado a las propiedades del DataGrid
           
         this.Desconectar(); //desconectarse de la bd.
         }*/


    }
}