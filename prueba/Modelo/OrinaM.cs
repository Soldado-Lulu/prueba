using prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio.Modelo
{
    public class OrinaM : ExamenBase
    {
        public int IdOrina { get; set; }
        public int IdPaciente { get; set; } // Relación con el Paciente
        public string Aspecto { get; set; }
        public string Color { get; set; }
        public string Olor { get; set; }
        public string Densidad { get; set; }
        public string Reaccion { get; set; }
        public string Glucosa { get; set; }
        public string Bilirrubina { get; set; }
        public string Cetonas { get; set; }
        public string Sangre { get; set; }
        public string Proteina { get; set; }
        public string Urobiliogeno { get; set; }
        public string Nitrito { get; set; }
        public string Leucocito1 { get; set; }
        public string Eritrocito { get; set; }
        public string Leucocito2 { get; set; }
        public string CED { get; set; }
        public string Redonda { get; set; }
        public string Embarazo { get; set; }
        public string Otros { get; set; }
        public string Observaciones { get; set; }
        public string Flora { get; set; }
        public string Piocito { get; set; }
        public string Cristale { get; set; }
        public string Cilindro { get; set; }

    }
}
