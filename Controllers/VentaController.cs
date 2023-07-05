using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Coder_C_.ADOHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_.Controllers
{
    public class VentaController : ControllerBase
    {
        public bool CargarVenta([FromBody] List<PostVenta> listaDeProductosVendidos)
        {
            
            Producto producto = new Producto();
            Usuario usuario = new Usuario();
            foreach (PostVenta item in listaDeProductosVendidos)
            {
                producto = ProductoHandler.TraerProducto_conId(item.Id);
                if (Convert.ToInt32(producto.Id) <= 0) 
                {
                    return false;
                }

                if (item.Stock <= 0) 
                {
                    return false;
                }

                if (producto.Stock < item.Stock) 
                {
                    return false;
                }

                usuario = UsuarioHandler.TraerUsuario_conId(item.IdUsuario);
                if (Convert.ToInt32(usuario.Id) <= 0) 
                {
                    return false;
                }
            }

            
            Venta venta = new Venta();
            long idVenta = VentaHandler.CargarVenta(venta);
            
            if (idVenta >= 0)
            {
                
                List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
                foreach (PostVenta item in listaDeProductosVendidos)
                {
                    ProductoVendido productoVendido = new ProductoVendido();
                    productoVendido.IdProducto = item.Id;
                    productoVendido.Stock = item.Stock;
                    productoVendido.IdVenta = idVenta;
                    productosVendidos.Add(productoVendido);
                }
                
                if (ProductoVendidoHandler.CargarProductosVendidos(productosVendidos))
                {
                    
                    bool resultado = false;

                    
                    foreach (ProductoVendido item in productosVendidos)
                    {
                        producto.Id = item.IdProducto;
                        producto = ProductoHandler.ConsultarStock(producto);
                        producto.Stock = producto.Stock - item.Stock;
                        resultado = ProductoHandler.ActualizarStock(producto);
                        if (resultado == false) 
                        {
                            break;
                        }
                    }
                    return resultado;
                }
                else
                {
                    return false; 
                }
            }
            else
            {
                return false; 
            }
        }
    }
}
