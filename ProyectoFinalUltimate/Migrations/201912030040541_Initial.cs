namespace ProyectoFinalUltimate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        CorreoElectronico = c.String(nullable: false),
                        Sexo = c.Int(nullable: false),
                        Usuario = c.String(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        TipoUser = c.Int(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        CorreoElectronico = c.String(nullable: false),
                        TipoProveedor = c.Int(nullable: false),
                        ContraCorreo = c.String(nullable: false),
                        ContraApp = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mensajes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usuario_Envio = c.String(),
                        Contactos_Envio = c.String(nullable: false),
                        Asunto = c.String(nullable: false),
                        Mensaje = c.String(nullable: false),
                        Fecha_Envio = c.DateTime(nullable: false),
                        Estatus = c.String(nullable: false),
                        Mensaje_Error = c.String(),
                        Fk_User = c.Int(nullable: false),
                        FK_Contact = c.Int(nullable: false),
                        Contact_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Contact_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensajes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Mensajes", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "User_Id", "dbo.Users");
            DropIndex("dbo.Mensajes", new[] { "User_Id" });
            DropIndex("dbo.Mensajes", new[] { "Contact_Id" });
            DropIndex("dbo.Contacts", new[] { "User_Id" });
            DropTable("dbo.Mensajes");
            DropTable("dbo.Users");
            DropTable("dbo.Contacts");
        }
    }
}
