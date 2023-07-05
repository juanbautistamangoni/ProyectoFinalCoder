using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_.ADOHandlers
{
    public static class ProductoHandler
    {
        public const string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True;";

        /// <summary>
        /// Obtiene todos los productos existentes
        /// </summary>
        /// <returns></returns>
        public static List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>(); //LISTA DE OBJETOS PRODUCTO

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string query = "select id,descripciones,costo,precioVenta,stock,idUsuario from Producto"; //TRAE TODOS LOS PRODUCTOS
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto(); //INSTANCIA NUEVO OBJETO PRODUCTO
                    producto.Id = (string)reader["id"];
                    producto.Descripcion = (string)reader["descripciones"];
                    producto.Costo = Convert.ToInt32(reader["costo"]);
                    producto.PrecioVenta = Convert.ToInt32(reader["precioVenta"]);
                    producto.Stock = Convert.ToInt32(reader["stock"]);
                    producto.IdUsuario = (string)reader["idUsuario"];

                    productos.Add(producto); //AGREGA A LA LISTA DE PRODUCTOS
                }
                reader.Close();
            }
            return productos;
        }

        /// <summary>
        /// Obtiene los productos existentes del usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static List<Producto> ObtenerProductos(string idUsuario)  //OBTIENE PRODUCTOS DEL USUARIO
        {
            List<Producto> productos = new List<Producto>(); //LISTA DE OBJETOS PRODUCTO

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string query = @"select id,descripciones,costo,precioVenta,stock,idUsuario from Producto where idUsuario = @idUsuario"; //TRAE TODOS LOS PRODUCTOS DEL USUARIO PASADO COMO PARAMETRO
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
                    Producto producto = new Producto(); //INSTANCIA NUEVO OBJETO PRODUCTO
                    producto.Id = (string)reader["id"];
                    producto.Descripcion = (string)reader["descripciones"];
                    producto.Costo = Convert.ToInt32(reader["costo"]);
                    producto.PrecioVenta = Convert.ToInt32(reader["precioVenta"]);
                    producto.Stock = Convert.ToInt32(reader["stock"]);
                    producto.IdUsuario = (string)reader["idUsuario"];

                    productos.Add(producto); //AGREGA A LA LISTA DE PRODUCTOS
                }
                reader.Close();
            }
            return productos;
        }

        public static bool CrearProducto(Producto producto)
        {
            bool resultado = false;
            long idProducto = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Producto] (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                                        "VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario) " +
                                        "SELECT @@IDENTITY";

                var parameterDescripciones = new SqlParameter("descripciones", SqlDbType.VarChar);
                parameterDescripciones.Value = producto.Descripcion;

                var parameterCosto = new SqlParameter("costo", SqlDbType.Money);
                parameterCosto.Value = producto.Costo;

                var parameterPrecioVenta = new SqlParameter("precioVenta", SqlDbType.Money);
                parameterPrecioVenta.Value = producto.PrecioVenta;

                var parameterStock = new SqlParameter("stock", SqlDbType.Int);
                parameterStock.Value = producto.Stock;

                var parameterIdUsuario = new SqlParameter("idUsuario", SqlDbType.BigInt);
                parameterIdUsuario.Value = producto.IdUsuario;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterDescripciones);
                    sqlCommand.Parameters.Add(parameterCosto);
                    sqlCommand.Parameters.Add(parameterPrecioVenta);
                    sqlCommand.Parameters.Add(parameterStock);
                    sqlCommand.Parameters.Add(parameterIdUsuario);
                    idProducto = Convert.ToInt64(sqlCommand.ExecuteScalar());
                }
                sqlConnection.Close();
                if (idProducto != 0)
                {
                    resultado = true;
                }
                return resultado;
            }
        }

        public static bool EliminarProducto(long id)
        {

            if (id <= 0)
            {
                return false;
            }

            bool resultado = false;
            int rowsAffected = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Producto] " +
                                        "WHERE Id = @id";

                var parameterId = new SqlParameter("id", SqlDbType.BigInt);
                parameterId.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
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

        public static Producto TraerProducto_conId(long id)
        {
            Producto producto = new Producto();

            if (id <= 0)
            {
                return producto;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                const string queryGet = "SELECT * FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @id";

                using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                {
                    var sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "id";
                    sqlParameter.SqlDbType = SqlDbType.BigInt;
                    sqlParameter.Value = id;
                    sqlCommand.Parameters.Add(sqlParameter);

                    sqlConnection.Open();

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows & sqlDataReader.Read())
                        {
                            producto = InicializarProductoDesdeBD(sqlDataReader);
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return producto;
        }

        private static Producto InicializarProductoDesdeBD(SqlDataReader dataReader)
        {
            Producto nuevoProducto = new Producto(
                                            Convert.ToInt64(dataReader["Id"]),
                                            dataReader["Descripcion"].ToString(),
                                            Convert.ToDouble(dataReader["Costo"]),
                                            Convert.ToDouble(dataReader["PrecioVenta"]),
                                            Convert.ToInt32(dataReader["Stock"]),
                                            Convert.ToInt64(dataReader["IdUsuario"]));
            return nuevoProducto;
        }

        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;
            int rowsAffected = 0;

            if (Convert.ToInt32(producto.Id) <= 0)
            {
                return false;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto] " +
                                        "SET Descripciones = @descripciones, " +
                                        "Costo = @costo, " +
                                        "PrecioVenta = @precioVenta, " +
                                        "stock = @Stock, " +
                                        "IdUsuario = @idUsuario " +
                                        "WHERE Id = @id";


                var parameterId = new SqlParameter("id", SqlDbType.BigInt);
                parameterId.Value = producto.Id;

                var parameterDescripciones = new SqlParameter("descripciones", SqlDbType.VarChar);
                parameterDescripciones.Value = producto.Descripcion;

                var parameterCosto = new SqlParameter("costo", SqlDbType.Money);
                parameterCosto.Value = producto.Costo;

                var parameterPrecioVenta = new SqlParameter("precioVenta", SqlDbType.Money);
                parameterPrecioVenta.Value = producto.PrecioVenta;

                var parameterStock = new SqlParameter("stock", SqlDbType.Int);
                parameterStock.Value = producto.Stock;

                var parameterIdUsuario = new SqlParameter("idUsuario", SqlDbType.BigInt);
                parameterIdUsuario.Value = producto.IdUsuario;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameterId);
                    sqlCommand.Parameters.Add(parameterDescripciones);
                    sqlCommand.Parameters.Add(parameterCosto);
                    sqlCommand.Parameters.Add(parameterPrecioVenta);
                    sqlCommand.Parameters.Add(parameterStock);
                    sqlCommand.Parameters.Add(parameterIdUsuario);
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
    }
}
