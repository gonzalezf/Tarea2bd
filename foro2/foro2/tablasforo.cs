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

        
        public SqlDataReader RetornarCategoriasPublicas()
        {

            string connectionString = null;
            SqlConnection sqlCnn;
           
            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
           
            sql = "select nombre, descripcion from categoria where publico = 'True'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
    
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();

            /*
             * 
             sqlreader.Close();
             * sqlCmd.Dispose();
             * sqlCnn.Close();
            */
            return sqlreader;


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
            if(sqlreader.Read())
            {
                list.Add(sqlreader.GetInt32(0).ToString());
                list.Add(sqlreader.GetInt32(1).ToString());
                list.Add(sqlreader.GetInt32(2).ToString());
            }
            sqlreader.Close();
            sqlCnn.Close();
            return list;
        }
        /*
        public List<ModeloPM> RetornarMensajesPrivados(String inboxid)
        {
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "SELECT u.nombre as nombre_remitente, m.* FROM mensaje_privado m, (SELECT us.id_usuario, us.nombre FROM usuario us) u WHERE m.id_buzon = 2 AND u.id_usuario = m.id_remitente ORDER BY id_mensaje DESC";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            var list = new List<ModeloPM>();
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            while (sqlreader.Read())
            {
                list.Add(new ModeloPM(sqlreader.GetString(0), sqlreader.GetInt32(1), sqlreader.GetInt32(2), sqlreader.GetInt32(3), sqlreader.GetBoolean(4), sqlreader.GetString(5), sqlreader.GetString(6)));
            }

            return list;
        }*/

        public SqlDataReader RetornarTodasLasCategorias()
        {

            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "select nombre, descripcion from categoria";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();

            
   
            return sqlreader;


        }


        public SqlDataReader RetornarTemas(string nombrecategoria)
        {

            string connectionString = null;
            SqlConnection sqlCnn;
            SqlConnection sqlCnn0;

            SqlCommand sqlCmd;
            SqlCommand sqlCmd0;
            string sql0 = null; 
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            
            sql0 = "select id_categoria from categoria where nombre ='"+nombrecategoria+"'";
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

            sql = "select nombre, id_usuario, descripcion from tema where id_categoria = '"+string_id_categoria+"'"; //queremos obtener el id de la categoria
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);

            SqlDataReader sqlreader = sqlCmd.ExecuteReader();



            return sqlreader;


        }

        public int ObtenerIdUsuario(String usuario)
        {
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "SELECT id_usuario FROM usuario WHERE nombre ='" + usuario +"'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            if (sqlreader.Read())
            {
                return sqlreader.GetInt32(0);
            }
            return -1;
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

        public int LogIn(String usuario, String password)
        {
            string connectionString = null;
            SqlConnection sqlCnn;

            SqlCommand sqlCmd;
            string sql = null;

            connectionString = "Data Source=FELIPE\\SQLEXPRESS;Initial Catalog=XE;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            sql = "SELECT id_usuario FROM usuario WHERE nombre ='"+ usuario +"' AND contrasenna = '"+ password + "'";
            sqlCnn = new SqlConnection(connectionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlreader = sqlCmd.ExecuteReader();
            if(sqlreader.Read())
            {
                return sqlreader.GetInt32(0);
            }
            return -1;
        }



        public SqlDataReader RetornarComentarios(string nombretema)
        {

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



            return sqlreader;


        }


        public int EjecutarSql(String Query) // funcion para ejecutar todas nuestras querys...
        {
            SqlCommand MiComando = new SqlCommand(Query,this.MiConexion); //recibe cadena de texto

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