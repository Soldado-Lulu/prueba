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
    public class ExamenOrinaDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public bool Guardar(OrinaM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"INSERT INTO Orina 
                        (IdPaciente, Aspecto, Color, Olor, Densidad, Reaccion, Glucosa, Bilirrubina, Cetonas, Sangre, Proteina, 
                        Urobiliogeno, Nitrito, Leucocito1, Eritrocito, Leucocito2, CED, Redonda, Embarazo, Otros, Observaciones, 
                        Flora, Piocito, Cristale, Cilindro) 
                        VALUES 
                        (@idPaciente, @aspecto, @color, @olor, @densidad, @reaccion, @glucosa, @bilirrubina, @cetonas, @sangre, @proteina, 
                        @urobiliogeno, @nitrito, @leucocito1, @eritrocito, @leucocito2, @ced, @redonda, @embarazo, @otros, @observaciones, 
                        @flora, @piocito, @cristale, @cilindro)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@aspecto", obj.Aspecto);
                        cmd.Parameters.AddWithValue("@color", obj.Color);
                        cmd.Parameters.AddWithValue("@olor", obj.Olor);
                        cmd.Parameters.AddWithValue("@densidad", obj.Densidad);
                        cmd.Parameters.AddWithValue("@reaccion", obj.Reaccion);
                        cmd.Parameters.AddWithValue("@glucosa", obj.Glucosa);
                        cmd.Parameters.AddWithValue("@bilirrubina", obj.Bilirrubina);
                        cmd.Parameters.AddWithValue("@cetonas", obj.Cetonas);
                        cmd.Parameters.AddWithValue("@sangre", obj.Sangre);
                        cmd.Parameters.AddWithValue("@proteina", obj.Proteina);
                        cmd.Parameters.AddWithValue("@urobiliogeno", obj.Urobiliogeno);
                        cmd.Parameters.AddWithValue("@nitrito", obj.Nitrito);
                        cmd.Parameters.AddWithValue("@leucocito1", obj.Leucocito1);
                        cmd.Parameters.AddWithValue("@eritrocito", obj.Eritrocito);
                        cmd.Parameters.AddWithValue("@leucocito2", obj.Leucocito2);
                        cmd.Parameters.AddWithValue("@ced", obj.CED);
                        cmd.Parameters.AddWithValue("@redonda", obj.Redonda);
                        cmd.Parameters.AddWithValue("@embarazo", obj.Embarazo);
                        cmd.Parameters.AddWithValue("@otros", obj.Otros);
                        cmd.Parameters.AddWithValue("@observaciones", obj.Observaciones);
                        cmd.Parameters.AddWithValue("@flora", obj.Flora);
                        cmd.Parameters.AddWithValue("@piocito", obj.Piocito);
                        cmd.Parameters.AddWithValue("@cristale", obj.Cristale);
                        cmd.Parameters.AddWithValue("@cilindro", obj.Cilindro);

                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Console.WriteLine("Error al guardar el examen de orina: " + ex.Message);
                }
            }
            return respuesta;
        }
        public OrinaM ObtenerPorIdPaciente(int idPaciente)
        {
            OrinaM examen = null;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM Orina WHERE IdPaciente = @idPaciente LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            examen = new OrinaM()
                            {
                                Aspecto = dr["Aspecto"].ToString(),
                                Color = dr["Color"].ToString(),
                                Olor = dr["Olor"].ToString(),
                                Densidad = dr["Densidad"].ToString(),
                                Reaccion = dr["Reaccion"].ToString(),
                                Glucosa = dr["Glucosa"].ToString(),
                                Bilirrubina = dr["Bilirrubina"].ToString(),
                                Cetonas = dr["Cetonas"].ToString(),
                                Sangre = dr["Sangre"].ToString(),
                                Proteina = dr["Proteina"].ToString(),
                                Urobiliogeno = dr["Urobiliogeno"].ToString(),
                                Nitrito = dr["Nitrito"].ToString(),
                                Leucocito1 = dr["Leucocito1"].ToString(),
                                Eritrocito = dr["Eritrocito"].ToString(),
                                Leucocito2 = dr["Leucocito2"].ToString(),
                                CED = dr["CED"].ToString(),
                                Redonda = dr["Redonda"].ToString(),
                                Embarazo = dr["Embarazo"].ToString(),
                                Otros = dr["Otros"].ToString(),
                                Observaciones = dr["Observaciones"].ToString(),
                                Flora = dr["Flora"].ToString(),
                                Piocito = dr["Piocito"].ToString(),
                                Cristale = dr["Cristale"].ToString(),
                                Cilindro = dr["Cilindro"].ToString()
                            };
                        }
                    }
                }
            }

            return examen;
        }
        public bool Actualizar(OrinaM obj, int idPaciente)
        {
            bool respuesta = true;
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE Orina SET 
                Aspecto = @aspecto, Color = @color, Olor = @olor, Densidad = @densidad, Reaccion = @reaccion,
                Glucosa = @glucosa, Bilirrubina = @bilirrubina, Cetonas = @cetonas, Sangre = @sangre, Proteina = @proteina,
                Urobiliogeno = @urobiliogeno, Nitrito = @nitrito, Leucocito1 = @leucocito1, Eritrocito = @eritrocito,
                Leucocito2 = @leucocito2, CED = @ced, Redonda = @redonda, Embarazo = @embarazo, Otros = @otros,
                Observaciones = @observaciones, Flora = @flora, Piocito = @piocito, Cristale = @cristale, Cilindro = @cilindro
                WHERE IdPaciente = @idPaciente";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@aspecto", obj.Aspecto);
                        cmd.Parameters.AddWithValue("@color", obj.Color);
                        cmd.Parameters.AddWithValue("@olor", obj.Olor);
                        cmd.Parameters.AddWithValue("@densidad", obj.Densidad);
                        cmd.Parameters.AddWithValue("@reaccion", obj.Reaccion);
                        cmd.Parameters.AddWithValue("@glucosa", obj.Glucosa);
                        cmd.Parameters.AddWithValue("@bilirrubina", obj.Bilirrubina);
                        cmd.Parameters.AddWithValue("@cetonas", obj.Cetonas);
                        cmd.Parameters.AddWithValue("@sangre", obj.Sangre);
                        cmd.Parameters.AddWithValue("@proteina", obj.Proteina);
                        cmd.Parameters.AddWithValue("@urobiliogeno", obj.Urobiliogeno);
                        cmd.Parameters.AddWithValue("@nitrito", obj.Nitrito);
                        cmd.Parameters.AddWithValue("@leucocito1", obj.Leucocito1);
                        cmd.Parameters.AddWithValue("@eritrocito", obj.Eritrocito);
                        cmd.Parameters.AddWithValue("@leucocito2", obj.Leucocito2);
                        cmd.Parameters.AddWithValue("@ced", obj.CED);
                        cmd.Parameters.AddWithValue("@redonda", obj.Redonda);
                        cmd.Parameters.AddWithValue("@embarazo", obj.Embarazo);
                        cmd.Parameters.AddWithValue("@otros", obj.Otros);
                        cmd.Parameters.AddWithValue("@observaciones", obj.Observaciones);
                        cmd.Parameters.AddWithValue("@flora", obj.Flora);
                        cmd.Parameters.AddWithValue("@piocito", obj.Piocito);
                        cmd.Parameters.AddWithValue("@cristale", obj.Cristale);
                        cmd.Parameters.AddWithValue("@cilindro", obj.Cilindro);

                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar examen: " + ex.Message);
                    respuesta = false;
                }
            }
            return respuesta;
        }

    }
}
