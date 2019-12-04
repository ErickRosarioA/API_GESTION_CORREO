using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinalUltimate.Models
{
    public class ProyectoFinalUltimateContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ProyectoFinalUltimateContext() : base("name=ProyectoFinalUltimateContext")
        {
        }

        public System.Data.Entity.DbSet<ProyectoFinalUltimate.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinalUltimate.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinalUltimate.Models.Mensajes> Mensajes { get; set; }
    }
}
