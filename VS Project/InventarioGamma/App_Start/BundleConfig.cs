using System.Web.Optimization;

namespace InventarioGamma.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bscosmos").Include(
                "~/Content/bootstrap/bootstrap-cosmos.css"));

            bundles.Add(new StyleBundle("~/Content/login-css").Include(
              "~/Content/login/login.css"));

            bundles.Add(new StyleBundle("~/Content/AdminLTE").Include(
              "~/Content/app/AdminLTE.css"));

            bundles.Add(new StyleBundle("~/Content/skin-blue").Include(
              "~/Content/app/skin-blue.css"));

            bundles.Add(new StyleBundle("~/Content/waitMe").Include(
              "~/Content/app/waitMe.css"));

            bundles.Add(new StyleBundle("~/Content/sweetalert").Include(
              "~/Content/app/sweetalert.css"));

            bundles.Add(new ScriptBundle("~/Scripts/app-js").Include(
             "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/Scripts/reloj").Include(
            "~/Scripts/app/hora.js"));

            bundles.Add(new ScriptBundle("~/Scripts/login-js").Include(
            "~/Scripts/login/login.js"));

            bundles.Add(new ScriptBundle("~/Scripts/consultas-js").Include(
            "~/Scripts/consultas/consultas.js"));

            bundles.Add(new ScriptBundle("~/Scripts/historial-js").Include(
            "~/Scripts/historial/historial.js"));

            bundles.Add(new ScriptBundle("~/Scripts/altas-js").Include(
               "~/Scripts/altas/altas.js"));

            bundles.Add(new ScriptBundle("~/Scripts/waitMe").Include(
               "~/Scripts/app/waitMe.js"));

            bundles.Add(new ScriptBundle("~/Scripts/AltaUsuarios").Include(
               "~/Scripts/Usuarios/AltaUsuarios.js"));

            bundles.Add(new ScriptBundle("~/Scripts/actualizaciones-js").Include(
               "~/Scripts/actualizaciones/actualizaciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/sweetalert").Include(
               "~/Scripts/app/sweetalert.min.js"));

        }
    }
}