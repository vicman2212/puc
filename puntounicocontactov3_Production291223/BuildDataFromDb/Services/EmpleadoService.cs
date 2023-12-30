using BuildDataFromDb.Interface;
using Templates;
using Microsoft.EntityFrameworkCore;
using Webpuc;

namespace BuildDataFromDb.Services
{
    public class EmpleadoService : ICRUD<EmpleadoModel>
    {
        private readonly DbempleadosContext dbempleadosContext;

        public EmpleadoService(DbempleadosContext dbempleadosContext)
        {
            this.dbempleadosContext = dbempleadosContext;
        }
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExistAsync(EmpleadoModel id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(EmpleadoModel data)
        {


            throw new NotImplementedException();
        }

        public Task<EmpleadoModel> SelectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(EmpleadoModel id)
        {
            bool flag = false;
            //Obtener el objeto que deseas actualizar desde la base de datos: 
            var empleado = await dbempleadosContext.Empleados.FirstOrDefaultAsync(e => e.NoEmpleado == id.NoEmpleado);
            //Verificamos que no sea nulo el objeto
            if (empleado != null) {
                //Elementos actualizables
                //No se pone la entidad federativa ya que esta relacionado con el departamento
                //evaluacion de elementos que pueden ir nulos
                if (id.IdDirDeparment == 0)//Asignamos valor por default
                {
                    id.IdDirDeparment = empleado.DepartamentoId;
                }                 
                if (id.IdJob == 0) {
                    id.IdJob = empleado.PuestoId;
                }
                if (id.IsActive == null)
                {
                    id.IsActive = empleado.StatusEmpleado;
                }
                empleado.DepartamentoId = id.IdDirDeparment;
                empleado.PuestoId = (int)id.IdJob;
                empleado.StatusEmpleado = id.IsActive;
                empleado.Extension = id.Ext;
                flag = true;    
            }
            await dbempleadosContext.SaveChangesAsync();
            return flag;
        }
    }
}
