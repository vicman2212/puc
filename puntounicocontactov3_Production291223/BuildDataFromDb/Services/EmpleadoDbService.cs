using BuildDataFromDb.Interface;
using Microsoft.EntityFrameworkCore;
using Webpuc;

namespace BuildDataFromDb.Services
{
    public class EmpleadoDbService : ICRUD<Empleado>
    {
        private readonly DbempleadosContext dbempleadosContext;

        public EmpleadoDbService(DbempleadosContext dbempleadosContext)
        {
            this.dbempleadosContext = dbempleadosContext;
        }
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ExistAsync(Empleado id)
        {
            try
            {
                var numeroEmpleado = await dbempleadosContext.Empleados.AsNoTracking().FirstAsync(x => x.NoEmpleado == id.NoEmpleado);
                return numeroEmpleado.NoEmpleado;
            }
            catch (InvalidOperationException e) 
            {
                Console.WriteLine(e.Message);
                
            }
            return 0;                                  
        }

        public async Task<bool> InsertAsync(Empleado data)
        {
            bool flag = false;       
            try
            {
                dbempleadosContext.Empleados.Add(data);
                await dbempleadosContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error: " + e.Message);           
            }                     
            return flag;
        }

        public Task<Empleado> SelectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Empleado id)
        {
            throw new NotImplementedException();
        }
    }
}
