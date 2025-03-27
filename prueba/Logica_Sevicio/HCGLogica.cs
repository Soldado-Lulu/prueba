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
    public class HCGLogica
    {
        private static HCGLogica _instancia = null;
        private ExamenHCGDAO _hcgDAO = new ExamenHCGDAO();

        public static HCGLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new HCGLogica();
                }
                return _instancia;
            }
        }
        public HCGM ObtenerExamenPorPaciente(int idPaciente)
        {
            return _hcgDAO.ObtenerPorIdPaciente(idPaciente);
        }

        public bool ActualizarExamen(HCGM examen, int idPaciente)
        {
            return _hcgDAO.Actualizar(examen, idPaciente);
        }

        private HCGLogica() { }

        public bool GuardarExamen(HCGM examen, int idPaciente)
        {
            if (idPaciente <= 0 || string.IsNullOrEmpty(examen.Resultado))
            {
                Console.WriteLine("ID del paciente inválido o resultado vacío.");
                return false;
            }

            return _hcgDAO.Guardar(examen, idPaciente);
        }

    }
}
