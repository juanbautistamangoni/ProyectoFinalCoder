using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_.ADOHandlers
{
    public static class UsuarioHandler
    {

        public const string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True;";

        /// <summary>
        /// Obtiene los datos de todos los usuarios existentes
        /// </summary>
        /// <returns></returns>
        public static List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>(); //LISTA DE OBJETOS USUARIO

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string query = "select id,nombre,apellido,nombreUsuario,contrasena,mail from Usuario"; //TRAE TODOS LOS USUARIOS DE LA TABLA
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario(); //INSTANCIA NUEVO OBJETO USUARIO

                    usuario.Id = (string)reader["id"];
                    usuario.Nombre = (string)reader["nombre"];
                    usuario.Apellido = (string)reader["apellido"];
                    usuario.NombreUsuario = (string)reader["nombreUsuario"];
                    usuario.Contraseña = (string)reader["contrasena"];
                    usuario.Mail = (string)reader["mail"];

                    usuarios.Add(usuario); //AGREGA A LA LISTA DE USUARIOS, VA AGREGANDO AL FINAL
                }
                reader.Close();
            }
            return usuarios;
        }

        /// <summary>
        /// Obtiene los datos del usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static Usuario ObtenerUsuarios(string idUsuario) //OBTIENE USUARIO POR ID
        {
            Usuario usuario = new Usuario(); ;
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string query = @"select id,nombre,apellido,nombreUsuario,contrasena,mail from Usuario where id = @idUsuario"; //TRAE TODOS LOS USUARIOS DE LA TABLA
                var parametro = new SqlParameter();
                parametro.ParameterName = "idUsuario";
                parametro.SqlDbType = SqlDbType.BigInt;
                parametro.Value = Double.TryParse(idUsuario, out double i) ? i : 0;
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.Add(parametro);

                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usuario.Id = (string)reader["id"];
                    usuario.Nombre = (string)reader["nombre"];
                    usuario.Apellido = (string)reader["apellido"];
                    usuario.NombreUsuario = (string)reader["nombreUsuario"];
                    usuario.Contraseña = (string)reader["contrasena"];
                    usuario.Mail = (string)reader["mail"];
                }
                reader.Close();
            }
            return usuario;
        }

        public static Usuario IniciarSesion(string nombreUsuario, string contrasena)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"select id,nombre,apellido,nombreUsuario,contrasena,mail from Usuario where nombreUsuario = @nombreUsuario and contrasena = @contrasena";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@contrasena", contrasena);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = (string)reader["id"],
                                Nombre = (string)reader["nombre"],
                                Apellido = (string)reader["apellido"],
                                NombreUsuario = (string)reader["nombreUsuario"],
                                Contraseña = (string)reader["contrasena"],
                                Mail = (string)reader["mail"]
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;
            int rowsAffected = 0;

           
            if (Convert.ToInt32(usuario.Id) <= 0) 
            {
                return false;
            }

            if (String.IsNullOrEmpty(usuario.Nombre) ||
                String.IsNullOrEmpty(usuario.Apellido) ||
                String.IsNullOrEmpty(usuario.NombreUsuario) ||
                String.IsNullOrEmpty(usuario.Contraseña) ||
                String.IsNullOrEmpty(usuario.Mail))
            {
                return false; 
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Usuario] " + 
                                        "SET Nombre = @nombre, " +
                                            "Apellido = @apellido, " +
                                            "NombreUsuario = @nombreUsuario, " +
                                            "Contraseña = @contraseña, " +
                                            "Mail = @mail " +
                                            "WHERE Id = @id";

                var parameterNombre = new SqlParameter("nombre", SqlDbType.VarChar);
                parameterNombre.Value = usuario.Nombre;

                var parameterApellido = new SqlParameter("apellido", SqlDbType.VarChar);
                parameterApellido.Value = usuario.Apellido;

                var parameterNombreUsuario = new SqlParameter("nombreUsuario", SqlDbType.VarChar);
                parameterNombreUsuario.Value = usuario.NombreUsuario;

                var parameterContraseña = new SqlParameter("contraseña", SqlDbType.VarChar);
                parameterContraseña.Value = usuario.Contraseña;

                var parameterMail = new SqlParameter("mail", SqlDbType.VarChar);
                parameterMail.Value = usuario.Mail;

                var parameterId = new SqlParameter("id", SqlDbType.BigInt);
                parameterId.Value = usuario.Id;

                sqlConnection.Open(); 

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterNombre);
                    sqlCommand.Parameters.Add(parameterApellido);
                    sqlCommand.Parameters.Add(parameterNombreUsuario);
                    sqlCommand.Parameters.Add(parameterContraseña);
                    sqlCommand.Parameters.Add(parameterMail);
                    sqlCommand.Parameters.Add(parameterId);
                    rowsAffected = sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            if (rowsAffected == 1)
            {
                resultado = true;
            }
            return resultado;
        }

        public static Usuario TraerUsuario_conId(long id)
        {
            Usuario usuario = new Usuario();

            
            if (id <= 0)
            {
                return usuario; 
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[Usuario] WHERE Id = @id", sqlConnection))
                {
                    var sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "id";
                    sqlParameter.SqlDbType = SqlDbType.VarChar;
                    sqlParameter.Value = id;
                    sqlCommand.Parameters.Add(sqlParameter);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows & dataReader.Read())
                        {
                            usuario.Id = (dataReader["Id"]).ToString();
                            usuario.Nombre = dataReader["Nombre"].ToString();
                            usuario.Apellido = dataReader["Apellido"].ToString();
                            usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                            usuario.Contraseña = dataReader["Contraseña"].ToString();
                            usuario.Mail = dataReader["Mail"].ToString();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuario;
        }
    }
}
