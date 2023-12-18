
using System.ComponentModel.DataAnnotations;

/*Modelo de implementacion con las reglas para los parametros de un usuarios*/
namespace Templates

{
    public class UserIdentityModel:EmpleadoModel
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(20,ErrorMessage ="Sobrepaso el numero de caracteres para el campo {0}")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(100, ErrorMessage = "Sobrepaso el numero de caracteres para el campo {0}")]
        public string Password { get; set; }
        public string? CodPerfil { get; set; }
        public string? CodRol { get; set; }
        public byte IsActive { get; set; }

    }
}
