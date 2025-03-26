using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace prueba.DAO
{
    public class PacienteDAO
    {
        public int Crear(PacienteM paciente)
        {
            int nuevoId = -1;

            using (var conexion = ConexionSQLite.ObtenerConexion())
            {
                conexion.Open();
                string query = @"
                    INSERT INTO Paciente (Nombre, Apellido, Edad, Telefono, Medico, Fecha, Cuenta, Porcentaje, SaldoMedico, SaldoLab)
                    VALUES (@Nombre, @Apellido, @Edad, @Telefono, @Medico, @Fecha, @Cuenta, @Porcentaje, @SaldoMedico, @SaldoLab);
                    SELECT last_insert_rowid();";

                using (var cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", paciente.Apellido);
                    cmd.Parameters.AddWithValue("@Edad", paciente.Edad);
                    cmd.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                    cmd.Parameters.AddWithValue("@Medico", paciente.Medico);
                    cmd.Parameters.AddWithValue("@Fecha", paciente.Fecha);
                    cmd.Parameters.AddWithValue("@Cuenta", paciente.Cuenta);
                    cmd.Parameters.AddWithValue("@Porcentaje", paciente.Porcentaje);
                    cmd.Parameters.AddWithValue("@SaldoMedico", paciente.SaldoMedico);
                    cmd.Parameters.AddWithValue("@SaldoLab", paciente.SaldoLab);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        nuevoId = Convert.ToInt32(result);
                }
            }

            return nuevoId;
        }

        public bool Actualizar(PacienteM paciente)
        {
            using (var conexion = ConexionSQLite.ObtenerConexion())
            {
                conexion.Open();
                string query = @"UPDATE Paciente SET 
                                Nombre = @Nombre,
                                Apellido = @Apellido,
                                Edad = @Edad,
                                Telefono = @Telefono,
                                Medico = @Medico,
                                Fecha = @Fecha,
                                Cuenta = @Cuenta,
                                Porcentaje = @Porcentaje,
                                SaldoMedico = @SaldoMedico,
                                SaldoLab = @SaldoLab
                                WHERE IdPaciente = @IdPaciente";

                using (var cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", paciente.Apellido);
                    cmd.Parameters.AddWithValue("@Edad", paciente.Edad);
                    cmd.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                    cmd.Parameters.AddWithValue("@Medico", paciente.Medico);
                    cmd.Parameters.AddWithValue("@Fecha", paciente.Fecha);
                    cmd.Parameters.AddWithValue("@Cuenta", paciente.Cuenta);
                    cmd.Parameters.AddWithValue("@Porcentaje", paciente.Porcentaje);
                    cmd.Parameters.AddWithValue("@SaldoMedico", paciente.SaldoMedico);
                    cmd.Parameters.AddWithValue("@SaldoLab", paciente.SaldoLab);
                    cmd.Parameters.AddWithValue("@IdPaciente", paciente.IdPaciente);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (var conexion = ConexionSQLite.ObtenerConexion())
            {
                conexion.Open();
                string query = "DELETE FROM Paciente WHERE IdPaciente = @IdPaciente";

                using (var cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdPaciente", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public PacienteM ObtenerPorId(int id)
        {
            using (var conexion = ConexionSQLite.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT * FROM Paciente WHERE IdPaciente = @IdPaciente";

                using (var cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdPaciente", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PacienteM
                            {
                                IdPaciente = Convert.ToInt32(reader["IdPaciente"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Edad = reader["Edad"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Medico = reader["Medico"].ToString(),
                                Fecha = reader["Fecha"].ToString(),
                                Cuenta = reader["Cuenta"] != DBNull.Value ? Convert.ToDecimal(reader["Cuenta"]) : 0,
                                Porcentaje = reader["Porcentaje"] != DBNull.Value ? Convert.ToDecimal(reader["Porcentaje"]) : 0,
                                SaldoMedico = Convert.ToDecimal(reader["SaldoMedico"]),
                                SaldoLab = Convert.ToDecimal(reader["SaldoLab"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public List<PacienteM> ListarTodos()
        {
            var lista = new List<PacienteM>();
            using (var conexion = ConexionSQLite.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT * FROM Paciente ORDER BY IdPaciente DESC";

                using (var cmd = new SQLiteCommand(query, conexion))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new PacienteM
                            {
                                IdPaciente = Convert.ToInt32(reader["IdPaciente"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Edad = reader["Edad"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Medico = reader["Medico"].ToString(),
                                Fecha = reader["Fecha"].ToString(),
                                Cuenta = reader["Cuenta"] != DBNull.Value ? Convert.ToDecimal(reader["Cuenta"]) : 0,
                                Porcentaje = reader["Porcentaje"] != DBNull.Value ? Convert.ToDecimal(reader["Porcentaje"]) : 0,
                                SaldoMedico = Convert.ToDecimal(reader["SaldoMedico"]),
                                SaldoLab = Convert.ToDecimal(reader["SaldoLab"])
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
