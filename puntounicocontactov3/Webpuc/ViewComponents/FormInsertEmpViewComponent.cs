using Templates;
using Microsoft.AspNetCore.Mvc;
using MiscApp.Interface;

namespace Webpuc.ViewComponents
{
    public class FormInsertEmpViewComponent: ViewComponent
    {
        private readonly ICatalogosdb<LCatalogosModel> catalogosdb;

        public FormInsertEmpViewComponent(ICatalogosdb<LCatalogosModel> catalogosdb)
        {
            this.catalogosdb = catalogosdb;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var catalogos = await catalogosdb.GetCatalogoEmpleadosDb();

            return View(catalogos);
        }
    }
}
