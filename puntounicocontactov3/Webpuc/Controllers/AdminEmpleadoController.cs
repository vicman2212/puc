using BuildDataFromDb.Interface;
using Templates;
using Message;
using Message.Formats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiscApp.Services;
using System.Security.Claims;

namespace Webpuc.Controllers
{
    [Authorize(Roles = "RPA_004")]//Solo acceso al Rol de personal 
    public class AdminEmpleadoController : Controller
    {
        private readonly DbempleadosContext _empleados;
        private readonly ICRUD<EmpleadoModel> updateservice;
        private readonly ICRUD<Empleado> empleadoDb;
        private readonly IMessageBuilder<MessageModel> messageBuilder;

        public AdminEmpleadoController(DbempleadosContext empleados, ICRUD<EmpleadoModel> updateservice, ICRUD<Empleado> empleadoDb,
            IMessageBuilder<MessageModel> messageBuilder) {
            _empleados = empleados;
            this.updateservice = updateservice;
            this.empleadoDb = empleadoDb;
            this.messageBuilder = messageBuilder;
        }

        //Vistas
        public IActionResult NotificSucceded() => View();
        public IActionResult NotificError() => View();
        public IActionResult InsertEmpleado() => View();
        public IActionResult UpdateEmp(int NoEmpleado) {
            //VAriables de sesion Claim
            var NombreDepartamento = HttpContext.User.FindFirst("Area").Value;
            var NombreDepartamentoSup = HttpContext.User.FindFirst("Direc").Value;
            ViewData["idEmpleado"] = NoEmpleado;

            HeaderViewModel header = new HeaderViewModel() {
                Institucion = "Fideicomiso de Riesgo Compartido",
                Direccion = NombreDepartamento,
                Gerencia = "",
                TipoF = "F-UDP",
                VersionF = "1.0"
            };
            
            return View(header);
        }

        //Respuestas json con javascript
        /*Envio de listas desde peticiones ajax*/
        [HttpPost]
        public async Task<JsonResult> ProcChangeArea(int idDep)
        { /*Filtrar por ubicacion, topado al estado al que pertence*/
            string nivel = "";

            var Areajson = await _empleados.Departamentos.Where(x => x.IdDepartamento == idDep)
                .AsNoTracking()
                .ToListAsync();
            //1.- Obtener el nivel del departamento
            foreach (var item in Areajson.AsQueryable())
            {
                nivel = item.NivelP;
            }

            //2.-Obtener el id del puesto respecto al nivel que corresponde al área:
            var Puestoespecifico = await _empleados.Puestos
                .FirstAsync(x => x.Nivel == nivel);

            //3.-Obtenermos el puesto a partir del nivel arrojado por el area
            var Puestosjson = await _empleados.Puestos
                .Where(x => x.IdPuesto >= Puestoespecifico.IdPuesto)
                .AsNoTracking()
                .ToListAsync();
            return Json(Puestosjson);
        }

        [HttpPost]
        public async Task<JsonResult> ProcChangeEntidadFederativa(int idEF)
        { /*Filtrar por ubicacion, topado al estado al que pertence*/
            var Areajson = await _empleados.Departamentos.Where(x => x.GerenciaId == idEF)
                .AsNoTracking()
                .ToListAsync();
            return Json(Areajson);
        }

        [HttpPost]
        public async Task<string> UpdateEmpleado(EmpleadoModel empleado)
        {
            //Historico del status actual del empleado
            EmpleadoModel empHistObject = null;
            var empleadohistoricoDb = _empleados.ViewEmpleados.Where(x => x.NoEmpleado == empleado.NoEmpleado)
                                    .AsNoTracking()
                                    .AsQueryable();
            foreach (var item in empleadohistoricoDb.AsQueryable())
            {
                empHistObject = new EmpleadoModel() { 
                    NoEmpleado = empleado.NoEmpleado,
                    FullName = item.Nombres + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno,
                    State = item.EntidadFederativa,
                    DirDepartment = item.NombreDepartamento,
                    Job = item.Puesto,
                    Ext = item.Extension,
                    IsActive = (byte)item.StatusEmpleado
                };                
            }
            /*Actualizacion de los empleados en su ubicacion, area y puesto*/            
            //1.-Se realiza la actualizacion del empleado
            if (await updateservice.UpdateAsync(empleado)) 
            {
                UserIdentityModel empUpdated = null;
                //Establecer los datos del usuario logeado que esta realizando la operacion:
                UserIdentityModel usuarioSession = new UserIdentityModel() { 
                    FullName = HttpContext.User.Identity.Name,
                    Job = HttpContext.User.FindFirst("Puesto").Value,
                    DirDepartment = HttpContext.User.FindFirst("Area").Value,
                    Email = HttpContext.User.FindFirst(ClaimTypes.Email).Value,
                    Phone = HttpContext.User.FindFirst(ClaimTypes.OtherPhone).Value + "Ext." + HttpContext.User.FindFirst("Ext").Value
                };
                //Establecer la actualizacion del empleado
                var empleadoactualizado = _empleados.ViewEmpleados.Where(x => x.NoEmpleado == empleado.NoEmpleado)
                          .AsNoTracking()
                          .AsQueryable();
                foreach (var item in empleadoactualizado.AsQueryable())
                {
                    empUpdated = new UserIdentityModel()
                    {                     
                        State = item.EntidadFederativa,
                        DirDepartment = item.NombreDepartamento,
                        Job = item.Puesto,
                        Ext = item.Extension,
                        IsActive = (byte)item.StatusEmpleado
                    };
                }
                //Si se realizo la actualización enviar notificación:
                MessageNotifcService messageNotifcService = new MessageNotifcService(usuarioSession);//Establecer mensaje de body
                                                                                                     //Envio de correo
                MessageModel message = new MessageModel()
                {
                    To = new List<string> { "victor.rgarcia@firco.gob.mx" },
                    CCP = new List<string> { "soporte.tic@firco.gob.mx" },
                    Subject = "!ATENCION!, Actualizacion de Empleado",
                    BodyMessage = messageNotifcService.MsnUpdateEmpleado(empHistObject,empUpdated)
                };
                await messageBuilder.SendAsync(message);
                //Exito en en la actualizacion
                return "Se ha realizado la actualizacion";
            }
            return "Ocurrio un error";
        }

        //insertar elementos
        [HttpPost]
        public async Task<IActionResult> InsertEmpleadoAsync(InsertEmpModel empleado) {
            //1.- Validar el formulario
            if (!ModelState.IsValid)
            {
                return View(empleado);
            }

            //2.- Procedimiento para determinar un correo 

            GenerarCorreoService generarCorreoService = new GenerarCorreoService(_empleados);
            string correo = await generarCorreoService.GenerateEMail(empleado.Nombres, empleado.ApellidoPaterno, empleado.ApellidoMeterno);

            //3.- Procedimiento para insertar datos a la tabla Empleado
            Empleado empleadoDbobject = new Empleado(){ 
                NoEmpleado = empleado.NoEmpleado,
                Nombres = empleado.Nombres,
                ApellidoPaterno = empleado.ApellidoPaterno,
                ApellidoMaterno = empleado.ApellidoMeterno,
                Correo = correo,
                Extension = empleado.Extension,
                StatusEmpleado = empleado.StatusEmpleado,
                Delegado = empleado.Delegado,
                Rfc = empleado.Rfc,
                Curp = empleado.Curp,
                DepartamentoId = empleado.DepartamentoId,
                PuestoId = empleado.PuestoId,
                TituloId = empleado.TituloId,
            };
            //Verificar que el No. de empleado no existe
            if (await empleadoDb.ExistAsync(empleadoDbobject) != 0)
            {
                //Evaluar si se ha insertado un elemento a la tabla de Empleado
                if (await empleadoDb.InsertAsync(empleadoDbobject))
                {
                    //Notificacion general de exito en la operacion de la base de datos
                    return RedirectToAction("NotificSucceded","AdminEmpleado");
                }
            }

            //Notificacion general en el error general 
            return RedirectToAction("NotificError", "AdminEmpleado");
        }

    }
}
