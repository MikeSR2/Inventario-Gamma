//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventarioGamma.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public string Presentacion { get; set; }
        public double Precio_Costo { get; set; }
        public double Importe_Inventario { get; set; }
        public string Almacen { get; set; }
        public string Ubicacion { get; set; }
        public double Existencia { get; set; }
        public int Estatus { get; set; }
    }
}
