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
    public class ExamenCoprosDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        public bool Actualizar(CoprosM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE Copros SET 
                Consistencia = @consistencia, 
                Color = @color, 
                ExamenM = @examenM, 
                Observaciones = @observaciones 
                WHERE IdPaciente = @idPaciente";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@consistencia", obj.Consistencia);
                        cmd.Parameters.AddWithValue("@color", obj.Color);
                        cmd.Parameters.AddWithValue("@examenM", obj.ExamenM);
                        cmd.Parameters.AddWithValue("@observaciones", obj.Observaciones);

                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar examen de Copros: " + ex.Message);
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Guardar(CoprosM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO Copros (IdPaciente, Consistencia,Color, ExamenM,Observaciones) VALUES (@idPaciente, @consistencia, @color, @examenM, @observaciones)";

                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@idPaciente", obj.IdPaciente));
                        cmd.Parameters.Add(new SQLiteParameter("@consistencia", obj.Consistencia));
                        cmd.Parameters.Add(new SQLiteParameter("@color", obj.Color));
                        cmd.Parameters.Add(new SQLiteParameter("@examenM", obj.ExamenM));
                        cmd.Parameters.Add(new SQLiteParameter("@observaciones", obj.Observaciones));
                        cmd.CommandType = System.Data.CommandType.Text;
                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine(" capa dao  Error al guardar el examen de Copros:  " + ex.Message);
                }
            }
            return respuesta;
        }
        public CoprosM ObtenerPorIdPaciente(int idPaciente)
        {
            CoprosM examen = null;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM Copros WHERE IdPaciente = @idPaciente LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            examen = new CoprosM()
                            {
                                IdPaciente = idPaciente,
                                Consistencia = dr["Consistencia"].ToString(),
                                Color = dr["Color"].ToString(),
                                ExamenM = dr["ExamenM"].ToString(),
                                Observaciones = dr["Observaciones"].ToString()
                            };
                        }
                    }
                }
            }
            return examen;
        }


    }
}
