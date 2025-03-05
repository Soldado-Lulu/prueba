using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Prueba.Modelo;
using System.Data.SQLite;
using System.Data;
using prueba.DAO;

namespace prueba.Logica
{
    public class PacienteLogica
    {
     
            private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
            private static PacienteLogica _instancia = null;

            public PacienteLogica() { }

            public static PacienteLogica Instancia
            {
                get
                {
                    if (_instancia == null)
                    {
                        _instancia = new PacienteLogica();
                    }
                    return _instancia;
                }
            }

            // 🔹 Guardar Paciente
            public bool Guardar(PacienteM obj)
            {
                bool respuesta = true;
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    string query = "INSERT INTO Paciente (Nombre, Apellido, Telefono, Edad,Medico) VALUES (@nombre, @apellido, @telefono, @edad,@medico)";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
                        cmd.Parameters.AddWithValue("@telefono", obj.Telefono);
                        cmd.Parameters.AddWithValue("@edad", obj.Edad);
                    cmd.Parameters.AddWithValue("@medico", obj.Medico);


                    if (cmd.ExecuteNonQuery() < 1)
                        {
                            respuesta = false;
                        }
                    }
                }
                return respuesta;
            }
        /*
        public List<PacienteM> Listar()
        {
            List<PacienteM> lista = new List<PacienteM>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM Paciente";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new PacienteM()
                            {
                                IdPaciente = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Medico = dr["Medico"].ToString(),
                                Edad = dr["Edad"].ToString(),

                            });
                        }
                    }
                }
            }
            return lista;
        }
        -*/
        // 🔹 Editar Paciente
        public bool Editar(PacienteM obj)
            {
                bool respuesta = true;
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    string query = "UPDATE Paciente SET Nombre = @nombre, Apellido = @apellido, Telefono = @telefono, Edad = @edad,Medico =@medico WHERE IdPaciente = @id";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", obj.IdPaciente);
                        cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
                        cmd.Parameters.AddWithValue("@telefono", obj.Telefono);
                        cmd.Parameters.AddWithValue("@edad", obj.Edad);
                    cmd.Parameters.AddWithValue("@medico", obj.Medico);


                    if (cmd.ExecuteNonQuery() < 1)
                        {
                            respuesta = false;
                        }
                    }
                }
                return respuesta;
            }

            // 🔹 Eliminar Paciente
            public bool Eliminar(int id)
            {
                bool respuesta = true;
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    string query = "DELETE FROM Paciente WHERE IdPaciente = @id";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            respuesta = false;
                        }
                    }
                }
                return respuesta;
            }

        public List<PacienteM> ObtenerTodos()
        {
            List<PacienteM> listaPacientes = new List<PacienteM>();

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT IdPaciente, Nombre, Apellido FROM Paciente";
                SQLiteCommand cmd = new SQLiteCommand(query, conexion);

                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaPacientes.Add(new PacienteM()
                        {
                            IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString()
                        });
                    }
                }
            }
            return listaPacientes;
        }
       /* public int ObtenerUltimoPacienteId()
        {
            int idPaciente = -1;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT IdPaciente FROM Paciente ORDER BY IdPaciente DESC LIMIT 1";
                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    idPaciente = Convert.ToInt32(result);
                }
            }
            return idPaciente;
        }
        */
        public PacienteM ObtenerUltimoPaciente()
        {
            using (SQLiteConnection conexion = ConexionSQLite.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT * FROM Paciente ORDER BY IdPaciente DESC LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Si hay datos
                        {
                            return new PacienteM
                            {
                                IdPaciente = reader.GetInt32(reader.GetOrdinal("IdPaciente")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                                Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                Edad = reader.GetString(reader.GetOrdinal("Edad")),
                                Medico = reader.GetString(reader.GetOrdinal("Medico"))
                            };
                        }
                    }
                }
            }
            return null; // Retorna null si no hay pacientes en la BD
        }

        public DataTable ObtenerPacientesConExamenes()
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"
            SELECT p.IdPaciente AS 'ID Paciente', p.Nombre, p.Apellido, p.CI, p.Telefono, 
                   o.Aspecto, o.Color, o.Olor, o.Densidad, o.Reaccion, o.Glucosa, 
                   o.Bilirrubina, o.Cetonas, o.Sangre, o.Proteina, o.Urobiliogeno, 
                   o.Nitrito, o.Leucocito1, o.Eritrocito, o.Leucocito2, o.CED, 
                   o.Redonda, o.Embarazo, o.Otros, o.Observaciones, o.Flora, 
                   o.Piocito, o.Cristale, o.Cilindro
            FROM Paciente p
            LEFT JOIN Orina o ON p.IdPaciente = o.IdPaciente
            ORDER BY p.IdPaciente DESC";

                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }
        public DataTable ObtenerPacientesConExamenes(string tipoExamen)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();

                string query = "";

                // Dependiendo del tipo de examen, el query cambia
                switch (tipoExamen.ToLower())
                {
                    case "orina":
                        query = @"
                    SELECT p.IdPaciente AS 'ID Paciente', p.Nombre, p.Apellido, p.CI, p.Telefono, 
                           'Orina' AS Examen
                    FROM Paciente p
                    LEFT JOIN Orina o ON p.IdPaciente = o.IdPaciente
                    ORDER BY p.IdPaciente DESC";
                        break;
                    case "quimica":
                        query = @"
                    SELECT p.IdPaciente AS 'ID Paciente', p.Nombre, p.Apellido, p.CI, p.Telefono, 
                           'Quimica' AS Examen
                    FROM Paciente p
                    LEFT JOIN Quimica q ON p.IdPaciente = q.IdPaciente
                    ORDER BY p.IdPaciente DESC";
                        break;
                    case "hemograma":
                        query = @"
                    SELECT p.IdPaciente AS 'ID Paciente', p.Nombre, p.Apellido, p.CI, p.Telefono, 
                           'Hemograma' AS Examen
                    FROM Paciente p
                    LEFT JOIN Hemograma h ON p.IdPaciente = h.IdPaciente
                    ORDER BY p.IdPaciente DESC";
                        break;
                    case "micro":
                        query = @"
                    SELECT p.IdPaciente AS 'ID Paciente', p.Nombre, p.Apellido, p.CI, p.Telefono, 
                           'Micro' AS Examen
                    FROM Paciente p
                    LEFT JOIN Micro m ON p.IdPaciente = m.IdPaciente
                    ORDER BY p.IdPaciente DESC";
                        break;
                    default:
                        throw new ArgumentException("Tipo de examen no válido");
                }

                // Ejecución del query con el tipo de examen seleccionado
                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }


        // 🔹 Buscar Paciente por ID
        public PacienteM BuscarPorId(int id)
            {
                PacienteM paciente = null;
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    string query = "SELECT * FROM Paciente WHERE IdPaciente = @id";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                paciente = new PacienteM()
                                {
                                    IdPaciente = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Edad = dr["Edad"].ToString(),
                                    Medico = dr["Medico"].ToString()
                                };
                            }
                        }
                    }
                }
                return paciente;
            }

     

    }
}
