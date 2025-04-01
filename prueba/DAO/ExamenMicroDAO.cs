using prueba.Modelo;
using System;
using System.Configuration;
using System.Data.SQLite;

namespace prueba.DAO
{
    public class ExamenMicroDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public bool GuardarMicro(MicroM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO Microbiologia 
                        (IdPaciente, Muestra, Gram, M1, M2, M3, Cultivo, Colonia, Identificacion, Sensible, Resistentes,Nota) 
                        VALUES 
                        (@idPaciente, @muestra, @gram, @m1, @m2, @m3, @cultivo, @colonia, @identificacion, @sensible, @resistentes,@nota)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@muestra", obj.Muestra);
                        cmd.Parameters.AddWithValue("@gram", obj.Gram);
                        cmd.Parameters.AddWithValue("@m1", obj.M1);
                        cmd.Parameters.AddWithValue("@m2", obj.M2);
                        cmd.Parameters.AddWithValue("@m3", obj.M3);
                        cmd.Parameters.AddWithValue("@cultivo", obj.Cultivo);
                        cmd.Parameters.AddWithValue("@colonia", obj.Colonia);
                        cmd.Parameters.AddWithValue("@identificacion", obj.Identificacion);
                        cmd.Parameters.AddWithValue("@sensible", obj.Sensible);
                        cmd.Parameters.AddWithValue("@resistentes", obj.Resistencia);
                        cmd.Parameters.AddWithValue("@nota", obj.Nota);


                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al guardar el examen de microbiología: " + ex.Message);
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizarMicro(MicroM obj, int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"UPDATE Microbiologia SET 
                    Muestra = @muestra, Gram = @gram, M1 = @m1, M2 = @m2, M3 = @m3, 
                    Cultivo = @cultivo, Colonia = @colonia, Identificacion = @identificacion, 
                    Sensible = @sensible, Resistentes = @resistentes,Nota = @nota
                    WHERE IdPaciente = @idPaciente";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    cmd.Parameters.AddWithValue("@muestra", obj.Muestra);
                    cmd.Parameters.AddWithValue("@gram", obj.Gram);
                    cmd.Parameters.AddWithValue("@m1", obj.M1);
                    cmd.Parameters.AddWithValue("@m2", obj.M2);
                    cmd.Parameters.AddWithValue("@m3", obj.M3);
                    cmd.Parameters.AddWithValue("@cultivo", obj.Cultivo);
                    cmd.Parameters.AddWithValue("@colonia", obj.Colonia);
                    cmd.Parameters.AddWithValue("@identificacion", obj.Identificacion);
                    cmd.Parameters.AddWithValue("@sensible", obj.Sensible);
                    cmd.Parameters.AddWithValue("@resistentes", obj.Resistencia);
                    cmd.Parameters.AddWithValue("@nota", obj.Nota);


                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public MicroM ObtenerPorPaciente(int idPaciente)
        {
            MicroM examen = null;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM Microbiologia WHERE IdPaciente = @idPaciente LIMIT 1";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            examen = new MicroM
                            {
                                Muestra = dr["Muestra"].ToString(),
                                Gram = dr["Gram"].ToString(),
                                M1 = dr["M1"].ToString(),
                                M2 = dr["M2"].ToString(),
                                M3 = dr["M3"].ToString(),
                                Cultivo = dr["Cultivo"].ToString(),
                                Colonia = dr["Colonia"].ToString(),
                                Identificacion = dr["Identificacion"].ToString(),
                                Sensible = dr["Sensible"].ToString(),
                                Resistencia = dr["Resistentes"].ToString(),
                                Nota = dr["Nota"].ToString()

                            };
                        }
                    }
                }
            }
            return examen;
        }
    }
}
