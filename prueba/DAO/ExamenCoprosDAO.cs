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


    }
}
