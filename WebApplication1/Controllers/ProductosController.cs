using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.EF;

namespace WebApplication1.Controllers
{
    public class ProductosController : Controller
    {
        [HttpGet]
        public ActionResult VerProductos()
        {
            using (var context = new BD_KNEntities())
            {
                var resultado = context.tbProducto.Include("tbCategoria").ToList();

                return View(resultado);
            }
        }
    }
}