using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_Coder_C_
{
    public class Venta
    {
        #region Atributos

        private string _id;
        private string _comentarios;
        //private string _idProducto;
        //private int _stock;
        //private string _idVenta;

        #endregion

        #region Propiedades
        public string Id 
        {
            get 
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Comentarios
        {
            get
            {
                return _comentarios;
            }
            set
            {
                _comentarios = value;
            }
        }

        //public string IdProducto
        //{
        //    get
        //    {
        //        return _idProducto;
        //    }
        //    set
        //    {
        //        _idProducto = value;
        //    }
        //}

        //public int Stock
        //{
        //    get 
        //    {
        //    return _stock;
        //    }
        //    set
        //    {
        //        _stock = value;
        //    }
        //}

        //public string IdVenta
        //{
        //    get
        //    {
        //        return _idVenta;
        //    }
        //    set
        //    {
        //        _idVenta = value;
        //    }
        //}
        #endregion

        public Venta() 
        {
            _id = string.Empty; 
            _comentarios = string.Empty;
        }
    }
}
