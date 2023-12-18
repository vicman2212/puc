using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webpuc;
/*Directorio Personal Gerencias: Componente para mostrar al personal dependiendo de la gerencia estatal*/
namespace pucultimate.ViewComponents
{//DirectorioPersonalGerencia
    public class DirectorioPGViewComponent: ViewComponent
    {
        private readonly DbempleadosContext _empleados;
        public DirectorioPGViewComponent(DbempleadosContext empleados) {
            _empleados = empleados;
        }
        public async Task<IViewComponentResult> InvokeAsync(int idGerencia, int idDepartamento) {

            if (idDepartamento == 0)//Personal de Gerencias Estatales
            {
                var tablaEmpleados = await _empleados.ViewEmpleados.OrderBy(x => x.IdPuesto)
                                                                    .Where(x => x.IdGerencia == idGerencia && x.StatusEmpleado == 1)
                                                                    .AsNoTracking()
                                                                    .ToListAsync();
                return View(tablaEmpleados);
            }
            else { //Pesonal por departameto
                var tablaEmpleadosXArea = await _empleados.ViewEmpleados.OrderBy(x => x.IdPuesto)
                                                                    .Where(x => x.IdGerencia == idGerencia && x.IdDepartamento == idDepartamento && x.StatusEmpleado == 1)
                                                                    .AsNoTracking()
                                                                    .ToListAsync();
                return View(tablaEmpleadosXArea);
            }
        }
    }
}
