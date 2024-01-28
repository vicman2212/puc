using Templates;
using Microsoft.EntityFrameworkCore;
using MiscApp.Interface;
using Webpuc;

namespace MiscApp.Services
{
    public class CatalologosDbEmpService : ICatalogosdb<LCatalogosModel>
    {
        private readonly DbempleadosContext dbempleadosContext;

        public CatalologosDbEmpService(DbempleadosContext dbempleadosContext)
        {
            this.dbempleadosContext = dbempleadosContext;
        }
        public async Task<LCatalogosModel> GetCatalogoEmpleadosDb()//Catalogos en general
        {
            LCatalogosModel catalogos = new LCatalogosModel() { 
                viewGerencia = await dbempleadosContext.ViewGerencia.AsNoTracking().ToListAsync(),
                Puestos = await dbempleadosContext.Puestos.AsNoTracking().ToListAsync(),
            
            };
            return catalogos;
        }

        public async Task<LCatalogosModel> GetCatalogoEmpleadosDb(int id) { //Especifico a un empleado
        
            //Traer los datos del empleado para topar el area y el puesto dependiendo de la gerencia y el nivel
            var empleado = await dbempleadosContext.ViewEmpleados.FirstAsync(x => x.NoEmpleado == id);
            int gerenciaId = (int)empleado.IdGerencia;
            int puestoId = (int)empleado.IdPuesto;
            //Solo se despliega 
            LCatalogosModel catalogos = new LCatalogosModel() {
                viewGerencia = await dbempleadosContext.ViewGerencia.AsNoTracking().ToListAsync(),
                Departamentos = await dbempleadosContext.Departamentos
                                        .Where(x => x.GerenciaId == gerenciaId)
                                        .AsNoTracking().ToListAsync(),
                Puestos = await dbempleadosContext.Puestos
                                        .Where(x => x.IdPuesto >= puestoId)
                                        .AsNoTracking().ToListAsync()
            };
            return catalogos;            
        }
    }
}
