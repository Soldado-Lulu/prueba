using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.Modelo
{
    public class HCGM :ExamenBase
    {
        public int IdHCG { get; set; }
        public int IdPaciente { get; set; }
        public string Resultado { get; set; }
    }
}
