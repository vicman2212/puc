using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Definir el acceso a los modulos 
namespace Templates
{
    public class ModulosPerfilModel
    {
        public int idPermiso { get; set; }
        public int idPerfil { get; set; }
        public string CodigoPermiso { get; set; }
        public string CodigoModulo { get; set; }
        public string CodigoPerfil { get; set; }
        public byte ActivoPermiso { get; set; }
        public byte ActivoModulo { get; set; }
        public string NombreModulo { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Desc { get; set; }
        public string? Icon { get; set; }
        public string? NPerfil { get; set; } = null;

    }
}
