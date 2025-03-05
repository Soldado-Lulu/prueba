using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.Modelo
{
    public class HemogramaM :ExamenBase
    {
        public int IdHemograma { get; set; }
        public int IdPaciente { get; set; }

        public string Eritrocitos { get; set; }
        public string Leucocitos { get; set; }
        public string Hemoglobina { get; set; }
        public string Hematocrito { get; set; }
        public string Plaquetas { get; set; }

        public string Mielocitos { get; set; }

        public string Melamielocitos { get; set; }
        public string Cayados { get; set; }
        public string Segmentados { get; set; }
        public string Linfocitos { get; set; }
        public string Monocitos { get; set; }
        public string Eosinofilos { get; set; }
        public string Basofilos { get; set; }
        public string VES1 { get; set; }
        public string VES2 { get; set; }
        public string Ik { get; set; }
        public string GrupoSanguineo { get; set; }
        public string Factor { get; set; }
        public string TiempoSangria { get; set; }
        public string TiempoCoagulacion { get; set; }
        public string TiempoProtrombina { get; set; }
        public string PorcentajeActividad { get; set; }
        public string Aptt { get; set; }
        public string SerieRoja { get; set; }
        public string SerieBlanca { get; set; }
    }
}
