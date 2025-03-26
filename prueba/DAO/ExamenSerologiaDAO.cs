using prueba.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.DAO
{
    public class ExamenSerologiaDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        public bool ActualizarSerologia(SerologiaM obj, int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"UPDATE Serologia SET 
            Reaccion = @reaccion, Campo11 = @campo11, Campo12 = @campo12, Campo13 = @campo13, Campo14 = @campo14, Campo15 = @campo15,
            Campo21 = @campo21, Campo22 = @campo22, Campo23 = @campo23, Campo24 = @campo24, Campo25 = @campo25,
            Campo31 = @campo31, Campo32 = @campo32, Campo33 = @campo33, Campo34 = @campo34, Campo35 = @campo35,
            Campo41 = @campo41, Campo42 = @campo42, Campo43 = @campo43, Campo44 = @campo44, Campo45 = @campo45,
            Observaciones = @observaciones
            WHERE IdPaciente = @idPaciente";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    cmd.Parameters.AddWithValue("@reaccion", obj.Reaccion);
                    cmd.Parameters.AddWithValue("@campo11", obj.Campo11);
                    cmd.Parameters.AddWithValue("@campo12", obj.Campo12);
                    cmd.Parameters.AddWithValue("@campo13", obj.Campo13);
                    cmd.Parameters.AddWithValue("@campo14", obj.Campo14);
                    cmd.Parameters.AddWithValue("@campo15", obj.Campo15);
                    cmd.Parameters.AddWithValue("@campo21", obj.Campo21);
                    cmd.Parameters.AddWithValue("@campo22", obj.Campo22);
                    cmd.Parameters.AddWithValue("@campo23", obj.Campo23);
                    cmd.Parameters.AddWithValue("@campo24", obj.Campo24);
                    cmd.Parameters.AddWithValue("@campo25", obj.Campo25);
                    cmd.Parameters.AddWithValue("@campo31", obj.Campo31);
                    cmd.Parameters.AddWithValue("@campo32", obj.Campo32);
                    cmd.Parameters.AddWithValue("@campo33", obj.Campo33);
                    cmd.Parameters.AddWithValue("@campo34", obj.Campo34);
                    cmd.Parameters.AddWithValue("@campo35", obj.Campo35);
                    cmd.Parameters.AddWithValue("@campo41", obj.Campo41);
                    cmd.Parameters.AddWithValue("@campo42", obj.Campo42);
                    cmd.Parameters.AddWithValue("@campo43", obj.Campo43);
                    cmd.Parameters.AddWithValue("@campo44", obj.Campo44);
                    cmd.Parameters.AddWithValue("@campo45", obj.Campo45);
                    cmd.Parameters.AddWithValue("@observaciones", obj.Observaciones);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public SerologiaM ObtenerPorPaciente(int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM Serologia WHERE IdPaciente = @idPaciente LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new SerologiaM
                            {
                                Reaccion = dr["Reaccion"].ToString(),
                                Campo11 = dr["Campo11"].ToString(),
                                Campo12 = dr["Campo12"].ToString(),
                                Campo13 = dr["Campo13"].ToString(),
                                Campo14 = dr["Campo14"].ToString(),
                                Campo15 = dr["Campo15"].ToString(),
                                Campo21 = dr["Campo21"].ToString(),
                                Campo22 = dr["Campo22"].ToString(),
                                Campo23 = dr["Campo23"].ToString(),
                                Campo24 = dr["Campo24"].ToString(),
                                Campo25 = dr["Campo25"].ToString(),
                                Campo31 = dr["Campo31"].ToString(),
                                Campo32 = dr["Campo32"].ToString(),
                                Campo33 = dr["Campo33"].ToString(),
                                Campo34 = dr["Campo34"].ToString(),
                                Campo35 = dr["Campo35"].ToString(),
                                Campo41 = dr["Campo41"].ToString(),
                                Campo42 = dr["Campo42"].ToString(),
                                Campo43 = dr["Campo43"].ToString(),
                                Campo44 = dr["Campo44"].ToString(),
                                Campo45 = dr["Campo45"].ToString(),
                                Observaciones = dr["Observaciones"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool GuardarSerologia(SerologiaM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO Serologia 
            (IdPaciente, Reaccion, Campo11, Campo12, Campo13, Campo14, Campo15, 
             Campo21, Campo22, Campo23, Campo24, Campo25, 
             Campo31, Campo32, Campo33, Campo34, Campo35, 
             Campo41, Campo42, Campo43, Campo44, Campo45, Observaciones) 
            VALUES 
            (@idPaciente, @reaccion, @campo11, @campo12, @campo13, @campo14, @campo15, 
             @campo21, @campo22, @campo23, @campo24, @campo25, 
             @campo31, @campo32, @campo33, @campo34, @campo35, 
             @campo41, @campo42, @campo43, @campo44, @campo45, @observaciones)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@reaccion", obj.Reaccion);
                        cmd.Parameters.AddWithValue("@campo11", obj.Campo11);
                        cmd.Parameters.AddWithValue("@campo12", obj.Campo12);
                        cmd.Parameters.AddWithValue("@campo13", obj.Campo13);
                        cmd.Parameters.AddWithValue("@campo14", obj.Campo14);
                        cmd.Parameters.AddWithValue("@campo15", obj.Campo15);
                        cmd.Parameters.AddWithValue("@campo21", obj.Campo21);
                        cmd.Parameters.AddWithValue("@campo22", obj.Campo22);
                        cmd.Parameters.AddWithValue("@campo23", obj.Campo23);
                        cmd.Parameters.AddWithValue("@campo24", obj.Campo24);
                        cmd.Parameters.AddWithValue("@campo25", obj.Campo25);
                        cmd.Parameters.AddWithValue("@campo31", obj.Campo31);
                        cmd.Parameters.AddWithValue("@campo32", obj.Campo32);
                        cmd.Parameters.AddWithValue("@campo33", obj.Campo33);
                        cmd.Parameters.AddWithValue("@campo34", obj.Campo34);
                        cmd.Parameters.AddWithValue("@campo35", obj.Campo35);
                        cmd.Parameters.AddWithValue("@campo41", obj.Campo41);
                        cmd.Parameters.AddWithValue("@campo42", obj.Campo42);
                        cmd.Parameters.AddWithValue("@campo43", obj.Campo43);
                        cmd.Parameters.AddWithValue("@campo44", obj.Campo44);
                        cmd.Parameters.AddWithValue("@campo45", obj.Campo45);
                        cmd.Parameters.AddWithValue("@observaciones", obj.Observaciones);

                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine("Error al guardar el examen de serología: " + ex.Message);
                }
            }
            return respuesta;
        }

    }
}
