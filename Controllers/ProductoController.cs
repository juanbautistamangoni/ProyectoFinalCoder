using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Coder_C_.ADOHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Proyecto_Final_Coder_C_.Controllers
{
    public class ProductoController : ControllerBase
    {
        [HttpPut(Name = "ModificarProducto")]

        public bool ModificarProducto([FromBody] PutProducto producto)
        {
            try
            {
                return ProductoHandler.ModificarProducto(
                    new Producto
                    {
                        Id = producto.Id,
                        Descripcion = producto.Descripcion,
                        Costo = producto.Costo,
                        PrecioVenta = producto.PrecioVenta,
                        Stock = producto.Stock,
                        IdUsuario = producto.IdUsuario
                    }
                );
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost(Name = "CrearProducto")]  
                                            
        public bool CrearProducto([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.CrearProducto(
                    new Producto
                    {
                        Descripcion = producto.Descripcion,
                        Costo = producto.Costo,
                        PrecioVenta = producto.PrecioVenta,
                        Stock = producto.Stock,
                        IdUsuario = producto.IdUsuario
                    }
                );
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete] 

        public bool EliminarProducto([FromBody] long idProducto)
        {
            try
            {
                ProductoVendidoHandler.EliminarProductoVendido(idProducto); 
                return ProductoHandler.EliminarProducto(idProducto);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
