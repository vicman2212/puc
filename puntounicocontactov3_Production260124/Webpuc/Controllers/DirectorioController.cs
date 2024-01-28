using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webpuc;

namespace pucultimate.Controllers
{
    [AllowAnonymous]
    public class DirectorioController: Controller
    {
        private readonly DbempleadosContext _empleado;

        public DirectorioController(DbempleadosContext empleado)
        {
            _empleado = empleado;
        }

        public async Task<IActionResult> DirectorioFIRCO() {

            var list_gerencia = await _empleado.ViewGerencia.OrderBy(x=>x.IdGerencia).
                AsNoTracking().
                ToListAsync();
            return View(list_gerencia);
        }

        //Procedimientos de busqueda
        [HttpPost]
        public async Task<JsonResult> LocateByPuesto(int idPuesto) {            
            var ResultadoXPuesto = await _empleado.ViewEmpleados.OrderBy(x => x.IdPuesto)
                .Where(x => x.IdPuesto == idPuesto && x.StatusEmpleado == 1)
                .AsNoTracking()
                .ToListAsync();
            return Json(ResultadoXPuesto);
        }

        [HttpPost]
        public async Task<JsonResult> LocateByRegex(string rgex) {
            List<int> ids = new List<int>();

            var metaBusqueda = await _empleado.ViewMetaEmpleados.Where(x => x.Metadata.Contains(rgex)).AsNoTracking().ToArrayAsync();
            
            foreach (var item in metaBusqueda)
            {
                ids.Add(item.NoEmpleado);
            }

            var ResultadoXRegex = from item in await _empleado.ViewEmpleados.AsNoTracking().ToArrayAsync()
                                  where ids.Contains(item.NoEmpleado)
                                  select item;

            return Json(ResultadoXRegex);
        }
    }
}
