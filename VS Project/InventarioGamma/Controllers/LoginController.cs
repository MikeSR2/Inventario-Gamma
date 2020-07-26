using InventarioGamma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace InventarioGamma.Controllers
{
    /// <summary>
    /// Controlador para el inicio de sesión a la aplicación
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Método que regresa vista de Login
        /// </summary>
        /// <returns>Vista Login</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Método para iniciar sesión
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="LLave"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IniciaSesion(String Usuario, String LLave)
        {
            String ValueBack = "";
            var pass = Convert.ToBase64String(Encoding.UTF8.GetBytes(LLave));
            try
            {
                var contextoDB = new InventarioGammaEntities();
                var usuario = (from usuarios in contextoDB.Usuarios
                               where
                                   Usuario == usuarios.NombreUsuario && pass == usuarios.Llave
                               select new
                               {
                                   NombreUsuario = usuarios.NombreUsuario,
                                   Almacen = usuarios.Almacen


                               }).FirstOrDefault();
                if (usuario != null)
                {
                    Session["usuario"] = usuario.NombreUsuario;
                    Session["almacen"] = usuario.Almacen;
                   
                    return Redirect("consultas");
                    
                }
                else
                {

                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    ValueBack = "Datos de acceso incorrectos";
                    return Content(ValueBack, System.Net.Mime.MediaTypeNames.Text.Plain);
                }
            }
            catch (System.Data.EntityException ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                ValueBack = "Error interno, espere un momento y vuelva a intentar o contacte a su administrador del sistema";
                return Content(ValueBack, System.Net.Mime.MediaTypeNames.Text.Plain);
            }
        }
        /// <summary>
        /// Método para cerrar sesión
        /// </summary>
        /// <returns></returns>
        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            Session["almacen"] = null;
            return Redirect("login");
        }

    }
}
