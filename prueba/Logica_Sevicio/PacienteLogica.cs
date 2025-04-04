﻿using System;
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
        public List<string> ObtenerMedicosUnicos()
        {
            List<string> medicos = new List<string>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT DISTINCT Medico FROM Paciente WHERE Medico IS NOT NULL AND Medico <> ''";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        medicos.Add(dr["Medico"].ToString());
                    }
                }
            }
            return medicos;
        }

        public DataTable ObtenerPacientesPorMedico(string medico)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"
        SELECT 
            p.IdPaciente AS 'ID Paciente', 
            p.Nombre, 
            p.Apellido, 
p.ApellidoM,
            p.Telefono, p.Fecha, 
p.Medico,
            p.Medico AS 'Doctor', 
            p.SaldoMedico AS 'Monto',
            CASE WHEN o.IdPaciente IS NOT NULL THEN 'Orina' ELSE '' END AS 'Orina',
            CASE WHEN q.IdPaciente IS NOT NULL THEN 'Quimica' ELSE '' END AS 'Quimica',
            CASE WHEN h.IdPaciente IS NOT NULL THEN 'Hemograma' ELSE '' END AS 'Hemograma',
            CASE WHEN s.IdPaciente IS NOT NULL THEN 'Serologia' ELSE '' END AS 'Serologia',
            CASE WHEN hc.IdPaciente IS NOT NULL THEN 'HCG' ELSE '' END AS 'HCG',
            CASE WHEN c.IdPaciente IS NOT NULL THEN 'Coproparasitologia' ELSE '' END AS 'Coproparasitologia',
            CASE WHEN m.IdPaciente IS NOT NULL THEN 'Microbiologia' ELSE '' END AS 'Microbiologia',
            CASE WHEN v.IdPaciente IS NOT NULL THEN 'Varios' ELSE '' END AS 'Varios',
            CASE WHEN b.IdPaciente IS NOT NULL THEN 'Blanco' ELSE '' END AS 'Blanco'
        FROM Paciente p
        LEFT JOIN Orina o ON p.IdPaciente = o.IdPaciente
        LEFT JOIN Quimica q ON p.IdPaciente = q.IdPaciente
        LEFT JOIN Hematologia h ON p.IdPaciente = h.IdPaciente
        LEFT JOIN Serologia s ON p.IdPaciente = s.IdPaciente
        LEFT JOIN HCG hc ON p.IdPaciente = hc.IdPaciente
        LEFT JOIN Copros c ON p.IdPaciente = c.IdPaciente
        LEFT JOIN Microbiologia m ON p.IdPaciente = m.IdPaciente
        LEFT JOIN Varios v ON p.IdPaciente = v.IdPaciente
        LEFT JOIN Blanco b ON p.IdPaciente = b.IdPaciente
        WHERE p.Medico = @medico
        ORDER BY p.IdPaciente DESC";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@medico", medico);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // 🔹 Guardar Paciente
        public int GuardarYDevolverId(PacienteM obj)
        {
            int idGenerado = -1;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"INSERT INTO Paciente 
    (Nombre, Apellido, Telefono, Edad, Medico, Fecha, Cuenta, Porcentaje, SaldoMedico, SaldoLab,ApellidoM) 
    VALUES 
    (@nombre, @apellido, @telefono, @edad, @medico, @fecha, @cuenta, @porcentaje, @saldoMedico, @saldoLab,@apellidoM);
    SELECT last_insert_rowid();";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("@apellidoM", obj.ApellidoM);

                    cmd.Parameters.AddWithValue("@telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@edad", obj.Edad);
                    cmd.Parameters.AddWithValue("@medico", obj.Medico);
                    cmd.Parameters.AddWithValue("@fecha", obj.Fecha);
                    cmd.Parameters.AddWithValue("@cuenta", obj.Cuenta);
                    cmd.Parameters.AddWithValue("@porcentaje", obj.Porcentaje);
                    cmd.Parameters.AddWithValue("@saldoMedico", obj.SaldoMedico);
                    cmd.Parameters.AddWithValue("@saldoLab", obj.SaldoLab);


                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idGenerado = Convert.ToInt32(result);
                    }
                }
            }
            return idGenerado;
        }
        public DataTable ObtenerPacientesPorFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();

                string query = @"
        SELECT 
            p.IdPaciente AS 'ID Paciente', 
            p.Nombre, 
            p.Apellido, 
           p.ApellidoM,

            p.Fecha, 
            p.Medico AS 'Doctor', 
            p.SaldoMedico AS 'Monto',
            CASE WHEN o.IdPaciente IS NOT NULL THEN 'Orina' ELSE '' END AS 'Orina',
            CASE WHEN q.IdPaciente IS NOT NULL THEN 'Quimica' ELSE '' END AS 'Quimica',
            CASE WHEN h.IdPaciente IS NOT NULL THEN 'Hemograma' ELSE '' END AS 'Hemograma',
            CASE WHEN s.IdPaciente IS NOT NULL THEN 'Serologia' ELSE '' END AS 'Serologia',
            CASE WHEN hc.IdPaciente IS NOT NULL THEN 'HCG' ELSE '' END AS 'HCG',
            CASE WHEN c.IdPaciente IS NOT NULL THEN 'Coproparasitologia' ELSE '' END AS 'Coproparasitologia',
            CASE WHEN m.IdPaciente IS NOT NULL THEN 'Microbiologia' ELSE '' END AS 'Microbiologia',
            CASE WHEN v.IdPaciente IS NOT NULL THEN 'Varios' ELSE '' END AS 'Varios',
            CASE WHEN b.IdPaciente IS NOT NULL THEN 'Blanco' ELSE '' END AS 'Blanco'
        FROM Paciente p
        LEFT JOIN Orina o ON p.IdPaciente = o.IdPaciente
        LEFT JOIN Quimica q ON p.IdPaciente = q.IdPaciente
        LEFT JOIN Hematologia h ON p.IdPaciente = h.IdPaciente
        LEFT JOIN Serologia s ON p.IdPaciente = s.IdPaciente
        LEFT JOIN HCG hc ON p.IdPaciente = hc.IdPaciente
        LEFT JOIN Copros c ON p.IdPaciente = c.IdPaciente
        LEFT JOIN Microbiologia m ON p.IdPaciente = m.IdPaciente
        LEFT JOIN Varios v ON p.IdPaciente = v.IdPaciente
        LEFT JOIN Blanco b ON p.IdPaciente = b.IdPaciente
        WHERE DATE(p.Fecha) BETWEEN DATE(@fechaDesde) AND DATE(@fechaHasta)
        ORDER BY p.IdPaciente DESC";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@fechaDesde", fechaDesde.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@fechaHasta", fechaHasta.ToString("yyyy-MM-dd"));

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public bool Editar(PacienteM obj)
            {
                bool respuesta = true;
                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    string query = "UPDATE Paciente SET Nombre = @nombre, Apellido = @apellido, Telefono = @telefono, Edad = @edad,Medico =@medico,ApellidoM=@apellidoM WHERE IdPaciente = @id";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", obj.IdPaciente);
                        cmd.Parameters.AddWithValue("@nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("@apellidoM", obj.ApellidoM);
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
                string query = "SELECT * FROM Paciente";  // Obtener todos los campos

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaPacientes.Add(new PacienteM()
                            {
                                IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                ApellidoM = dr["ApellidoM"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Edad = dr["Edad"].ToString(),
                                Medico = dr["Medico"].ToString(),
                                Fecha = dr["Fecha"].ToString(),
                                Cuenta = Convert.ToSingle(dr["Cuenta"]),
                                Porcentaje = Convert.ToSingle(dr["Porcentaje"]),
                               

                                SaldoMedico = Convert.ToSingle(dr["SaldoMedico"]),
                                SaldoLab = Convert.ToSingle(dr["SaldoLab"])
                            });
                        }
                    }
                }
            }
            return listaPacientes;
        }

       
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
                                ApellidoM = reader.GetString(reader.GetOrdinal("ApellidoM")),

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
        SELECT 
            p.IdPaciente AS 'ID Paciente', 
            p.Nombre, 
            p.Apellido, 
p.ApellidoM, 
            p.Fecha,
p.Medico,
            p.SaldoMedico AS 'Monto',
            CASE WHEN o.IdPaciente IS NOT NULL THEN 'Orina' ELSE '' END AS 'Orina',
            CASE WHEN q.IdPaciente IS NOT NULL THEN 'Quimica' ELSE '' END AS 'Quimica',
            CASE WHEN h.IdPaciente IS NOT NULL THEN 'Hemograma' ELSE '' END AS 'Hemograma',
            CASE WHEN s.IdPaciente IS NOT NULL THEN 'Serologia' ELSE '' END AS 'Serologia',
            CASE WHEN hc.IdPaciente IS NOT NULL THEN 'HCG' ELSE '' END AS 'HCG',
            CASE WHEN c.IdPaciente IS NOT NULL THEN 'Coproparasitologia' ELSE '' END AS 'Coproparasitologia',
            CASE WHEN m.IdPaciente IS NOT NULL THEN 'Microbiologia' ELSE '' END AS 'Microbiologia',
            CASE WHEN v.IdPaciente IS NOT NULL THEN 'Varios' ELSE '' END AS 'Varios',
            CASE WHEN b.IdPaciente IS NOT NULL THEN 'Blanco' ELSE '' END AS 'Blanco'
        FROM Paciente p
        LEFT JOIN Orina o ON p.IdPaciente = o.IdPaciente
        LEFT JOIN Quimica q ON p.IdPaciente = q.IdPaciente
        LEFT JOIN Hematologia h ON p.IdPaciente = h.IdPaciente
        LEFT JOIN Serologia s ON p.IdPaciente = s.IdPaciente
        LEFT JOIN HCG hc ON p.IdPaciente = hc.IdPaciente
        LEFT JOIN Copros c ON p.IdPaciente = c.IdPaciente
        LEFT JOIN Microbiologia m ON p.IdPaciente = m.IdPaciente
        LEFT JOIN Varios v ON p.IdPaciente = v.IdPaciente
        LEFT JOIN Blanco b ON p.IdPaciente = b.IdPaciente
        ORDER BY p.IdPaciente DESC;";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public DataTable ObtenerPacientesPorMedicoYFecha(string medico, DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"
        SELECT 
            p.IdPaciente AS 'ID Paciente', 
            p.Nombre, 
            p.Apellido, 
            p.ApellidoM,
            p.Fecha, 
            p.Medico AS 'Doctor', 
            p.SaldoMedico AS 'Monto Total',
            CASE WHEN o.IdPaciente IS NOT NULL THEN 'Orina' ELSE '' END AS 'Orina',
            CASE WHEN q.IdPaciente IS NOT NULL THEN 'Quimica' ELSE '' END AS 'Quimica',
            CASE WHEN h.IdPaciente IS NOT NULL THEN 'Hemograma' ELSE '' END AS 'Hemograma',
            CASE WHEN s.IdPaciente IS NOT NULL THEN 'Serologia' ELSE '' END AS 'Serologia',
            CASE WHEN hc.IdPaciente IS NOT NULL THEN 'HCG' ELSE '' END AS 'HCG',
            CASE WHEN c.IdPaciente IS NOT NULL THEN 'Coproparasitologia' ELSE '' END AS 'Coproparasitologia',
            CASE WHEN m.IdPaciente IS NOT NULL THEN 'Microbiologia' ELSE '' END AS 'Microbiologia',
            CASE WHEN v.IdPaciente IS NOT NULL THEN 'Varios' ELSE '' END AS 'Varios',
            CASE WHEN b.IdPaciente IS NOT NULL THEN 'Blanco' ELSE '' END AS 'Blanco'
        FROM Paciente p
        LEFT JOIN Orina o ON p.IdPaciente = o.IdPaciente
        LEFT JOIN Quimica q ON p.IdPaciente = q.IdPaciente
        LEFT JOIN Hematologia h ON p.IdPaciente = h.IdPaciente
        LEFT JOIN Serologia s ON p.IdPaciente = s.IdPaciente
        LEFT JOIN HCG hc ON p.IdPaciente = hc.IdPaciente
        LEFT JOIN Copros c ON p.IdPaciente = c.IdPaciente
        LEFT JOIN Microbiologia m ON p.IdPaciente = m.IdPaciente
        LEFT JOIN Varios v ON p.IdPaciente = v.IdPaciente
        LEFT JOIN Blanco b ON p.IdPaciente = b.IdPaciente
        WHERE p.Medico = @medico AND p.Fecha BETWEEN @desde AND @hasta
        ORDER BY p.IdPaciente DESC";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@medico", medico);
                    cmd.Parameters.AddWithValue("@desde", desde.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@hasta", hasta.ToString("yyyy-MM-dd"));
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dt);
                }
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
                    case "Hemograma":
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
                                    IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    ApellidoM = dr["ApellidoM"].ToString(),
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

        public DataTable ObtenerPacientesPorNombreApellido(string nombre, string apellido,string apellidoM)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"
        SELECT 
            p.IdPaciente AS 'ID Paciente', 
            p.Nombre, 
            p.Apellido, 
            p.ApellidoM, 
            p.Fecha,
            p.Medico,
            p.SaldoMedico AS 'Monto',
            CASE WHEN o.IdPaciente IS NOT NULL THEN 'Orina' ELSE '' END AS 'Orina',
            CASE WHEN q.IdPaciente IS NOT NULL THEN 'Quimica' ELSE '' END AS 'Quimica',
            CASE WHEN h.IdPaciente IS NOT NULL THEN 'Hemograma' ELSE '' END AS 'Hemograma',
            CASE WHEN s.IdPaciente IS NOT NULL THEN 'Serologia' ELSE '' END AS 'Serologia',
            CASE WHEN hc.IdPaciente IS NOT NULL THEN 'HCG' ELSE '' END AS 'HCG',
            CASE WHEN c.IdPaciente IS NOT NULL THEN 'Coproparasitologia' ELSE '' END AS 'Coproparasitologia',
            CASE WHEN m.IdPaciente IS NOT NULL THEN 'Microbiologia' ELSE '' END AS 'Microbiologia',
            CASE WHEN v.IdPaciente IS NOT NULL THEN 'Varios' ELSE '' END AS 'Varios',
            CASE WHEN b.IdPaciente IS NOT NULL THEN 'Blanco' ELSE '' END AS 'Blanco'
        FROM Paciente p
        LEFT JOIN Orina o ON p.IdPaciente = o.IdPaciente
        LEFT JOIN Quimica q ON p.IdPaciente = q.IdPaciente
        LEFT JOIN Hematologia h ON p.IdPaciente = h.IdPaciente
        LEFT JOIN Serologia s ON p.IdPaciente = s.IdPaciente
        LEFT JOIN HCG hc ON p.IdPaciente = hc.IdPaciente
        LEFT JOIN Copros c ON p.IdPaciente = c.IdPaciente
        LEFT JOIN Microbiologia m ON p.IdPaciente = m.IdPaciente
        LEFT JOIN Varios v ON p.IdPaciente = v.IdPaciente
        LEFT JOIN Blanco b ON p.IdPaciente = b.IdPaciente
        WHERE (p.Nombre LIKE @nombre) AND (p.Apellido LIKE @apellido) AND (p.ApellidoM LIKE @apellidoM) 
        ORDER BY p.IdPaciente DESC";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%"); // Filtro parcial
                    cmd.Parameters.AddWithValue("@apellido", "%" + apellido + "%"); // Filtro parcial
                    cmd.Parameters.AddWithValue("@apellidoM", "%" + apellidoM + "%"); // Filtro parcial

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }


    }
}
