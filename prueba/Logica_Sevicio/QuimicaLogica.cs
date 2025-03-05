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
    public class QuimicaLogica
    {


        private static QuimicaLogica _instancia = null;
        private ExamenQuimicaDAO _quimicaDAO = new ExamenQuimicaDAO();

        public static QuimicaLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new QuimicaLogica();
                }
                return _instancia;
            }
        }

        private QuimicaLogica() { }

        public bool GuardarExamen(QuimicaM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _quimicaDAO.GuardarQuimica(examen, idPaciente);
        }
    }
}
