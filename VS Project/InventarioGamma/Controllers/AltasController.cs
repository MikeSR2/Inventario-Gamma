using InventarioGamma.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace InventarioGamma.Controllers
{
    /// <summary>
    /// Controlador que realiza las altas de productos
    /// </summary>
    public class AltasController : Controller
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Método que regresa la vista principal de altas
        /// </summary>
        /// <returns>Vista Altas</returns>
        public ActionResult Altas()
        {
            // Establece títulos para la vista
            ViewBag.titSeccion = ConstantesTitulos.TituloAltas;
            ViewBag.descSeccion = ConstantesTitulos.DescripcionAltas;
            ViewBag.nivel = ConstantesTitulos.NivelAltas;
            ViewBag.Usuario = Session["usuario"];
            ViewBag.Sucursal = Session["almacen"];
            return View();
        }

        /// <summary>
        /// Método que crea nuevo producto
        /// </summary>
        /// <param name="producto">Producto a registrar</param>
        /// <returns>Estado de la operación</returns>
        [HttpPost]
        public ActionResult nuevo(Producto producto)
        {
            var contextoDB = new InventarioGammaEntities();
            String valueBack = "";
            Producto prod = producto;
            prod.Estatus = 1;
            prod.Existencia = 0;
            Historial hist = new Historial();
            if (!Session["almacen"].Equals(producto.Almacen) && !Session["almacen"].Equals("Admin"))
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                valueBack = "No pertenece al almacen, no es posible registrar producto";
                return Content(valueBack, MediaTypeNames.Text.Plain);
            }
            try
            {
                //alta el producto
                contextoDB.Productos.Add(prod);
                contextoDB.SaveChanges();
                //genera historial
                hist.Producto = prod.IdProducto;
                hist.Fecha_Movimiento = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                hist.Tipo_Movimiento = "Registro";
                hist.Origen = "Distribuidor";
                hist.Destino = prod.Almacen;
                hist.Cantidad = 0;
                hist.Cantidad_Anterior = 0;
                hist.Cantidad_Actual = 0;
                contextoDB.Historials.Add(hist);
                contextoDB.SaveChanges();
                valueBack="Alta exitosa!";
            }
            catch(System.Data.EntityException ex)
            {
                logger.Error("Error:" + ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                valueBack = "Ocurrió un error durante registro, verifique los datos y vuelva a intentarlo";
                return Content(valueBack, MediaTypeNames.Text.Plain);
            }

            return Content(valueBack, MediaTypeNames.Text.Plain);
        }

    }
}
