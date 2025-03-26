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
    public class CoprosLogica
    {
        private static CoprosLogica _instancia = null;
        private ExamenCoprosDAO _blancoDAO = new ExamenCoprosDAO();
        public bool ActualizarExamen(CoprosM examen, int idPaciente)
        {
            return _blancoDAO.Actualizar(examen, idPaciente);
        }

        public static CoprosLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CoprosLogica();
                }
                return _instancia;
            }
        }
        public CoprosM ObtenerExamenPorPaciente(int idPaciente)
        {
            return _blancoDAO.ObtenerPorIdPaciente(idPaciente);
        }

        private CoprosLogica() { }

        public bool GuardarExamen(CoprosM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _blancoDAO.Guardar(examen, idPaciente);
        }
    }
}
