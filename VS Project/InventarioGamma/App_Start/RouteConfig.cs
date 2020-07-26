using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InventarioGamma
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Inicio",
                url: "",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Iniciar",
                url: "inicio-sesion",
                defaults: new { controller = "Login", action = "IniciaSesion", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Consultas",
                url: "consultas",
                defaults: new { controller = "Consultas", action = "Consultas", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Historial",
              url: "historial",
              defaults: new { controller = "Historial", action = "Historial", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Altas",
                url: "altas",
                defaults: new { controller = "Altas", action = "Altas", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Ayuda",
               url: "ayuda",
               defaults: new { controller = "Ayuda", action = "Ayuda", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Admin",
              url: "admin",
              defaults: new { controller = "Admin", action = "Admin", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Nuevo",
              url: "altas/nuevo",
              defaults: new { controller = "Altas", action = "nuevo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "LoadData",
              url: "consultas/LoadDataTable",
              defaults: new { controller = "Consultas", action = "LoadDataTable", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "LoadDataHist",
              url: "historial/LoadDataTable",
              defaults: new { controller = "Historial", action = "LoadDataTable", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "AltUser",
              url: "Admin/AltUser",
              defaults: new { controller = "Admin", action = "AltUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Eliminar",
              url: "Actualizaciones/Eliminar",
              defaults: new { controller = "Actualizaciones", action = "Eliminar", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Modificar",
              url: "Consultas/modificar",
              defaults: new { controller = "Consultas", action = "Modificar", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Actualizar",
              url: "actualizaciones/actualizar",
              defaults: new { controller = "Actualizaciones", action = "ActualizarDatos", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "AltaInventario",
              url: "actualizaciones/alta-inventario",
              defaults: new { controller = "Actualizaciones", action = "ActualizarExistencia", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "TransferenciaAlmacen",
              url: "actualizaciones/transferencia",
              defaults: new { controller = "Actualizaciones", action = "TransferenciaAlmacen", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Venta",
              url: "actualizaciones/venta",
              defaults: new { controller = "Actualizaciones", action = "Venta", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Salir",
              url: "cerrar-sesion",
              defaults: new { controller = "Login", action = "CerrarSesion", id = UrlParameter.Optional }
            );


        }
    }
}