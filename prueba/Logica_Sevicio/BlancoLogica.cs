using prueba.Logica;
using prueba.Modelo;
using prueba.DAO;
using prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SQLite;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
namespace prueba.Logica_Sevicio
{
    public class BlancoLogica
    {
        private static BlancoLogica _instancia = null;
        private ExamenBlancoDAO _blancoDAO = new ExamenBlancoDAO();

        public static BlancoLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new BlancoLogica();
                }
                return _instancia;
            }
        }

        private BlancoLogica() { }

        public bool GuardarExamen(BlancoM examen, int idPaciente)
        {
            if (idPaciente <= 0)
                return false; // No se puede guardar sin un paciente
            return _blancoDAO.Guardar(examen, idPaciente);
        }
    }
}
