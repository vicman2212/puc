using BuildDataFromDb.Interface;
using Microsoft.EntityFrameworkCore;
using Webpuc;
using Templates;

namespace BuildDataFromDb.Services
{
    public class AccesoModulosService : ICRUD<List<ModulosPerfilModel>>
    {
        private readonly AccesoFircoContext accesoFircoContext;

        public AccesoModulosService(AccesoFircoContext accesoFircoContext) {
            this.accesoFircoContext = accesoFircoContext;
        }
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExistAsync(List<ModulosPerfilModel> id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(List<ModulosPerfilModel> data)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ModulosPerfilModel>> SelectAsync(int id)
        {
            var lista = new List<ModulosPerfilModel>();
            ModulosPerfilModel objeto = null;
            int idRol = 0;
            int idPerfil = 0;
            //1.- Obtener el id del Perfil del empleado
            var usuario = await accesoFircoContext.Usuarios.Where(x => x.NoEmpleado == id)
                .AsNoTracking()
                .ToListAsync();
            foreach (var item in usuario.AsQueryable())
            {
                idRol = item.RolId;
                
            }

            var perfil = await accesoFircoContext.Rols.Where(x => x.IdRol == idRol)
                .AsNoTracking() 
                .ToListAsync();

            foreach (var item in perfil.AsQueryable())
            {
                idPerfil = item.PerfilId;
            }
            //2.- Obtener la lista de modulos de acceso
            var dbObjeto = await accesoFircoContext.Permisos
                            .Join(accesoFircoContext.Modulos,
                                  pe => pe.ModuloId,
                                  m => m.IdModulo,
                                  (pe, m) => new { pe,m })
                            .Join(accesoFircoContext.Perfils,
                                  per => per.pe.PerfilId,
                                  cpe => cpe.IdPerfil,
                                  (per, cper) => new
                                  {
                                      IdPermiso = per.pe.IdPermiso,
                                      IdPerfil = cper.IdPerfil,
                                      CodigoPermiso = per.pe.CodigoPermiso,
                                      CodigoModulo = per.m.CodigoModulo,
                                      CodigoPerfil = cper.CodigoPerfil,
                                      ActivoP = per.pe.Activo, //Bit para Permiso activo
                                      ActivoM = per.m.Activo, //Bit para Modulo activo
                                      NModulo = per.m.NombreModulo,
                                      Controller = per.m.Ruta,
                                      Action = per.m.Modulo1,
                                      Icon = per.m.Icon,
                                      NPerfil = cper.Perfil1
                                  }).Where(x => x.IdPerfil == idPerfil)
                            .ToListAsync();

            
            
            foreach (var item in dbObjeto.AsQueryable())
            {
                if (item.ActivoM == 1)//Desplegamos solo los modulos activos
                {
                    objeto = new ModulosPerfilModel()
                    {
                        idPermiso = item.IdPermiso,
                        idPerfil = item.IdPerfil,
                        CodigoPermiso = item.CodigoPermiso,
                        CodigoModulo = item.CodigoModulo,
                        CodigoPerfil = item.CodigoPerfil,
                        ActivoPermiso = item.ActivoP,
                        ActivoModulo = item.ActivoM,
                        NombreModulo = item.NModulo,
                        Controller = item.Controller,
                        Action = item.Action,
                        Icon = item.Icon,
                        NPerfil = item.NPerfil
                    };
                    lista.Add(objeto);
                }                
            }

            return lista;
        }
        public Task<bool> UpdateAsync(List<ModulosPerfilModel> id)
        {
            throw new NotImplementedException();
        }
    }
}
