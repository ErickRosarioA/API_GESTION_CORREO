using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinalUltimate.Models
{
    public class Contact
    {

        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento:")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [Display(Name = "Correo Electronico:")]
        public string CorreoElectronico { get; set; }
        [Required]
        public virtual Sexo Sexo{ get; set; }
        [Required]
        public string Usuario { get; set; }

        public int UsuarioId { get; set; }

        public User User { get; set; }
    }


    public enum Sexo { 
        Masculino,
        Femenino
    }

}