using InventarioGamma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioGamma.Controllers
{
    /// <summary>
    /// Controlador para ayuda del sistema
    /// </summary>
    public class AyudaController : Controller
    {
        /// <summary>
        /// Método que regresa vista principal de ayuda
        /// </summary>
        /// <returns>Vista Ayuda</returns>
        public ActionResult Ayuda()
        {
            // Establece titulos para la vista
            ViewBag.titSeccion = ConstantesTitulos.TituloAyuda;
            ViewBag.descSeccion = ConstantesTitulos.DescripcionAyuda;
            ViewBag.nivel = ConstantesTitulos.NivelAyuda;
            ViewBag.Usuario = Session["usuario"];
            ViewBag.Sucursal = Session["almacen"];
            return View();
        }

    }
}
