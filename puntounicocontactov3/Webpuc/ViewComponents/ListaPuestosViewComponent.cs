
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pucultimate;
using Webpuc;

namespace PuntoUnicoContacto.ViewComponents
{
    public class ListaPuestosViewComponent : ViewComponent
    {
        private readonly DbempleadosContext _puestos;

        public ListaPuestosViewComponent(DbempleadosContext puestos)
        {
            _puestos = puestos;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lista = await _puestos.Puestos
                .AsNoTracking()
                .ToListAsync();
            return View(lista);
        }
    }
}
