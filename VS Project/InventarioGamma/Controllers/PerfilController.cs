using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventarioGamma.Models;
using System.Web.Mvc;
using System.Text;

namespace InventarioGamma.Controllers
{
    public class PerfilController : Controller
    {
        //
        // GET: /Perfil/

        [HttpPost]
        public ActionResult Actualizar(String Password,String newPassword)
        {
            var usuario = Session["usuario"];
            String ValueBack = "";


            try
            {
                var oldPass = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
                var newPass = Convert.ToBase64String(Encoding.UTF8.GetBytes(newPassword));
                var contextoDB = new InventarioGammaEntities();
                var thisUser = (from usuarios in contextoDB.Usuarios
                                where
                                    usuario == usuarios.NombreUsuario && oldPass == usuarios.Llave
                                select usuarios).FirstOrDefault();

                if (thisUser != null)
                {
                    thisUser.Llave = newPass;
                    contextoDB.SaveChanges();
                    ValueBack = "Contraese&ntilde;a actualizada!";
                }
                else
                {

                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    ValueBack = "La contraseña actual es incorrecta, verifique y vuelva a intentarlo";
                    return Content(ValueBack, System.Net.Mime.MediaTypeNames.Text.Plain);

                }

            }
            catch (System.Data.EntityException ex)
            {
                Console.Write(ex.InnerException);
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                ValueBack = "Ocurrió un error durante la operación, verifique los datos y vuelva a intentarlo";
                return Json(new { mensaje = ValueBack }, JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return Json(new { mensaje = ValueBack }, JsonRequestBehavior.AllowGet);
        }

    }
}
