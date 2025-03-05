using prueba.DAO;
using prueba.Modelo;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.Logica_Sevicio
{
    public class HemogramaLogica
    {
        private static HemogramaLogica _instancia = null;
        private ExamenHemogramaDAO _hemogramaDAO = new ExamenHemogramaDAO();

        public static HemogramaLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new HemogramaLogica();
                }
                return _instancia;
            }
        }

        private HemogramaLogica() { }

        public bool GuardarExamen(HemogramaM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _hemogramaDAO.Guardar(examen, idPaciente);
        }
    }
}
