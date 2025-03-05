using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.Modelo
{
    public class CoprosM:ExamenBase
    {
        public string Consistencia { get; set; }
        public string Color { get; set; }
        public string ExamenM { get; set; }
        public string Observaciones { get; set; }
        public int IdCopros { get; set; }
        public int IdPaciente { get; set; }

    }
}
