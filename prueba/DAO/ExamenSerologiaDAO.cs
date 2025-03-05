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
