using BuildDataFromDb.Interface;
using Templates;
using Microsoft.AspNetCore.Mvc;
using MiscApp.Interface;

namespace Webpuc.ViewComponents
{
    public class UpdateEmpViewComponent : ViewComponent
    {
        private readonly ICRUD<UserIdentityModel> usuario;
        private readonly ICatalogosdb<LCatalogosModel> catalogosdb;

        public UpdateEmpViewComponent(ICRUD<UserIdentityModel> usuario, ICatalogosdb<LCatalogosModel> catalogosdb)
        {
            this.usuario = usuario;
            this.catalogosdb = catalogosdb;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id) {//Se recibe el numero de empleado  
            
            //Objeto para la vista requisitos de actualizacion de empleado
            UpdateEmpModel updateEmpModel = new UpdateEmpModel() { 
                UserIdentity = await usuario.SelectAsync(id),
                lCatalogosModels = await catalogosdb.GetCatalogoEmpleadosDb(id)
            };

            return View(updateEmpModel);
        }



    }
}
