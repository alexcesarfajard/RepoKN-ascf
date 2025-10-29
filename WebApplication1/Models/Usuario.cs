using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Usuario
    {
        public long ConsecutivoUsuario { get; set; }

        public string Identificacion { get; set; }

        public string Nombre { get; set; }

        public string CorreoElectronico { get; set; }

        public string Contrasenna { get; set; }

        public string NombrePerfil { get; set; }

        public string ContrasennaConfirmar { get; set; }


    }
}