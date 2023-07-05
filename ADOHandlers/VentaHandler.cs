using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_.ADOHandlers
{
    public static class VentaHandler
    {
        public const string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True;";

        /// <summary>
        /// Obtiene todas las ventas existentes
        /// </summary>
        /// <returns></returns>
        public static List<Venta> ObtenerVentas()
        {
            List<Venta> ventas = new List<Venta>(); //LISTA DE OBJETOS VENTA

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select id,comentarios,idUsuario from Venta"; //TRAE TODAS LAS VENTAS
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta();  //INSTANCIA NUEVO OBJETO VENTA
                    venta.Id = (string)reader["id"];
                    venta.Comentarios = (string)reader["comentarios"];
                    venta.IdUsuario = (string)reader["idUsuario"];

                    ventas.Add(venta); //AGREGA A LA LISTA DE VENTAS
                }
                reader.Close();
            }
            return ventas;
        }

        /// <summary>
        /// Obtiene las ventas del usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static List<Venta> ObtenerVentas(string idUsuario)  //OBTIENE VENTAS DEL USUARIO
        {
            List<Venta> ventas = new List<Venta>(); //LISTA DE OBJETOS VENTA

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"select id,comentarios,idUsuario from Venta where idUsuario = @idUsuario"; //TRAE TODAS LAS VENTAS DEL USUARIO PASADO COMO PARAMETRO
                var parametro = new SqlParameter();
                parametro.ParameterName = "idUsuario";
                parametro.SqlDbType = SqlDbType.BigInt;
                parametro.Value = Double.TryParse(idUsuario, out double i) ? i : 0;
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(parametro);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta();  //INSTANCIA NUEVO OBJETO VENTA
                    venta.Id = (string)reader["id"];
                    venta.Comentarios = (string)reader["comentarios"];
                    venta.IdUsuario = (string)reader["idUsuario"];

                    ventas.Add(venta); //AGREGA A LA LISTA DE VENTAS
                }
                reader.Close();
            }
            return ventas;
        }

        public static long CargarVenta(Venta venta)
        {
            bool resultado = false;
            long idVenta = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Venta] (Comentarios) " + 
                                        "VALUES (@comentarios) " +
                                        "SELECT @@IDENTITY";

                var parameterComentarios = new SqlParameter("comentarios", SqlDbType.VarChar);
                parameterComentarios.Value = venta.Comentarios;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterComentarios);
                    idVenta = Convert.ToInt64(sqlCommand.ExecuteScalar());
                }
                sqlConnection.Close();
            }
            return idVenta;
        }

    }
}
