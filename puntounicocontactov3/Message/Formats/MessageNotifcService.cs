using Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Formats
{
    public class MessageNotifcService
    {
        private readonly UserIdentityModel usuario;
        private string? msn;
        public MessageNotifcService(UserIdentityModel usuario)
        {
            this.usuario = usuario;
        }

        public string MsnResetPassword(string PassTemporal) {

            return $@"
                <!DOCTYPE html>
<html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>olvidoPass</title>
    </head>
    <body>
        <div style='font-family: ""Montserrat"";margin: auto; width: 70%'>
            <div style='background: #b38e5d;height: 0.5rem;'></div>
            <div style='background-color: #F6F6F6; text-align: center; padding: 1rem;'>
                <div style='display: flex;'>
                    <div style=' width: 20%;height: 7rem; padding-top: .5rem;'>
                        <img src='http://10.1.66.226/puntounicocontacto/icon/Firco-Logo-Original.png' width='30%'/>
                    </div>
                    <div style='width: 70%;padding-top: 1rem;'>
                        <div style='font-size: larger; margin-bottom: .4rem;'><strong>FIDEICOMISO DE RIESGO COMPARTIDO</strong></div>
                        <div style='font-size: medium; margin-bottom: .4rem;'><strong>{usuario.Usuario}</strong></div>
                    </div>
                </div>    
            </div>
            <div style='background: white;height: 0.5rem;'></div>
            <div style='background-color: #F6F6F6;font-family: ""Montserrat"";text-align: justify; padding: 1rem;'>
                <h4 style='border-bottom: #b38e5d solid;margin-top: .5rem;margin-bottom: 1rem;'>PROCESO DE RESETEO DE PASSWORD</h4>
                <div style='margin-bottom: 1.5rem;'>
                    <div style='margin-bottom: .6rem;'><strong>Solicitante: </strong><span style='background-color: white; padding: .3rem;'>{usuario.FullName}</span></div>
                    <div style='margin-bottom: .6rem;'><strong>Puesto: </strong><sapn style='background-color: white; padding: .3rem;'>{usuario.Job}</sapn></div>
                    <div style='margin-bottom: .6rem;'><strong>Área: </strong><sapn style='background-color: white; padding: .3rem;'>{usuario.DirDepartment}</sapn></div>
                    <div style='margin-bottom: .6rem;'><strong>Correo: </strong><sapn style='background-color: white; padding: .3rem;'>{usuario.Email}</sapn></div>
                    <div style='margin-bottom: .6rem;'><strong>Telefono: </strong><sapn style='background-color: white; padding: .3rem;'>{usuario.Phone}</sapn></div>
                </div>
                <h4 style='border-bottom: #b38e5d solid;margin-top: .5rem;margin-bottom: 1rem;'>CREDENCIALES DE ACCESO TEMPORAL</h4>
                <div style='margin-bottom: .6rem;'><strong>INGRESE CON LA SIGUIENTE CONTRASEÑA TEMPORAL: </strong><span style='background-color: yellow; padding: .2rem;'>{PassTemporal}</span></div>
                <div style='margin-bottom: .6rem; color: #842029; background-color: #f8d7da;border-color:#f5c2c7; position: relative;
                            padding: 1rem 1rem;
                            margin-bottom: 1rem;
                            border: 1px solid transparent;
                             border-radius: 0.25rem;'><strong>ATENCIÓN: Recuerde modificar sus credenciales con las siguientes caracteristicas </strong></div>
                <div style=""color: #055160;
                            background-color: #cff4fc;
                            border-color: #b6effb;
                            position: relative;
                            padding: 1rem 1rem;
                            margin-bottom: 1rem;
                            border: 1px solid transparent;
                            border-radius: 0.25rem;"">
                    <ul>
                        <li>Inicie sesion en el PUC</li>
                        <li>Ingrese con la contraseña temporal</li>
                        <li>Modifique la contraseña temporal</li>
                    </ul>
                </div>
            </div>
            <div style='background: #b38e5d;width: 100%;height: 0.5rem;'></div>
        </div>
    </body>
</html>
            ";
        }




        public string MsnUpdateEmpleado(EmpleadoModel dataHistoric,EmpleadoModel dataUpdate) {

            string Formato = "PUC-UE";//PUC-UpdateEmpleado
            string Version = "1.0";
            string Firma = Guid.NewGuid().ToString();

            return this.msn = $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>FormatoUpdateEmpleado</title>
                        </head>
                        <body>
                            <div style='font-family: ""Montserrat"";margin: auto; width: 70%'>
                                <div style='background: #b38e5d;height: 0.5rem;'></div>
                                <div style='background-color: #F6F6F6; text-align: center; padding: 1rem;'>
                                    <div style='display: flex;'>
                                        <div style=' width: 20%;height: 7rem; padding-top: .5rem;'>
                                            <img src='http://10.1.66.226/puntounicocontacto/icon/Firco-Logo-Original.png' width='30%'/>
                                        </div>
                                        <div style='width: 70%;padding-top: 1rem;'>
                                            <div style='font-size: larger; margin-bottom: .4rem;'><strong>FIDEICOMISO DE RIESGO COMPARTIDO</strong></div>
                                            <div style='font-size: medium; margin-bottom: .4rem;'><strong>{usuario.DirDepartment}</strong></div>
                                        </div>
                                        <div style='width: 20%; padding-top: 1rem;'>
                                            <div style='font-size: medium; margin-bottom: .4rem;'><strong>Formato: </strong><span>{Formato}</span></div>
                                            <div style='font-size: medium; margin-bottom: .4rem;'><strong>Version: </strong><span>{Version}</span></div>
                                        </div>
                                    </div>    
                                </div>
                                <div style='background: white;height: 0.5rem;'></div>
                                <div style='background-color: #F6F6F6;font-family: ""Montserrat"";text-align: justify; padding: 1rem;'>
                                    <h4 style='border-bottom: #b38e5d solid;margin-top: .5rem;margin-bottom: 1rem;'>USUARIO QUE ACTUALIZA EN DIRECTORIO: </h4>
                                    <div style='margin-bottom: 1.5rem;'>
                                        <div style='margin-bottom: .6rem;'><strong>Solicitante: </strong><span style='background-color: white; padding: .3rem;'>{usuario.FullName}</span></div>
                                        <div style='margin-bottom: .6rem;'><strong>Puesto: </strong><sapn style='background-color: white; padding: .3rem;'>{usuario.Job}</sapn></div>
                                        <div style='margin-bottom: .6rem;'><strong>Área: </strong><sapn style='background-color: white; padding: .3rem;'>{usuario.DirDepartment}</sapn></div>
                                        <div style='margin-bottom: .6rem;'><strong>Correo: </strong><sapn style='background-color: white; padding: .3rem;'>{usuario.Email}</sapn></div>
                                        <div style='margin-bottom: .6rem;'><strong>Telefono: </strong><sapn style='background-color: white; padding: .3rem;'>{usuario.Phone}</sapn></div>
                                    </div>
                                    <h4 style='border-bottom: #b38e5d solid;margin-top: .5rem;margin-bottom: 1rem;'>ELEMENTOS ACTUALIZADOS:</h4>
                                    <div style='margin-bottom: .6rem;'><strong>Número de empleado: </strong><span style='background-color: white; padding: .3rem;'>{dataHistoric.NoEmpleado}</span></div>
                                    <div style='margin-bottom: .6rem;'><strong>Nombre Completo: </strong><span style='background-color: white; padding: .3rem;'>{dataHistoric.FullName}</span></div>
                                    <div style='display: flex;'>                    
                                        <div style='width: 50%;margin-right: 3rem;border-top: orange solid;'>
                                            <h5>Datos historicos</h5>
                                            <div style='margin-bottom: .6rem;'><strong>Gerencia </strong><span style='background-color: white; padding: .3rem;'>{dataHistoric.State}</span></div>
                                            <div style='margin-bottom: .6rem;'><strong>Área </strong><span style='background-color: white; padding: .3rem;'>{dataHistoric.DirDepartment}</span></div>
                                            <div style='margin-bottom: .6rem;'><strong>Puesto </strong><span style='background-color: white; padding: .3rem;'>{dataHistoric.Job}</span></div>
                                            <div style='margin-bottom: .6rem;'><strong>Extensión </strong><span style='background-color: white; padding: .3rem;'>{dataHistoric.Ext}</span></div>
                                            <div style='margin-bottom: .6rem;'><strong>¿Activo/desactivado? </strong><span style='background-color: white; padding: .3rem;'>{dataHistoric.IsActive}</span></div>
                                        </div>
                                        <div style='width: 50%;border-top: green solid;'>
                                            <h5>Datos actualizados</h5>
                                            <div style='margin-bottom: .6rem;'><strong>Gerencia </strong><span style='background-color: white; padding: .3rem;'>{dataUpdate.State}</span></div>
                                            <div style='margin-bottom: .6rem;'><strong>Área </strong><span style='background-color: white; padding: .3rem;'>{dataUpdate.DirDepartment}</span></div>
                                            <div style='margin-bottom: .6rem;'><strong>Puesto </strong><span style='background-color: white; padding: .3rem;'>{dataUpdate.Job}</span></div>
                                            <div style='margin-bottom: .6rem;'><strong>Extensión </strong><span style='background-color: white; padding: .3rem;'>{dataUpdate.Ext}</span></div>
                                            <div style='margin-bottom: .6rem;'><strong>¿Activo/desactivado? </strong><span style='background-color: white; padding: .3rem;'>{dataUpdate.IsActive}</span></div>
                                        </div>
                                    </div>
                                </div>
                                <div style='background: white;width: 100%;height: 0.5rem;'></div>
                                <div style='background-color: #F6F6F6; font-family: ""Montserrat""; text-align: center; padding: 1rem;'>
                                    <h4>Firma de usuario</h4>
                                    <div style='width: 25rem; height: 4rem;background: white; margin: auto;padding: 1rem;'>
                                        <div><strong>{Firma}</strong></div>
                                        <div><strong>{usuario.FullName}</strong></div>
                                    </div>          
                                </div>
                                <div style='background: #b38e5d;width: 100%;height: 0.5rem;'></div>
                            </div>
                        </body>
                    </html>
                ";
        }

    }
}
