using prueba.Modelo;
using Prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.DAO
{
    public class ExamenHemogramaDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public bool Guardar(HemogramaM obj, int idPaciente)
        {
            bool respuesta = false;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO Hematologia 
                (IdPaciente, Eritrocitos, Leucocitos, Hemoglobina, Hematocrito, Plaquetas, Mielocitos, Melamielocitos, Cayados, 
                Segmentados, Linfocitos, Monocitos, Eosinofilos, Basofilos, VES1, VES2, Ik, GrupoSanguineo, Factor, 
                TiempoSangria, TiempoCoagulacion, TiempoProtrombina, PorcentajeActividad, Aptt, SerieRoja, SerieBlanca) 
                VALUES 
                (@idPaciente, @eritrocitos, @leucocitos, @hemoglobina, @hematocrito, @plaquetas, @mielocitos, @melamielocitos, @cayados, 
                @segmentados, @linfocitos, @monocitos, @eosinofilos, @basofilos, @ves1, @ves2, @ik, @grupoSanguineo, @factor, 
                @tiempoSangria, @tiempoCoagulacion, @tiempoProtrombina, @porcentajeActividad, @aptt, @serieRoja, @serieBlanca)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@eritrocitos", obj.Eritrocitos);
                        cmd.Parameters.AddWithValue("@leucocitos", obj.Leucocitos);
                        cmd.Parameters.AddWithValue("@hemoglobina", obj.Hemoglobina);
                        cmd.Parameters.AddWithValue("@hematocrito", obj.Hematocrito);
                        cmd.Parameters.AddWithValue("@plaquetas", obj.Plaquetas);
                        cmd.Parameters.AddWithValue("@mielocitos", obj.Mielocitos);
                        cmd.Parameters.AddWithValue("@melamielocitos", obj.Melamielocitos);
                        cmd.Parameters.AddWithValue("@cayados", obj.Cayados);
                        cmd.Parameters.AddWithValue("@segmentados", obj.Segmentados);
                        cmd.Parameters.AddWithValue("@linfocitos", obj.Linfocitos);
                        cmd.Parameters.AddWithValue("@monocitos", obj.Monocitos);
                        cmd.Parameters.AddWithValue("@eosinofilos", obj.Eosinofilos);
                        cmd.Parameters.AddWithValue("@basofilos", obj.Basofilos);
                        cmd.Parameters.AddWithValue("@ves1", obj.VES1);
                        cmd.Parameters.AddWithValue("@ves2", obj.VES2);
                        cmd.Parameters.AddWithValue("@ik", obj.Ik);
                        cmd.Parameters.AddWithValue("@grupoSanguineo", obj.GrupoSanguineo);
                        cmd.Parameters.AddWithValue("@factor", obj.Factor);
                        cmd.Parameters.AddWithValue("@tiempoSangria", obj.TiempoSangria);
                        cmd.Parameters.AddWithValue("@tiempoCoagulacion", obj.TiempoCoagulacion);
                        cmd.Parameters.AddWithValue("@tiempoProtrombina", obj.TiempoProtrombina);
                        cmd.Parameters.AddWithValue("@porcentajeActividad", obj.PorcentajeActividad);
                        cmd.Parameters.AddWithValue("@aptt", obj.Aptt);
                        cmd.Parameters.AddWithValue("@serieRoja", obj.SerieRoja);
                        cmd.Parameters.AddWithValue("@serieBlanca", obj.SerieBlanca);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        respuesta = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    System.Windows.Forms.MessageBox.Show($"Error al guardar el examen de hemograma:\n{ex.Message}",
                                                         "Error", System.Windows.Forms.MessageBoxButtons.OK,
                                                         System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return respuesta;
        }
        // Método para obtener los datos del hemograma por idPaciente
        public HemogramaM ObtenerHemogramaPorPaciente(int idPaciente)
        {
            HemogramaM hemograma = null;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT * FROM Hematologia WHERE IdPaciente = @idPaciente";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        SQLiteDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read(); // Lee la primera fila

                            // Crear el objeto HemogramaM con los datos obtenidos
                            hemograma = new HemogramaM
                            {
                                IdPaciente = idPaciente,
                                Eritrocitos = reader["Eritrocitos"].ToString(),
                                Leucocitos = reader["Leucocitos"].ToString(),
                                Hemoglobina = reader["Hemoglobina"].ToString(),
                                Hematocrito = reader["Hematocrito"].ToString(),
                                Plaquetas = reader["Plaquetas"].ToString(),
                                Mielocitos = reader["Mielocitos"].ToString(),
                                Melamielocitos = reader["Melamielocitos"].ToString(),
                                Cayados = reader["Cayados"].ToString(),
                                Segmentados = reader["Segmentados"].ToString(),
                                Linfocitos = reader["Linfocitos"].ToString(),
                                Monocitos = reader["Monocitos"].ToString(),
                                Eosinofilos = reader["Eosinofilos"].ToString(),
                                Basofilos = reader["Basofilos"].ToString(),
                                VES1 = reader["VES1"].ToString(),
                                VES2 = reader["VES2"].ToString(),
                                Ik = reader["Ik"].ToString(),
                                GrupoSanguineo = reader["GrupoSanguineo"].ToString(),
                                Factor = reader["Factor"].ToString(),
                                TiempoSangria = reader["TiempoSangria"].ToString(),
                                TiempoCoagulacion = reader["TiempoCoagulacion"].ToString(),
                                TiempoProtrombina = reader["TiempoProtrombina"].ToString(),
                                PorcentajeActividad = reader["PorcentajeActividad"].ToString(),
                                Aptt = reader["Aptt"].ToString(),
                                SerieRoja = reader["SerieRoja"].ToString(),
                                SerieBlanca = reader["SerieBlanca"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Error al obtener el hemograma:\n{ex.Message}",
                                                         "Error", System.Windows.Forms.MessageBoxButtons.OK,
                                                         System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return hemograma;
        }

        public bool GuardarNuevoPacienteYHemograma(HemogramaM hemograma, PacienteM paciente)
        {
            bool respuesta = false;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();

                    // Empezamos una transacción para garantizar que ambas inserciones se realicen correctamente
                    using (var transaction = conexion.BeginTransaction())
                    {
                        // Primero, insertar el nuevo paciente (sin IdPaciente especificado)
                        string queryInsertPaciente = @"
                    INSERT INTO Paciente (Nombre, Apellido, Telefono,Edad) 
                    VALUES (@Nombre, @Apellido, @Telefono, @CI);
                    SELECT last_insert_rowid();"; // Esto obtiene el último Id insertado (nuevo IdPaciente)

                        int nuevoIdPaciente;
                        using (SQLiteCommand cmdPaciente = new SQLiteCommand(queryInsertPaciente, conexion, transaction))
                        {
                            // Añadir parámetros para el nuevo paciente
                            cmdPaciente.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                            cmdPaciente.Parameters.AddWithValue("@Apellido", paciente.Apellido);
                            cmdPaciente.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                            cmdPaciente.Parameters.AddWithValue("@edad", paciente.Edad);

                            nuevoIdPaciente = Convert.ToInt32(cmdPaciente.ExecuteScalar()); // Obtenemos el nuevo IdPaciente
                        }

                        // Luego, insertar el hemograma asociado con el nuevo paciente (nuevo IdPaciente)
                        string queryInsertHemograma = @"
                    INSERT INTO Hematologia 
                    (IdPaciente, Eritrocitos, Leucocitos, Hemoglobina, Hematocrito, Plaquetas, 
                     Mielocitos, Melamielocitos, Cayados, Segmentados, Linfocitos, Monocitos, 
                     Eosinofilos, Basofilos, VES1, VES2, Ik, GrupoSanguineo, Factor, 
                     TiempoSangria, TiempoCoagulacion, TiempoProtrombina, PorcentajeActividad, 
                     Aptt, SerieRoja, SerieBlanca) 
                    VALUES 
                    (@IdPaciente, @Eritrocitos, @Leucocitos, @Hemoglobina, @Hematocrito, @Plaquetas, 
                     @Mielocitos, @Melamielocitos, @Cayados, @Segmentados, @Linfocitos, @Monocitos, 
                     @Eosinofilos, @Basofilos, @VES1, @VES2, @Ik, @GrupoSanguineo, @Factor, 
                     @TiempoSangria, @TiempoCoagulacion, @TiempoProtrombina, @PorcentajeActividad, 
                     @Aptt, @SerieRoja, @SerieBlanca);";

                        using (SQLiteCommand cmdHemograma = new SQLiteCommand(queryInsertHemograma, conexion, transaction))
                        {
                            // Insertar los valores del hemograma para el nuevo paciente (nuevoIdPaciente)
                            cmdHemograma.Parameters.AddWithValue("@IdPaciente", nuevoIdPaciente);
                            cmdHemograma.Parameters.AddWithValue("@Eritrocitos", hemograma.Eritrocitos);
                            cmdHemograma.Parameters.AddWithValue("@Leucocitos", hemograma.Leucocitos);
                            cmdHemograma.Parameters.AddWithValue("@Hemoglobina", hemograma.Hemoglobina);
                            cmdHemograma.Parameters.AddWithValue("@Hematocrito", hemograma.Hematocrito);
                            cmdHemograma.Parameters.AddWithValue("@Plaquetas", hemograma.Plaquetas);
                            cmdHemograma.Parameters.AddWithValue("@Mielocitos", hemograma.Mielocitos);
                            cmdHemograma.Parameters.AddWithValue("@Melamielocitos", hemograma.Melamielocitos);
                            cmdHemograma.Parameters.AddWithValue("@Cayados", hemograma.Cayados);
                            cmdHemograma.Parameters.AddWithValue("@Segmentados", hemograma.Segmentados);
                            cmdHemograma.Parameters.AddWithValue("@Linfocitos", hemograma.Linfocitos);
                            cmdHemograma.Parameters.AddWithValue("@Monocitos", hemograma.Monocitos);
                            cmdHemograma.Parameters.AddWithValue("@Eosinofilos", hemograma.Eosinofilos);
                            cmdHemograma.Parameters.AddWithValue("@Basofilos", hemograma.Basofilos);
                            cmdHemograma.Parameters.AddWithValue("@VES1", hemograma.VES1);
                            cmdHemograma.Parameters.AddWithValue("@VES2", hemograma.VES2);
                            cmdHemograma.Parameters.AddWithValue("@Ik", hemograma.Ik);
                            cmdHemograma.Parameters.AddWithValue("@GrupoSanguineo", hemograma.GrupoSanguineo);
                            cmdHemograma.Parameters.AddWithValue("@Factor", hemograma.Factor);
                            cmdHemograma.Parameters.AddWithValue("@TiempoSangria", hemograma.TiempoSangria);
                            cmdHemograma.Parameters.AddWithValue("@TiempoCoagulacion", hemograma.TiempoCoagulacion);
                            cmdHemograma.Parameters.AddWithValue("@TiempoProtrombina", hemograma.TiempoProtrombina);
                            cmdHemograma.Parameters.AddWithValue("@PorcentajeActividad", hemograma.PorcentajeActividad);
                            cmdHemograma.Parameters.AddWithValue("@Aptt", hemograma.Aptt);
                            cmdHemograma.Parameters.AddWithValue("@SerieRoja", hemograma.SerieRoja);
                            cmdHemograma.Parameters.AddWithValue("@SerieBlanca", hemograma.SerieBlanca);

                            cmdHemograma.ExecuteNonQuery();
                        }

                        // Si ambas inserciones fueron exitosas, hacer commit de la transacción
                        transaction.Commit();
                        respuesta = true;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    System.Windows.Forms.MessageBox.Show($"Error al guardar el nuevo paciente y hemograma:\n{ex.Message}",
                                                         "Error", System.Windows.Forms.MessageBoxButtons.OK,
                                                         System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return respuesta;
        }

        public bool Actualizar(HemogramaM obj, int idPaciente)
        {
            bool respuesta = false;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE Hematologia SET 
                Eritrocitos = @Eritrocitos,
                Leucocitos = @Leucocitos,
                Hemoglobina = @Hemoglobina,
                Hematocrito = @Hematocrito,
                Plaquetas = @Plaquetas,
                Mielocitos = @Mielocitos,
                Melamielocitos = @Melamielocitos,
                Cayados = @Cayados,
                Segmentados = @Segmentados,
                Linfocitos = @Linfocitos,
                Monocitos = @Monocitos,
                Eosinofilos = @Eosinofilos,
                Basofilos = @Basofilos,
                VES1 = @VES1,
                VES2 = @VES2,
                Ik = @Ik,
                GrupoSanguineo = @GrupoSanguineo,
                Factor = @Factor,
                TiempoSangria = @TiempoSangria,
                TiempoCoagulacion = @TiempoCoagulacion,
                TiempoProtrombina = @TiempoProtrombina,
                PorcentajeActividad = @PorcentajeActividad,
                Aptt = @Aptt,
                SerieRoja = @SerieRoja,
                SerieBlanca = @SerieBlanca
                WHERE IdPaciente = @IdPaciente";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@Eritrocitos", obj.Eritrocitos);
                        cmd.Parameters.AddWithValue("@Leucocitos", obj.Leucocitos);
                        cmd.Parameters.AddWithValue("@Hemoglobina", obj.Hemoglobina);
                        cmd.Parameters.AddWithValue("@Hematocrito", obj.Hematocrito);
                        cmd.Parameters.AddWithValue("@Plaquetas", obj.Plaquetas);
                        cmd.Parameters.AddWithValue("@Mielocitos", obj.Mielocitos);
                        cmd.Parameters.AddWithValue("@Melamielocitos", obj.Melamielocitos);
                        cmd.Parameters.AddWithValue("@Cayados", obj.Cayados);
                        cmd.Parameters.AddWithValue("@Segmentados", obj.Segmentados);
                        cmd.Parameters.AddWithValue("@Linfocitos", obj.Linfocitos);
                        cmd.Parameters.AddWithValue("@Monocitos", obj.Monocitos);
                        cmd.Parameters.AddWithValue("@Eosinofilos", obj.Eosinofilos);
                        cmd.Parameters.AddWithValue("@Basofilos", obj.Basofilos);
                        cmd.Parameters.AddWithValue("@VES1", obj.VES1);
                        cmd.Parameters.AddWithValue("@VES2", obj.VES2);
                        cmd.Parameters.AddWithValue("@Ik", obj.Ik);
                        cmd.Parameters.AddWithValue("@GrupoSanguineo", obj.GrupoSanguineo);
                        cmd.Parameters.AddWithValue("@Factor", obj.Factor);
                        cmd.Parameters.AddWithValue("@TiempoSangria", obj.TiempoSangria);
                        cmd.Parameters.AddWithValue("@TiempoCoagulacion", obj.TiempoCoagulacion);
                        cmd.Parameters.AddWithValue("@TiempoProtrombina", obj.TiempoProtrombina);
                        cmd.Parameters.AddWithValue("@PorcentajeActividad", obj.PorcentajeActividad);
                        cmd.Parameters.AddWithValue("@Aptt", obj.Aptt);
                        cmd.Parameters.AddWithValue("@SerieRoja", obj.SerieRoja);
                        cmd.Parameters.AddWithValue("@SerieBlanca", obj.SerieBlanca);

                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error al actualizar hemograma: " + ex.Message);
                }
            }
            return respuesta;
        }


    }
}
