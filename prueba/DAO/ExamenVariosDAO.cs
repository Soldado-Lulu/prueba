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
    public class ExamenVariosDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public bool Guardar(VariosM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO Varios (IdPaciente, Muestra,Examen, Datos,Otros,Valores,Paciente) VALUES (@idPaciente, @muestra, @examen, @datos, @otros,@valores,@paciente)";

                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@idPaciente", idPaciente));
                        cmd.Parameters.Add(new SQLiteParameter("@muestra", obj.Muestra));
                        cmd.Parameters.Add(new SQLiteParameter("@examen", obj.Examen));
                        cmd.Parameters.Add(new SQLiteParameter("@datos", obj.Datos));
                        cmd.Parameters.Add(new SQLiteParameter("@otros", obj.Otros));
                        cmd.Parameters.Add(new SQLiteParameter("@valores", obj.Valores));
                        cmd.Parameters.Add(new SQLiteParameter("@paciente", obj.Paciente));
                        cmd.CommandType = System.Data.CommandType.Text;
                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine(" capa dao  Error al guardar el examen de Varios:  " + ex.Message);
                }
            }
            return respuesta;
        }
        public bool Actualizar(VariosM obj, int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"UPDATE Varios SET 
            Muestra = @muestra, 
            Examen = @examen, 
            Datos = @datos, 
            Otros = @otros, 
            Valores = @valores, 
            Paciente = @paciente 
            WHERE IdPaciente = @idPaciente";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    cmd.Parameters.AddWithValue("@muestra", obj.Muestra);
                    cmd.Parameters.AddWithValue("@examen", obj.Examen);
                    cmd.Parameters.AddWithValue("@datos", obj.Datos);
                    cmd.Parameters.AddWithValue("@otros", obj.Otros);
                    cmd.Parameters.AddWithValue("@valores", obj.Valores);
                    cmd.Parameters.AddWithValue("@paciente", obj.Paciente);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public VariosM ObtenerPorPaciente(int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM Varios WHERE IdPaciente = @idPaciente LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new VariosM
                            {
                                IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                Muestra = dr["Muestra"].ToString(),
                                Examen = dr["Examen"].ToString(),
                                Datos = dr["Datos"].ToString(),
                                Otros = dr["Otros"].ToString(),
                                Valores = dr["Valores"].ToString(),
                                Paciente = dr["Paciente"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
