using BuildDataFromDb.Interface;
using Templates;
using Microsoft.EntityFrameworkCore;
using Webpuc;

namespace BuildDataFromDb.Services
{
    //Devuelve el conjunto de los datos del usuario recabado en la base de datos
    public class UserService : ICRUD<UserIdentityModel>
    {
        private readonly AccesoFircoContext _accesoFircoContext;
        private readonly DbempleadosContext _dbempleadosContext;

        public UserService(AccesoFircoContext accesoFircoContext, DbempleadosContext dbempleadosContext)
        {
            _accesoFircoContext = accesoFircoContext;
            _dbempleadosContext = dbempleadosContext;
        }

        public Task<bool> UpdateAsync(UserIdentityModel id) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ExistAsync(UserIdentityModel id)//Para el logeo 
        {
            int idObj = 0;
            
            var usuarioDb = await _accesoFircoContext.Usuarios
                                .AsNoTracking()
                                .ToListAsync();

            foreach (var item in usuarioDb.AsQueryable())
            {
                if (item.Usuario1 == id.Usuario && item.Pass == id.Password)
                {
                    idObj = item.NoEmpleado;
                }
            }
            
            return idObj;
            
        }
        public Task<bool> InsertAsync(UserIdentityModel data) {

            throw new NotImplementedException();
        }
        public async Task<UserIdentityModel> SelectAsync(int id)
        {          
            var usuarioDb = await _accesoFircoContext.Usuarios.Where(item => item.NoEmpleado == id)
                            .AsNoTracking()
                            .ToListAsync();
            
           var empleadoDb = await _dbempleadosContext.ViewEmpleados.Where(item => item.NoEmpleado == id)
                                    .AsNoTracking().ToListAsync();

            var objetoUsuario = new UserIdentityModel();
            objetoUsuario.NoEmpleado = id;
            foreach (var item in usuarioDb.AsQueryable())
            {
                objetoUsuario.Usuario = item.Usuario1;
                //Obtener el perfil y rol
                var rolDb = await _accesoFircoContext.Rols.FirstAsync(x => x.IdRol == item.RolId);          
                objetoUsuario.CodRol = rolDb.CodigoRol;
                // Obtener el perfil
                var perfilDb = await _accesoFircoContext.Perfils.FirstAsync(x => x.IdPerfil == rolDb.PerfilId);
                objetoUsuario.CodPerfil = perfilDb.CodigoPerfil;
                //Obtener que este activo
                objetoUsuario.IsActive = item.Activo;
            }

            foreach (var item in empleadoDb.AsQueryable())
            {
                objetoUsuario.FullName = item.Nombres + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
                objetoUsuario.Job = item.Puesto;
                objetoUsuario.Email = item.Correo;
                objetoUsuario.State = item.EntidadFederativa;
                objetoUsuario.Locate = "Calle: " + item.Calle + "; Localidad " + item.Localidad + "; Municipio " + item.Municipio +
                                                "; Numero: " + item.Numero + "; Código Postal " + item.CodigoPostal;
                objetoUsuario.IdSupDeparment = item.DepartamentoId;
                objetoUsuario.DirDepartment = item.NombreDepartamento;
                objetoUsuario.Phone = item.Conmutador;
                objetoUsuario.Ext = item.Extension;
                //Id de elemntos
                objetoUsuario.IdLocate = item.IdEntidadFederativa;
                objetoUsuario.IdJob = item.IdPuesto;
                objetoUsuario.IdDirDeparment = item.IdDepartamento;
            }
            //obtener el nombre del departamento superior. Atencion manejar el nulo para DG            
            if (objetoUsuario.IdSupDeparment != null)//Validar que no sea Top o nulo
            {
                var depsuperior = await _dbempleadosContext.Departamentos.FirstAsync(x => x.IdDepartamento == objetoUsuario.IdSupDeparment);
                objetoUsuario.SupDeparment = depsuperior.NombreDepartamento;
            }
            else {
                objetoUsuario.SupDeparment = "FIRCO";
            }
            

            return objetoUsuario;
        }        
    }

}
