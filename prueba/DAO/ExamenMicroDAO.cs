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
                (IdPaciente, Muestra, Gram, M1, M2, M3, Cultivo, Colonia, Identificacion, Sensible, Resistentes) 
                VALUES 
                (@idPaciente, @muestra, @gram, @m1, @m2, @m3, @cultivo, @colonia, @identificacion, @sensible, @resistentes)";

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

                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine("Error al guardar el examen de microbiología: " + ex.Message);
                }
            }
            return respuesta;
        }


    }
}
