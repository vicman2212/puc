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
        private readonly IUpdateData<UserIdentityModel> resetpass;

        public HomeController(ICRUD<UserIdentityModel> identidad, ICRUD<List<ModulosPerfilModel>> accesoModulos,
            IEncrypt<string> encrypt,IExistData<UserIdentityModel> existData, IMessageBuilder<MessageModel> messageBuilder,
            IUpdateData<UserIdentityModel> resetpass) {
            this.identidad = identidad;
            this.accesoModulos = accesoModulos;
            this.encrypt = encrypt;
            this.existData = existData;
            this.messageBuilder = messageBuilder;
            this.resetpass = resetpass;
        }
        /*Despliegue de vistas*/
        public IActionResult Index() => View();

        [AllowAnonymous]
        public IActionResult Login() => View();

        [AllowAnonymous]
        public IActionResult OlvidoPass() => View();

        [Authorize(Policy = "Logeado")]
        public IActionResult ResetPasswd() => View();

        [Authorize (Policy = "PA003")]
        public IActionResult MainApp() => View();

        //Vistas de notificaciones
        [AllowAnonymous]
        public IActionResult NotificSuccededRP() => View();


        /*Funcionalidad de acciones http post, get, put, etc*/
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> Login(UserIdentityModel usuarioModel) {
             
            if (!ModelState.IsValid)
            {
                return "Atención ocurrio un error al enviar las credenciales";
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
                        new Claim("Usuario", usr.Usuario),
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
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //4.- Crear la Cookie de autenticacion
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    // verificar que sea la primera vez que ingresa
                    if (usr.Resetado != 0)
                    {
                        switch (usr.CodPerfil)
                        {
                            case "PA003":
                            case "PU002":
                                return "/Home/MainApp";
                            case "PS001":
                                return "/ERISC/HomeERISC";
                            default:
                                break;
                        }
                    }
                    else {
                        return "/Home/ResetPasswd";
                    }
                    
                    return "/Home/MainApp";           
                }
                else { //No existe el usuario
                    return "0";//Enviar pagina de error
                }
            }            
        }
        
        [HttpPost]
        public async Task<IActionResult> OlvidoPass(string usuario) {
            //validar que el correo exista
            var usr = new UserIdentityModel() { 
                Usuario = usuario,
            };
            //Construir datos del usuario     
            var ObjectUsuario = await existData.IsExist(usr);

            if (ObjectUsuario != null)
            {
                string tempPassword = "";
                //Generar password aleatorio
                StringRandom stringRandom = new StringRandom();
                tempPassword = stringRandom.GenerarCadenaAleatoria(20);
                usr.Password = encrypt.EncryptData(tempPassword);
                //****
                //Actualizar la contraseña en la tabla de usuarios
                if (await identidad.UpdateAsync(usr))//Si es correcta la actualizacion de la contraseña enviamos correo de exito
                {
                    MessageNotifcService messageNotifcService = new MessageNotifcService(ObjectUsuario);

                    MessageModel message = new MessageModel()
                    {
                        To = new List<string> { ObjectUsuario.Email },
                        CCP = new List<string> { "soporte.tic@firco.gob.mx" },
                        Subject = "[Punto Único de Contacto] Contraseña Temporal",
                        BodyMessage = messageNotifcService.MsnResetPassword(tempPassword)
                    };
                    await messageBuilder.SendAsync(message);
                    return RedirectToAction("NotificSuccededRP", "Home");
                }
                else {//Ocurrio un error al actualizar la contraseña
                    MessageModel message = new MessageModel()
                    {
                        To = new List<string> { ObjectUsuario.Email },
                        CCP = new List<string> { "soporte.tic@firco.gob.mx" },
                        Subject = "[Punto Único de Contacto] Contraseña Temporal",
                        BodyMessage = "Atencion ocurrio un error al actualizar la contraseña"
                    };
                    await messageBuilder.SendAsync(message);

                }
                //Enviar correo con datos
                //return RedirectToAction("NotificSuccededRP","Home");
            }
            return RedirectToAction("NotificErrorRP", "Home");
        }

        [Authorize(Policy = "Logeado")]
        [HttpPost]
        public async Task<string> ResetPasswd(UserIdentityModel usr)
        {
            string flag = "";
            //Construir datos del usuario     
            var ObjectUsuario = await existData.IsExist(usr);

            if (ObjectUsuario != null)
            {
                usr.Password = encrypt.EncryptData(usr.Password);
                //****************************************************
                //Actualizar la contraseña en la tabla de usuarios
                if (await resetpass.UpdateDataAsync(usr) == 'v')//Si es correcta la actualizacion de la contraseña enviamos correo de exito
                {
                    MessageNotifcService messageNotifcService = new MessageNotifcService(ObjectUsuario);
                    MessageModel message = new MessageModel()
                    {
                        To = new List<string> { ObjectUsuario.Email },
                        CCP = new List<string> { "victor.rgarcia@firco.gob.mx" },
                        Subject = "[Punto Único de Contacto] Contraseña Actualizada",
                        BodyMessage = messageNotifcService.MsnResetPassword("Se ha actualizado su contraseña.")
                    };
                    await messageBuilder.SendAsync(message);
                    //Cerramos la sesion actual
                    if (HttpContext.User.Identity.IsAuthenticated)
                    {
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    }
                    flag = "1";
                }
                else if (await resetpass.UpdateDataAsync(usr) == 'e')
                {//Esta enviando la misma contraseña ingresada en la BD
                    flag = "00";
                }
                else
                {//Ocurrio un error al actualizar la contraseña
                    MessageModel message = new MessageModel()
                    {
                        To = new List<string> { ObjectUsuario.Email },
                        //CCP = new List<string> { "soporte.tic@firco.gob.mx" },
                        Subject = "[Punto Único de Contacto] Contraseña Temporal",
                        BodyMessage = "Atencion ocurrio un error al actualizar la contraseña"
                    };
                    await messageBuilder.SendAsync(message);

                }
                //Enviar correo con datos
                //return RedirectToAction("NotificSuccededRP","Home");
            }
            return flag;
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