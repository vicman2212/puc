//Plantilla para definir los datos de insercion de un nuevo empleado
using System.ComponentModel.DataAnnotations;

namespace Templates

{
    public class InsertEmpModel
    {
        //[Required (ErrorMessage = "Ingrese el número del empleado")]
        //[RegularExpression(@"^[\d]{1,5}$",ErrorMessage = "Este campo solo acepta valores numericos")]
        public int NoEmpleado { get; set; }
        //[Range(0.01, double.MaxValue, ErrorMessage = "Selecciona el puesto y el area")]
        public int PuestoId { get; set; }
        //[Range(0.01, double.MaxValue, ErrorMessage = "Selecciona la ubicacion y el area")]
        public int DepartamentoId { get; set; }
        public int TituloId { get; set; } = 0;
        //[Required (ErrorMessage = "Debe de ingresar los nombres")]
        //[RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\\s\\s]+$", ErrorMessage = "Solo puede contener letras y espacios")]
        public string Nombres { get; set; }
        //[Required (ErrorMessage ="Debe ingresar el apellido paterno")]
        //[RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\\s\\s]+$", ErrorMessage = "Solo puede contener letras y espacios")]
        public string ApellidoPaterno { get; set; }
        //[Required(ErrorMessage = "Debe ingresar el apellido paterno")]
        //[RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\\s\\s]+$", ErrorMessage = "Solo puede contener letras y espacios")]
        public string ApellidoMaterno { get; set; }
        public string? Correo { get; set; }
        public string? Extension { get; set; }
        public byte StatusEmpleado { get; set; } = 1;
        public byte Delegado { get; set; } = 1;
        public string? Rfc { get; set; } = null;
        public string? Curp { get; set; } = null;
    }
}
