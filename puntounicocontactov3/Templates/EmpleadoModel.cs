using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates
{
    /*Ficha general del empleado*/
    public class EmpleadoModel
    {
        public int NoEmpleado { get; set; }
        public string? Title { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Ext { get; set; }
        public string? Job { get; set; }
        public string? DirDepartment { get; set; }
        public string? SupDeparment { get; set; }
        public string? Phone { get; set; }
        public string? Locate { get; set; }
        public string? State { get; set; }
        public byte IsActive { get; set; }
        public int? IdJob { get; set; }
        public int IdDirDeparment { get; set; }
        public int? IdSupDeparment { get; set; }
        public int IdLocate { get; set; }
    }
}
