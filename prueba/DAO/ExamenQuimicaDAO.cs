using Prueba.Modelo;
using System;
using System.Configuration;
using System.Data.SQLite;

namespace prueba.DAO
{
    public class ExamenQuimicaDAO
    {
        // Cadena de conexión a la base de datos
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        public bool ActualizarQuimica(QuimicaM obj, int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = @"
            UPDATE Quimica SET 
                Glucosa = @glucosa, Urea = @urea, Creatina = @creatina, BUN = @bun, Urico = @urico, 
                Colesterol = @colesterol, HDL = @hdl, LDL = @ldl, Triglicerido = @triglicerido, 
                Bilirrubina = @bilirrubina, Directa = @directa, Indirecta = @indirecta, Total = @total, 
                GOT = @got, GPT = @gpt, FosfatasaAlcalina = @fosfatasaAlcalina, Amilasa = @amilasa, 
                Proteina = @proteina, Albumina = @albumina, Globulina = @globulina, Relacion = @relacion, 
                FofatasaAcida = @fofatasaAcida, FosfatasaAcidaProtastica = @fosfatasaAcidaProtastica, 
                CKMB = @ckmb, CPK = @cpk, Hemogloblina = @hemogloblina, Sodio = @sodio, 
                Potasio = @potasio, Cloro = @cloro, Calcio = @calcio
            WHERE IdPaciente = @idPaciente";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    cmd.Parameters.AddWithValue("@glucosa", obj.Glucosa);
                    cmd.Parameters.AddWithValue("@urea", obj.Urea);
                    cmd.Parameters.AddWithValue("@creatina", obj.Creatina);
                    cmd.Parameters.AddWithValue("@bun", obj.BUN);
                    cmd.Parameters.AddWithValue("@urico", obj.Urico);
                    cmd.Parameters.AddWithValue("@colesterol", obj.Colesterol);
                    cmd.Parameters.AddWithValue("@hdl", obj.HDL);
                    cmd.Parameters.AddWithValue("@ldl", obj.LDL);
                    cmd.Parameters.AddWithValue("@triglicerido", obj.Triglicerido);
                    cmd.Parameters.AddWithValue("@bilirrubina", obj.Bilirrubina);
                    cmd.Parameters.AddWithValue("@directa", obj.Directa);
                    cmd.Parameters.AddWithValue("@indirecta", obj.Indirecta);
                    cmd.Parameters.AddWithValue("@total", obj.Total);
                    cmd.Parameters.AddWithValue("@got", obj.GOT);
                    cmd.Parameters.AddWithValue("@gpt", obj.GPT);
                    cmd.Parameters.AddWithValue("@fosfatasaAlcalina", obj.FosfatasaAlcalina);
                    cmd.Parameters.AddWithValue("@amilasa", obj.Amilasa);
                    cmd.Parameters.AddWithValue("@proteina", obj.Proteina);
                    cmd.Parameters.AddWithValue("@albumina", obj.Albumina);
                    cmd.Parameters.AddWithValue("@globulina", obj.Globulina);
                    cmd.Parameters.AddWithValue("@relacion", obj.Relacion);
                    cmd.Parameters.AddWithValue("@fofatasaAcida", obj.FosfatasaAcida);
                    cmd.Parameters.AddWithValue("@fosfatasaAcidaProtastica", obj.FosfatasaAcidaProstatica);
                    cmd.Parameters.AddWithValue("@ckmb", obj.CKMB);
                    cmd.Parameters.AddWithValue("@cpk", obj.CPK);
                    cmd.Parameters.AddWithValue("@hemogloblina", obj.Hemoglobina);
                    cmd.Parameters.AddWithValue("@sodio", obj.Sodio);
                    cmd.Parameters.AddWithValue("@potasio", obj.Potasio);
                    cmd.Parameters.AddWithValue("@cloro", obj.Cloro);
                    cmd.Parameters.AddWithValue("@calcio", obj.Calcio);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public QuimicaM ObtenerPorPaciente(int idPaciente)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "SELECT * FROM Quimica WHERE IdPaciente = @idPaciente LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new QuimicaM
                            {
                                Glucosa = dr["Glucosa"].ToString(),
                                Urea = dr["Urea"].ToString(),
                                Creatina = dr["Creatina"].ToString(),
                                BUN = dr["BUN"].ToString(),
                                Urico = dr["Urico"].ToString(),
                                Colesterol = dr["Colesterol"].ToString(),
                                HDL = dr["HDL"].ToString(),
                                LDL = dr["LDL"].ToString(),
                                Triglicerido = dr["Triglicerido"].ToString(),
                                Bilirrubina = dr["Bilirrubina"].ToString(),
                                Directa = dr["Directa"].ToString(),
                                Indirecta = dr["Indirecta"].ToString(),
                                Total = dr["Total"].ToString(),
                                GOT = dr["GOT"].ToString(),
                                GPT = dr["GPT"].ToString(),
                                FosfatasaAlcalina = dr["FosfatasaAlcalina"].ToString(),
                                Amilasa = dr["Amilasa"].ToString(),
                                Proteina = dr["Proteina"].ToString(),
                                Albumina = dr["Albumina"].ToString(),
                                Globulina = dr["Globulina"].ToString(),
                                Relacion = dr["Relacion"].ToString(),
                                FosfatasaAcida = dr["FofatasaAcida"].ToString(),
                                FosfatasaAcidaProstatica = dr["FosfatasaAcidaProtastica"].ToString(),
                                CKMB = dr["CKMB"].ToString(),
                                CPK = dr["CPK"].ToString(),
                                Hemoglobina = dr["Hemogloblina"].ToString(),
                                Sodio = dr["Sodio"].ToString(),
                                Potasio = dr["Potasio"].ToString(),
                                Cloro = dr["Cloro"].ToString(),
                                Calcio = dr["Calcio"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool GuardarQuimica(QuimicaM obj, int idPaciente)
        {
            bool respuesta = true;

            // Establecer la conexión a la base de datos
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                try
                {
                    conexion.Open();

                    // Consulta SQL para insertar los datos de Química Sanguínea
                    string query = @"
                        INSERT INTO Quimica 
                        (IdPaciente, Glucosa, Urea, Creatina, BUN, Urico, Colesterol, HDL, LDL, Triglicerido, 
                         Bilirrubina, Directa, Indirecta, Total, GOT, GPT, FosfatasaAlcalina, Amilasa, 
                         Proteina, Albumina, Globulina, Relacion, FofatasaAcida, FosfatasaAcidaProtastica, 
                         CKMB, CPK, Hemogloblina, Sodio, Potasio, Cloro, Calcio) 
                        VALUES 
                        (@idPaciente, @glucosa, @urea, @creatina, @bun, @urico, @colesterol, @hdl, @ldl, @triglicerido, 
                         @bilirrubina, @directa, @indirecta, @total, @got, @gpt, @fosfatasaAlcalina, @amilasa, 
                         @proteina, @albumina, @globulina, @relacion, @fofatasaAcida, @fosfatasaAcidaProtastica, 
                         @ckmb, @cpk, @hemogloblina, @sodio, @potasio, @cloro, @calcio)";

                    // Crear el comando SQL con la conexión
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        // Agregar los parámetros de la consulta
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.Parameters.AddWithValue("@glucosa", obj.Glucosa);
                        cmd.Parameters.AddWithValue("@urea", obj.Urea);
                        cmd.Parameters.AddWithValue("@creatina", obj.Creatina);
                        cmd.Parameters.AddWithValue("@bun", obj.BUN);
                        cmd.Parameters.AddWithValue("@urico", obj.Urico);
                        cmd.Parameters.AddWithValue("@colesterol", obj.Colesterol);
                        cmd.Parameters.AddWithValue("@hdl", obj.HDL);
                        cmd.Parameters.AddWithValue("@ldl", obj.LDL);
                        cmd.Parameters.AddWithValue("@triglicerido", obj.Triglicerido);
                        cmd.Parameters.AddWithValue("@bilirrubina", obj.Bilirrubina);
                        cmd.Parameters.AddWithValue("@directa", obj.Directa);
                        cmd.Parameters.AddWithValue("@indirecta", obj.Indirecta);
                        cmd.Parameters.AddWithValue("@total", obj.Total);
                        cmd.Parameters.AddWithValue("@got", obj.GOT);
                        cmd.Parameters.AddWithValue("@gpt", obj.GPT);
                        cmd.Parameters.AddWithValue("@fosfatasaAlcalina", obj.FosfatasaAlcalina);
                        cmd.Parameters.AddWithValue("@amilasa", obj.Amilasa);
                        cmd.Parameters.AddWithValue("@proteina", obj.Proteina);
                        cmd.Parameters.AddWithValue("@albumina", obj.Albumina);
                        cmd.Parameters.AddWithValue("@globulina", obj.Globulina);
                        cmd.Parameters.AddWithValue("@relacion", obj.Relacion);
                        cmd.Parameters.AddWithValue("@fofatasaAcida", obj.FosfatasaAcida);
                        cmd.Parameters.AddWithValue("@fosfatasaAcidaProtastica", obj.FosfatasaAcidaProstatica);
                        cmd.Parameters.AddWithValue("@ckmb", obj.CKMB);
                        cmd.Parameters.AddWithValue("@cpk", obj.CPK);
                        cmd.Parameters.AddWithValue("@hemogloblina", obj.Hemoglobina);
                        cmd.Parameters.AddWithValue("@sodio", obj.Sodio);
                        cmd.Parameters.AddWithValue("@potasio", obj.Potasio);
                        cmd.Parameters.AddWithValue("@cloro", obj.Cloro);
                        cmd.Parameters.AddWithValue("@calcio", obj.Calcio);

                        // Ejecutar el comando y verificar si se insertaron filas
                        respuesta = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    // En caso de error, se captura la excepción y se imprime el mensaje de error
                    respuesta = false;
                    Console.WriteLine("Error al guardar el examen de química sanguínea: " + ex.Message);
                }
            }

            return respuesta;
        }
    }
}
