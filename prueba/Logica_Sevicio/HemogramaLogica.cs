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
    

    public HemogramaM ObtenerExamenPorPaciente(int idPaciente)
        {
            if (idPaciente <= 0)
                return null; // Retorna null si el idPaciente no es válido

            return _hemogramaDAO.ObtenerHemogramaPorPaciente(idPaciente);
        }
        public bool GuardarNuevoPaciente(HemogramaM hemograma, PacienteM paciente)
        {
            bool respuesta = false;

            try
            {
                // Llamar al método GuardarNuevoPacienteYHemograma para guardar el paciente y el hemograma
                respuesta = _hemogramaDAO.GuardarNuevoPacienteYHemograma(hemograma, paciente);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error al guardar el nuevo paciente y hemograma:\n{ex.Message}",
                                                     "Error", System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
            }

            return respuesta;
        }
        public bool ActualizarExamen(HemogramaM examen, int idPaciente)
        {
            return _hemogramaDAO.Actualizar(examen, idPaciente);
        }

    }
}
