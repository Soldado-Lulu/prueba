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
                TiempoSangria, TiempoCoagulacion, TiempoProtrombina, PorcentajeActividad, Aptt, SerieRoja, SerieBlanca,Reticulocitos,Isi,Vcm,Inr) 
                VALUES 
                (@idPaciente, @eritrocitos, @leucocitos, @hemoglobina, @hematocrito, @plaquetas, @mielocitos, @melamielocitos, @cayados, 
                @segmentados, @linfocitos, @monocitos, @eosinofilos, @basofilos, @ves1, @ves2, @ik, @grupoSanguineo, @factor, 
                @tiempoSangria, @tiempoCoagulacion, @tiempoProtrombina, @porcentajeActividad, @aptt, @serieRoja, @serieBlanca,@reticulocitos,@isi,@vcm,@inr)";

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
                        cmd.Parameters.AddWithValue("@isi", obj.Isi);
                        cmd.Parameters.AddWithValue("@vcm", obj.Vcm);
                        cmd.Parameters.AddWithValue("@inr", obj.Inr);



                        cmd.Parameters.AddWithValue("@reticulocitos",obj.Reticulocitos);
                        respuesta = cmd.ExecuteNonQuery() > 0;
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
        public HemogramaM ObtenerPorIdPaciente(int idPaciente)
        {
            HemogramaM hemograma = null;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                  conexion.Open();
                string query = "SELECT * FROM Hematologia WHERE IdPaciente = @idPaciente LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {

                   
                        if (dr.Read())
                    {
                        // Crear el objeto HemogramaM con los datos obtenidos
                        hemograma = new HemogramaM
                            {
                                IdPaciente = idPaciente,
                                Eritrocitos = dr["Eritrocitos"].ToString(),
                                Leucocitos = dr["Leucocitos"].ToString(),
                                Hemoglobina = dr["Hemoglobina"].ToString(),
                                Hematocrito = dr["Hematocrito"].ToString(),
                                Plaquetas = dr["Plaquetas"].ToString(),
                                Mielocitos = dr["Mielocitos"].ToString(),
                                Melamielocitos = dr["Melamielocitos"].ToString(),
                                Cayados = dr["Cayados"].ToString(),
                                Segmentados = dr["Segmentados"].ToString(),
                                Linfocitos = dr["Linfocitos"].ToString(),
                                Monocitos = dr["Monocitos"].ToString(),
                                Eosinofilos = dr["Eosinofilos"].ToString(),
                                Basofilos = dr["Basofilos"].ToString(),
                                VES1 = dr["VES1"].ToString(),
                                VES2 = dr["VES2"].ToString(),
                                Ik = dr["Ik"].ToString(),
                                GrupoSanguineo = dr["GrupoSanguineo"].ToString(),
                                Factor = dr["Factor"].ToString(),
                                TiempoSangria = dr["TiempoSangria"].ToString(),
                                TiempoCoagulacion = dr["TiempoCoagulacion"].ToString(),
                                TiempoProtrombina = dr["TiempoProtrombina"].ToString(),
                                PorcentajeActividad = dr["PorcentajeActividad"].ToString(),
                                Aptt = dr["Aptt"].ToString(),
                                Reticulocitos = dr["Reticulocitos"].ToString(),
                                Isi = dr["Isi"].ToString(),
                            SerieRoja = dr["SerieRoja"].ToString(),
                                SerieBlanca = dr["SerieBlanca"].ToString(),
                                                                Vcm = dr["Vcm"].ToString(),
                            Inr = dr["Inr"].ToString()


                        };
                        }
                    }
                }

            }
            return hemograma;
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
                Eritrocitos = @eritrocitos, Leucocitos = @leucocitos, Hemoglobina = @hemoglobina, Hematocrito = @hematocrito,
                Plaquetas = @plaquetas, Mielocitos = @mielocitos, Melamielocitos = @melamielocitos, Cayados = @cayados,
                Segmentados = @segmentados, Linfocitos = @linfocitos, Monocitos = @monocitos, Eosinofilos = @eosinofilos,
                Basofilos = @basofilos, VES1 = @ves1, VES2 = @ves2, Ik = @ik, GrupoSanguineo = @grupoSanguineo,
                Factor = @factor, TiempoSangria = @tiempoSangria, TiempoCoagulacion = @tiempoCoagulacion,
                TiempoProtrombina = @tiempoProtrombina, PorcentajeActividad = @porcentajeActividad, Aptt = @aptt,
                SerieRoja = @serieRoja, SerieBlanca = @serieBlanca, Reticulocitos=@reticulocitos,Isi=@isi,Vcm =@vcm,Inr=@inr
                WHERE IdPaciente = @idPaciente";

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
                        cmd.Parameters.AddWithValue("@reticulocitos", obj.Reticulocitos);
                        cmd.Parameters.AddWithValue("@isi", obj.Isi);
                        cmd.Parameters.AddWithValue("@serieRoja", obj.SerieRoja);
                        cmd.Parameters.AddWithValue("@vcm", obj.Vcm);
                        cmd.Parameters.AddWithValue("@inr", obj.Inr);

                        cmd.Parameters.AddWithValue("@serieBlanca", obj.SerieBlanca);

                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Error al actualizar hemograma:\n{ex.Message}",
                        "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return respuesta;
        }

    }
}
