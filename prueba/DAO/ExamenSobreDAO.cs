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
    public class ExamenSobreDAO
    {


        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public bool GuardarSobre(SobreM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO Sobre (IdPaciente, Presente) VALUES (@idPaciente, @presente)";

                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@idPaciente", obj.IdPaciente));
                        cmd.Parameters.Add(new SQLiteParameter("@presente", obj.Presente));

                        cmd.CommandType = System.Data.CommandType.Text;
                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine(" capa dao  Error al guardar el examen de sobre:  " + ex.Message);
                }
            }
            return respuesta;
        }
        public bool ActualizarSobre(SobreM obj, int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "UPDATE Sobre SET Presente = @presente WHERE IdPaciente = @idPaciente";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    cmd.Parameters.AddWithValue("@presente", obj.Presente);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public SobreM ObtenerPorPaciente(int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM Sobre WHERE IdPaciente = @idPaciente LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new SobreM
                            {
                                IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                Presente = dr["Presente"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

    }
}
