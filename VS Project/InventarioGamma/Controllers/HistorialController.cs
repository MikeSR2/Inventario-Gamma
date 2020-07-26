using InventarioGamma.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioGamma.Controllers
{
    /// <summary>
    /// Controlador para operaciones relacionadas a el historial del inventario
    /// </summary>
    public class HistorialController : Controller
    {
       /// <summary>
       /// Método que regresa vista principal de Historial
       /// </summary>
       /// <returns>Vista Historial</returns>
        public ActionResult Historial()
        {
            ViewBag.titSeccion = ConstantesTitulos.TituloHistorial;
            ViewBag.descSeccion = ConstantesTitulos.DescripcionHistorial;
            ViewBag.nivel = ConstantesTitulos.NivelHistorial;
            ViewBag.Total = 0.0;
            ViewBag.Usuario = Session["usuario"];
            ViewBag.Sucursal = Session["almacen"];
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sEcho"></param>
        /// <param name="sSearch"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="iDisplayStart"></param>
        /// <param name="iSortCol_0"></param>
        /// <param name="sSortDir_0"></param>
        /// <param name="Almacen"></param>
        /// <param name="Periodo"></param>
        /// <param name="InVentas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoadDataTable(int sEcho, String sSearch, int iDisplayLength,
            int iDisplayStart, int iSortCol_0, String sSortDir_0, String Almacen, String Periodo, String InVentas)
        {
            double totalV = 0;
            DateTime FechaIn = Convert.ToDateTime(Periodo.Split('-')[0].Trim());
            DateTime FechaFin = Convert.ToDateTime(Periodo.Split('-')[1].Trim());
            if (("true").Equals(InVentas))
            {
                sSearch = "Venta";
            }
            if (!("").Equals(Almacen))
            {
                sSearch = Almacen;
            }
            try
            {   
                var contextoDB = new InventarioGammaEntities();
                var myList=(from historial in contextoDB.Historials.AsEnumerable()
                              from productos in contextoDB.Productos.AsEnumerable()
                              where historial.Producto == productos.IdProducto &&
                              (historial.Fecha_Movimiento>FechaIn && historial.Fecha_Movimiento<FechaFin) &&
                              (productos.Nombre.IndexOf(sSearch, StringComparison.OrdinalIgnoreCase) >= 0 || 
                              productos.Descripcion.IndexOf(sSearch, StringComparison.OrdinalIgnoreCase) >=0 ||
                              historial.Tipo_Movimiento.IndexOf(sSearch, StringComparison.OrdinalIgnoreCase) >=0 || 
                              historial.Fecha_Movimiento.ToString("dd/MM/yyyy").IndexOf(sSearch, StringComparison.OrdinalIgnoreCase) >=0)
                              select new
                              {
                                  Clave = productos.Clave,
                                  Producto = productos.Nombre,
                                  Descripcion = productos.Descripcion,
                                  Fecha_Movimiento = historial.Fecha_Movimiento.ToString("dd/MM/yyyy"),
                                  Tipo_Movimiento = historial.Tipo_Movimiento,
                                  Origen = historial.Origen,
                                  Destino = historial.Destino,
                                  Cantidad = historial.Cantidad.ToString(),
                                  Cantidad_Anterior = historial.Cantidad_Anterior.ToString(),
                                  Cantidad_Actual = historial.Cantidad_Actual.ToString()

                              }).ToList();
                if (("true").Equals(InVentas))
                {
                    var venta = (from historial in contextoDB.Historials.AsEnumerable()
                                 from productos in contextoDB.Productos.AsEnumerable()
                                 where historial.Producto == productos.IdProducto &&
                                 (historial.Fecha_Movimiento > FechaIn && historial.Fecha_Movimiento < FechaFin) &&
                                 (productos.Nombre.IndexOf(sSearch, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                 productos.Descripcion.IndexOf(sSearch, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                 historial.Tipo_Movimiento.IndexOf(sSearch, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                 historial.Fecha_Movimiento.ToString("dd/MM/yyyy").IndexOf(sSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                                 select new
                                 {
                                     Precio=productos.Importe_Inventario.ToString(),
                                     Cantidad = historial.Cantidad.ToString()

                                 }).ToList();
                    foreach (var x in venta)
                    {
                        totalV = totalV + (Convert.ToDouble(x.Cantidad) * Convert.ToDouble(x.Precio));
                    }
                }
                if (sSortDir_0.Equals("asc"))
                {
                    switch (iSortCol_0)
                    {
                        case 0:
                            myList = myList.OrderBy(x => x.Clave).ToList();
                            break;
                        case 1:
                            myList = myList.OrderBy(x => x.Producto).ToList();
                            break;
                        case 2:
                            myList = myList.OrderBy(x => x.Descripcion).ToList();
                            break;
                        case 3:
                            myList = myList.OrderBy(x => x.Fecha_Movimiento).ToList();
                            break;
                        case 4:
                            myList = myList.OrderBy(x => x.Tipo_Movimiento).ToList();
                            break;

                    }
                }
                else
                {
                    switch (iSortCol_0)
                    {
                        case 0:
                            myList = myList.OrderByDescending(x => x.Clave).ToList();
                            break;
                        case 1:
                            myList = myList.OrderByDescending(x => x.Producto).ToList();
                            break;
                        case 2:
                            myList = myList.OrderByDescending(x => x.Descripcion).ToList();
                            break;
                        case 3:
                            myList = myList.OrderByDescending(x => x.Fecha_Movimiento).ToList();
                            break;
                        case 4:
                            myList = myList.OrderByDescending(x => x.Tipo_Movimiento).ToList();
                            break;

                    }
                }
                var returnData = myList.Skip(iDisplayStart).Take(iDisplayLength).ToList();
                var result = new
                {
                    sEcho = sEcho,
                    Total = totalV,
                    iTotalRecords=myList.Count,
                    iTotalDisplayRecords=myList.Count,
                    aaData = returnData 
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                return Json("");
            }
        }
        /// <summary>
        /// Método para filtrar datos de historial por fecha y almacén
        /// </summary>
        /// <param name="periodo">Periodo a filtrar</param>
        /// <param name="inputAlmacen">Almacén a filtrar</param>
        /// <returns>Objeto JSON con registros de historial filtrados</returns>
        [HttpPost]
        public ActionResult FilterDataTable(String periodo, String inputAlmacen)
        {

            var contextoDB = new InventarioGammaEntities();
            var myList = (from historial in contextoDB.Historials
                          from productos in contextoDB.Productos
                          where historial.Producto==productos.IdProducto
                          select new
                          {
                              Clave = productos.Clave,
                              Producto = productos.Nombre,
                              Descripcion = productos.Descripcion,
                              Fecha_Movimiento = historial.Fecha_Movimiento,
                              Tipo_Movimiento = historial.Tipo_Movimiento,
                              Origen = historial.Origen,
                              Destino = historial.Destino,
                              Cantidad_Anterior = historial.Cantidad_Anterior,
                              Cantidad_Actual = historial.Cantidad_Actual

                          }).ToList();
            return Json(myList, JsonRequestBehavior.AllowGet);
        }
    }
}
