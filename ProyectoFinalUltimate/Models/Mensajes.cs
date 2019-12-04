using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinalUltimate.Models
{
    public class Mensajes
    {
        public int Id { get; set; }
        [Display(Name = "Usuario Emisor:")]
        public string Usuario_Envio { get; set; }
        [Required]
        [Display(Name = "Contactos Receptores:")]
        public string Contactos_Envio { get; set; }
        [Required]
        public string Asunto { get; set; }
        [Required]
        public string Mensaje { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Envio:")]
        public DateTime Fecha_Envio { get; set; }
        [Required]
        [Display(Name = "Estatus Del Mensaje:")]
        public string Estatus { get; set; }

        public string Mensaje_Error { get; set; }

        public int Fk_User { get; set; }

        public int FK_Contact { get; set; }

        public User User { get; set; }

        public Contact Contact { get; set; }

    }
}