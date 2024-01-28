using Microsoft.AspNetCore.Mvc;
using Templates;

namespace Webpuc.ViewComponents
{
    public class FormatsHeaderViewComponent : ViewComponent
    {
        //Definir la cabecer 
        public Task<IViewComponentResult> InvokeAsync(HeaderViewModel headerViewModel) {

            return Task.FromResult<IViewComponentResult>(View(headerViewModel));
        }
    }
}
