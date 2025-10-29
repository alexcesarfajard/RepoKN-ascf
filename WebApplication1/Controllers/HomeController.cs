using KN_ProyectoWeb.Services;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Web.UI;


namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {

        Utilitarios utilitarios = new Utilitarios();

        #region Iniciar Sesion

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {

            /*Progra para validar si usuario.CorreoElectronico y usuario.Contrasenna son válidas */

            using (var context = new BD_KNEntities())
            {

                //var resultado = context.T_Usuarios.Where
                //   (x => x.CorreoElectronico == usuario.CorreoElectronico && x.Contrasenna == usuario.Contrasenna 
                //   && x.Estado == true ).FirstOrDefault();

                var resultado = context.ValidarUsuarios(usuario.CorreoElectronico, usuario.Contrasenna).FirstOrDefault();

                if (resultado != null)
                {
                    Session["ConsecutivoUsuario"] = resultado.ConsecutivoUsuario;
                    Session["NombreUsuario"] = resultado.Nombre;
                    Session["PerfilUsuario"] = resultado.ConsecutivoPerfil;
                    return RedirectToAction("Principal", "Home");
                }

                ViewBag.Mensaje = "La informacion no se ha podido autenticar";
                return View();

            }
        }

        #endregion

        #region Registro de usuario

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            /*Progra para guardar un usuario en la BD */
            using (var context = new BD_KNEntities())
            {
                //var nuevoUsuario = new T_Usuarios()
                //{
                //    Identificacion = usuario.Identificacion,
                //    Nombre = usuario.Nombre,
                //    CorreoElectronico = usuario.CorreoElectronico,
                //    Contrasenna = usuario.Contrasenna,
                //    ConsecutivoPerfil = 2,
                //    Estado = true

                //};

                //context.T_Usuarios.Add(nuevoUsuario); 
                //context.SaveChanges(); // guardar nuevo usuario en la BD.T_Usuarios. Básicamente un Insert

                var resultado = context.CrearUsuarios
                    (usuario.Identificacion, usuario.Nombre, usuario.CorreoElectronico, usuario.Contrasenna);

                // -1 no se cumple sentencia
                // 0 es que intentó pero no hizo nada
                // > 0 la cantidad de filas que se insertaron


                if (resultado > 0) {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Mensaje = "La informacion no se ha podido registrar";
                return View();

            }
        }

        #endregion

        #region Recuperar Acceso 

        [HttpGet]
        public ActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarAcceso(Usuario usuario) 
        {

            /*Validar si el usuario existe*/
            /*Generar una contraseña temporal*/
            /*Enviar esa contraseña temporal para iniciar sesion*/

            using (var context = new BD_KNEntities())
            {
                //Se valida si el usuario ya existe
                var resultadoConsulta = context.T_Usuarios.Where
                    (x => x.CorreoElectronico == usuario.CorreoElectronico).FirstOrDefault();

                //Si existe se manda a recupear el acceso
                if (resultadoConsulta != null)
                {
                    var contrasennaGenerada = utilitarios.GenerarContrasenna();

                    // actualizar contraseña

                    resultadoConsulta.Contrasenna = contrasennaGenerada;

                    var resultadoActualizacion = context.SaveChanges(); // guardando nueva contraseña

                    //Informar cual es la nueva contraseña
                    if (resultadoActualizacion > 0)
                    {

                        //StringBuilder mensaje = new StringBuilder();
                        //mensaje.Append("Estimado(a) " + resultadoConsulta.Nombre + "<br>");
                        //mensaje.Append("Se ha generado una solicitud de recuperación de acceso a su nombre" + "<br><br>");
                        //mensaje.Append("Su nueva contraseña de acceso es: <b>" + contrasennaGenerada + "</b><br><br>");
                        //mensaje.Append("Procure realizar el cambio de su contraseña una vez ingrese al sistema" + "<br><br>");
                        //mensaje.Append("Muchas gracias");

                        string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                        string path = Path.Combine(projectRoot, "TemplateRecuperacion.html");

                        // Leer todo el HTML
                        string htmlTemplate = System.IO.File.ReadAllText(path);

                        // Reemplazar placeholders
                        string mensaje = htmlTemplate
                            .Replace("{{Nombre}}", resultadoConsulta.Nombre)
                            .Replace("{{Contrasena}}", contrasennaGenerada);

                        utilitarios.EnviarCorreo("Contraseña de acceso", mensaje, resultadoConsulta.CorreoElectronico);
                        return RedirectToAction("Index", "Home");


                    }


                }

                ViewBag.Mensaje = "La información no se ha podido reestablecer";
                return View();
            }

        }

        #endregion

        #region principal

        [Seguridad]
        [HttpGet]
        public ActionResult Principal()
        {
            return View();
        }

        #endregion principal

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}