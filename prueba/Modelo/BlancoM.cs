﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prueba.Modelo;

namespace Prueba.Modelo
{
    public class BlancoM : ExamenBase
    {
        public string Muestra { get; set; }
        public string Examen { get; set; }
        public string Datos { get; set; }
        public string Otros { get; set; }
        public string Paciente { get; set; }
        public string Valores { get; set; }
        public int IdBlanco { get; set; }
        public int IdPaciente { get; set; }

    }
}
