using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_.ADOHandlers
{
    public static class ProductoVendidoHandler
    {
        public const string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True;";

        /// <summary>
        /// Obtiene todos los productos vendidos
        /// </summary>
        /// <returns></returns>
        public static List<ProductoVendido> ObtenerProductosVendidos()
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>(); //LISTA DE OBJETOS PRODUCTOVENDIDO

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string query = "select id,idProducto,stock,idVenta from ProductoVendido"; //TRAE TODOS LOS PRODUCTOS VENDIDOS
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductoVendido productoVendido = new ProductoVendido(); //INSTANCIA NUEVO OBJETO PRODUCTOVENDIDO
                    productoVendido.Id = (string)reader["id"];
                    productoVendido.IdProducto = (string)reader["idProducto"];
                    productoVendido.Stock = Convert.ToInt32(reader["stock"]);
                    productoVendido.IdVenta = (string)reader["idVenta"];

                    productosVendidos.Add(productoVendido); //AGREGA A LA LISTA DE PRODUCTOSVENDIDOS
                }
                reader.Close();
            }
            return productosVendidos;
        }

        /// <summary>
        /// Obtiene todos los productos vendidos por el usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static List<ProductoVendido> ObtenerProductosVendidos(string idUsuario) //OBTIENE PRODUCTOS VENDIDOS DEL USUARIO
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>(); //LISTA DE OBJETOS PRODUCTOVENDIDO

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string query = @"select PV.id, PV.idProducto, PV.stock, PV.idVenta from Producto P inner join ProductoVendido PV on P.idProducto = PV.idProducto where idUsuario = @idUsuario";   //TRAE LOS PRODUCTOS VENDIDOS DEL USUARIO PASADO COMO PARAMETRO
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
                    ProductoVendido productoVendido = new ProductoVendido(); //INSTANCIA NUEVO OBJETO PRODUCTOVENDIDO
                    productoVendido.Id = (string)reader["id"];
                    productoVendido.IdProducto = (string)reader["idProducto"];
                    productoVendido.Stock = Convert.ToInt32(reader["stock"]);
                    productoVendido.IdVenta = (string)reader["idVenta"];

                    productosVendidos.Add(productoVendido); //AGREGA A LA LISTA DE PRODUCTOSVENDIDOS
                }
                reader.Close();
            }
            return productosVendidos;
        }

        public static bool EliminarProductoVendido(long idProducto)
        {
            bool resultado = false;
            int rowsAffected = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryUpdate = "DELETE FROM [SistemaGestion].[dbo].[ProductoVendido] " + 
                                        "WHERE IdProducto = @idProducto";

                var parameterIdProducto = new SqlParameter("idProducto", SqlDbType.BigInt);
                parameterIdProducto.Value = idProducto;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterIdProducto);
                    rowsAffected = sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            if (rowsAffected >= 1)
            {
                resultado = true;
            }
            return resultado;
        }
    }
}
