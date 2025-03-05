using prueba.DAO;
using prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.Logica_Sevicio
{
    public class SobreLogica
    {
        private static SobreLogica _instancia = null;
        private ExamenSobreDAO _blancoDAO = new ExamenSobreDAO();

        public static SobreLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new SobreLogica();
                }
                return _instancia;
            }
        }

        private SobreLogica() { }

        public bool GuardarExamen(SobreM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _blancoDAO.GuardarSobre(examen, idPaciente);
        }
    }
}
