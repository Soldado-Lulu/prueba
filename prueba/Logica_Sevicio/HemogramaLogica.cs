using prueba.DAO;
using prueba.Modelo;

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
        return _hemogramaDAO.Guardar(examen, idPaciente);
    }

    public bool ActualizarExamen(HemogramaM examen, int idPaciente)
    {
        return _hemogramaDAO.Actualizar(examen, idPaciente);
    }

    public HemogramaM ObtenerExamenPorPaciente(int idPaciente)
    {
        return _hemogramaDAO.ObtenerPorIdPaciente(idPaciente);
    }
}
