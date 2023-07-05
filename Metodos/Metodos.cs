using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Final_Coder_C_.Modelos;

namespace Proyecto_Final_Coder_C_.Metodos
{
    public class Metodos
    {
        static string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True;";

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

    }
}
