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

            return RedirectToAction("Principal", "Home");
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

                context.CrearUsuarios(usuario.Identificacion, usuario.Nombre, usuario.CorreoElectronico, usuario.Contrasenna);

            }


            return View();
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