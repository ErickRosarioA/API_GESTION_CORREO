using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinalUltimate.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        [Display(Name = "Tipo de Usuario:")]
        public TipoUser TipoUser { get; set; }
        [Display(Name = "Fecha de Nacimiento:")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [Display(Name = "Correo Electronico:")]
        public string CorreoElectronico { get; set; }
        [Required]
        [Display(Name = "Tipo de Porveedor:")]
        public virtual TipoProveedor TipoProveedor { get; set; }
        [Required]
        [Display(Name = "Contraseña del Correo:")]
        public string ContraCorreo { get; set; }
        [Required]
        [Display(Name = "Contraseña de Inicio de Sesion:")]
        public string ContraApp { get; set; }

    }

    public enum TipoProveedor {
    Gmail,
    Outlook,
    Hotmail,
    Yahoo

    }
    public enum TipoUser
    {
        ADMIN,
        SENDER
    }


}