using Microsoft.EntityFrameworkCore;
using Webpuc;

namespace MiscApp.Services
{
    public class GenerarCorreoService
    {
        private readonly string DOMINIO = "@firco.gob.mx";
        private readonly DbempleadosContext empleados;

        public GenerarCorreoService(DbempleadosContext empleados) {
            this.empleados = empleados;
        }

        public async Task<string> GenerateEMail(string nombres, string apellP, string apellM)
        {
            
            string correoBuild = string.Empty;
            string[] arrayNombres = nombres.Split(' ');
            string[] colCorreos = new string[1000];
            int x = 0;
            //1.-Proponer la construccion de un correo
            correoBuild = arrayNombres[0]+ "." + apellP + DOMINIO;

            //2.-Validar que no exista la cuenta de correo propuesta
            var empleado = await empleados.Empleados.AsNoTracking().ToListAsync();
            foreach (var item in empleado.AsQueryable())
            {
                colCorreos[x] = item.Correo;//Lo mandamos a un array de datos 
                x++;                
            }
            for (int i = 0; i < colCorreos.Length; i++)
            {
                if (colCorreos[i] == correoBuild.ToLower())//Mientras el correo generado sea igual a lo que hay en la base de datos
                {
                    i = 0; //Reiniciamos el bucle para volver a evaluar el correo
                    if (arrayNombres.Length > 1)
                    { //si viene un segundo nombre tomamos ese
                      //Generar otro correo
                        correoBuild = arrayNombres[1] + "." + apellP + DOMINIO;
                    }
                    else
                    {
                        char[] arrayapellP = apellP.ToCharArray();
                        correoBuild = arrayNombres[0] + "." + arrayapellP[0] + apellM;
                    }
                }
            }
            return correoBuild.ToLower();
        }
    }
}
