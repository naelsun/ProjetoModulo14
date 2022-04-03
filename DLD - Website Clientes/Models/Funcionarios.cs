using System;
using System.Collections.Generic;
using System.Text;

namespace EmpresaEntregas.Models
{
    public class Funcionarios
    {
        public int nfunc { get; set; }
        public string nome { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public string dispositivo { get; set; } = null!;
        public string expira { get; set; } = null!;
    }
}
