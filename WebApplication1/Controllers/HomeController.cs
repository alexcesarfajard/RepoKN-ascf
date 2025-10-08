using System.Linq;
using System.Web.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
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

            return View();
        }

        #endregion

        #region principal

        [HttpGet]
        public ActionResult Principal()
        {
            return View();
        }



        #endregion principal

    }
}