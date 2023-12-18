using BuildDataFromDb.Interface;
using Templates;
using Microsoft.EntityFrameworkCore;
using Webpuc;

namespace BuildDataFromDb.Services
{
    public class SearchUsrService : IExistData<UserIdentityModel>
    {
        private readonly AccesoFircoContext accesoFircoContext;
        private readonly DbempleadosContext dbempleadosContext;

        public SearchUsrService(AccesoFircoContext accesoFircoContext,DbempleadosContext dbempleadosContext) {
            this.accesoFircoContext = accesoFircoContext;
            this.dbempleadosContext = dbempleadosContext;
        }
        public async Task<UserIdentityModel> IsExist(UserIdentityModel data)
        {
            UserIdentityModel? objectUsuario = null;
            var usuarioData = await accesoFircoContext.Usuarios
                            .AsNoTracking()
                            .FirstAsync(x => x.Usuario1 == data.Usuario && x.Activo != 0);//Evaluar que el usuario exista y que no este desahabilitado

            if (usuarioData.IdUsuario != 0)
            {
                var empleadoData = await dbempleadosContext.ViewEmpleados
                                        .AsNoTracking()
                                        .FirstAsync(x => x.NoEmpleado == usuarioData.NoEmpleado);

                objectUsuario = new UserIdentityModel() { 
                    FullName = empleadoData.Nombres + " " + empleadoData.ApellidoPaterno + " " + empleadoData.ApellidoMaterno,
                    Job = empleadoData.Puesto,
                    DirDepartment = empleadoData.NombreDepartamento,
                    Email = empleadoData.Correo,
                    Phone = empleadoData.Conmutador + "Ext. " + empleadoData.Extension,
                    Usuario = data.Usuario,
                };

                return objectUsuario;
            }
            return objectUsuario;
        }
    }
}
