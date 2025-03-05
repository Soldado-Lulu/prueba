using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.Modelo
{
    public class MicroM:ExamenBase
    {
        public int IdMicro { get; set; }
        public int IdPaciente { get; set; }
        public string Muestra { get; set; }
        public string Gram { get; set; }
        public string M1 { get; set; }
        public string M2 { get; set; }
        public string M3 { get; set; }
        public string Cultivo { get; set; }
        public string Colonia { get; set; }
        public string Identificacion { get; set; }
        public string Sensible { get; set; }
        public string Resistencia { get; set; }
    }
}
