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
    public class MicroLogica
    {

        private static MicroLogica _instancia = null;
        private ExamenMicroDAO _microDAO = new ExamenMicroDAO();

        public static MicroLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new MicroLogica();
                }
                return _instancia;
            }
        }

        private MicroLogica() { }

        public bool GuardarExamen(MicroM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _microDAO.GuardarMicro(examen, idPaciente);
        }
    }
}

