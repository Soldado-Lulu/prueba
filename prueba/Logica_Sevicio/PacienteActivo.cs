using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.Logica
{
    public static class PacienteActivo
    {
        public static int IdPaciente { get;  set; }
        public static string Nombre { get;  set; }
        public static string Apellido { get;  set; }
        public static string Edad { get;  set; }
        public static string Medico { get;  set; }

        public static void EstablecerPaciente(PacienteM paciente)
        {
            IdPaciente = paciente.IdPaciente;
            Nombre = paciente.Nombre;
            Apellido = paciente.Apellido;
            Edad = paciente.Edad;
            Medico = paciente.Medico;
        }

        public static void Limpiar()
        {
            IdPaciente = 0;
            Nombre = "";
            Apellido = "";
            Edad = "";
            Medico = "";
        }
    }

}


