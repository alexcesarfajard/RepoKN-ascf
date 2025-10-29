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

    public class UsuarioController : Controller
    {

        [HttpGet]
        public ActionResult VerPerfil()
        {
            using (var context = new BD_KNEntities())
            {
                var consecutivo = int.Parse(Session["ConsecutivoUsuario"].ToString());

                //Tomar objeto de la BD
                var resultado = context.T_Usuarios.Include("T_Perfiles")
                    .Where(x => x.ConsecutivoUsuario == consecutivo).ToList();

                //Convertir en objeto propio
                var datos = resultado.Select(p => new Usuario
                {
                    Identificacion = p.Identificacion,
                    Nombre = p.Nombre,
                    CorreoElectronico = p.CorreoElectronico,
                    NombrePerfil = p.T_Perfiles.Nombre
                }).FirstOrDefault();

                return View(datos);
            }

        }


        [HttpPost]
        public ActionResult VerPerfil(Usuario usuario)
        {
            ViewBag.Mensaje = "Error al actualizar la información";

            using (var context = new BD_KNEntities())
            {
                //Obtener el ID del usuario que está loggeado
                var consecutivo = int.Parse(Session["ConsecutivoUsuario"].ToString());

                //Verificamos que exista en la BD
                var resultadoConsulta = context.T_Usuarios
                    .Where(x => x.ConsecutivoUsuario == consecutivo).FirstOrDefault();

                //Si existe, lo actualizamos
                if (resultadoConsulta != null)
                {
                    resultadoConsulta.Identificacion = usuario.Identificacion;
                    resultadoConsulta.Nombre = usuario.Nombre;
                    resultadoConsulta.CorreoElectronico = usuario.CorreoElectronico;

                    var resultadoActualizacion = context.SaveChanges();

                    if (resultadoActualizacion > 0)
                    {

                        ViewBag.Mensaje = "Información actualizada correctamente";
                        Session["NombreUsuario"] = usuario.Nombre;
                    }

                }
                return View();
            }
        }


        [HttpGet]
        public ActionResult CambiarAcceso()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CambiarAcceso(Usuario usuario)
        {
            return View();
        }



    }
}