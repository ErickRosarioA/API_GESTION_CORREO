using ProyectoFinalUltimate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinalUltimate.Controllers
{
    public class HomeController : Controller
    {
        private ProyectoFinalUltimateContext userTable = new ProyectoFinalUltimateContext();
        private string proveedor { get; set; }
        public ActionResult Inicio()
        {

            try
            {
                User login = (User)Session["UsuarioLogin"];

                ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            } catch (Exception e) {

                return RedirectToAction("Index");

            }
            return View();  
        }



        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {


            foreach (var item in userTable.Users.ToList())
            {
                ViewBag.Login = "";
                if (user.CorreoElectronico == item.CorreoElectronico && user.ContraApp == item.ContraApp)
                {
                    Session["UsuarioLogin"] = item;
                   
                    return RedirectToAction("Inicio");
                }

            }

            ViewBag.Login = "USUARIO INCORRECTO";
            return View();
        }




        [HttpGet]
        public ActionResult UserList()
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();

            IEnumerable<User> users = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/");
       
                var responseTask = client.GetAsync("Users");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<User>>();
                    readTask.Wait();

                    users = readTask.Result;
                }
                else 
                {
                    users = Enumerable.Empty<User>();

                    ModelState.AddModelError(string.Empty, "Servidor esta dando error para obtener la lista de estos usuarios");
                }
            }
            return View(users);
        }

        public ActionResult CrearUser()
        {
            User login = (User)Session["UsuarioLogin"];
            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            return View();
        }


        [HttpPost]
        public ActionResult CrearUser(User user)
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/Users");

    
                var postTask = client.PostAsJsonAsync<User>("Users", user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                   
                    try {
                        string EmailOrigen = "erickrosarioalcantara116@gmail.com";
                        string EmailDestino = user.CorreoElectronico.ToString();
                        string pass = "erickelcano";
                        MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Se ha registrado un usuario en la aplicación EnvioFlhas ", "Saludos &nbsp;  " + user.Nombre.ToUpper()+ "&nbsp;  " + user.Apellido.ToUpper()+" <br/><br/>"+ " Se le informa que ha sido creado un usuario con la siguientes:&nbsp;<br/><b>Nombre:</b>" + user.Nombre+ "<br/><b>Apellido:&nbsp;</b>" + user.Apellido+ "<br/><br/><b>Credenciales de la App EnviosFlahs</b><br/> <b>Usuario:&nbsp;     </b>" + user.CorreoElectronico+ " <br/> <b>Contraseña:&nbsp;    </b>" + user.ContraApp+"");
                        oMailMessage.IsBodyHtml = true;

                        if (user.TipoProveedor==TipoProveedor.Outlook )
                        {
                            proveedor = "mail.outlook";
                        }else if (user.TipoProveedor == TipoProveedor.Gmail)
                        {
                            proveedor = "gmail";
                        }else if(user.TipoProveedor == TipoProveedor.Yahoo)
                        {
                            proveedor = "mail.yahoo";
                        }else if (user.TipoProveedor == TipoProveedor.Hotmail)
                        {
                            proveedor = "live";

                        }
                        SmtpClient oSmtpCliente = new SmtpClient("smtp."+proveedor+".com");

                        oSmtpCliente.EnableSsl = true;
                        oSmtpCliente.UseDefaultCredentials = false;
                        oSmtpCliente.Port = 587;
                        oSmtpCliente.Credentials = new System.Net.NetworkCredential(EmailOrigen, pass);
                        oSmtpCliente.Send(oMailMessage);
                        oSmtpCliente.Dispose();

                        Response.Write("<script>alert('El mensaje fue enviado con exito')</script>");
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notificacion", "alert('Se ha enviado el mensaje correctamente')", true);
                        ViewBag.correoValidation = "El correo llego exitoso";
                    }
                    catch (Exception e) {
                            ModelState.AddModelError(string.Empty, "El servidor est en Error"+e.ToString());
                    }
                    

                    return RedirectToAction("UserList");
                }
            }

            ModelState.AddModelError(string.Empty, "El servidor est en Error");
            return View(user);
        }



        public ActionResult EditarUser(int id)
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            User user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Users?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();

                    user = readTask.Result;
                }
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditarUser(User user)
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/Users/");

          
                var putTask = client.PutAsJsonAsync<User>("Users", user);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("UserList");
                }
            }
            return View(user);
        }


        public ActionResult BorrarUser(int id)
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/");
                var deleteTask = client.DeleteAsync("Users/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("UserList");
                }
            }

            return RedirectToAction("UserList");
        }


        [HttpGet]
        public ActionResult ContactoList()
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            IEnumerable<Contact> contact = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/");

                var responseTask = client.GetAsync("Contacts");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Contact>>();
                    readTask.Wait();

                    contact = readTask.Result;
                }
                else
                {
                    contact = Enumerable.Empty<Contact>();

                    ModelState.AddModelError(string.Empty, "Servidor esta dando error para obtener la lista de estos usuarios");
                }
            }
            return View(contact);

        }


        public ActionResult CrearContacto()
        {
            User login = (User)Session["UsuarioLogin"];
            ViewBag.tablaUser = userTable.Users;
            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            return View();
        }

        [HttpPost]
        public ActionResult CrearContacto(Contact contact)
        {
            User login = (User)Session["UsuarioLogin"];
            ViewBag.tablaUser = userTable.Users;
            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/Contacts");


                var postTask = client.PostAsJsonAsync<Contact>("Contacts", contact);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    try
                    {

                        Response.Write("<script>alert('El mensaje fue enviado con exito')</script>");
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notificacion", "alert('Se ha enviado el mensaje correctamente')", true);
                        ViewBag.correoValidation = "El correo llego exitoso";
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError(string.Empty, "El servidor est en Error" + e.ToString());
                    }


                    return RedirectToAction("ContactoList");
                }
            }

            ModelState.AddModelError(string.Empty, "El servidor est en Error");
            return View(contact);
        }


        public ActionResult EditarContacto(int id)
        {
            User login = (User)Session["UsuarioLogin"];
            ViewBag.tablaUser = userTable.Users;
            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            ViewBag.NombreUser = login.Nombre.ToString();
            ViewBag.IdUser = login.Id.ToString();
            Contact contact = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Contacts?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Contact>();
                    readTask.Wait();

                    contact = readTask.Result;
                }
            }
            return View(contact);


        }

        [HttpPost]
        public ActionResult EditarContacto(Contact contact)
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/Contacts/");


                var putTask = client.PutAsJsonAsync<Contact>("Contacts", contact);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("ContactoList");
                }
            }
            return View(contact);
        }



        public ActionResult BorrarContacto(int id)
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/");
                var deleteTask = client.DeleteAsync("Contacts/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("ContactoList");
                }
            }

            return RedirectToAction("ContactoList");

        }


        [HttpGet]
        public ActionResult MensajesList()
        {
            User login = (User)Session["UsuarioLogin"];

            ViewBag.loginData = login.TipoUser.ToString().ToUpper();

            IEnumerable<Mensajes> mensajes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/api/");

                var responseTask = client.GetAsync("Mensajes");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Mensajes>>();
                    readTask.Wait();

                    mensajes = readTask.Result;
                }
                else
                {
                    mensajes = Enumerable.Empty<Mensajes>();

                    ModelState.AddModelError(string.Empty, "Servidor esta dando error para obtener la lista de estos usuarios");
                }
            }
            return View(mensajes);
        }

    }
}
