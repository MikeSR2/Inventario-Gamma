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
    /// Controlador para la administrracion de usuarios
    /// </summary>
    public class AdminController : Controller
    {
       
        /// <summary>
        /// Metodo que regresa la vista principal de adminitracion
        /// </summary>
        /// <returns>Vista Admin</returns>
        public ActionResult Admin()
        {
            // Establece titulos para la vista
            ViewBag.titSeccion = ConstantesTitulos.TituloAdmin;
            ViewBag.descSeccion = ConstantesTitulos.DescripcionAdmin;
            ViewBag.nivel = ConstantesTitulos.NivelAdmin;
            ViewBag.Usuario = Session["usuario"];
            ViewBag.Sucursal = Session["almacen"];
            return View();
        }

        [HttpPost]
        public ActionResult AltUser(Usuario user)
        {
            var contexDBUser = new InventarioGammaEntities();
            String valueBack = "";
            Usuario users = user;
            users.Llave = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Llave));
            try
            {
                contexDBUser.Usuarios.Add(users);
                contexDBUser.SaveChanges();
                valueBack = "Alta exitosa";
            }
            catch (InvalidOperationException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                valueBack = "Ocurrió un error durante registro, verifique los datos";
            }

            return Content(valueBack);
        }


    }
}
