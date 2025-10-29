using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Services;


namespace WebApplication1.Controllers
{
    [Seguridad]

    public class ProductosController : Controller
    {
        [HttpGet]
        public ActionResult VerProductos()
        {
            using (var context = new BD_KNEntities())
            {
                //Tomar el objeto de la Base de datos
                var resultado = context.tbProducto.Include("tbCategoria").ToList();

                //Convertirlo en un objeto propio
                var datos = resultado.Select(p => new Producto
                {
                    ConsecutivoProducto = p.ConsecutivoProducto,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    NombreCategoria = p.tbCategoria.Nombre,
                    Estado = p.Estado,
                    Imagen = p.Imagen
                }).ToList();


                return View(datos);
            }
        }
    }
}