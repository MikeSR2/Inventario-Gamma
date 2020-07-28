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

        /// <summary>
        /// da de alta un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// da de baja un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelUser(String usuario)
        {
            var contexDBUser = new InventarioGammaEntities();
            String valueBack = "";
            
            try
            {
                var user = new Usuario { NombreUsuario = usuario };
                contexDBUser.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                contexDBUser.SaveChanges();
                valueBack = "200";
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            }
            catch (InvalidOperationException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                valueBack = "Ocurrió un error durante registro, verifique los datos";
            }

            return Content(valueBack);
        }

        /// <summary>
        /// reset de la password de un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetUser(String usuario)
        {
            var contexDBUser = new InventarioGammaEntities();
            var newPass = Convert.ToBase64String(Encoding.UTF8.GetBytes(usuario));
            String valueBack = "";

            try
            {
                var thisUser = (from user in contexDBUser.Usuarios
                                where
                                    user.NombreUsuario==usuario
                                select user).FirstOrDefault();

                
                thisUser.Llave = newPass;
                contexDBUser.SaveChanges();
                valueBack = "200";
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

            }
            catch (InvalidOperationException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                valueBack = "Ocurrió un error durante registro, verifique los datos";
            }

            return Content(valueBack);
        }

        /// <summary>
        /// Método que carga los usuarios 
        /// </summary>
        /// <returns>Objeto JSON con lista de productos</returns>
        [HttpPost]
        public ActionResult ListaUsuarios()
        {
            try
            {
                var contextoDB = new InventarioGammaEntities();
                var myList = (from usuarios in contextoDB.Usuarios
                              select new
                              {
                                  Usuario = usuarios.NombreUsuario,
                                  Almacen = usuarios.Almacen,
                              }).ToList();
                return Json(myList, JsonRequestBehavior.AllowGet);


            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                return Json("");
            }
        }


    }
}
