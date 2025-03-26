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
    public class ExamenHCGDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        public bool Actualizar(HCGM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE HCG SET Resultado = @resultado WHERE IdPaciente = @idPaciente";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@resultado", obj.Resultado);
                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine("Error al actualizar el examen HCG: " + ex.Message);
                }
            }
            return respuesta;
        }

        public HCGM ObtenerPorIdPaciente(int idPaciente)
        {
            HCGM examen = null;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM HCG WHERE IdPaciente = @idPaciente LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            examen = new HCGM()
                            {
                                IdPaciente = idPaciente,
                                Resultado = dr["Resultado"].ToString()
                            };
                        }
                    }
                }
            }

            return examen;
        }

        public bool Guardar(HCGM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO HCG (IdPaciente, Resultado) VALUES (@idPaciente, @resultado)";

                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@idPaciente", obj.IdPaciente));
                        cmd.Parameters.Add(new SQLiteParameter("@resultado", obj.Resultado));
             
                        cmd.CommandType = System.Data.CommandType.Text;
                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine(" capa dao  Error al guardar el examen de HCG:  " + ex.Message);
                }
            }
            return respuesta;
        }

    }
}
