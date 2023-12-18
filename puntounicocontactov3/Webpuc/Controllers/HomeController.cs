using BuildDataFromDb.Interface;
using Templates;
using Message;
using Message.Formats;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiscApp;
using System.Diagnostics;
using System.Security.Claims;
using Webpuc.Models;

namespace Webpuc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICRUD<UserIdentityModel> identidad;
        private readonly ICRUD<List<ModulosPerfilModel>> accesoModulos;
        private readonly IEncrypt<string> encrypt;
        private readonly IExistData<UserIdentityModel> existData;
        private readonly IMessageBuilder<MessageModel> messageBuilder;

        public HomeController(ICRUD<UserIdentityModel> identidad, ICRUD<List<ModulosPerfilModel>> accesoModulos,
            IEncrypt<string> encrypt,IExistData<UserIdentityModel> existData, IMessageBuilder<MessageModel> messageBuilder) {
            this.identidad = identidad;
            this.accesoModulos = accesoModulos;
            this.encrypt = encrypt;
            this.existData = existData;
            this.messageBuilder = messageBuilder;
        }
        /*Despliegue de vistas*/
        public IActionResult Index() => View();

        [AllowAnonymous]
        public IActionResult Login() => View();

        [AllowAnonymous]
        public IActionResult OlvidoPass() => View();

        [Authorize (Policy = "PA003")]
        public IActionResult MainApp() => View();

        //Vistas de notificaciones
        [AllowAnonymous]
        public IActionResult NotificSuccededRP() => View();


        /*Funcionalidad de acciones http post, get, put, etc*/
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserIdentityModel usuarioModel) {
             
            if (!ModelState.IsValid)
            {
                return View(usuarioModel);
            }
            else {
                usuarioModel.Password = encrypt.EncryptData(usuarioModel.Password);
                int NoEmpleado = await identidad.ExistAsync(usuarioModel);
                //2.- Validar que el usuario y la contraseña sean correctos en la base de datos
               
                if (NoEmpleado != 0)//Si el usuario existe
                {
                    //var per = await accesoModulos.SelectAsync(NoEmpleado);
                    var usr = await identidad.SelectAsync(NoEmpleado); 
                    //3.- Generar el par de datos key value para para la cookie mediante las reclamaciones
                    var claims = new List<Claim>{
                        new Claim("Id", NoEmpleado.ToString()),
                        new Claim(ClaimTypes.Name, usr.FullName),
                        new Claim(ClaimTypes.Role, usr.CodRol),
                        new Claim(ClaimTypes.Email,usr.Email),
                        new Claim("Puesto", usr.Job),
                        new Claim("Direc",usr.SupDeparment),
                        new Claim("Area",usr.DirDepartment),
                        new Claim(ClaimTypes.Locality, usr.State),
                        new Claim(ClaimTypes.StreetAddress, usr.Locate),
                        new Claim(ClaimTypes.OtherPhone,usr.Phone),
                        new Claim("Ext",usr.Ext),
                    };
                    //Establecemos el tipo de autorizacion, en este caso establecemos un esquema basado en cookie
                    var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    //4.- Crear la Cookie de autenticacion
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));

                    switch (usr.CodPerfil)
                    {
                        case "PA003":
                        case "PU002":
                            return RedirectToAction("MainApp", "Home");
                        case "PS001":
                            return RedirectToAction("HomeERISC", "ERISC");
                        default:
                            break;
                    }
                    return RedirectToAction("MainApp", "Home");
                }
                else { //No existe el usuario
                    return RedirectToAction("Error", "Home");//Enviar pagina de error
                }
            }            
        }
        
        [HttpPost]
        public async Task<IActionResult> OlvidoPass(string usuario) {
            //validar que el correo exista
            var usr = new UserIdentityModel() { 
                Usuario = usuario,
            };
            var ObjectUsuario = await existData.IsExist(usr);

            if (ObjectUsuario != null)
            {
                string tempPassword = "";
                //Generar password aleatorio
                StringRandom stringRandom = new StringRandom();
                tempPassword = stringRandom.GenerarCadenaAleatoria(20);
                //Obtener correo para el que va dirigido
                //****
                //Enviar correo con datos
                //Construir datos del usuario      
                MessageNotifcService messageNotifcService = new MessageNotifcService(ObjectUsuario);         

                MessageModel message = new MessageModel()
                {
                    To = new List<string> { ObjectUsuario.Email },
                    CCP = new List<string> { "soporte.tic@firco.gob.mx"},
                    Subject = "[Punto Único de Contacto] Contraseña Temporal",
                    BodyMessage = messageNotifcService.MsnResetPassword(tempPassword)
                };
                await messageBuilder.SendAsync(message);
                return RedirectToAction("NotificSuccededRP","Home");
            }

            return RedirectToAction("NotificErrorRP", "Home");

        }
       
        public async Task<IActionResult> Logout()//Cerrar la sesion
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }           
            
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}