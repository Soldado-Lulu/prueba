using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prueba.Modelo;
using System.Configuration;
using System.Data.SQLite;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
using prueba.DAO;
using prueba.Modelo;
namespace prueba.Logica_Servicio
{

    public class OrinaLogica
    {
        private static OrinaLogica _instancia = null;
        private ExamenOrinaDAO _orinaDAO = new ExamenOrinaDAO();
        public static OrinaLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new OrinaLogica();
                }
                return _instancia;
            }
        }
        private OrinaLogica() { }
        public bool GuardarExamen(OrinaM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _orinaDAO.Guardar(examen, idPaciente);
        }
        public OrinaM ObtenerExamenPorPaciente(int idPaciente)
        {
            return _orinaDAO.ObtenerPorIdPaciente(idPaciente);
        }
        public bool ActualizarExamen(OrinaM examen, int idPaciente)
        {
            return _orinaDAO.Actualizar(examen, idPaciente);
        }

    }
}
