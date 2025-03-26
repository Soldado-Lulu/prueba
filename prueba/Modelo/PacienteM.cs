using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Modelo
{
    public class PacienteM
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Medico { get; set; }
        public string Edad { get; set; }
        public string Telefono { get; set; }
        public string Fecha { get; set; }
        public decimal? Cuenta { get; set; }
        public decimal? Porcentaje { get; set; }

        public decimal SaldoMedico { get; set; }
        public decimal SaldoLab { get; set; }

    }
}
