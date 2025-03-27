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
    public class VariosLogica
    {

        private static VariosLogica _instancia = null;
        private ExamenVariosDAO _variosDAO = new ExamenVariosDAO();

        public static VariosLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new VariosLogica();
                }
                return _instancia;
            }
        }
        public bool ActualizarExamen(VariosM examen, int idPaciente)
        {
            return _variosDAO.Actualizar(examen, idPaciente);
        }

        public VariosM ObtenerExamenPorPaciente(int idPaciente)
        {
            return _variosDAO.ObtenerPorPaciente(idPaciente);
        }

        private VariosLogica() { }

        public bool GuardarExamen(VariosM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _variosDAO.Guardar(examen, idPaciente);
        }
    }
}
