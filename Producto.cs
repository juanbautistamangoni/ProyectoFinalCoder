using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_
{
    public class Producto
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private float _costo;
        private float _precioVenta;
        private int _stock;
        private string _idUsuario;

        #endregion

        #region Propiedades
        public string Id
        { 
            get 
            { 
                return this._id; 
            } 
            set 
            {  
                this._id = value; 
            } 
        }

        public string Descripcion
        {
            get
            {
                return this._descripcion;
            }
            set 
            { 
                this._descripcion = value;
            }
        }

        public float Costo
        {
            get
            { 
                return this._costo; 
            }
            set
            {
                this._costo = value;
            }
        }

        public float PrecioVenta
        {
            get
            {
                return this._precioVenta;
            }
            set
            {
                this._precioVenta = value;
            }
        }

        public int Stock
        {
            get
            {
                return this._stock;
            }
            set
            {
                this._stock = value;
            }
        }

        public string IdUsuario
        {
            get
            {
                return this._idUsuario;
            }
            set
            {
                this._idUsuario = value;
            }
        }
        #endregion

        public Producto()
        {
            _id = string.Empty;
            _descripcion = string.Empty;
            _costo = 0;
            _precioVenta = 0;
            _stock = 0;
            _idUsuario = string.Empty;
        }
    }
}
