//Directorio mostrado por jerarquia para la vista
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webpuc;

namespace pucultimate.ViewComponents
{//DirectorioVistaOficiansCentales
    public class DirectorioVOCViewComponent: ViewComponent
    {
        private readonly DbempleadosContext _contextDb;
        public DirectorioVOCViewComponent(DbempleadosContext contextDb)
        {
           _contextDb = contextDb;
        }

        public async Task<IViewComponentResult> InvokeAsync(int idDepartamento)
        {

            if (idDepartamento == 0)//Caso cuando es Direcciones
            {
                var direcciones = await _contextDb.Departamentos.Where(propa => propa.IdDepartamento == 100)
                                                                .AsNoTracking()
                                                                .ToListAsync();
                return View(direcciones);
            }
            else //Caso herencia de idDepartamento
            {
                //Si quitas el propa.GerenciaId == 9 automatiza para todas las gerencias la division por areas
                var subordinado = await _contextDb.Departamentos.Where(propa => propa.GerenciaId == 9 && propa.DepartamentoId == idDepartamento)
                                                                .AsNoTracking()
                                                                .ToListAsync();
                return View(subordinado);
            }
        }
    }
}
