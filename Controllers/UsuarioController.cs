using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Coder_C_.ADOHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_.Controllers
{
    public class UsuarioController : ControllerBase
    {
        [HttpPut(Name = "ModificarUsuario")]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            try
            {
                Usuario usuarioExistente = new Usuario();
                usuarioExistente = UsuarioHandler.TraerUsuario_conId(usuario.Id);
                if (Convert.ToInt32(usuarioExistente.Id) <= 0)
                {
                    return false; 
                }
                else
                {
                    return UsuarioHandler.ModificarUsuario(
                    new Usuario
                    {
                        Id = usuario.Id,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        NombreUsuario = usuario.NombreUsuario,
                        Contraseña = usuario.Contraseña,
                        Mail = usuario.Mail
                    }
                    );
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
