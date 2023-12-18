

namespace MiscApp.Interface
{//Servicio para catalogos de empleado
    public interface ICatalogosdb<T>where T : class
    {
        public Task<T> GetCatalogoEmpleadosDb();
        public Task<T> GetCatalogoEmpleadosDb(int id);
    }
}
