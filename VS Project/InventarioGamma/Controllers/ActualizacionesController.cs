using InventarioGamma.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace InventarioGamma.Controllers
{
    /// <summary>
    /// Controlador para las operaciones de tipo actualizacion de productos
    /// </summary>
    public class ActualizacionesController : Controller
    {
       
        /// <summary>
        /// Metodo para actualizar datos de un producto
        /// </summary>
        /// <param name="Productos">Producto a actualizar</param>
        /// <returns>Resultado de la operacion</returns>
        [HttpPost]
        public ActionResult ActualizarDatos(Producto Product)
        {
            String ValueBack = "";
            int resultado = -1;
            if (!Session["almacen"].Equals(Product.Almacen) && !Session["almacen"].Equals("Admin"))
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                ValueBack = "No pertenece al almacen, no es posible registrar producto";
                return Content(ValueBack, MediaTypeNames.Text.Plain);
            }
           
            
            try
            {
                var contextoDB = new InventarioGammaEntities();
                //Actualiza producto
                var producto = (from prod in contextoDB.Productos
                                where prod.IdProducto == Product.IdProducto &&
                                (prod.Estatus==1 || prod.Estatus==2)
                                select prod).FirstOrDefault();
                producto.Almacen = Product.Almacen;
                producto.Clave = Product.Clave;
                producto.Descripcion = Product.Descripcion;
                producto.Importe_Inventario = Product.Importe_Inventario;
                producto.Precio_Costo = Product.Precio_Costo;
                producto.Presentacion = Product.Presentacion;
                producto.Ubicacion = Product.Ubicacion;
                resultado = contextoDB.SaveChanges();
                ///Crea registro de historial
                Historial hist = new Historial();
                hist.Producto = producto.IdProducto;
                hist.Fecha_Movimiento = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                hist.Tipo_Movimiento = "Modificacion";
                hist.Origen = producto.Almacen;
                hist.Destino = producto.Almacen;
                hist.Cantidad = 0;
                hist.Cantidad_Anterior = producto.Existencia;
                hist.Cantidad_Actual = producto.Existencia;
                contextoDB.Historials.Add(hist);
                contextoDB.SaveChanges();
                ValueBack = "Registro actualizado correctamente";
            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                ValueBack = "Ocurrió un error durante actualización, verifique los datos y vuelva a intentarlo";
                return Content(ValueBack, MediaTypeNames.Text.Plain);
            }
            return Content(ValueBack);
        }

        /// <summary>
        /// Metodo para realizar transferencia de productos entre almacenes
        /// </summary>
        /// <param name="Clave">Clave del producto</param>
        /// <param name="Almacen">Almacen de destino</param>
        /// <returns>Resultado de la operacion</returns>
        [HttpPost]
        public ActionResult TransferenciaAlmacen(int idProducto, String Almacen, int Cantidad, String ClaveT)
        {
            String ValueBack = "";
            int resultado = -1;
            
           
            try
            {
                var contextoDB = new InventarioGammaEntities();
                Producto PodTrans = new Producto();
                ///Realiza actualizacion/transferencia
                var producto = (from prod in contextoDB.Productos
                                where prod.IdProducto == idProducto &&
                                (prod.Estatus==1 || prod.Estatus==2)
                                select prod).FirstOrDefault();
                if (!Session["almacen"].Equals(producto.Almacen) && !Session["almacen"].Equals("Admin"))
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    ValueBack = "No pertenece al almacen, no es posible registrar producto";
                    return Content(ValueBack, MediaTypeNames.Text.Plain);
                }
                if (!(producto.Existencia >= Cantidad))
                {

                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    ValueBack = "No hay cantidad suficiente para transferir";
                    return Content(ValueBack, MediaTypeNames.Text.Plain);
                }
                if (producto.Almacen == Almacen)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    ValueBack = "Ha seleccionado el mismo almacén, Debe seleccionar otro almacén de destino";
                    return Content(ValueBack, MediaTypeNames.Text.Plain);
                }

                var productoD = (from prod in contextoDB.Productos
                                 where prod.Clave == ClaveT && prod.Almacen == Almacen && 
                                 (prod.Estatus == 1 || prod.Estatus==2)
                                select prod).FirstOrDefault();
                if (productoD!=null)
                {
                    productoD.Existencia = productoD.Existencia + Cantidad;
                    ValueBack = "El producto ya existía en esa sucursal, se actualizó existencia!";

                }
                else
                {
                    PodTrans.Almacen = Almacen;
                    PodTrans.Clave = producto.Clave;
                    PodTrans.Descripcion = producto.Descripcion;
                    PodTrans.Estatus = 2;
                    PodTrans.Existencia = Cantidad;
                    PodTrans.Importe_Inventario = producto.Importe_Inventario;
                    PodTrans.Marca = producto.Marca;
                    PodTrans.Nombre = producto.Nombre;
                    PodTrans.Precio_Costo = producto.Precio_Costo;
                    PodTrans.Presentacion = producto.Presentacion;
                    PodTrans.Ubicacion = "NA";
                    contextoDB.Productos.Add(PodTrans);
                    ValueBack = "Transferencia realizada!";
                }

                
                producto.Existencia = producto.Existencia-Cantidad;
                if (producto.Estatus == 2 && producto.Existencia==0)
                {
                    producto.Estatus = 0;
                }
                

                resultado = contextoDB.SaveChanges();
                ///Crea registro de historial
                Historial hist = new Historial();
                hist.Producto = producto.IdProducto;
                hist.Fecha_Movimiento = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                hist.Tipo_Movimiento = "Transferencia entre sucursales";
                hist.Origen = producto.Almacen;
                hist.Destino = Almacen;
                hist.Cantidad = 0;
                hist.Cantidad_Anterior = producto.Existencia;
                hist.Cantidad_Actual = producto.Existencia;
                contextoDB.Historials.Add(hist);
                contextoDB.SaveChanges();
               
            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                ValueBack = "Ocurrió un error durante transferencia, verifique los datos y vuelva a intentarlo";
                return Content(ValueBack, MediaTypeNames.Text.Plain);
            }
            return Content(ValueBack);
        }

        /// <summary>
        /// Metodo que actualiza la existencia de productos
        /// </summary>
        /// <param name="Clave">Clave del producto</param>
        /// <param name="cantidad">Cantidad a actualizar</param>
        /// <returns>Resultado de la operacion</returns>
        [HttpPost]
        public ActionResult ActualizarExistencia(int IdProducto, int Cantidad)
        {
            String ValueBack = "";
            int resultado = -1;
            double CantidadAnterior = 0;
         
           
            try
            {
                var contextoDB = new InventarioGammaEntities();
                //Actualiza existencia/actualizacion
                var producto = (from prod in contextoDB.Productos
                                where prod.IdProducto == IdProducto &&
                                prod.Estatus!=0
                                select prod).FirstOrDefault();

                if (!Session["almacen"].Equals(producto.Almacen) && !Session["almacen"].Equals("Admin"))
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    ValueBack = "No pertenece al almacen, no es posible registrar producto";
                    return Content(ValueBack, MediaTypeNames.Text.Plain);
                }

                CantidadAnterior = producto.Existencia;
                producto.Existencia = producto.Existencia + Cantidad;
                resultado = contextoDB.SaveChanges();
                ///Crea registro de historial
                Historial hist = new Historial();
                hist.Producto = producto.IdProducto;
                hist.Fecha_Movimiento = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                hist.Tipo_Movimiento = "Alta";
                hist.Origen = "Distribuidor";
                hist.Destino = producto.Almacen;
                hist.Cantidad = Cantidad;
                hist.Cantidad_Anterior = CantidadAnterior;
                hist.Cantidad_Actual = producto.Existencia;
                contextoDB.Historials.Add(hist);
                contextoDB.SaveChanges();
                ValueBack = "Actualizado";
            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                ValueBack = "Ocurrió un error durante alta de inventario, verifique los datos y vuelva a intentarlo";
                return Content(ValueBack, MediaTypeNames.Text.Plain);
            }
            return Content(ValueBack);
        }

        /// <summary>
        /// Da de baja un producto del inventario
        /// </summary>
        /// <param name="Clave">Clave del producto a eliminar</param>
        /// <returns>Resultado de la operacion</returns>
        [HttpPost]
        public ActionResult Eliminar(int idProducto)
        {   String ValueBack = "";
            int resultado=-1;
         
            try
            {
                var contextoDB = new InventarioGammaEntities();
                //Elimina producto/baja
                var producto = (from prod in contextoDB.Productos
                                where prod.IdProducto == idProducto &&
                                prod.Estatus!=0
                                select prod).FirstOrDefault();


                if (!Session["almacen"].Equals(producto.Almacen) && !Session["almacen"].Equals("Admin"))
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    ValueBack = "No pertenece al almacen, no es posible registrar producto";
                    return Content(ValueBack, MediaTypeNames.Text.Plain);
                }
           

                producto.Estatus = 0;
                resultado = contextoDB.SaveChanges();
                ///Crea registro de historial
                Historial hist= new Historial();
                hist.Producto = producto.IdProducto;
                hist.Fecha_Movimiento = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                hist.Tipo_Movimiento = "Eliminacion";
                hist.Origen = producto.Almacen;
                hist.Destino = producto.Almacen;
                hist.Cantidad = 0;
                hist.Cantidad_Anterior = 0;
                hist.Cantidad_Actual = 0;
                contextoDB.Historials.Add(hist);
                contextoDB.SaveChanges();
                ValueBack = "Registro eliminado!";
            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                ValueBack = "Ocurrió un error durante eliminación, verifique los datos y vuelva a intentarlo";
                return Json(new { mensaje = ValueBack }, JsonRequestBehavior.AllowGet);
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return Json(new { mensaje = ValueBack }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo para realizar ventas
        /// </summary>
        /// <param name="Venta"> Lista de productos a vender</param>
        /// <returns>Mensaje de estado de operación</returns>
        [HttpPost]
        public ActionResult Venta(int IdProducto, int Cantidad)
        {

            String ValueBack = "";
           
           
            int resultado = -1;
            try
            {
                var contextoDB = new InventarioGammaEntities();
                Producto PodTrans = new Producto();
                ///Realiza actualizacion/transferencia
                var producto = (from prod in contextoDB.Productos
                                where prod.IdProducto == IdProducto &&
                                (prod.Estatus == 1 || prod.Estatus == 2)
                                select prod).FirstOrDefault();

                if (!Session["almacen"].Equals(producto.Almacen) && !Session["almacen"].Equals("Admin"))
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    ValueBack = "No pertenece al almacen, no es posible registrar producto";
                    return Content(ValueBack, MediaTypeNames.Text.Plain);
                }

                if (!(producto.Existencia >= Cantidad))
                {

                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    ValueBack = "No hay cantidad suficiente para vender";
                    return Content(ValueBack, MediaTypeNames.Text.Plain);
                }

                producto.Existencia = producto.Existencia - Cantidad;
                if (producto.Estatus == 2 && producto.Existencia == 0)
                {
                    producto.Estatus = 0;
                }
                resultado = contextoDB.SaveChanges();

                ValueBack = "Venta realizada!";
                ///Crea registro de historial
                Historial hist = new Historial();
                hist.Producto = producto.IdProducto;
                hist.Fecha_Movimiento = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                hist.Tipo_Movimiento = "Venta";
                hist.Origen = producto.Almacen;
                hist.Destino = producto.Almacen;
                hist.Cantidad = Cantidad;
                hist.Cantidad_Anterior = producto.Existencia+Cantidad;
                hist.Cantidad_Actual = producto.Existencia;
                contextoDB.Historials.Add(hist);
                contextoDB.SaveChanges();

            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                ValueBack = "Ocurrió un error durante la venta, verifique los datos y vuelva a intentarlo";
                return Content(ValueBack, MediaTypeNames.Text.Plain);
            }
            return Content(ValueBack);
        }
    }
}
