# Inventario-Gamma
## Acerca de ésta aplicación
Aplicación sencilla hecha en .NET ASP MVC para la gestión de inventarios de un pequeño negocio.
## Sobre la funcionalidad
Esta es una aplicación sencilla para administrar el inventario de un pequeño negocio con tres almacenes.
El inventario puede darse de alta con un usuario asociado a cualquier almacén y un usuario no puede estar asociado a más de una almacén. 
El usuario puede dar de alta mercancía en el inventario, puede transferir esa mercancía a otra almacén o bien modificar la existencia del producto en base a la venta. 
El usuario también puede consultar el historial de movimientos del inventario en base a ciertos parámetros como fecha, almacén, venta nombre del producto, etc.
Sólo el usuario administrador o admin puede gestionar los usuarios. (Agregar usuarios, eliminar o resetear contraseña)
## Sobre la implementación del código y tecnologías utilizadas
Las tecnologías utilizadas para el desarrollo de ésta aplicación son: ASP .NET MVC, Entity Framework, Razor, jQuery, Bootstrap y algunas otras librerias de javascript como sweetalert.js, waitMe.js y jquery.dataTables para mostrar información. La base de datos se encuentra en SSQL Server.
La implementación es bastante sencilla y aún carece de ciertas características de seguridad (que debido a el requerimiento y la información manejada no se ha implementado aún)
## Contenido del proyecto
- ScriptsDB: Contiene algunos scripts que se utilizaron en la creación de la base de datos.
- VS Project: Es la solución de Visual Studio
