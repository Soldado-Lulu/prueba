using prueba.DAO;
using prueba.Modelo;

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
            return _microDAO.GuardarMicro(examen, idPaciente);
        }

        public bool ActualizarExamen(MicroM examen, int idPaciente)
        {
            return _microDAO.ActualizarMicro(examen, idPaciente);
        }

        public MicroM ObtenerExamenPorPaciente(int idPaciente)
        {
            return _microDAO.ObtenerPorPaciente(idPaciente);
        }
    }
}
