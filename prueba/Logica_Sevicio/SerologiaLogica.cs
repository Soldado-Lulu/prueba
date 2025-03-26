
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
    public class SerologiaLogica
    {

        private static SerologiaLogica _instancia = null;
        private ExamenSerologiaDAO _blancoDAO = new ExamenSerologiaDAO();

        public static SerologiaLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new SerologiaLogica();
                }
                return _instancia;
            }
        }
        public bool ActualizarExamen(SerologiaM examen, int idPaciente)
        {
            return _blancoDAO.ActualizarSerologia(examen, idPaciente);
        }

        public SerologiaM ObtenerExamenPorPaciente(int idPaciente)
        {
            return _blancoDAO.ObtenerPorPaciente(idPaciente);
        }

        private SerologiaLogica() { }

        public bool GuardarExamen(SerologiaM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _blancoDAO.GuardarSerologia(examen, idPaciente);
        }
    }
}
