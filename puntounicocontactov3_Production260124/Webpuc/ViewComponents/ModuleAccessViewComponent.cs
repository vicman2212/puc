using BuildDataFromDb.Interface;
using Templates;
using Microsoft.AspNetCore.Mvc;

namespace Webpuc.ViewComponents
{
    //Definir el acceso a los modulos de nuestra aplicaciones dependiendo del la autorizacion por perfiles  y modulos de la base de datos
    public class ModuleAccessViewComponent : ViewComponent
    {
        private readonly ICRUD<List<ModulosPerfilModel>> accesoModulos;

        //Programar 
        public ModuleAccessViewComponent(ICRUD<List<ModulosPerfilModel>> accesoModulos)
        {
            this.accesoModulos = accesoModulos;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var catalogo = await accesoModulos.SelectAsync(id);

            return View(catalogo);
        }
    }
}
