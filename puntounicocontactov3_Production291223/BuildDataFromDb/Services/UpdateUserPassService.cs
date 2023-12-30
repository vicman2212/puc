using BuildDataFromDb.Interface;
using Microsoft.EntityFrameworkCore;
using Templates;
using Webpuc;

namespace BuildDataFromDb.Services
{
    public class UpdateUserPassService : IUpdateData<UserIdentityModel>
    {
        private readonly AccesoFircoContext accesoFircoContext;
        public char flag = 'f';
        public UpdateUserPassService(AccesoFircoContext accesoFircoContext)
        {
            this.accesoFircoContext = accesoFircoContext;
        }

        public async Task<char> UpdateDataAsync(UserIdentityModel id)
        {
            //Proceso de reseteo de contraseña para usuario logeado             
            try
            {
                var objusuario = await accesoFircoContext.Usuarios.FirstOrDefaultAsync(e => e.Usuario1 == id.Usuario);
                if (objusuario != null)
                {
                    if (objusuario.Pass != id.Password)//Validar que no ingrese la misma contraseña 
                    {
                        //1 Actualizar bandera de control
                        objusuario.PassCtrl = 1;
                        //Actualizar password
                        objusuario.Pass = id.Password;
                        await accesoFircoContext.SaveChangesAsync();   //Actualizacion de contraseña temporal 
                        flag = 'v';
                    }
                    else {//Esta enviando la misma contraseña
                        flag = 'e';
                    }                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error al actualizar el password: " + e.Message);
            }  
            return flag;
        }
    }
}
