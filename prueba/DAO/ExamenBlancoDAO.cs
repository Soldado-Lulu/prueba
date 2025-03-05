using Prueba.Modelo;
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
    public class ExamenBlancoDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public bool Guardar(BlancoM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO Blanco (IdPaciente Muestra,Examen, Datos,Otros) VALUES (@idPaciente, @muestra, @examen, @datos, @otros)";

                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@IdPaciente", obj.IdPaciente));
                    cmd.Parameters.Add(new SQLiteParameter("@muestra", obj.Muestra));
                    cmd.Parameters.Add(new SQLiteParameter("@examen", obj.Examen));
                    cmd.Parameters.Add(new SQLiteParameter("@datos", obj.Datos));
                    cmd.Parameters.Add(new SQLiteParameter("@otros", obj.Otros));
                    cmd.CommandType = System.Data.CommandType.Text;
                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine("Error al guardar el examen de Blanco: " + ex.Message);
                }
            }
            return respuesta;
        }
    }
}
