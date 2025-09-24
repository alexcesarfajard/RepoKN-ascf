using System.Web.Mvc;
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

        [HttpGet]
        public ActionResult Principal()
        {
            return View();
        }

    }
}