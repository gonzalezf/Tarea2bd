using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

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