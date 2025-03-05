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

    }
}
