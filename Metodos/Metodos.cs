using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_.Metodos
{
    public class Metodos
    {
        static string connectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True;";

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

        public static List<Venta> ObtenerVentas()
        {
            List<Venta> ventas = new List<Venta>(); //LISTA DE OBJETOS VENTA

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                string query = "select id,comentarios from Venta"; //TRAE TODAS LAS VENTAS
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta();  //INSTANCIA NUEVO OBJETO VENTA
                    venta.Id = (string)reader["id"];
                    venta.Comentarios = (string)reader["comentarios"];

                    ventas.Add(venta); //AGREGA A LA LISTA DE VENTAS
                }
                reader.Close();
            }
            return ventas;
        }
    }
}
