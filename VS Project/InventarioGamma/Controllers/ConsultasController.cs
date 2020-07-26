using InventarioGamma.Filters;
using InventarioGamma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioGamma.Controllers
{
    /// <summary>
    /// Controlador para realizar consultas de productos
    /// </summary>
    [AuthorizationFilter]
    public class ConsultasController : Controller
    {
        /// <summary>
        /// Método que regresa la vista principal de consultas
        /// </summary>
        /// <returns>Vista Consultas</returns>
        public ActionResult Consultas()
        {
            // Establece titulos para la vista
            ViewBag.titSeccion = ConstantesTitulos.TituloConsultas;
            ViewBag.descSeccion = ConstantesTitulos.DescripcionConsultas;
            ViewBag.nivel = ConstantesTitulos.NivelConsultas;
            ViewBag.Usuario = Session["usuario"];
            ViewBag.Sucursal = Session["almacen"];
            return View();
        }

        /// <summary>
        /// Método que carga los productos 
        /// </summary>
        /// <returns>Objeto JSON con lista de productos</returns>
        [HttpPost]
        public ActionResult LoadDataTable()
        {
            try
            {
                var contextoDB = new InventarioGammaEntities();
                 var myList = (from productos in contextoDB.Productos
                                  where productos.Estatus == 1 || productos.Estatus ==2
                                  select new
                                  {
                                      DT_RowId = productos.IdProducto,
                                      Clave = productos.Clave,
                                      Nombre = productos.Nombre,
                                      Marca = productos.Marca,
                                      Descripcion = productos.Descripcion,
                                      Presentacion = productos.Presentacion,
                                      Precio_Costo = productos.Precio_Costo,
                                      Importe_Inventario = productos.Importe_Inventario,
                                      Almacen = productos.Almacen,
                                      Ubicacion = productos.Ubicacion,
                                      Existencia = productos.Existencia
                                  }).ToList();
                    return Json(myList, JsonRequestBehavior.AllowGet);
                
                
            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                return Json("");
            }
        }

        public ActionResult Modificar()
        {
            return View();
        }
    }
}
